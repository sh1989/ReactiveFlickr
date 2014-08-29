using System;

namespace ReactiveFlickr
{
    public interface IImageService
    {
        IObservable<SearchResult> GetImages(string searchText);
    }
}
