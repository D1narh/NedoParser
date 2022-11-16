
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            var title3 = document.DocumentNode.SelectNodes("//*[@itemprop=\"image\"]");


            HtmlNodeCollection span = document.DocumentNode.SelectNodes("//span[@class=\"image\"]");

            foreach(var item in span)
            {

                text2.Text += " " + item.InnerHtml.Replace("  ", " ");
            }


            //var title = document.DocumentNode.SelectNodes("//*[@id=\"hidden-by-loader\"]/li/article/div/div/div/div/div[3]/span/img").First();//проблема с изображением тут

            var texter = title2.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                               .Where(str => !string.IsNullOrWhiteSpace(str))
                               .Aggregate("", (rez, str) => rez + str).Trim();

            //var texter = title2.ToString();

            //title2.ToList().ForEach(x => { texter = x.InnerHtml; });

            string stra = texter;

            while (stra.Contains("  "))
            {
                stra = stra.Replace("  ", " ");
            }
            text.Text = stra.Trim();

            //text2.Text = title3;

        }
    }
}
