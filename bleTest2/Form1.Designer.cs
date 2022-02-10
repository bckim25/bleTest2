
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
            this.txbRXHex = new System.Windows.Forms.TextBox();
            this.btnMeasure = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnMeasure2 = new System.Windows.Forms.Button();
            this.lbCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(27, 330);
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
            this.btnRst.Location = new System.Drawing.Point(167, 330);
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
            this.listRec.Location = new System.Drawing.Point(12, 19);
            this.listRec.Name = "listRec";
            this.listRec.Size = new System.Drawing.Size(370, 304);
            this.listRec.TabIndex = 1;
            this.listRec.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listRec_MouseDoubleClick);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(576, 333);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(116, 34);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txbRXHex
            // 
            this.txbRXHex.Location = new System.Drawing.Point(388, 19);
            this.txbRXHex.Multiline = true;
            this.txbRXHex.Name = "txbRXHex";
            this.txbRXHex.Size = new System.Drawing.Size(308, 304);
            this.txbRXHex.TabIndex = 2;
            // 
            // btnMeasure
            // 
            this.btnMeasure.Location = new System.Drawing.Point(347, 339);
            this.btnMeasure.Name = "btnMeasure";
            this.btnMeasure.Size = new System.Drawing.Size(75, 23);
            this.btnMeasure.TabIndex = 3;
            this.btnMeasure.Text = "measure";
            this.btnMeasure.UseVisualStyleBackColor = true;
            this.btnMeasure.Click += new System.EventHandler(this.btnMeasure_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(444, 337);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 24);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "end";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "AutoSet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMeasure2
            // 
            this.btnMeasure2.Location = new System.Drawing.Point(347, 389);
            this.btnMeasure2.Name = "btnMeasure2";
            this.btnMeasure2.Size = new System.Drawing.Size(75, 23);
            this.btnMeasure2.TabIndex = 6;
            this.btnMeasure2.Text = "measure";
            this.btnMeasure2.UseVisualStyleBackColor = true;
            this.btnMeasure2.Click += new System.EventHandler(this.btnMeasure2_Click);
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Location = new System.Drawing.Point(486, 380);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(11, 12);
            this.lbCount.TabIndex = 7;
            this.lbCount.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 423);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.btnMeasure2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnMeasure);
            this.Controls.Add(this.txbRXHex);
            this.Controls.Add(this.listRec);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRst);
            this.Controls.Add(this.btnScan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ListBox tb;
        private System.Windows.Forms.Button btnRst;
        private System.Windows.Forms.ListBox listRec;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txbRXHex;
        private System.Windows.Forms.Button btnMeasure;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnMeasure2;
        private System.Windows.Forms.Label lbCount;
    }
}

