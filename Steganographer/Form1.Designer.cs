namespace Steganographer
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
            label1 = new Label();
            FilePathBox = new TextBox();
            MessageBox = new TextBox();
            label2 = new Label();
            ApplyBtn = new Button();
            BitsBox = new TextBox();
            ExtractBtn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 34);
            label1.Name = "label1";
            label1.Size = new Size(66, 20);
            label1.TabIndex = 0;
            label1.Text = "File path";
            // 
            // FilePathBox
            // 
            FilePathBox.Location = new Point(99, 31);
            FilePathBox.Name = "FilePathBox";
            FilePathBox.Size = new Size(260, 27);
            FilePathBox.TabIndex = 1;
            FilePathBox.DoubleClick += FilePathBox_DoubleClick;
            // 
            // MessageBox
            // 
            MessageBox.Location = new Point(99, 95);
            MessageBox.Name = "MessageBox";
            MessageBox.Size = new Size(260, 27);
            MessageBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 98);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 2;
            label2.Text = "Message";
            // 
            // ApplyBtn
            // 
            ApplyBtn.Location = new Point(30, 154);
            ApplyBtn.Name = "ApplyBtn";
            ApplyBtn.Size = new Size(155, 29);
            ApplyBtn.TabIndex = 4;
            ApplyBtn.Text = "Apply";
            ApplyBtn.UseVisualStyleBackColor = true;
            ApplyBtn.Click += ApplyBtn_Click;
            // 
            // BitsBox
            // 
            BitsBox.Location = new Point(30, 202);
            BitsBox.Multiline = true;
            BitsBox.Name = "BitsBox";
            BitsBox.ReadOnly = true;
            BitsBox.ScrollBars = ScrollBars.Vertical;
            BitsBox.Size = new Size(329, 153);
            BitsBox.TabIndex = 5;
            // 
            // ExtractBtn
            // 
            ExtractBtn.Location = new Point(204, 154);
            ExtractBtn.Name = "ExtractBtn";
            ExtractBtn.Size = new Size(155, 29);
            ExtractBtn.TabIndex = 6;
            ExtractBtn.Text = "Extract";
            ExtractBtn.UseVisualStyleBackColor = true;
            ExtractBtn.Click += ExtractBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ExtractBtn);
            Controls.Add(BitsBox);
            Controls.Add(ApplyBtn);
            Controls.Add(MessageBox);
            Controls.Add(label2);
            Controls.Add(FilePathBox);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox FilePathBox;
        private TextBox MessageBox;
        private Label label2;
        private Button ApplyBtn;
        private TextBox BitsBox;
        private Button ExtractBtn;
    }
}
