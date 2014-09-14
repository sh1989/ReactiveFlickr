using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
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
using ReactiveUI;
using Splat;

namespace ReactiveFlickr.Desktop
{
    /// <summary>
    /// Interaction logic for ImageTile.xaml
    /// </summary>
    public partial class ImageTileView : UserControl, IViewFor<SearchResultViewModel>
    {
        public ImageTileView()
        {
            InitializeComponent();

            this.WhenActivated(d => {
                d(this.WhenAny(x => x.ViewModel.Image, x => x.Value.ToNative())
                    .BindTo(this, x => x.ImageHost.Source));

                d(this.OneWayBind(ViewModel, vm => vm.Title, v => v.ImageHost.ToolTip));
            });
        }

        public SearchResultViewModel ViewModel {
            get { return (SearchResultViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(SearchResultViewModel), typeof(ImageTileView), new PropertyMetadata(null));

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = (SearchResultViewModel)value; }
        }
    }
}
