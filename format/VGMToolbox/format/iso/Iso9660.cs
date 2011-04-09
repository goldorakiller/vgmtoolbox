﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text;

using VGMToolbox.util;

namespace VGMToolbox.format.iso
{
    public class Iso9660
    {
        public const long EMPTY_HEADER_SIZE = 0x8000;        
        public static readonly byte[] STANDARD_IDENTIFIER = new byte[] { 0x43, 0x44, 0x30, 0x30, 0x31 };
        public static readonly byte[] EMPTY_DATETIME = new byte[] { 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 
                                                                    0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x00 };

        public string FileName { set; get; }

        //-------------
        // Constructor
        //-------------
        public Iso9660(string path)
        {
            this.FileName = path;
        }

        public virtual long GetStartingOffset()
        {
            return EMPTY_HEADER_SIZE;
        }

        public virtual void Initialize()
        {
            long currentOffset;
            long fileLength;

            long pathTableOffset;

            Iso9660Volume volumeDescriptor;

            using (FileStream fs = File.OpenRead(this.FileName))
            {
                fileLength = fs.Length;
                
                currentOffset = this.GetStartingOffset();
                volumeDescriptor = new Iso9660Volume();

                // Load volume, add loop later to get all volumes
                volumeDescriptor.Initialize(fs, currentOffset);
                
                // calculate offset to path table
                pathTableOffset = volumeDescriptor.LocationOfOccurrenceOfTypeLPathTable * volumeDescriptor.LogicalBlockSize;


            } // using (FileStream fs = File.OpenRead(this.FileName))
        }

        public static DateTime GetIsoDateTime(byte[] isoDateArray)
        {
            DateTime dateValue = new DateTime();
            string dateString;

            if (ParseFile.CompareSegment(isoDateArray, 0, EMPTY_DATETIME))
            {
                dateValue = DateTime.MinValue;
            }
            else
            {
                dateString = ByteConversion.GetAsciiText(isoDateArray);
                dateValue = new DateTime(Int32.Parse(dateString.Substring(0, 4)),
                                         Int16.Parse(dateString.Substring(4, 2).ToString()),
                                         Int16.Parse(dateString.Substring(6, 2).ToString()),
                                         Int16.Parse(dateString.Substring(8, 2).ToString()),
                                         Int16.Parse(dateString.Substring(10, 2).ToString()),
                                         Int16.Parse(dateString.Substring(12, 2).ToString()),
                                         Int16.Parse(dateString.Substring(14, 2).ToString()));
            }

            return dateValue;
        }
    }

    public class Iso9660Volume
    {
        public byte VolumeDescriptorType { set; get; }
        public byte[] StandardIdentifier { set; get; }
        public byte VolumeDescriptorVersion { set; get; }

        public byte UnusedField1 { set; get; }

        public string SystemIdentifier { set; get; }
        public string VolumeIdentifier { set; get; }

        public byte[] UnusedField2 { set; get; }

        public uint VolumeSpaceSize { set; get; }

        public byte[] UnusedField3 { set; get; }

        public ushort VolumeSetSize { set; get; }
        public ushort VolumeSequenceNumber { set; get; }
        public ushort LogicalBlockSize { set; get; }

        public uint PathTableSize { set; get; }
        public uint LocationOfOccurrenceOfTypeLPathTable { set; get; }
        public uint LocationOfOptionalOccurrenceOfTypeLPathTable { set; get; }
        public uint LocationOfOccurrenceOfTypeMPathTable { set; get; }
        public uint LocationOfOptionalOccurrenceOfTypeMPathTable { set; get; }

        public byte[] DirectoryRecordForRootDirectory { set; get; }

        public string VolumeSetIdentifier { set; get; }
        public string PublisherIdentifier { set; get; }
        public string DataPreparerIdentifier { set; get; }
        public string ApplicationIdentifier { set; get; }
        public string CopyrightFileIdentifier { set; get; }
        public string AbstractFileIdentifier { set; get; }
        public string BibliographicFileIdentifier { set; get; }

        public DateTime VolumeCreationDateAndTime { set; get; }
        public DateTime VolumeModificationDateAndTime { set; get; }
        public DateTime VolumeExpirationDateAndTime { set; get; }
        public DateTime VolumeEffectiveDateAndTime { set; get; }

