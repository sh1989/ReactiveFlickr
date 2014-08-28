using Splat;

namespace ReactiveFlickr
{
    public class SearchResult
    {
        private readonly IBitmap image;
        private readonly string title;

        public SearchResult(IBitmap image, string title)
        {
            this.image = image;
            this.title = title;
        }

        public IBitmap Image { get { return image; } }
        public string Title { get { return title; } }
    }
}
