using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Proyecto01
{
    class SQLTextParser
    {


        static StreamReader dictionary;
        static string[] keyWords;

        public static Queue<SQLCommands> ReadActions(RichTextBox richTextBox)
        {
            dictionary = new StreamReader(new FileStream("C:/microSQL/microSQL.ini", FileMode.Open), new UTF8Encoding());
            keyWords = new string[9];
            //Leer y encontrar todas las acciones que quiere hacer. Solo que hacer y no especificaciones del como, etc.
            Queue<SQLCommands> commmandsQueue = new Queue<SQLCommands>();
            FindKeywords();

            var lines = richTextBox.Text.Split('\n');
            int j = 0;
            int i = 0;
            while (i < lines.Length)
            {
                while (j < keyWords.Length)
                {
                    if (lines[i] == keyWords[j])
                    {
                        switch (keyWords[j])
                        {
                            case "SELECT":
                                commmandsQueue.Enqueue(SQLCommands.select);
                                i++;
                                j = 0;
                                break;
                            //case "FROM":
                            //    commmandsQueue.Enqueue(SQLCommands.fr);
                            //    break;
                            case "DELETE":
                                commmandsQueue.Enqueue(SQLCommands.delete);
                                i++;
                                j = 0;
                                break;
                            //case "WHERE":
                            //    commmandsQueue.Enqueue(SQLCommands.select);
                            //    break;
                            case "CREATE TABLE":
                                commmandsQueue.Enqueue(SQLCommands.createTable);
                                i++;
                                j = 0;
                                break;
                            case "DROP TABLE":
                                commmandsQueue.Enqueue(SQLCommands.dropTable);
                                i++;
                                j = 0;
                                break;
                            case "INSERT INTO":
                                commmandsQueue.Enqueue(SQLCommands.insert);
                                i++;
                                j = 0;
                                break;
                                //case "VALUES":
                                //    commmandsQueue.Enqueue(SQLCommands.);
                                //    break;
                                //case "GO":
                                //    commmandsQueue.Enqueue(SQLCommands.select);
                                //    break;
                        }
                    }
                    else
                        j++;
                }
                i++; // Si no se encuentra hay que comparar con la otra linea
                j = 0; // Se pone en 0 para poder volver a comparar la palabra de la linea con la del arreglo desde el inicio.
            }

            dictionary.Dispose();

            return commmandsQueue;
        }

        private static string[] GetInstructionsArray(RichTextBox richTextBox)
        {
            string createTableInstructions = "tableName||columnName,type,specialAttribute|columnName,type,specialAttribute";
            string selectInstructions = "columnName, columnName,columnName||tableName||columnaAFiltrar,valorABuscar";
            string deleteInstructions = "tableName||columnaAFiltrar,valorABuscar";
            string dropTableInstructions = "tableName";
            string insertInstructions = "tableName||columnName,columnName,columnName||value,value,value";
            string updateInstructions = "tableName||columnName,newValue||valorLlavePrimaria";

            //Ya esta la info en el arreglo
            string[] instructionArray = new string[6];
            instructionArray[0] = createTableInstructions;
            instructionArray[1] = selectInstructions;
            instructionArray[2] = deleteInstructions;
            instructionArray[3] = dropTableInstructions;
            instructionArray[4] = insertInstructions;
            instructionArray[5] = updateInstructions;

            return instructionArray;
        }

        public static string GetCreateTableInstructions(RichTextBox richTextBox)
        {
            return GetInstructionsArray(richTextBox)[0];
        }
        public static string GetSelectInstructions(RichTextBox richTextBox)
        {
            return GetInstructionsArray(richTextBox)[1];
        }
        public static string GetDeleteInstructions(RichTextBox richTextBox)
        {
            return GetInstructionsArray(richTextBox)[2];
        }
        public static string GetDropInstructions(RichTextBox richTextBox)
        {
            return GetInstructionsArray(richTextBox)[3];
        }
        public static string GetInsertInstructions(RichTextBox richTextBox)
        {
            return GetInstructionsArray(richTextBox)[4];
        }
        public static string GetUpdateInstructions(RichTextBox richTextBox)
        {
            return GetInstructionsArray(richTextBox)[5];
        }


        static private void FindKeywords()
        {
            string line = "";
            string correctedKeyWord = "";
            int i = 0;
            while (!dictionary.EndOfStream)
            {
                line = dictionary.ReadLine();
                var keyWordsInFile = line.Split(',');
                for (int j = 0; j < keyWordsInFile[1].Length; j++)
                {
                    keyWordsInFile[1] = keyWordsInFile[1].Trim();
                    if (keyWordsInFile[1][j] != '<' && keyWordsInFile[1][j] != '>')
                        correctedKeyWord += keyWordsInFile[1][j];
                }
                keyWords[i] = correctedKeyWord;
                correctedKeyWord = "";
                i++;
            }
            dictionary.Dispose();
        }


    }
}
