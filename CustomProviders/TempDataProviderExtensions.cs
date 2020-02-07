using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp.CustomProviders
{
    public static class TempDataProviderExtensions
    {
        public static void SetData<T>(this ITempDataDictionary temp, string key, T value) where T : class
        {
            temp[key] = JsonConvert.SerializeObject(value);
        }

        public static T GetData<T>(this ITempDataDictionary temp, string key) where T : class
        {
            object obj;
            temp.TryGetValue(key, out obj);
            return obj == null ? null : JsonConvert.DeserializeObject<T>((string)obj);
        }
    }
}
