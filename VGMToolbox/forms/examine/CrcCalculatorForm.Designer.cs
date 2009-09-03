﻿namespace VGMToolbox.forms.examine
{
    partial class CrcCalculatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpSourceFiles = new System.Windows.Forms.GroupBox();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.checkForDuplicatesFlag = new System.Windows.Forms.CheckBox();
            this.cbDoVgmtChecksums = new System.Windows.Forms.CheckBox();
            this.pnlLabels.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.grpSourceFiles.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLabels
            // 
            this.pnlLabels.Location = new System.Drawing.Point(0, 501);
            this.pnlLabels.Size = new System.Drawing.Size(879, 19);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Size = new System.Drawing.Size(879, 20);
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(0, 424);
            this.tbOutput.Size = new System.Drawing.Size(879, 77);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 404);
            this.pnlButtons.Size = new System.Drawing.Size(879, 20);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(819, 0);
            // 
            // btnDoTask
            // 
            this.btnDoTask.Location = new System.Drawing.Point(759, 0);
            // 
            // grpSourceFiles
            // 
            this.grpSourceFiles.Controls.Add(this.grpOptions);
            this.grpSourceFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSourceFiles.Location = new System.Drawing.Point(0, 23);
            this.grpSourceFiles.Name = "grpSourceFiles";
            this.grpSourceFiles.Size = new System.Drawing.Size(879, 381);
            this.grpSourceFiles.TabIndex = 5;
            this.grpSourceFiles.TabStop = false;
            this.grpSourceFiles.Text = "Drop Files Here";
            this.grpSourceFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.grpSourceFiles_DragDrop);
            this.grpSourceFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.doDragEnter);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.checkForDuplicatesFlag);
            this.grpOptions.Controls.Add(this.cbDoVgmtChecksums);
            this.grpOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpOptions.Location = new System.Drawing.Point(3, 312);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(873, 66);
            this.grpOptions.TabIndex = 6;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // checkForDuplicatesFlag
            // 
            this.checkForDuplicatesFlag.AutoSize = true;
            this.checkForDuplicatesFlag.Location = new System.Drawing.Point(6, 42);
            this.checkForDuplicatesFlag.Name = "checkForDuplicatesFlag";
            this.checkForDuplicatesFlag.Size = new System.Drawing.Size(285, 17);
            this.checkForDuplicatesFlag.TabIndex = 1;
            this.checkForDuplicatesFlag.Text = "Check for duplicates and include a report in the output.";
            this.checkForDuplicatesFlag.UseVisualStyleBackColor = true;
            // 
            // cbDoVgmtChecksums
            // 
            this.cbDoVgmtChecksums.AutoSize = true;
            this.cbDoVgmtChecksums.Location = new System.Drawing.Point(6, 19);
            this.cbDoVgmtChecksums.Name = "cbDoVgmtChecksums";
            this.cbDoVgmtChecksums.Size = new System.Drawing.Size(221, 17);
            this.cbDoVgmtChecksums.TabIndex = 0;
            this.cbDoVgmtChecksums.Text = "Include VGMToolbox method checksums";
            this.cbDoVgmtChecksums.UseVisualStyleBackColor = true;
            // 
            // CrcCalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 542);
            this.Controls.Add(this.grpSourceFiles);
            this.Name = "CrcCalculatorForm";
            this.Text = "CrcCalculatorForm";
            this.Controls.SetChildIndex(this.pnlLabels, 0);
            this.Controls.SetChildIndex(this.tbOutput, 0);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlButtons, 0);
            this.Controls.SetChildIndex(this.grpSourceFiles, 0);
            this.pnlLabels.ResumeLayout(false);
            this.pnlLabels.PerformLayout();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.grpSourceFiles.ResumeLayout(false);
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSourceFiles;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox cbDoVgmtChecksums;
        private System.Windows.Forms.CheckBox checkForDuplicatesFlag;
    }
}