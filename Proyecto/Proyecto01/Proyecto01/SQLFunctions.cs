using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
