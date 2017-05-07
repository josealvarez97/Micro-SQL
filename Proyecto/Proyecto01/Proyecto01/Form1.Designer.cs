namespace Proyecto01
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.outputGridView = new System.Windows.Forms.DataGridView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.fileExplorer = new System.Windows.Forms.TreeView();
            this.exportToCSV = new System.Windows.Forms.Button();
            this.exportToExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.outputGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // outputGridView
            // 
            this.outputGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outputGridView.Location = new System.Drawing.Point(551, 240);
            this.outputGridView.Name = "outputGridView";
            this.outputGridView.Size = new System.Drawing.Size(621, 218);
            this.outputGridView.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(551, 1);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(621, 207);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(1097, 214);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "RUN";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // fileExplorer
            // 
            this.fileExplorer.Location = new System.Drawing.Point(13, 13);
            this.fileExplorer.Name = "fileExplorer";
            this.fileExplorer.Size = new System.Drawing.Size(532, 445);
            this.fileExplorer.TabIndex = 4;
            this.fileExplorer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.fileExplorer_AfterSelect);
            // 
            // exportToCSV
            // 
            this.exportToCSV.Location = new System.Drawing.Point(992, 214);
            this.exportToCSV.Name = "exportToCSV";
            this.exportToCSV.Size = new System.Drawing.Size(99, 23);
            this.exportToCSV.TabIndex = 5;
            this.exportToCSV.Text = "Exportar a CSV";
            this.exportToCSV.UseVisualStyleBackColor = true;
            this.exportToCSV.Click += new System.EventHandler(this.exportToCSV_Click);
            // 
            // exportToExcel
            // 
            this.exportToExcel.Location = new System.Drawing.Point(877, 214);
            this.exportToExcel.Name = "exportToExcel";
            this.exportToExcel.Size = new System.Drawing.Size(109, 23);
            this.exportToExcel.TabIndex = 6;
            this.exportToExcel.Text = "Exportar Excel";
            this.exportToExcel.UseVisualStyleBackColor = true;
            this.exportToExcel.Click += new System.EventHandler(this.exportToExcel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 470);
            this.Controls.Add(this.exportToExcel);
            this.Controls.Add(this.exportToCSV);
            this.Controls.Add(this.fileExplorer);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.outputGridView);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "microSQL";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.outputGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView outputGridView;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TreeView fileExplorer;
        private System.Windows.Forms.Button exportToCSV;
        private System.Windows.Forms.Button exportToExcel;
    }
}

