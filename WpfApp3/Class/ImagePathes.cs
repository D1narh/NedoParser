using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3.Class
{
    public class ImagePathes
    {
        public List<ImagePath> Paths { get; set; }
        public ImagePathes(List<ImagePath> paths)
        {
            Paths = paths;
        }

        public async void AllPathes()
        {
            foreach (var path in Paths)
            {
                var NewTask = Task.Factory.StartNew(() =>
                {
                    MessageBox.Show(path.Path);
                });
            }
        }
    }
}
