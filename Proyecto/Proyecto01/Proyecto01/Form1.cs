using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace Proyecto01
{
    public partial class Form1 : Form
    {
        StreamReader dictionary;
        ReservedWord[] keyWords;
        public Form1()
        {
            InitializeComponent();
            dictionary = new StreamReader(new FileStream("C:/microSQL/microSQL.ini", FileMode.Open), new UTF8Encoding());
            keyWords = new ReservedWord[10];
            keyWords = ReservedWord.FindKeywords(dictionary);
            exportToCSV.Enabled = false;
            exportToCSV.Visible = false;
            exportToExcel.Enabled = false;
            exportToExcel.Visible = false;
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Por si escribe con las originales
            this.PaintKeyword(keyWords[0].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[1].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[2].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[3].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[4].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[5].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[6].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[7].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[8].originalReservedWord, Color.Blue, 0);
            this.PaintKeyword(keyWords[9].originalReservedWord, Color.Blue, 0);

            //O con su traduccion
            this.PaintKeyword(keyWords[0].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[1].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[2].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[3].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[4].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[5].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[6].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[7].translation, Color.Blue, 0);
            this.PaintKeyword(keyWords[8].translation, Color.Blue, 0);


            //Tipos de dato
            this.PaintKeyword("INT", Color.Red, 0);
            this.PaintKeyword("VARCHAR(100)", Color.Red, 0);
            this.PaintKeyword("DATETIME", Color.Red, 0);
            this.PaintKeyword("PRIMARY KEY", Color.Red, 0);
        }
        private void PaintKeyword(string word, Color color, int startIndex)
        {
            if (this.richTextBox1.Text.ToUpper().Contains(word)) 
            {
                int index = -1;
                int selectStart = this.richTextBox1.SelectionStart;

                while ((index = this.richTextBox1.Text.ToUpper().IndexOf(word, (index + 1))) != -1)
                {
                    this.richTextBox1.Select((index + startIndex), word.Length);
                    this.richTextBox1.SelectionColor = color;
                    this.richTextBox1.Select(selectStart, 0);
                    this.richTextBox1.SelectionColor = Color.Black;
                }
            }

        }
        private void runButton_Click(object sender, EventArgs e)
        {
            //Cola de comandos y colas de instrucciones para saber en que orden ejecutarlas
            Queue<SQLCommands> commandsToExecute = SQLTextParser.ReadActions(richTextBox1);
            Queue<string> instructionsToExecute = SQLTextParser.ReadInstructions(richTextBox1);

            while (commandsToExecute.Count != 0)
            {
                if (commandsToExecute.Peek() == SQLCommands.createTable)
                {
                    SQLFunctions.ExecuteCommand(commandsToExecute.Dequeue(), instructionsToExecute.Dequeue(), outputGridView);
                    UpdateTreeView(); // Actualizo TreeView Para mostrar la nueva tabla creada
                }
                // Validamos cuando la accion sea select porque solo en ese momento estara disponible la opcion de EXPORT TO CSV o EXPORT TO EXCEL
                else if (commandsToExecute.Peek() == SQLCommands.select)
                {
                    SQLFunctions.ExecuteCommand(commandsToExecute.Dequeue(), instructionsToExecute.Dequeue(), outputGridView);
                    exportToCSV.Visible = true;
                    exportToCSV.Enabled = true;
                    exportToExcel.Visible = true;
                    exportToExcel.Enabled = true;
                }
                else
                    SQLFunctions.ExecuteCommand(commandsToExecute.Dequeue(), instructionsToExecute.Dequeue(), outputGridView);
            }
            // Necesitamos limpiar rich despues de cada instruccion
            richTextBox1.Clear();
        }
        #region Export to CSV Methods
        private void exportToCSV_Click(object sender, EventArgs e)
        {

            // Si al abrirlo en excel no lo separa es porque el separador de listas default no es la coma en la seccion region, es decir la que esta usando la pc que abrio el archivo 
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "CSV file|*.csv";
            saveFileDialog.Title = "Guardar Archivo CSV";
            saveFileDialog.DefaultExt = "*.csv";
            saveFileDialog.ShowDialog();


            string CsvFpath = saveFileDialog.FileName;

            WriteCSVFile(CsvFpath);
            
        }

        public void WriteCSVFile(string CsvFpath)
        {
            try
            {
                StreamWriter csvFileWriter = new StreamWriter(CsvFpath, false);

                string columnHeaderText = "";

                if (outputGridView.ColumnCount > 0)
                {
                    columnHeaderText = outputGridView.Columns[0].HeaderText;
                }

                for (int i = 1; i < outputGridView.ColumnCount; i++)
                {
                    columnHeaderText = columnHeaderText + "," + outputGridView.Columns[i].HeaderText;
                }


                csvFileWriter.WriteLine(columnHeaderText);

                foreach (DataGridViewRow dataRowObject in outputGridView.Rows)
                {
                    if (!dataRowObject.IsNewRow)
                    {
                        string dataFromGrid = "";



                        for (int i = 0; i < outputGridView.ColumnCount; i++)
                        {
                            if (i != outputGridView.ColumnCount - 1)
                                dataFromGrid += dataRowObject.Cells[i].Value.ToString() + ",";
                            else
                                dataFromGrid += dataRowObject.Cells[i].Value.ToString();
                        }
                        csvFileWriter.WriteLine(dataFromGrid);
                    }
                }

                csvFileWriter.Flush();
                csvFileWriter.Close();
            }


            catch (Exception exceptionObject)
            {
                MessageBox.Show(exceptionObject.ToString());
            }
        }
        #endregion
        private void fileExplorer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateTreeView();
            outputGridView.AutoResizeColumns();

            // Ajusta el datagridView para que se ajuste a su espacio
            outputGridView.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode./*Fill*/AllCells;

            outputGridView.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.AllCells;

            //Deshabilita la ultima linea que era para que el usuario ingresara datos
            outputGridView.AllowUserToAddRows = false;

            // Deshabilitar la columna default
            outputGridView.RowHeadersVisible = false;
        }
        #region Tree View Methods

        public void FullTreeNodesWithColumnInfo(FileInfo file, TreeNode nodes, int counterOfNodes)
        {
            // Crea nodo "COLUMNAS"
            fileExplorer.Nodes[0].Nodes[counterOfNodes].Nodes.Add("COLUMNAS");
            // EJEMPLO FORMATO TABLA
            // ID,INT,KEY|MARCA,VARCHAR(100),NULL|LINEA,VARCHAR(100),NULL|SALIDAALMERCADO,DATETIME,NULL|CABALLOSDEFUERZA,INT,NULL
            StreamReader reader = file.OpenText();

            string tableInfo = reader.ReadToEnd();

            string[] columns = tableInfo.Split('|');

            string[] columnDetails = new string[3];

            for (int i = 0; i < columns.Length; i++)
            {
                columnDetails = columns[i].Split(',');
                string columnName = columnDetails[0];
                string columnType = columnDetails[1];
                string isKey = columnDetails[2];


                //NODOS                  TABLAS -> Nombre de la tabla ->  COLUMNAS  -> Todas las columnas en nodos individuales 
                // Vamos a ubicar en TABLAS (Primer Nodes[0]) en la tabla correspondiente ("Nombre de la tabla") (Segundo Nodes[counterOfNodes]) ...
                // ... en COLUMNAS (Tercer Nodes[0]) un nuevo nodo por cada columna que tenga la tabla
                nodes = fileExplorer.Nodes[0].Nodes[counterOfNodes].Nodes[0].Nodes.Add(columnName.ToUpper() + "(" + columnType.ToLower() + "," + isKey.ToLower() + ")");
            }

            reader.Close();

        }

        public void UpdateTreeView()
        {
            fileExplorer.Nodes.Clear();
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\microSQL\tablas");
            if (Directory.Exists(@"C:\microSQL\tablas"))
            {
                try
                {

                    // Necesitamos crear el nodo de "TABLAS" usando el nombre de la carpeta

                    if (fileExplorer.Nodes.Count == 0)
                        fileExplorer.Nodes.Add(directoryInfo.Name.ToUpper());

                    FileInfo[] files = directoryInfo.GetFiles();

                    int counterOfNodes = 0;
                    int defaultPosition = 0;
                    // Luego para cada archivo en la carpeta tablas creamos un nodo
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        if (file.Exists)
                        {
                            // Agregamos en TABLAS un nodo por cada tabla que exista 
                            TreeNode nodes = fileExplorer.Nodes[defaultPosition].Nodes.Add(file.Name);

                            // Se colocas dos Nodes[0] porque queremos que el nodo columnas sea un subnodo del nodo "NOMBRE DE TABLA"

                            FullTreeNodesWithColumnInfo(file, nodes, counterOfNodes);
                            counterOfNodes++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion
        private void exportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Excel file|*.xlsx";
            saveFileDialog.Title = "Guardar Archivo Excel";
            saveFileDialog.DefaultExt = "*.xlsx";
            saveFileDialog.ShowDialog();


            string csvFpath = saveFileDialog.FileName;

            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workBook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            

            // Activar hoja en la que se escribira
            worksheet = workBook.ActiveSheet;

            
            // Escribo el titulo de cada columna
            for (int i = 1; i < outputGridView.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = outputGridView.Columns[i - 1].HeaderText;
            }
            //Escribo lo que posee cada columna
            for (int i = 0; i < outputGridView.Rows.Count - 1; i++)
            {
                for (int j = 0; j < outputGridView.Columns.Count; j++)
                {
                    if (outputGridView.Rows[i].Cells[j].Value != null)
                    {
                        worksheet.Cells[i + 2, j + 1] = outputGridView.Rows[i].Cells[j].Value.ToString();
                    }
                    else
                    {
                        worksheet.Cells[i + 2, j + 1] = "";
                    }
                }
            }

            workBook.SaveAs(csvFpath);
            workBook.Close();

        }
    }
}
