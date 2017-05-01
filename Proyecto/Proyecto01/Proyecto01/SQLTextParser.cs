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


        static StreamReader dictionary = new StreamReader(new FileStream("C:/microSQL/microSQL.ini", FileMode.Open), new UTF8Encoding());
        static ReservedWord[] keyWords = ReservedWord.FindKeywords(dictionary);
        static string[] dataType = new string[3];



        public static Queue<SQLCommands> ReadActions(RichTextBox richTextBox)
        {
            //Leer y encontrar todas las acciones que quiere hacer. Solo que hacer y no especificaciones del como, etc.
            Queue<SQLCommands> commmandsQueue = new Queue<SQLCommands>();


            var lines = richTextBox.Text.Split('\n');
            int j = 0;
            int i = 0;
            while (i < lines.Length)
            {
                while (j < keyWords.Length)
                {
                    // ME interesa solo tomar la palabra "DELETE" de "DELETE FROM" para el switch
                    if (lines[i].Trim() == keyWords[2].originalReservedWord + " " + keyWords[1].originalReservedWord || lines[i].Trim() == keyWords[2].translation + " " + keyWords[1].translation)
                    {
                        var getDeleteWord = lines[i].Split();

                        lines[i] = getDeleteWord[0].Trim();
                    }
                    // Crear objeto "Palabra reservada" o algo por el estilo, para que tenga dos atributos.... traduccion y palabra reservada... ??????? una clase solo para tener dos atributos
                    if (lines[i] == keyWords[j].originalReservedWord || lines[i] == keyWords[j].translation)
                    {
                        switch (keyWords[j].originalReservedWord)
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
            string createTableInstructions = "";
            string selectInstructions = "";
            string deleteInstructions = "";
            string dropTableInstructions = "";
            string insertInstructions = "";
            string updateInstructions = "";

            // Leer rich textBox

            var lines = richTextBox.Text.Split('\n');
            int j = 0;
            int i = 0;
            while (i < lines.Length)
            {

                while (j < keyWords.Length)
                {
                    // ME interesa solo tomar la palabra "DELETE" de "DELETE FROM" para el switch
                    if (lines[i].Trim() == keyWords[2].originalReservedWord + " " + keyWords[1].originalReservedWord || lines[i].Trim() == keyWords[2].translation + " " + keyWords[1].translation)
                    {
                        var getDeleteWord = lines[i].Split();

                        lines[i] = getDeleteWord[0].Trim();
                    }
                    if (lines[i].Trim() == keyWords[j].originalReservedWord || lines[i].Trim() == keyWords[j].translation)
                    {
                        switch (keyWords[j].originalReservedWord)
                        {
                            //columnName, columnName,columnName||tableName||columnaAFiltrar,valorABuscar
                            case "SELECT":

                                // Aumentar i para empezar a tomar despues del "SELECT"
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                // Si NO es asterisco pues tomo todos los nombres de las columnas
                                if (lines[i].Trim() != "*")
                                {

                                    while (lines[i] != keyWords[1].originalReservedWord /*FROM*/ || lines[i] != keyWords[1].translation)
                                    {
                                        lines[i] = lines[i].Trim();
                                        lines[i] = lines[i].TrimEnd(',');

                                        if (lines[i + 1] != keyWords[1].originalReservedWord /*FROM*/ || lines[i + 1] != keyWords[1].translation)
                                            selectInstructions += lines[i] + ",";
                                        else
                                            selectInstructions += lines[i] + "||";

                                        //Aumentar i para empezar a tomar despues de la palabra concatenada

                                        i++;

                                        i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);
                                    }

                                    //Aumentar i para empezar a tomar despues de "FROM"
                                    i++;

                                    i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);


                                    selectInstructions += lines[i].Trim() /*tableName*/+ "||";

                                    //Aumentar i para empezar a tomar despues de la palabra concatenada

                                    i++;

                                    i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                    // Si hay para filtrar pues lo leo
                                    if (lines[i].Trim().ToUpper() == keyWords[3].originalReservedWord /*WHERE*/|| lines[i].Trim().ToUpper() == keyWords[3].translation)
                                    {
                                        //Aumentar i para empezar a tomar despues de "WHERE"
                                        i++;

                                        i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                        // Separo para ver el valor a buscar en determinada columna
                                        var valuesToSelect = lines[i].Split('=');

                                        valuesToSelect[0] = valuesToSelect[0].Trim();
                                        valuesToSelect[1] = valuesToSelect[1].Trim();

                                        selectInstructions += valuesToSelect[0] /*Columna a Filtrar*/ + ",";
                                        selectInstructions += valuesToSelect[1] /*Valor a Buscar*/;
                                    }

                                }


                                else
                                {

                                    // Concatenar el asterisco
                                    selectInstructions += lines[i].Trim() /* * */ + "||";

                                    //Aumentar i para empezar a tomar despues de "*"
                                    i++;

                                    i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                    //Aumentar i para empezar a tomar despues de "FROM"
                                    i++;

                                    i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                    selectInstructions += lines[i].Trim() /* Table Name*/;

                                    //Aumentar i para empezar a tomar despues de la palabra concatenada

                                    i++;

                                    i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                    if (i < lines.Length)
                                    {
                                        // Si hay para filtrar pues lo leo
                                        if (lines[i].Trim().ToUpper() == keyWords[3].originalReservedWord /*WHERE*/|| lines[i].Trim().ToUpper() == keyWords[3].translation)
                                        {
                                            //Aumentar i para empezar a tomar despues de "WHERE"
                                            i++;

                                            i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                            selectInstructions += "||";

                                            // Separo para ver el valor a buscar en determinada columna
                                            var valuesToSelect = lines[i].Split('=');

                                            valuesToSelect[0] = valuesToSelect[0].Trim();
                                            valuesToSelect[1] = valuesToSelect[1].Trim();

                                            selectInstructions += valuesToSelect[0] /*Columna a Filtrar*/ + ",";
                                            selectInstructions += valuesToSelect[1] /*Valor a Buscar*/;
                                        }
                                    }
                                }
                                break;

                            //tableName||columnaAFiltrar,valorABuscar
                            case "DELETE":

                                //Aumentar i para tomar despues de "DELETE FROM"
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                deleteInstructions += lines[i].Trim();

                                //Aumentar i para empezar a tomar despues de la palabra concatenada

                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                // Si hay para filtrar pues lo leo
                                if (i < lines.Length)
                                {
                                    if (lines[i].Trim().ToUpper() == keyWords[3].originalReservedWord /*WHERE*/|| lines[i].Trim().ToUpper() == keyWords[3].translation)
                                    {
                                        //Aumentar i para empezar a tomar despues de "WHERE"
                                        i++;

                                        i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                        deleteInstructions += "||";

                                        // Separo para ver el valor a buscar en determinada columna
                                        var valuesToSelect = lines[i].Split('=');

                                        valuesToSelect[0] = valuesToSelect[0].Trim();
                                        valuesToSelect[1] = valuesToSelect[1].Trim();

                                        deleteInstructions += valuesToSelect[0] /*Columna a Filtrar*/ + ",";
                                        deleteInstructions += valuesToSelect[1] /*Valor a Buscar*/;

                                    }
                                }                               
                                break;

                            //tableName || columnName,type,specialAttribute|columnName,type,specialAttribute
                            case "CREATE TABLE":

                                //Aumentar i para empezar a analizar despues de CREATE TABLE
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                createTableInstructions += lines[i] + "||";


                                //Aumentar i para empezar a tomar despues de la palabra concatenada

                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                //Saltar (
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                while (lines[i] != ")")
                                {
                                    var columnsInfo = lines[i].Split(' ');

                                    if (columnsInfo.Length > 2)
                                        createTableInstructions += columnsInfo[0].Trim() + "," + columnsInfo[1].Trim() + "," + "KEY" + "|";
                                    else
                                        createTableInstructions += columnsInfo[0].Trim() + "," + columnsInfo[1].TrimEnd(',') + "," + "NULL" + "|";

                                    //Aumentar i para empezar a tomar despues de la palabra concatenada
                                    i++;

                                    i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                }

                                // Solo quito el pipe del final para mantener el formato
                                createTableInstructions = createTableInstructions.TrimEnd('|');
                                break;

                                //tableName
                            case "DROP TABLE":
                                //Aumentar i para empezar a analizar despues de DROP TABLE
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                dropTableInstructions += lines[i].Trim();

                                break;

                            //tableName||columnName,columnName,columnName||value,value,value
                            case "INSERT INTO":

                                // Aumentar i para empezar a leer despues del insert
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                insertInstructions += lines[i].Trim() + "||";

                                // Aumentar i para empezar a tomar despues de la palabra concatenada
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                // Aumentar i para pasar el (
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                while (lines[i] != ")")
                                {
                                    lines[i] = lines[i].TrimEnd(',');
                                    if (lines[i + 1] != ")")
                                        insertInstructions += lines[i].Trim() + ",";
                                    else
                                        insertInstructions += lines[i].Trim() + "||";

                                    // Aumentar i para empezar a tomar despues de la palabra concatenada
                                    i++;

                                    i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);
                                }

                                // Aumentar i para ignora )
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                // Aumentar i para ignorar "VALUES"
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                // Aumentar i para pasar el (
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                while (lines[i] != ")")
                                {
                                    lines[i] = lines[i].TrimEnd(',');
                                    lines[i] = lines[i].Trim("'".ToCharArray());
                                    lines[i] = lines[i].Trim();
                                    if (lines[i + 1] != ")")
                                        insertInstructions += lines[i] + ",";
                                    else
                                        insertInstructions += lines[i].TrimEnd(',');

                                    i++;

                                    i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);
                                }
                                break;


                                //tableName||columnName,newValue||valorLlavePrimaria
                            case "UPDATE":

                                // Aumentar i para empezar a leer despues del UPDATE
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                updateInstructions += lines[i].Trim() + "||";

                                // Aumentar i para empezar a leer despues del dato concatenado
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                // Aumentar i para saltar el "SET"
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                var newValueToSet = lines[i].Trim().Split('=');

                                newValueToSet[0] = newValueToSet[0].Trim();
                                newValueToSet[1] = newValueToSet[1].Trim();
                                newValueToSet[1] = newValueToSet[1].Trim("'".ToCharArray());
                                newValueToSet[1] = newValueToSet[1].Trim();

                                updateInstructions += newValueToSet[0] + "," + newValueToSet[1] + "||";

                                // Aumentar i para empezar a leer despues del dato concatenado
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                // Aumentar i parar saltar el "WHERE"
                                i++;

                                i = CheckIfThereIsAWhiteSpaceOrALineBreak(lines, i);

                                var primaryKeyValue = lines[i].Trim().Split('=');

                                primaryKeyValue[1] = primaryKeyValue[1].Trim();
                                primaryKeyValue[1] = primaryKeyValue[1].Trim("'".ToCharArray());

                                updateInstructions += primaryKeyValue[1];
                                break;
                        }
                    }
                    else
                        j++;
                }
                i++; // Si no se encuentra hay que comparar con la otra linea
                j = 0; // Se pone en 0 para poder volver a comparar la palabra de la linea con la del arreglo desde el inicio.
            }


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

        public static int CheckIfThereIsAWhiteSpaceOrALineBreak(string[] lines, int i)
        {
            if (i < lines.Length)
            {
                //En caso de que haya espacios o saltos de lineas los saltamos
                if (lines[i] == "" || lines[i] == " ")
                {
                    while (lines[i] == " " || lines[i] == "")
                    {
                        i++;
                    }
                }
            }
            return i;
        }
    }
}
