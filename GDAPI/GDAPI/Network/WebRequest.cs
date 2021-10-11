using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GDAPI.Enumerations;
using GDAPI.Functions.Extensions;

namespace GDAPI.Network
{
    /// <summary>Base class for a web request.</summary>
    public abstract class WebRequest
    {
        /// <summary>Relative URL path (starting from www.boomlings.com/)</summary>
        protected abstract string Path { get; }
        
        /// <summary>The full URL path.</summary>
        public virtual Uri FullPath => new (string.Concat("https://www.boomlings.com/", Path));
        
        /// <summary>Default properties this web request should always use. (e.g. secret)</summary>
        protected abstract object DefaultProperties { get; }
        
        /// <summary>Custom and or dynamic properties to be used.</summary>
        public object Properties { get; }
        
        /// <summary>The method to be using when attempting to make the request.</summary>
        public readonly WebRequestMethod Method;

        /// <summary>Instantiates this web request.</summary>
        /// <param name="method">The method to be using.</param>
        /// <param name="properties">The properties to be using. Note that in GET or DELETE requests, this will be unused.</param>
        protected WebRequest(WebRequestMethod method, object properties = null!)
        {
            Properties = properties!;
            Method = method;
        }

        /// <summary>Converts all of the object properties into a readable <see cref="Dictionary{TKey,TValue}"/>.</summary>
        /// <returns></returns>
        public Dictionary<string, string> PropertiesAsDictionary()
        {
            var def = DefaultProperties.ToDictionary();
            var props = Properties.ToDictionary();

            return def.Concat(props).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
        }

        /// <summary>Sends the web request to the server.</summary>
        /// <param name="client">The HTTP Client to be using.</param>
        /// <typeparam name="T">The type to convert to when done.</typeparam>
        public T MakeRequest<T>(HttpClient client)
            where T : WebResult
        {
            if (client == null)
                throw new NullReferenceException();

            var body = new FormUrlEncodedContent(PropertiesAsDictionary());
            var response = ExecuteSuitableMethod(client, body).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            
            body.Dispose();
            
            var webResult = Activator.CreateInstance(typeof(T)) as WebResult;
            webResult.ParseWebResponse(result);
            webResult.StatusCode = response.StatusCode;

            return ((T) webResult)!;
        }

        private Task<HttpResponseMessage> ExecuteSuitableMethod(HttpClient client, HttpContent content)
        {
            return Method switch
            {
                WebRequestMethod.Get => client.GetAsync(FullPath),
                WebRequestMethod.Post => client.PostAsync(FullPath, content),
                WebRequestMethod.Put => client.PutAsync(FullPath, content),
                WebRequestMethod.Patch => client.PatchAsync(FullPath, content),
                WebRequestMethod.Delete => client.DeleteAsync(FullPath),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}