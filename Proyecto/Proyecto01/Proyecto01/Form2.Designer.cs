namespace Proyecto01
{
    partial class Form2
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
            this.uploadDictionary = new System.Windows.Forms.Button();
            this.uploadDictionaryDefault = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uploadDictionary
            // 
            this.uploadDictionary.Location = new System.Drawing.Point(103, 12);
            this.uploadDictionary.Name = "uploadDictionary";
            this.uploadDictionary.Size = new System.Drawing.Size(184, 23);
            this.uploadDictionary.TabIndex = 0;
            this.uploadDictionary.Text = "Cargar Diccionario";
            this.uploadDictionary.UseVisualStyleBackColor = true;
            this.uploadDictionary.Click += new System.EventHandler(this.uploadDictionary_Click);
            // 
            // uploadDictionaryDefault
            // 
            this.uploadDictionaryDefault.Location = new System.Drawing.Point(103, 41);
            this.uploadDictionaryDefault.Name = "uploadDictionaryDefault";
            this.uploadDictionaryDefault.Size = new System.Drawing.Size(184, 23);
            this.uploadDictionaryDefault.TabIndex = 1;
            this.uploadDictionaryDefault.Text = "Usar Diccionario Predeterminado";
            this.uploadDictionaryDefault.UseVisualStyleBackColor = true;
            this.uploadDictionaryDefault.Click += new System.EventHandler(this.uploadDictionaryDefault_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 71);
            this.Controls.Add(this.uploadDictionaryDefault);
            this.Controls.Add(this.uploadDictionary);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uploadDictionary;
        private System.Windows.Forms.Button uploadDictionaryDefault;
    }
}