using FilesMouver;
using FilesMove.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileMover.Classes
{
    internal class FrontManager : PostgreSqlManager
    {
        public string executionTime { get; set; }
        public AnalizFile SposobSerching { get; set; }
        public TextBox TextBox { get; set; }


        public DataGridView Dgw { get; set; }
        public System.Windows.Forms.Label Lab1 { get; set; }
        public System.Windows.Forms.Label Lab2 { get; set; }
        public FrontManager() { }
        public FrontManager(TextBox textBox, DataGridView Dgw, System.Windows.Forms.Label lab1, System.Windows.Forms.Label lab2, AnalizFile sposobSerching)
        {
            TextBox = textBox;
            this.Dgw = Dgw;
            Lab1 = lab1;
            Lab2 = lab2;
            SposobSerching = sposobSerching;
        }
        public void FrontTabSearch(TextBox dirIn, TextBox dirOut, TextBox keyWordDir)
        {
            dirIn.Text = ConfigurationManager.AppSettings["dirIn"];
            dirOut.Text = ConfigurationManager.AppSettings["dirOut"];
            keyWordDir.Text = ConfigurationManager.AppSettings["keyWordDir"];
        }
        public async Task ShowFrontTabAnalizAsync()
        {
            TextBox.Text += $"\r\ncпособ поиска: {SposobSerching.SposobSearching} ";

            TextBox.Text += $"\r\nвремя выполнения: {executionTime}";

            var countMatches = await CountMatchesAsync();
            TextBox.Text += $"\r\nколичество файлов с совпадением: {countMatches}";

            //из последнего поиска считает в скольки файлах есть ключевое слово 
            var countFilesByWord = new List<SearchResult>(await CountFilesByMatchesAsync());
            foreach (SearchResult C in countFilesByWord)
            {
                TextBox.Text += $"\r\nфайлов со словом: {C.key_word} = {C.count}шт";
            }

            // показывает в dataGridView1 все строки из последнего поиска
            Dgw.DataSource = await GetAllRowsLastSearchAsync();

            Lab1.Text = $"Всего запросов: {new List<SearchResult>(TotalRequests()).Count.ToString()}";

            
            Lab2.Text = $"Всего записей: {TotalRows().ToString()}";

            CloseConnection();
        }

    }

}
