﻿namespace Downsizer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSelectImage = new Button();
            btnDownscale = new Button();
            txtFilePath = new TextBox();
            txtScale = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // btnSelectImage
            // 
            btnSelectImage.Location = new Point(134, 95);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(100, 23);
            btnSelectImage.TabIndex = 0;
            btnSelectImage.Text = "Select Image";
            btnSelectImage.UseVisualStyleBackColor = true;
            btnSelectImage.Click += btnSelectImage_Click;
            // 
            // btnDownscale
            // 
            btnDownscale.Location = new Point(94, 185);
            btnDownscale.Name = "btnDownscale";
            btnDownscale.Size = new Size(100, 23);
            btnDownscale.TabIndex = 0;
            btnDownscale.Text = "Resize Image";
            btnDownscale.UseVisualStyleBackColor = true;
            btnDownscale.Click += btnDownscale_Click;
            // 
            // txtFilePath
            // 
            txtFilePath.Enabled = false;
            txtFilePath.Location = new Point(94, 66);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(183, 23);
            txtFilePath.TabIndex = 1;
            // 
            // txtScale
            // 
            txtScale.Location = new Point(94, 134);
            txtScale.Name = "txtScale";
            txtScale.Size = new Size(100, 23);
            txtScale.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 69);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 3;
            label1.Text = "File Path:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 137);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 4;
            label2.Text = "Scale Factor:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(197, 137);
            label3.Name = "label3";
            label3.Size = new Size(17, 15);
            label3.TabIndex = 5;
            label3.Text = "%";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(289, 258);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtScale);
            Controls.Add(txtFilePath);
            Controls.Add(btnDownscale);
            Controls.Add(btnSelectImage);
            Name = "Form1";
            Text = "Image Downsizer";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectImage;
        private Button btnDownscale;
        private TextBox txtFilePath;
        private TextBox txtScale;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
