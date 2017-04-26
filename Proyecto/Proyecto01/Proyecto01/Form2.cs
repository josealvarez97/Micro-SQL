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

namespace Proyecto01
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }



        // Carga diccionario del usario y lo escribe en el diccionario principal.
        private void uploadDictionary_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Dictionary files | *.ini"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                StreamWriter dictionary = new StreamWriter(new FileStream("C:/microSQL/microSQL.ini", FileMode.Create), new UTF8Encoding());
                String path = dialog.FileName; // get name of file
                string line = "";
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
                {
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        dictionary.WriteLine(line);
                        dictionary.Flush();
                    }
                }
            }
        }


        // Crea y escribe en el archivo los valores predeterminados
        private void uploadDictionaryDefault_Click(object sender, EventArgs e)
        {
            using (StreamWriter dictionary = new StreamWriter(new FileStream("C:/microSQL/microSQL.ini", FileMode.OpenOrCreate), new UTF8Encoding()))
            {
                dictionary.WriteLine("<SELECT>, <SELECT>");
                dictionary.WriteLine("<FROM>, <FROM>");
                dictionary.WriteLine("<DELETE>, <DELETE>");
                dictionary.WriteLine("<WHERE>, <WHERE>");
                dictionary.WriteLine("<CREATE TABLE>, <CREATE TABLE>");
                dictionary.WriteLine("<DROP TABLE>, <DROP TABLE>");
                dictionary.WriteLine("<INSERT INTO>, <INSERT INTO>");
                dictionary.WriteLine("<VALUES>, <VALUES>");
                dictionary.WriteLine("<GO>, <GO>");
                dictionary.Flush();
            }
        }
    }
}
