using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto01
{
    class tableFilesHandler
    {
        /*FORMATO:     tableName||columnName,type,specialAttribute|  */

        static public int ReadNumberOfColumns(string path)
        {
            using (FileStream filestream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                StreamReader reader = new StreamReader(filestream);
                string line = reader.ReadLine();
                string[] fields = line.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                string[] columns = fields[1].Split('|');

                return columns.Length;
            }
        }
        static public string ReadTypeOfKey(string path)
        {
            using (FileStream filestream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                StreamReader reader = new StreamReader(filestream);
                string line = reader.ReadLine();
                //string[] fields = line.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                string[] columns = line.Split('|');

                for(int i = 0; i < columns.Length; i++)
                {
                    string[] columnAttributes = columns[i].Split(',');
                    string SpecialAttribute = columnAttributes[2];
                    if (SpecialAttribute == "KEY")
                    {
                        string type = columnAttributes[1];

                        return type;

                    }
                }

            }

            return "keyNotFounded...";

        }

        static public Row GetRowModelFromFile(string path)
        {
            // NECESITO ALGO ASI -> Row aRow = new Row(/*columnName,type,specialAttribute|columnName,type,specialAttribute*/);

            using (FileStream filestream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                StreamReader reader = new StreamReader(filestream);
                string line = reader.ReadLine();
                //string[] fields = line.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

                string columnInfo = line;

                Row rowModel = new Row(columnInfo);
                return rowModel;

            }



        }

        static public void CreateTableFile(string path, string tableInfo)
        {
            using (FileStream filestream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                StreamWriter writer = new StreamWriter(filestream);
                writer.Write(tableInfo);
                writer.Flush();
                writer.Dispose();
            }
        }

        static public void DeleteTableFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                throw new Exception("Error eliminando tabla: " + path + ", ¿Existe el archivo?", e);
            }


        }
        static public void DeleteTreeFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                throw new Exception("Error eliminando arbol: " + path + ", ¿Existe el archivo?", e);
            }
        }


    }
}
