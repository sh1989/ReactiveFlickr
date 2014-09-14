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

            this.Bind(ViewModel, vm => vm.SearchText, v => v.SearchText.Text);
            this.OneWayBind(ViewModel, vm => vm.CanEnterSearchText, v => v.SearchText.IsEnabled);
            this.BindCommand(ViewModel, vm => vm.Search, v => v.Search);

            this.OneWayBind(ViewModel, vm => vm.IsLoading, v => v.IsLoading.Visibility);

            // NB: Because Images doesn't have an ItemTemplate, ReactiveUI will 
            // give you one Automagically, that will look up Views based on their
            // IViewFor<ViewModel> type
            this.OneWayBind(ViewModel, vm => vm.Images, v => v.Images.ItemsSource);

            this.OneWayBind(ViewModel, vm => vm.ShowError, v => v.ShowError.Visibility);
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
