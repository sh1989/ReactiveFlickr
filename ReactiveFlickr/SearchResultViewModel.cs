using Splat;
using ReactiveUI;

namespace ReactiveFlickr
{
    public class SearchResultViewModel : ReactiveObject
    {
        private readonly IBitmap image;
        private readonly string title;

        public SearchResultViewModel(IBitmap image, string title)
        {
            this.image = image;
            this.title = title;
        }

        public IBitmap Image { get { return image; } }
        public string Title { get { return title; } }
    }
}
