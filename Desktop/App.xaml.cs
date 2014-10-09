using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ReactiveFlickr;
using ReactiveFlickr.Desktop;
using ReactiveUI;
using Splat;

namespace FlickrSearch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Locator.CurrentMutable.Register(() => new ImageTileView(), typeof(IViewFor<SearchResultViewModel>));
        }
    }
}
