using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Net.Http.Formatting;
using System.Web.Script.Serialization;

namespace Printer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void plotButton_Click(object sender, RoutedEventArgs e)
        {
            string func = functionTextbox.Text;
            Add(func);
        }
        private void Add(string f)
        {
            HttpClient client = new HttpClient();
            Polyline polyline = new Polyline { Stroke = Brushes.Black };
            string url = string.Format("http://localhost:12952/api/Graph?function={0}", f);
            client.PostAsync(url,f, new JsonMediaTypeFormatter()).ContinueWith(response =>
            {
                if (response.Exception != null)
                {
                    MessageBox.Show(response.Exception.Message);
                }
                else
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    HttpResponseMessage message = response.Result;
                    string responseText = message.Content.ReadAsStringAsync().Result;
                    List<Point> list = jss.Deserialize<List<Point>>(responseText);


                    Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        (Action)(() =>
                        {
                            foreach (var item in list)
                            {
                                polyline.Points.Add(CorrespondingPoint(new Point(item.X, item.Y)));
                            }

                            canvas.Children.Add(polyline);
                        }));
                }

            });
        }

        private Point CorrespondingPoint(Point pt)
        {
            double xmin = 0;
            double xmax = 20;
            double ymin = -1.1;
            double ymax = 1.1;



            var result = new Point
            {
                X = (pt.X - xmin) * canvas.Width / (xmax * 10 - xmin),
                Y = canvas.Height - (pt.Y - ymin) * canvas.Height / (ymax - ymin)
            };
            return result;
        }

    }
}