        public byte FileStructureVersion { set; get; }

        public byte Reserved1 { set; get; }

        public byte[] ApplicationUse { set; get; }

        public byte[] Reserved2 { set; get; }

        
        public void Initialize(Stream isoStream, long offset)
        {
            this.VolumeDescriptorType = ParseFile.ParseSimpleOffset(isoStream, offset + 0x00, 1)[0];
            this.StandardIdentifier = ParseFile.ParseSimpleOffset(isoStream, offset + 0x01, 5);
            this.VolumeDescriptorVersion = ParseFile.ParseSimpleOffset(isoStream, offset + 0x06, 1)[0];

            this.UnusedField1 = ParseFile.ParseSimpleOffset(isoStream, offset + 0x07, 1)[0];
            
            this.SystemIdentifier = ByteConversion.GetAsciiText(ParseFile.ParseSimpleOffset(isoStream, offset + 0x08, 0x20)).Trim();
            this.VolumeIdentifier = ByteConversion.GetAsciiText(ParseFile.ParseSimpleOffset(isoStream, offset + 0x28, 0x20)).Trim();

            this.UnusedField2 = ParseFile.ParseSimpleOffset(isoStream, offset + 0x48, 0x08);

            this.VolumeSpaceSize = BitConverter.ToUInt32(ParseFile.ParseSimpleOffset(isoStream, offset + 0x50, 0x04), 0);

            this.UnusedField3 = ParseFile.ParseSimpleOffset(isoStream, offset + 0x58, 0x20);

            this.VolumeSetSize = BitConverter.ToUInt16(ParseFile.ParseSimpleOffset(isoStream, offset + 0x78, 0x02), 0);
            this.VolumeSequenceNumber = BitConverter.ToUInt16(ParseFile.ParseSimpleOffset(isoStream, offset + 0x7C, 0x02), 0);
            this.LogicalBlockSize = BitConverter.ToUInt16(ParseFile.ParseSimpleOffset(isoStream, offset + 0x80, 0x02), 0);

            this.PathTableSize = BitConverter.ToUInt32(ParseFile.ParseSimpleOffset(isoStream, offset + 0x84, 0x04), 0);
            this.LocationOfOccurrenceOfTypeLPathTable = BitConverter.ToUInt32(ParseFile.ParseSimpleOffset(isoStream, offset + 0x8C, 0x04), 0);
            this.LocationOfOptionalOccurrenceOfTypeLPathTable = BitConverter.ToUInt32(ParseFile.ParseSimpleOffset(isoStream, offset + 0x90, 0x04), 0);
            this.LocationOfOccurrenceOfTypeMPathTable = ByteConversion.GetUInt32BigEndian(ParseFile.ParseSimpleOffset(isoStream, offset + 0x94, 0x04));
            this.LocationOfOptionalOccurrenceOfTypeMPathTable = ByteConversion.GetUInt32BigEndian(ParseFile.ParseSimpleOffset(isoStream, offset + 0x98, 0x04));

            this.DirectoryRecordForRootDirectory = ParseFile.ParseSimpleOffset(isoStream, offset + 0x9C, 0x22);

            this.VolumeSetIdentifier = ByteConversion.GetAsciiText(ParseFile.ParseSimpleOffset(isoStream, offset + 0xBE, 0x80)).Trim();
            this.PublisherIdentifier = ByteConversion.GetAsciiText(ParseFile.ParseSimpleOffset(isoStream, offset + 0x13E, 0x80)).Trim();
            this.DataPreparerIdentifier = ByteConversion.GetAsciiText(ParseFile.ParseSimpleOffset(isoStream, offset + 0x1BE, 0x80)).Trim();
            this.ApplicationIdentifier = ByteConversion.GetAsciiText(ParseFile.ParseSimpleOffset(isoStream, offset + 0x23E, 0x80)).Trim();
            this.CopyrightFileIdentifier = ByteConversion.GetAsciiText(ParseFile.ParseSimpleOffset(isoStream, offset + 0x2BE, 0x25)).Trim();
            this.AbstractFileIdentifier = ByteConversion.GetAsciiText(ParseFile.ParseSimpleOffset(isoStream, offset + 0x2E3, 0x25)).Trim();
            this.BibliographicFileIdentifier = ByteConversion.GetAsciiText(ParseFile.ParseSimpleOffset(isoStream, offset + 0x308, 0x25)).Trim();

            this.VolumeCreationDateAndTime = Iso9660.GetIsoDateTime(ParseFile.ParseSimpleOffset(isoStream, offset + 0x32D, 0x11));
            this.VolumeModificationDateAndTime = Iso9660.GetIsoDateTime(ParseFile.ParseSimpleOffset(isoStream, offset + 0x33E, 0x11));
            this.VolumeExpirationDateAndTime = Iso9660.GetIsoDateTime(ParseFile.ParseSimpleOffset(isoStream, offset + 0x34F, 0x11));
            this.VolumeEffectiveDateAndTime = Iso9660.GetIsoDateTime(ParseFile.ParseSimpleOffset(isoStream, offset + 0x360, 0x11));           
            
            this.FileStructureVersion = ParseFile.ParseSimpleOffset(isoStream, offset + 0x371, 1)[0];

            this.Reserved1 = ParseFile.ParseSimpleOffset(isoStream, offset + 0x372, 1)[0];

            this.ApplicationUse = ParseFile.ParseSimpleOffset(isoStream, offset + 0x373, 0x200);

            this.Reserved2 = ParseFile.ParseSimpleOffset(isoStream, offset + 0x573, 0x28D);
        }

    }

