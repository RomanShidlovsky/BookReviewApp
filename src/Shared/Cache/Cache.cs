using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Serilog.Formatting.Json;

namespace Shared.Cache;

public static class Cache<T>
{
    public static T? GetData(byte[] cache)
    {
        var serializedData = Encoding.UTF8.GetString(cache);
        var data = JsonConvert.DeserializeObject<T>(serializedData);

        return data;
    }

    public static byte[] GetCache(T data, out DistributedCacheEntryOptions options)
    {
        var serializedData = JsonConvert.SerializeObject(data);
        var bytes = Encoding.UTF8.GetBytes(serializedData);

        options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(3))
            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10));

        return bytes;
    }
}