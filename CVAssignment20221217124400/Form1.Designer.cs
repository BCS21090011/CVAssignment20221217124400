namespace CVAssignment20221217124400
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
            this.OriImgBox = new System.Windows.Forms.PictureBox();
            this.CroppedImgBox = new System.Windows.Forms.PictureBox();
            this.BrowseImgButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.PrevButton = new System.Windows.Forms.Button();
            this.CroppedImgNameLabel = new System.Windows.Forms.Label();
            this.TriggeredIndicatorLabel = new System.Windows.Forms.Label();
            this.LoadingProcessingProgressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.OriImgBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CroppedImgBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OriImgBox
            // 
            this.OriImgBox.Location = new System.Drawing.Point(12, 34);
            this.OriImgBox.Name = "OriImgBox";
            this.OriImgBox.Size = new System.Drawing.Size(348, 213);
            this.OriImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OriImgBox.TabIndex = 0;
            this.OriImgBox.TabStop = false;
            // 
            // CroppedImgBox
            // 
            this.CroppedImgBox.Location = new System.Drawing.Point(440, 34);
            this.CroppedImgBox.Name = "CroppedImgBox";
            this.CroppedImgBox.Size = new System.Drawing.Size(348, 213);
            this.CroppedImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CroppedImgBox.TabIndex = 1;
            this.CroppedImgBox.TabStop = false;
            // 
            // BrowseImgButton
            // 
            this.BrowseImgButton.Location = new System.Drawing.Point(153, 253);
            this.BrowseImgButton.Name = "BrowseImgButton";
            this.BrowseImgButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseImgButton.TabIndex = 2;
            this.BrowseImgButton.Text = "Browse image";
            this.BrowseImgButton.UseVisualStyleBackColor = true;
            this.BrowseImgButton.Click += new System.EventHandler(this.BrowseImgButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(713, 253);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 4;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // PrevButton
            // 
            this.PrevButton.Location = new System.Drawing.Point(440, 253);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(75, 23);
            this.PrevButton.TabIndex = 5;
            this.PrevButton.Text = "Prev";
            this.PrevButton.UseVisualStyleBackColor = true;
            this.PrevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // CroppedImgNameLabel
            // 
            this.CroppedImgNameLabel.AutoSize = true;
            this.CroppedImgNameLabel.Location = new System.Drawing.Point(552, 9);
            this.CroppedImgNameLabel.Name = "CroppedImgNameLabel";
            this.CroppedImgNameLabel.Size = new System.Drawing.Size(0, 16);
            this.CroppedImgNameLabel.TabIndex = 7;
            this.CroppedImgNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TriggeredIndicatorLabel
            // 
            this.TriggeredIndicatorLabel.AutoSize = true;
            this.TriggeredIndicatorLabel.Location = new System.Drawing.Point(521, 256);
            this.TriggeredIndicatorLabel.Name = "TriggeredIndicatorLabel";
            this.TriggeredIndicatorLabel.Size = new System.Drawing.Size(0, 16);
            this.TriggeredIndicatorLabel.TabIndex = 8;
            this.TriggeredIndicatorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoadingProcessingProgressBar
            // 
            this.LoadingProcessingProgressBar.Location = new System.Drawing.Point(570, 130);
            this.LoadingProcessingProgressBar.Name = "LoadingProcessingProgressBar";
            this.LoadingProcessingProgressBar.Size = new System.Drawing.Size(100, 23);
            this.LoadingProcessingProgressBar.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LoadingProcessingProgressBar);
            this.Controls.Add(this.TriggeredIndicatorLabel);
            this.Controls.Add(this.CroppedImgNameLabel);
            this.Controls.Add(this.PrevButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.BrowseImgButton);
            this.Controls.Add(this.CroppedImgBox);
            this.Controls.Add(this.OriImgBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OriImgBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CroppedImgBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox OriImgBox;
        private System.Windows.Forms.PictureBox CroppedImgBox;
        private System.Windows.Forms.Button BrowseImgButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button PrevButton;
        private System.Windows.Forms.Label CroppedImgNameLabel;
        private System.Windows.Forms.Label TriggeredIndicatorLabel;
        private System.Windows.Forms.ProgressBar LoadingProcessingProgressBar;
    }
}

