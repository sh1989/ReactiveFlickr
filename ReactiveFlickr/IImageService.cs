using ReactiveUI;
using System.Threading.Tasks;

namespace ReactiveFlickr
{
    public interface IImageService
    {
        Task<ReactiveList<SearchResult>> GetImages(string searchTerm);
    }
}
