using System.Windows;
using ReactiveUI;

namespace ReactiveFlickr.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<FlickrSearchViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            var service = new FlickrImageService();
            ViewModel = new FlickrSearchViewModel(service);
        }

        public FlickrSearchViewModel ViewModel {
            get { return (FlickrSearchViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(FlickrSearchViewModel), typeof(MainWindow), new PropertyMetadata(null));

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = (FlickrSearchViewModel)value; }
        }
    }
}
