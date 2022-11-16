using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3.Class
{
    public class DownloadFromSite
    {
        private static readonly Regex ImgRegex = new Regex(@"\<img.+?src=\""(?<imgsrc>.+?)\"".+?\>",
            RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public void DownloadFiles(string site)
        {

            string data;
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(site))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        data = reader.ReadToEnd();
                    }
                }
            }

            // Создаём директорию под картинки
            string directory = new Uri(site).Host;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            List<ImagePath> path = new List<ImagePath>();

            ImgRegex.Matches(data)
                .Cast<Match>()
                //*Данный*из*группы*регулярного*выражения
                .Select(m => m.Groups["imgsrc"].Value.Trim())
                // Удаляем повторяющиеся
                .Distinct()
                //*Добавляем*название*сайта,*если*ссылки*относительные
                .Select(url => url.Contains("http://") ? url : (site + url))
                //*Получаем*название*картинки
                .Select(url => new { url, name = url.Split(new[] { '/' }).Last() })
                //*Проверяем*его
                .Where(arg => Regex.IsMatch(arg.name, @"[^\s\/]\.(jpg|png|gif|bmp)\z"))
                // Параллелим на 6 потоков
                .AsParallel()
                .WithDegreeOfParallelism(21)
                // Загружаем асинхронно
                .ForAll(value => {
                    string savePath = Path.Combine(directory, value.name);
                    using (WebClient localClient = new WebClient())
                    {
                        path.Add(new ImagePath(value.url));
                        ImagePath paths = new ImagePath(value.url);
                    }
                });
            ImagePathes pathes = new ImagePathes(path);
            pathes.AllPathes();
        }
    }
}
