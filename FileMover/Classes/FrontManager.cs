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
        public AnalizFile Sposob { get; set; }

        public FrontManager() 
        { 
        }
        public FrontManager(AnalizFile sposobSerching)
        {
            Sposob = sposobSerching;
        }
        public void FrontTabSearchLoadFromConfig(TextBox dirIn, TextBox dirOut, TextBox keyWordDir)
        {
            dirIn.Text = ConfigurationManager.AppSettings["dirIn"];
            dirOut.Text = ConfigurationManager.AppSettings["dirOut"];
            keyWordDir.Text = ConfigurationManager.AppSettings["keyWordDir"];
        }

        public void SavePropertiesToConfigFile(TextBox dirIn, TextBox dirOut, TextBox keyWordDir)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["dirIn"].Value = dirIn.Text;
            config.AppSettings.Settings["dirOut"].Value = dirOut.Text;
            config.AppSettings.Settings["keyWordDir"].Value=keyWordDir.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        public async Task ShowFrontTabAnalizAsync(TextBox textBox, DataGridView Dgw, System.Windows.Forms.Label lab1, System.Windows.Forms.Label lab2)
        {
            textBox.Text += $"cпособ поиска: {Sposob.SposobSearching} ";

            textBox.Text += $"\r\nвремя выполнения: {executionTime}";

            var countMatches = await CountMatchesAsync();
            textBox.Text += $"\r\nколичество файлов с совпадением: {countMatches}";

            //из последнего поиска считает в скольки файлах есть ключевое слово 
            var countFilesByWord = new List<SearchResult>(await CountFilesByMatchesAsync());
            foreach (SearchResult C in countFilesByWord)
            {
                textBox.Text += $"\r\nфайлов со словом: {C.key_word} = {C.count}шт";
            }

            // показывает в dataGridView1 все строки из последнего поиска
            Dgw.DataSource = await GetAllRowsLastSearchAsync();

            lab1.Text = $"Всего запросов: {new List<SearchResult>(TotalRequests()).Count.ToString()}";

            lab2.Text = $"Всего записей: {TotalRows().ToString()}";

            CloseConnection();
        }

    }

}
