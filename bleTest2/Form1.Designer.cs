﻿
namespace bleTest2
{
    partial class Form1
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
            this.btnScan = new System.Windows.Forms.Button();
            this.btnRst = new System.Windows.Forms.Button();
            this.listRec = new System.Windows.Forms.ListBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(26, 298);
            this.btnScan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(116, 34);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnRst
            // 
            this.btnRst.Location = new System.Drawing.Point(166, 298);
            this.btnRst.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRst.Name = "btnRst";
            this.btnRst.Size = new System.Drawing.Size(116, 34);
            this.btnRst.TabIndex = 0;
            this.btnRst.Text = "result";
            this.btnRst.UseVisualStyleBackColor = true;
            this.btnRst.Click += new System.EventHandler(this.btnRst_Click);
            // 
            // listRec
            // 
            this.listRec.FormattingEnabled = true;
            this.listRec.ItemHeight = 12;
            this.listRec.Location = new System.Drawing.Point(26, 19);
            this.listRec.Name = "listRec";
            this.listRec.Size = new System.Drawing.Size(356, 244);
            this.listRec.TabIndex = 1;
            this.listRec.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listRec_MouseDoubleClick);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(565, 298);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(116, 34);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtReceive
            // 
            this.txtReceive.Location = new System.Drawing.Point(388, 19);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.Size = new System.Drawing.Size(292, 240);
            this.txtReceive.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 360);
            this.Controls.Add(this.txtReceive);
            this.Controls.Add(this.listRec);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRst);
            this.Controls.Add(this.btnScan);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ListBox tb;
        private System.Windows.Forms.Button btnRst;
        private System.Windows.Forms.ListBox listRec;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtReceive;
    }
}

