using ReactiveUI;
using System;
using System.Reactive.Linq;

namespace ReactiveFlickr
{
    public class FlickrSearchViewModel : ReactiveObject
    {
        public FlickrSearchViewModel(IImageService imageService)
        {
            Images = new ReactiveList<SearchResultViewModel>();

            var canExecute = this.WhenAnyValue(x => x.SearchText)
                .Select(x => !String.IsNullOrWhiteSpace(x));

            Search = ReactiveCommand.CreateAsyncObservable(
                canExecute,
                _ =>
                {
                    Images.Clear();
                    ShowError = false;
                    return imageService.GetImages(SearchText);
                });

            Search.Subscribe(images => Images.Add(images));

            Search.ThrownExceptions.Subscribe(_ => ShowError = true);

            isLoading = Search.IsExecuting.ToProperty(this, vm => vm.IsLoading);

            canEnterSearchText = this.WhenAnyValue(x => x.IsLoading)
                .Select(x => !x)
                .ToProperty(this, vm => vm.CanEnterSearchText);
        }

        public ReactiveCommand<SearchResultViewModel> Search { get; set; }

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

        private readonly ObservableAsPropertyHelper<bool> canEnterSearchText;
        public bool CanEnterSearchText
        {
            get { return canEnterSearchText.Value; }
        }

        private ReactiveList<SearchResultViewModel> images;
        public ReactiveList<SearchResultViewModel> Images
        {
            get { return images; }
            set { this.RaiseAndSetIfChanged(ref images, value); }
        }
        
    }
}
