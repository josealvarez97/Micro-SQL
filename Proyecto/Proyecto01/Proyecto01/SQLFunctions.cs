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


        public static void ExecuteCommand(SQLCommands comando, RichTextBox richTextBox, DataGridView outputGridView)
        {
            //ExecuteCommand(SQLTextParser.ReadAction(richTextBox))
            switch (comando)
            {
                case SQLCommands.createTable:
                    CreateTable(SQLTextParser.GetCreateTableInstructions(richTextBox));
                    break;
                case SQLCommands.select:
                    Select(SQLTextParser.GetSelectInstructions(richTextBox), outputGridView);
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


        // modelo para utilizar una cola de instrucciones
        public static void ExecuteCommand(SQLCommands comando, string instruction, DataGridView outputGridView)
        {
            //ExecuteCommand(SQLTextParser.ReadAction(richTextBox))
            switch (comando)
            {
                case SQLCommands.createTable:
                    CreateTable(instruction);
                    break;
                case SQLCommands.select:
                    Select(instruction, outputGridView);
                    break;
                case SQLCommands.delete:
                    Delete(instruction);
                    break;
                case SQLCommands.dropTable:
                    DropTable(instruction);
                    break;
                case SQLCommands.insert:
                    Insert(instruction);
                    break;
                case SQLCommands.updateTable:
                    UpdateTable(instruction);
                    break;
                default:
                    break;

            }
        }
        public static void CreateTable(string createTableInstructions)
        {
            /*string createTableInstructions = "tableName||columnName,type,specialAttribute|columnName,type,specialAttribute";*/

            // Separar tableName de Columnas con arreglo generalFields

            string[] createTableFields = createTableInstructions.Split(new string[] { "||" },StringSplitOptions.RemoveEmptyEntries);// http://stackoverflow.com/questions/8928601/how-can-i-split-a-string-with-a-string-delimiter

            string tableName = createTableFields[0];
            //Separar columnas con arreglo columns
            string[] columns = createTableFields[1].Split('|');
            string rowInfo = createTableFields[1];

            Row rowModel = new Row(rowInfo);


            string btreeFilePath = Program.APPLICATION_TABLES_PATH + tableName + ".arbolb";
            string tableFilePath = Program.APPLICATION_TABLES_PATH + tableName + ".tabla";
            //Row rowModel = new Row();




            //Leer columnas de arreglo columns. PARA CREAR ROW MODEL
            //for (int i = 0; i < columns.Length; i++)
            //{
            //    //Separar atributo de columnas con arreglo columnAttributes
            //    string[] columnAttributes = columns[i].Split(',');

            //    //Agregar columna correspondiente a nuestra tabla
            //    switch (columnAttributes[1]/*type*/) // columnName en la 0, type en la 1, special attribute en la 2
            //    {
            //        case "INT":
            //            rowModel.intColumnsDictionary.Add(columnAttributes[0]/*columnName*/, new Int());
            //            break;
            //        case "VARCHAR-100":
            //            rowModel.varcharColumnsDictionary.Add(columnAttributes[0]/*columnName*/, new Varchar());
            //            break;
            //        case "DATETIME":
            //            rowModel.timeDateColumnsDictionary.Add(columnAttributes[0]/*columnName*/, new TimeDate());
            //            break;
            //        default:
            //            break;
            //    }

            //}

            //((IStringParseable<Row>)rowModel).ParseToObjectType(""); Cuando se implementa explicitamente la interfaz en una clase




            int order = 7; //no sabemos que ponerle...
            //Leer columnas de arreglo columns. PARA CREAR BTREE (o sea ahora solo buscamos la que tenga special attribute key...) 
            for (int i = 0; i < columns.Length; i++)
            {
                //Separar atributo de columnas con arreglo columnAttributes 
                string[] columnAttributes = columns[i].Split(',');

                // Crear arbol b cuando encontremos la columna que tiene el specialAttribute == KEY
                if (columnAttributes[2] == "KEY")
                    // Crear arbol de acuerdo al tipo de KEY requerido
                    switch (columnAttributes[1])
                    {
                        case "INT":
                            BTree<Int, Row> intTableBTree = new BTree<Int, Row>(order, btreeFilePath, rowModel);
                            intTableBTree.Create();
                            break;
                        case "VARCHAR-100":
                            BTree<Varchar, Row> varcharTableBTree = new BTree<Varchar, Row>(order, btreeFilePath, rowModel);
                            varcharTableBTree.Create();
                            break;
                        case "DATETIME":
                            BTree<TimeDate, Row> timeDateTableBTree = new BTree<TimeDate, Row>(order, btreeFilePath, rowModel);
                            timeDateTableBTree.Create();
                            break;
                        default:
                            break;
                    }
            }

            tableFilesHandler.CreateTableFile(tableFilePath, rowInfo);
        }

        public static void Select(string selectInstructions, DataGridView outputGridView)
        {
            outputGridView.Columns.Clear();
            // Select instructions model
            //columnName, columnName,columnName||tableName||columnaAFiltrar,valorABuscar

            // Cocinamos la data... para digerirla mejor...
            string[] selectInstructionsFields = selectInstructions.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            string[] columnNamesToSelect = selectInstructionsFields[0].Split(',');
            string tableName = selectInstructionsFields[1];
            string[] filterFields;
            string columnToFilter = "";
            string valueToSeek = "";
            bool seekSpecificValue;

            if(selectInstructionsFields.Length > 2)
            {
                filterFields = selectInstructionsFields[2].Split(',');
                columnToFilter = filterFields[0];
                valueToSeek = filterFields[1];
                seekSpecificValue = true;
            }
            else
            {
                seekSpecificValue = false;
            }

          

            string btreeFilePath = Program.APPLICATION_TABLES_PATH + tableName + ".arbolb";
            string tableFilePath = Program.APPLICATION_TABLES_PATH + tableName + ".tabla";
            string typeOfKey = tableFilesHandler.ReadTypeOfKey(tableFilePath);
            Row rowModel = tableFilesHandler.GetRowModelFromFile(tableFilePath);
            Row rowToAdd;
            int order = 7;

            // AÑADIR COLS AL GRID
            for (int i = 0; i < rowModel.columnNames.Count; i++)
            {
                if (columnNamesToSelect.Contains<string>(rowModel.columnNames[i]) || columnNamesToSelect[0] == "*" )
                    outputGridView.Columns.Add(rowModel.columnNames[i], rowModel.columnNames[i]);
            }



            // Como siempre dependemos del type of key para el codigo sobre jalar el arbol a RAM
            switch (typeOfKey)
            {
                case "INT":
                    // Jalamos arbol a RAM
                    BTree<Int, Row> ramBTreeInt = BTree<Int, Row>.ReadBTreeFromFile(btreeFilePath, order, rowModel);
                    // Recorremos arbol, o sea, recorremos cada fila de la tabla
                    foreach (Int key in ramBTreeInt)
                    {
                        // Recibimos row correspondiente a una key determinada
                        rowToAdd = ramBTreeInt.Search(key).value;

                        // Verificamos que dicha row contenga el valor valueToSeek para mostrarla / añadir al gridview, O QUE NO HAYAN RESTRICCIONES ( seekSpecificValue == false)
                        if (rowToAdd.ValueExistsInRow(columnToFilter, valueToSeek) || seekSpecificValue == false)
                            if (columnNamesToSelect[0] != "*")
                            {

                                outputGridView.Rows.Add(rowToAdd.RowValuesToString(columnNamesToSelect));
                            }
                            else
                            {
                                outputGridView.Rows.Add(rowToAdd.RowValuesToString());
                            }


                    }
                    break;
                case "VARCHAR(100)":
                    // Jalamos arbol a RAM
                    BTree<Varchar, Row> ramBTreeVarchar = BTree<Varchar, Row>.ReadBTreeFromFile(btreeFilePath, order, rowModel);
                    // Recorremos arbol, o sea, recorremos cada fila de la tabla
                    foreach (Varchar key in ramBTreeVarchar)
                    {
                        // Recibimos row correspondiente a una key determinada
                        rowToAdd = ramBTreeVarchar.Search(key).value;

                        // Verificamos que dicha row contenga el valor valueToSeek para mostrarla / añadir al gridview
                        if (rowToAdd.ValueExistsInRow(columnToFilter, valueToSeek) || seekSpecificValue == false)
                            if (columnNamesToSelect[0] != "*")
                            {
                                outputGridView.Rows.Add(rowToAdd.RowValuesToString(columnNamesToSelect));
                            }
                            else
                            {
                                outputGridView.Rows.Add(rowToAdd.RowValuesToString());
                            }
                    }
                    break;
                case "DATETIME":
                    // Jalamos arbol a RAM
                    BTree<TimeDate, Row> ramBTreeTimeDate = BTree<TimeDate, Row>.ReadBTreeFromFile(btreeFilePath, order, rowModel);
                    // Recorremos arbol, o sea, recorremos cada fila de la tabla
                    foreach (TimeDate key in ramBTreeTimeDate)
                    {
                        // Recibimos row correspondiente a una key determinada
                        rowToAdd = ramBTreeTimeDate.Search(key).value;

                        // Verificamos que dicha row contenga el valor valueToSeek para mostrarla / añadir al gridview
                        if (rowToAdd.ValueExistsInRow(columnToFilter, valueToSeek) || seekSpecificValue == false)
                            if (columnNamesToSelect[0] != "*")
                            {
                                outputGridView.Rows.Add(rowToAdd.RowValuesToString(columnNamesToSelect));
                            }
                            else
                            {
                                outputGridView.Rows.Add(rowToAdd.RowValuesToString());
                            }
                    }
                    break;
            }


        }
        public static void Delete(string deleteInstructions)
        {

        }
        public static void DropTable(string dropInstructions)
        {

        }
        public static void Insert(string insertInstructions)
        {
            //string insertInstructions = "tableName||columnName1,columnName2,columnName3||value1,value2,value3;

            string[] insertFields = insertInstructions.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            string tableName = insertFields[0];

            string[] columns = insertFields[1].Split(',');
            string[] values = insertFields[2].Split(',');

            string btreeFilePath = Program.APPLICATION_TABLES_PATH + tableName + ".arbolb";
            string tableFilePath = Program.APPLICATION_TABLES_PATH + tableName + ".tabla";


            string keyType = tableFilesHandler.ReadTypeOfKey(tableFilePath);

            Row rowModel = tableFilesHandler.GetRowModelFromFile(tableFilePath);
            int order = 7;//no sabemos que ponerle...



            Row rowToAdd = new Row();
            for (int i = 0; i < columns.Length; i++)
            {
                rowToAdd.columnNames.Add(columns[i]);
                // Insert corresponding value into Row
                switch (rowModel.columnTypeOrder[i])
                {
                    case ColumnType.INT:
                        rowToAdd.intColumnsDictionary.Add(columns[i], new Int().ParseToObjectType(values[i]));
                        rowToAdd.columnTypeOrder.Add(ColumnType.INT);
                        break;
                    case ColumnType.VARCHAR:
                        rowToAdd.varcharColumnsDictionary.Add(columns[i], new Varchar().ParseToObjectType(values[i]));
                        rowToAdd.columnTypeOrder.Add(ColumnType.VARCHAR);
                        break;
                    case ColumnType.TIMEDATE:
                        rowToAdd.timeDateColumnsDictionary.Add(columns[i], new TimeDate().ParseToObjectType(values[i]));
                        rowToAdd.columnTypeOrder.Add(ColumnType.TIMEDATE);
                        break;
                    default:
                        break;
                }
            }



            switch (keyType) // WE FIRST NEED TO KNOW WHAT KEY TYPE DOES THE TABLE (BTREE) HAS...
            {
                case "INT":
                    BTree<Int, Row> intRamBTree = BTree<Int, Row>.ReadBTreeFromFile(btreeFilePath, order, rowModel);
                    //// Get key where we want to insert rest of values. El constructor de Entry lo hará
                    //Int key = new Int().ParseToObjectType(values[0]);
                    // AÑADIR ENTRY
                    Entry<Int, Row> entryInts = new Entry<Int, Row>(values[0], new Row().ParseToString(rowToAdd));
                    intRamBTree.Insert(entryInts);
                    break;

                case "VARCHAR-100":
                    BTree<Varchar, Row> varcharRamBTree = BTree<Varchar, Row>.ReadBTreeFromFile(btreeFilePath, order, rowModel);
                    // AÑADIR ENTRY
                    Entry<Varchar, Row> entryVarchars = new Entry<Varchar, Row>(values[0], new Row().ParseToString(rowToAdd));
                    varcharRamBTree.Insert(entryVarchars);
                    break;

                case "TIMEDATE":
                    BTree<TimeDate, Row> timeDateRamBTree = BTree<TimeDate, Row>.ReadBTreeFromFile(btreeFilePath, order, rowModel);
                    // AÑADIR ENTRY
                    Entry<TimeDate, Row> entryTimeDates = new Entry<TimeDate, Row>(values[0], new Row().ParseToString(rowToAdd));
                    timeDateRamBTree.Insert(entryTimeDates);
                    break;
            }

        }
        public static void UpdateTable(string updateInstructions)
        {

        }







    }



}



