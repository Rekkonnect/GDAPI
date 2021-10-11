using System.Collections.Generic;
using System.Net.Http;

namespace GDAPI.Network
{
    /// <summary>A static network client to handle and queue web requests.</summary>
    public static class NetworkClient
    {
        private static readonly HttpClient client = new();

        private static readonly Queue<WebRequest> requestQueue = new();

        /// <summary>Enqueues a web request.</summary>
        /// <param name="request">The web request to queue.</param>
        public static void Enqueue(WebRequest request)
        {
            requestQueue.Enqueue(request);
        }

        /// <summary>Dequeues and executes a web request.</summary>
        /// <typeparam name="T">The type of result it should convert into.</typeparam>
        public static T? Dequeue<T>()
            where T : WebResult, new()
            => requestQueue.TryDequeue(out var request) ? request.MakeRequest<T>(client) : null;
    }
}