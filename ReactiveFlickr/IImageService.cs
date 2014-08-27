using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;

namespace ReactiveFlickr
{
    public interface IImageService
    {
        Task<ReactiveList<IBitmap>> GetImages(string searchTerm);
    }
}
