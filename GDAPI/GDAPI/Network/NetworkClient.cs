using System.Collections.Generic;
using System.Net.Http;

namespace GDAPI.Network
{
    /// <summary>A network client to handle and queue web requests.</summary>
    public class NetworkClient : HttpClient
    {
        private readonly Queue<WebRequest> requestQueue = new();

        /// <summary>Enqueues a web request.</summary>
        /// <param name="request">The web request to queue.</param>
        public void Enqueue(WebRequest request)
        {
            requestQueue.Enqueue(request);
        }

        /// <summary>Dequeues and executes a web request.</summary>
        /// <typeparam name="T">The type of result it should convert into.</typeparam>
        public T? Dequeue<T>()
            where T : WebResult, new()
            => requestQueue.TryDequeue(out var request) ? request.MakeRequest<T>(this) : null;
    }
}