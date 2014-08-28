using ReactiveUI;
using Splat;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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


        public async Task<ReactiveList<SearchResult>> GetImages(string searchTerm)
        {
            var list = new ReactiveList<SearchResult>();
            var c = new HttpClient();
            var address = new Uri(string.Format(searchUrlFormat, searchTerm));
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

            foreach (var photo in photos.Take(12))
            {
                var imageData = await c.GetByteArrayAsync(photo.Url);
                var stream = new MemoryStream(imageData);
                var image = await BitmapLoader.Current.Load(stream, null, null);
                list.Add(new SearchResult(image, photo.Title));
            }

            /*
             * expected response:
             * <photos>
             *  <photo id="" owner="" secret="" server="" title="" farm="" />
             * </photos>
             * 
             * to map to a url:
             * https://farm{farm}.staticflickr.com/{server}/{id}_{secret}.jpg
             * 
             * optionally add _[size], where size =
             * s: 75x75
             * q: 150x150
             * 
             * t: 100 on longest side
             * m: 240 on longest side
             * n: 320 on longest side
             * -: 500 on longest side
             * z: 640 on longest side
             * c: 800 on longest side
             * b: 1024 on longest side
             * 
             * o: original
             */

            //await Task.Delay(rand.Next(1000, 3000));

            return list;
        }
    }
}