    public class Iso9660Directory
    {
        public byte LengthOfDirectoryRecord { set; get; }
        public byte ExtendedAttributeRecordLength { set; get; }
        public uint LocationOfExtent { set; get; }
        public uint DataLength { set; get; }
        public DateTime RecordingDateAndTime { set; get; }
        public byte FileFlags { set; get; }
        public byte FileUnitSize { set; get; }
        public byte InterleaveGapSize { set; get; }
        public ushort VolumeSequenceNumber { set; get; }
        public byte LengthOfFileIdentifier { set; get; }

        public byte[] PaddingField { set; get; }
        public byte[] SystemUse { set; get; }

        public bool FlagExistance { set; get; }
        public bool FlagDirectory { set; get; }
        public bool FlagAssociatedFile { set; get; }
        public bool FlagRecord { set; get; }
        public bool FlagProtection { set; get; }
        public bool FlagMultiExtent { set; get; }


        public Iso9660Directory(byte[] directoryBytes)
        {
            this.LengthOfDirectoryRecord = directoryBytes[0];
            this.ExtendedAttributeRecordLength = directoryBytes[1];
            this.LocationOfExtent = BitConverter.ToUInt32(ParseFile.ParseSimpleOffset(directoryBytes, 2, 4), 0);
            this.DataLength = BitConverter.ToUInt32(ParseFile.ParseSimpleOffset(directoryBytes, 0x0A, 4), 0);

            this.RecordingDateAndTime = new DateTime(directoryBytes[0x12] + 1900,
                                                     directoryBytes[0x13],
                                                     directoryBytes[0x14],
                                                     directoryBytes[0x15],
                                                     directoryBytes[0x16],
                                                     directoryBytes[0x17]);

            this.FileFlags = directoryBytes[0x19];
            /*
            this.FlagExistance = this.FileFlags[0];
            this.FlagDirectory { set; get; }
            this.FlagAssociatedFile { set; get; }
            this.FlagRecord { set; get; }
            this.FlagProtection { set; get; }
            this.FlagMultiExtent { set; get; }
            */

            this.FileUnitSize = directoryBytes[0x1A];            
            this.InterleaveGapSize = directoryBytes[0x1B];
            this.VolumeSequenceNumber = BitConverter.ToUInt16(ParseFile.ParseSimpleOffset(directoryBytes, 0x1C, 2), 0);
            this.LengthOfFileIdentifier = directoryBytes[0x20];

            /*
            
            public byte[] PaddingField { set; get; }
            public byte[] SystemUse { set; get; }        
            */ 
        }
    }
}
