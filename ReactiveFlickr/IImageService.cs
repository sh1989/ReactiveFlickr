using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace FlickrSearch
{
    public interface IImageService
    {
        Task<ReactiveList<object>> GetImages(string searchTerm);
    }
}
