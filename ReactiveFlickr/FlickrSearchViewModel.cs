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

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(500), RxApp.MainThreadScheduler)
                .Select(x =>
                {
                    IsLoading = true;
                    ShowError = false;

                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return Task.FromResult(new ReactiveList<SearchResult>());
                    }

                    return imageService.GetImages(x);
                })
                .Switch()
                .SubscribeOn(RxApp.MainThreadScheduler)
                .Subscribe(
                    images =>
                    {
                        IsLoading = false;
                        Images = images;
                    },
                    exception => ShowError = true,
                    () => { });
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

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { this.RaiseAndSetIfChanged(ref isLoading, value); }
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
