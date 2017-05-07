using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataStructuresURL_3._0;



namespace Proyecto01
{
    enum SQLCommands { createTable, select, delete, dropTable, insert, updateTable }


    static class Program
    {


        public static string APPLICATION_TABLES_PATH = "C:/microSQL/tablas/";
        public static string APPLICATION_TREES_PATH = "C:/microSQL/arboles/";

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            System.IO.Directory.CreateDirectory("C:/microSQL/arboles");
            System.IO.Directory.CreateDirectory("C:/microSQL/tablas");

            
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form2());
            }
            catch (System.IO.FileNotFoundException e)
            {                
                MessageBox.Show("Archivo no encontrado: {0}", e.Message);
            }
            catch (Exception e)
            {
                //https://msdn.microsoft.com/en-us/library/ms229007(v=vs.110).aspx


                MessageBox.Show("Algo salió mal... ("+(e.Message)+") la aplicación se reiniciará");
                Application.Restart();
            }







        }
    }
}
