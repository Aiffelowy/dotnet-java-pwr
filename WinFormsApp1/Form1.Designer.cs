namespace WinFormsApp1
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
            run_button = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            n_items_box = new NumericUpDown();
            capacity_box = new NumericUpDown();
            seed_box = new NumericUpDown();
            listBox1 = new ListBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)n_items_box).BeginInit();
            ((System.ComponentModel.ISupportInitialize)capacity_box).BeginInit();
            ((System.ComponentModel.ISupportInitialize)seed_box).BeginInit();
            SuspendLayout();
            // 
            // run_button
            // 
            run_button.Location = new Point(36, 211);
            run_button.Name = "run_button";
            run_button.Size = new Size(94, 29);
            run_button.TabIndex = 3;
            run_button.Text = "run";
            run_button.UseVisualStyleBackColor = true;
            run_button.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 7);
            label1.Name = "label1";
            label1.Size = new Size(40, 20);
            label1.TabIndex = 4;
            label1.Text = "seed";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 69);
            label2.Name = "label2";
            label2.Size = new Size(118, 20);
            label2.TabIndex = 5;
            label2.Text = "number of items";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 132);
            label3.Name = "label3";
            label3.Size = new Size(130, 20);
            label3.TabIndex = 6;
            label3.Text = "backpack capacity";
            // 
            // n_items_box
            // 
            n_items_box.Location = new Point(12, 92);
            n_items_box.Name = "n_items_box";
            n_items_box.Size = new Size(150, 27);
            n_items_box.TabIndex = 7;
            // 
            // capacity_box
            // 
            capacity_box.Location = new Point(12, 155);
            capacity_box.Name = "capacity_box";
            capacity_box.Size = new Size(150, 27);
            capacity_box.TabIndex = 8;
            // 
            // seed_box
            // 
            seed_box.Location = new Point(12, 30);
            seed_box.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            seed_box.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            seed_box.Name = "seed_box";
            seed_box.Size = new Size(150, 27);
            seed_box.TabIndex = 9;
            seed_box.Value = new decimal(new int[] { 42069, 0, 0, 0 });
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(285, 15);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(503, 424);
            listBox1.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 278);
            label4.Name = "label4";
            label4.Size = new Size(45, 20);
            label4.TabIndex = 11;
            label4.Text = "result";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(listBox1);
            Controls.Add(seed_box);
            Controls.Add(capacity_box);
            Controls.Add(n_items_box);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(run_button);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)n_items_box).EndInit();
            ((System.ComponentModel.ISupportInitialize)capacity_box).EndInit();
            ((System.ComponentModel.ISupportInitialize)seed_box).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button run_button;
        private Label label1;
        private Label label2;
        private Label label3;
        private NumericUpDown n_items_box;
        private NumericUpDown capacity_box;
        private NumericUpDown seed_box;
        private ListBox listBox1;
        private Label label4;
    }
}
