using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace FlickrSearch
{
    public class FlickrSearchViewModel : ReactiveObject
    {
        public FlickrSearchViewModel(IImageService imageService)
        {
            var canExecute = this.WhenAnyValue(x => x.SearchText)
                .Select(x => !String.IsNullOrWhiteSpace(x));

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(500))
                .InvokeCommand(Update);

            Update = ReactiveCommand.CreateAsyncTask(canExecute, o =>
            {
                Console.WriteLine(SearchText);
                ShowError = false;
                return imageService.GetImages(SearchText);
            });
            Update.Subscribe(images =>
            {
                Images.Clear();
                Images = images;
            });
            Update.ThrownExceptions.Subscribe(_ => ShowError = true);
            isLoading = Update.IsExecuting.ToProperty(this, vm => vm.IsLoading);
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set { this.RaiseAndSetIfChanged(ref searchText, value); }
        }

        private bool showError;
        public bool ShowError
        {
            get { return showError; }
            set { this.RaiseAndSetIfChanged(ref showError, value); }
        }

        private readonly ObservableAsPropertyHelper<bool> isLoading;
        public bool IsLoading { get { return isLoading.Value; } }

        public ReactiveList<object> Images { get; set; }

        public ReactiveCommand<ReactiveList<object>> Update { get; set; }

        
    }
}
