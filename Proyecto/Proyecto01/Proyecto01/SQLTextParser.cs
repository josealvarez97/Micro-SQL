using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto01
{
    class SQLTextParser
    {




        public static Queue<SQLCommands> ReadActions(RichTextBox richTextBox)
        {
            //Leer y encontrar todas las acciones que quiere hacer. Solo que hacer y no especificaciones del como, etc.
            Queue<SQLCommands> commandsList = new Queue<SQLCommands>();
            return commandsList;
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





    }
    }