/*
 * About NON-TYPE PARAMETERS IN C++, why they're kinda impossible in C#
 * 
 * 
 * https://www.quora.com/What-data-structure-is-used-in-SQL-database
http://use-the-index-luke.com/sql/anatomy/the-tree
http://sqlity.net/en/2445/b-plus-tree/
https://www.google.com.gt/search?q=work+around+for+nontype+class+parameters+in+c%23&oq=work+around+for+nontype+class+parameters+in+c%23&aqs=chrome..69i57.10830j0j1&sourceid=chrome&ie=UTF-8
http://stackoverflow.com/questions/24886947/workaround-for-new-constraint-with-parameters-in-generics
http://stackoverflow.com/questions/15423191/is-it-possible-to-pass-an-attribute-argument-from-a-derived-class-to-its-base-cl
http://joelabrahamsson.com/a-neat-little-type-inference-trick-with-c/
http://blog.slaks.net/2015-06-16/code-snippets-variadic-generics-csharp/
https://forums.asp.net/t/1461172.aspx?Who+can+help+me+with+this+question+
https://www.codeproject.com/Articles/257589/An-Idiots-Guide-to-Cplusplus-Templates-Part
https://en.wikipedia.org/wiki/Variadic_template
https://www.quora.com/
https://www.quora.com/What-C++-templates-can-do-but-generics-in-Java-C-cant
http://www.linuxtopia.org/online_books/programming_books/c++_practical_programming/c++_practical_programming_106.html
https://msdn.microsoft.com/en-us/library/x5w1yety.aspx
http://stackoverflow.com/questions/4636512/c-sharp-generic-with-constant
https://www.google.com.gt/search?q=non-type+template+parameters+c%23&oq=non-type+template+parameters+c%23&aqs=chrome..69i57.9741j0j1&sourceid=chrome&ie=UTF-8#q=non-type+template+parameters+c%23&start=10
http://csharpindepth.com/Articles/General/Overloading.aspx
http://www.cplusplus.com/doc/tutorial/functions2/
https://msdn.microsoft.com/en-us/library/c6cyy67b.aspx
https://github.com/Microsoft/TypeScript/issues/209
https://blogs.msdn.microsoft.com/ericlippert/2009/12/10/constraints-are-not-part-of-the-signature/
http://stackoverflow.com/questions/9620320/overloading-generic-type-parameters-disallowed
https://msdn.microsoft.com/en-us/library/ms173129.aspx
https://msdn.microsoft.com/en-us/library/0zk36dx2.aspx
https://msdn.microsoft.com/en-us/library/sz6zd40f.aspx
https://docs.microsoft.com/es-es/dotnet/articles/csharp/
https://docs.microsoft.com/es-es/visualstudio/
https://msdn.microsoft.com/en-us/library/ms173149.aspx
https://msdn.microsoft.com/en-us/library/system.diagnostics.stopwatch(v=vs.110).aspx
 * 
 * 
 * 
 */

