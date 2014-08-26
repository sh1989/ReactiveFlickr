using ReactiveUI;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FlickrSearch
{
    public class FlickrImageService : IImageService
    {
        private readonly Random rand = new Random();
        private readonly static string endpoint = "flickr.photos.search";
        private readonly static string api_key = "16bbec210cb487b52ba397c3d98d4def";
        private readonly string secret = "268fa82bf686cf02";

        private readonly string searchUrlFormat = "https://api.flickr.com/services/rest?method=" + endpoint + "&api_key=" + api_key + "&text={0}&safe_search=1&content_type=1&media=photos";
        private readonly string photoUrlFormat = "https://farm{0}.staticflickr.com/{1}/{2}_{3}_q.jpg";


        public async Task<ReactiveList<object>> GetImages(string searchTerm)
        {
            var c = new WebClient();
            var address = new Uri(string.Format(searchUrlFormat, searchTerm));
            var searchResults = await c.DownloadStringTaskAsync(address);

            // for each photo
            // var imageAddress = new Uri(string.Format(photoUrlFormat, photo.farm, photo.server, photo.id, photo.secret);
            // byte[] imageBytes = await wc.DownloadDataTaskAsync(imageAddress);
            // IBitmap image = await BitmapLoader.Current.Load(imageBytes, null, null);

            // In the view: ImageView.Source = <path to image on view model>.ToNative();


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

            await Task.Delay(rand.Next(1000, 3000));

            return new ReactiveList<object>();
        }
    }
}
