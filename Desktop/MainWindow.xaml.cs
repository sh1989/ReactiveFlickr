using System.Windows;

namespace ReactiveFlickr.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var service = new FlickrImageService();
            DataContext = new FlickrSearchViewModel(service);
        }
    }
}
