using System;

namespace ReactiveFlickr
{
    public interface IImageService
    {
        IObservable<SearchResultViewModel> GetImages(string searchText);
    }
}
