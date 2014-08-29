using ReactiveUI;
using Splat;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ReactiveFlickr
{
    public class FlickrSearchViewModel : ReactiveObject
    {
        public FlickrSearchViewModel(IImageService imageService)
        {
            Images = new ReactiveList<SearchResult>();

            var canExecute = this.WhenAnyValue(x => x.SearchText)
                .Select(x => !String.IsNullOrWhiteSpace(x));

            Search = ReactiveCommand.CreateAsyncTask(
                canExecute,
                _ =>
                {
                    ShowError = false;
                    return imageService.GetImages(SearchText);
                });
            Search.Subscribe(images => Images = images);
            Search.ThrownExceptions.Subscribe(_ => ShowError = true);

            isLoading = Search.IsExecuting.ToProperty(this, vm => vm.IsLoading);
        }

        public ReactiveCommand<ReactiveList<SearchResult>> Search { get; set; }

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
        public bool IsLoading
        {
            get { return isLoading.Value; }
        }

        private ReactiveList<SearchResult> images;
        public ReactiveList<SearchResult> Images
        {
            get { return images; }
            set { this.RaiseAndSetIfChanged(ref images, value); }
        }

        public ReactiveCommand<ReactiveList<IBitmap>> Update { get; set; }

        
    }
}
