using FilesMouver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileMover.Classes
{
    internal class FrontManager: PostgreSqlManager
    {
        public string executionTime { get; set; }
        public FrontManager() { }

        public async Task ShowFrontAsync(TextBox textBox, DataGridView dgv, System.Windows.Forms.Label lab1, System.Windows.Forms.Label lab2)
        {
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
            dgv.DataSource = await GetAllRowsLastSearchAsync();

            lab1.Text = $"Всего запросов: {new List<SearchResult>(TotalRequests()).Count.ToString()}";
            
            lab2.Text = $"Всего записей: {TotalRows().ToString()}";

            CloseConnection();
        }

    }

}
