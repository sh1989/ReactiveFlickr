using Splat;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Xml.Linq;

namespace ReactiveFlickr
{
    public class FlickrImageService : IImageService
    {
        private readonly Random rand = new Random();
        private readonly static string endpoint = "flickr.photos.search";
        private readonly static string api_key = "16bbec210cb487b52ba397c3d98d4def";

        private readonly string searchUrlFormat = "https://api.flickr.com/services/rest?method=" + endpoint + "&api_key=" + api_key + "&text={0}&safe_search=1&content_type=1&media=photos";
        private readonly string photoUrlFormat = "https://farm{0}.staticflickr.com/{1}/{2}_{3}_q.jpg";

        public IObservable<SearchResultViewModel> GetImages(string searchText)
        {
            return Observable.Create<SearchResultViewModel>(async observer =>
            {
                var c = new HttpClient();
                var address = new Uri(string.Format(searchUrlFormat, searchText));
                var searchResults = await c.GetStringAsync(address);

                var pa = XDocument.Parse(searchResults)
                    .Descendants("photos")
                    .Descendants("photo");
                var photos = pa
                    .Select(p => new
                    {
                        Url = string.Format(
                            photoUrlFormat,
                            p.Attribute("farm").Value,
                            p.Attribute("server").Value,
                            p.Attribute("id").Value,
                            p.Attribute("secret").Value),
                        Title = p.Attribute("title").Value
                    });

                foreach (var photo in photos)
                {
                    try
                    {
                        var imageData = await c.GetByteArrayAsync(photo.Url);
                        var stream = new MemoryStream(imageData);
                        var image = await BitmapLoader.Current.Load(stream, null, null);
							observer.OnNext(new SearchResultViewModel(image, photo.Title));
                    }
                    catch (HttpRequestException e)
                    {
                        // Right now do nothing
                    }
                }
                observer.OnCompleted();
            });
        }
    }
}
