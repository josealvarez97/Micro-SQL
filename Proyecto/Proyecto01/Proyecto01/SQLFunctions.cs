using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataStructuresURL_3._0;

namespace Proyecto01
{
    class SQLFunctions
    {


        public static void ExecuteCommand(SQLCommands comando, RichTextBox richTextBox)
        {
            //ExecuteCommand(SQLTextParser.ReadAction(richTextBox))
            switch (comando)
            {
                case SQLCommands.createTable:
                    CreateTable(SQLTextParser.GetCreateTableInstructions(richTextBox));
                    break;
                case SQLCommands.select:
                    Select(SQLTextParser.GetSelectInstructions(richTextBox));
                    break;
                case SQLCommands.delete:
                    Delete(SQLTextParser.GetDeleteInstructions(richTextBox));
                    break;
                case SQLCommands.dropTable:
                    DropTable(SQLTextParser.GetDeleteInstructions(richTextBox));
                    break;
                case SQLCommands.insert:
                    Insert(SQLTextParser.GetInsertInstructions(richTextBox));
                    break;
                case SQLCommands.updateTable:
                    UpdateTable(SQLTextParser.GetUpdateInstructions(richTextBox));
                    break;
                default:
                    break;

            }
        }
        public static void CreateTable(string createTableInstructions)
        {

            /*string createTableInstructions = "tableName||columnName,type,specialAttribute|columnName,type,specialAttribute";*/

            // Separar tableName de Columnas con arreglo generalFields
            string[] generalFields = createTableInstructions.Split("||".ToCharArray());
            string tableName = generalFields[0];
            //Separar columnas con arreglo columns
            string[] columns = generalFields[1].Split('|');

            int order = 5;
            Row TableColumns = new Row();

            //Leer columnas de arreglo columns
            for (int i = 0; i < columns.Length; i++)
            {
                //Separar atributo de columnas con arreglo columnAttributes
                string[] columnAttributes = columns[0].Split(',');

                // Crear arbol b cuando encontremos la columna que tiene el specialAttribute == KEY
                if (columnAttributes[2] == "KEY")
                    // Crear arbol de acuerdo al tipo de KEY requerido
                    switch (columnAttributes[1])
                    {
                        case "INT":
                            BTree<Int, Row> intTableBTree = new BTree<Int, Row>(order, Program.APPLICATION_TABLES_PATH + tableName);
                            break;
                        case "VARCHAR-100":
                            BTree<Varchar, Row> varcharTableBTree = new BTree<Varchar, Row>(order, Program.APPLICATION_TABLES_PATH + tableName);
                            break;
                        case "DATETIME":
                            BTree<TimeDate, Row> timeDateTableBTree = new BTree<TimeDate, Row>(order, Program.APPLICATION_TABLES_PATH + tableName);
                            break;
                        default:
                            break;
                    }

                // Columna no tiene specialAttribute == KEY
                else
                    //Agregar columna correspondiente a nuestra tabla
                    switch (columnAttributes[1])
                    {
                        // EMPIEZO A CREER QUE ESTO NO ES DEL TODO NECESARIO...
                        // NOTA: Necesitamos metodos para instanciar un arbol leyendolo desde un archivo...
                        case "INT":
                            BTree<Int, Row> intTableBTree = BTree<Int, Row>.ReadBTreeFromFile(Program.APPLICATION_TABLES_PATH + tableName);
                            break;
                        case "VARCHAR-100":
                            //BTree<Varchar, TableColsValue> varcharTableBTree = new BTree<Varchar, TableColsValue>(order, Program.APPLICATION_TABLES_PATH + tableName);
                            break;
                        case "DATETIME":
                            //BTree<TimeDate, TableColsValue> timeDateTableBTree = new BTree<TimeDate, TableColsValue>(order, Program.APPLICATION_TABLES_PATH + tableName);
                            break;
                        default:
                            break;
                    }
            }
        }

        public static void Select(string selectInstructions)
        {

        }
        public static void Delete(string deleteInstructions)
        {

        }
        public static void DropTable(string dropInstructions)
        {

        }
        public static void Insert(string insertInstructions)
        {

        }
        public static void UpdateTable(string updateInstructions)
        {

        }







    }
}
