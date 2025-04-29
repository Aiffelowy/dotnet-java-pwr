namespace ImageProcessing
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
            imgbox_orig = new PictureBox();
            file_picker = new OpenFileDialog();
            button1 = new Button();
            img_mirror = new PictureBox();
            img_negative = new PictureBox();
            img_grey = new PictureBox();
            img_edge = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)imgbox_orig).BeginInit();
            ((System.ComponentModel.ISupportInitialize)img_mirror).BeginInit();
            ((System.ComponentModel.ISupportInitialize)img_negative).BeginInit();
            ((System.ComponentModel.ISupportInitialize)img_grey).BeginInit();
            ((System.ComponentModel.ISupportInitialize)img_edge).BeginInit();
            SuspendLayout();
            // 
            // imgbox_orig
            // 
            imgbox_orig.Location = new Point(12, 219);
            imgbox_orig.Name = "imgbox_orig";
            imgbox_orig.Size = new Size(475, 388);
            imgbox_orig.SizeMode = PictureBoxSizeMode.StretchImage;
            imgbox_orig.TabIndex = 0;
            imgbox_orig.TabStop = false;
            // 
            // file_picker
            // 
            file_picker.FileName = "openFileDialog1";
            file_picker.FileOk += file_picker_FileOk;
            // 
            // button1
            // 
            button1.Location = new Point(201, 159);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "pick image";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // img_mirror
            // 
            img_mirror.Location = new Point(722, 12);
            img_mirror.Name = "img_mirror";
            img_mirror.Size = new Size(359, 201);
            img_mirror.SizeMode = PictureBoxSizeMode.StretchImage;
            img_mirror.TabIndex = 2;
            img_mirror.TabStop = false;
            // 
            // img_negative
            // 
            img_negative.Location = new Point(722, 219);
            img_negative.Name = "img_negative";
            img_negative.Size = new Size(359, 180);
            img_negative.SizeMode = PictureBoxSizeMode.StretchImage;
            img_negative.TabIndex = 3;
            img_negative.TabStop = false;
            // 
            // img_grey
            // 
            img_grey.Location = new Point(722, 405);
            img_grey.Name = "img_grey";
            img_grey.Size = new Size(359, 202);
            img_grey.SizeMode = PictureBoxSizeMode.StretchImage;
            img_grey.TabIndex = 4;
            img_grey.TabStop = false;
            // 
            // img_edge
            // 
            img_edge.Location = new Point(722, 613);
            img_edge.Name = "img_edge";
            img_edge.Size = new Size(359, 211);
            img_edge.SizeMode = PictureBoxSizeMode.StretchImage;
            img_edge.TabIndex = 5;
            img_edge.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1093, 836);
            Controls.Add(img_edge);
            Controls.Add(img_grey);
            Controls.Add(img_negative);
            Controls.Add(img_mirror);
            Controls.Add(button1);
            Controls.Add(imgbox_orig);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)imgbox_orig).EndInit();
            ((System.ComponentModel.ISupportInitialize)img_mirror).EndInit();
            ((System.ComponentModel.ISupportInitialize)img_negative).EndInit();
            ((System.ComponentModel.ISupportInitialize)img_grey).EndInit();
            ((System.ComponentModel.ISupportInitialize)img_edge).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox imgbox_orig;
        private OpenFileDialog file_picker;
        private Button button1;
        private PictureBox img_mirror;
        private PictureBox img_negative;
        private PictureBox img_grey;
        private PictureBox img_edge;
    }
}
