namespace Eyes
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Front = new System.Windows.Forms.PictureBox();
            this.GetPhotoButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Front)).BeginInit();
            this.SuspendLayout();
            // 
            // Front
            // 
            this.Front.Location = new System.Drawing.Point(33, 23);
            this.Front.Name = "Front";
            this.Front.Size = new System.Drawing.Size(690, 452);
            this.Front.TabIndex = 0;
            this.Front.TabStop = false;
            this.Front.Paint += new System.Windows.Forms.PaintEventHandler(this.Front_Paint);
            this.Front.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Front_MouseDown);
            this.Front.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Front_MouseMove);
            this.Front.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Front_MouseUp);
            // 
            // GetPhotoButton
            // 
            this.GetPhotoButton.Location = new System.Drawing.Point(737, 23);
            this.GetPhotoButton.Name = "GetPhotoButton";
            this.GetPhotoButton.Size = new System.Drawing.Size(195, 73);
            this.GetPhotoButton.TabIndex = 1;
            this.GetPhotoButton.Text = "Получить фото";
            this.GetPhotoButton.UseVisualStyleBackColor = true;
            this.GetPhotoButton.Click += new System.EventHandler(this.GetPhotoButton_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(30, 478);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(35, 13);
            this.StatusLabel.TabIndex = 2;
            this.StatusLabel.Text = "label1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 543);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.GetPhotoButton);
            this.Controls.Add(this.Front);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.Front)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Front;
        private System.Windows.Forms.Button GetPhotoButton;
        private System.Windows.Forms.Label StatusLabel;
    }
}

