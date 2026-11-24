
using System.Runtime.InteropServices;
using types.common;

namespace caches
{

    public class MediaCache
    {

        private Dictionary<string, MediaMetaData> _cache = new Dictionary<string, MediaMetaData>(); 
        
        public MediaCache()
        {
            // TODO: Implement checker on audio folder for existing audio files.
        }

        public void CacheMediaInfo(string id, MediaMetaData info)
        {
            Console.WriteLine($"Caching {id} where {info.Title} is stored in {info.filePath}");
            this._cache.Add(id, info);
        }

        public Dictionary<string, MediaMetaData> GetMediaCache()
        {
            return this._cache;
        }

    }




}