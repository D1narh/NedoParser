
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.Class;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("https://101hotels.com/main/cities/moskva?viewType=tiles&page=2");//проблема с отсрочкой в переменную

            var title2 = document.DocumentNode.SelectNodes("//*[@id=\"hidden-by-loader\"]").First().InnerText;

            //HtmlNodeCollection span = document.DocumentNode.SelectNodes("//span[@class=\"image\"]");


            //Ссылка на сайт
            var urls = new[] {
                "https://101hotels.com/main/cities/moskva?viewType=tiles&page=2"
            };


            DownloadFromSite DFS = new DownloadFromSite();
            var NewTask = Task.Factory.StartNew(() => 
            {
                Parallel.ForEach(urls, DFS.DownloadFiles);
            });
            ChetkayaStrika(title2);
        }

        public void ChetkayaStrika(string title2)
        {
            var texter = title2.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                            .Where(str => !string.IsNullOrWhiteSpace(str))
                            .Aggregate("", (rez, str) => rez + str).Trim();

            string stra = texter;

            while (stra.Contains("  "))
            {
                stra = stra.Replace("  ", " ");
            }
            text.Text = stra.Trim();
        }
    }
}
