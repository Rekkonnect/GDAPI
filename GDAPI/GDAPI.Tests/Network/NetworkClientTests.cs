using System;
using System.Dynamic;
using System.Net;
using GDAPI.Enumerations;
using GDAPI.Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using WebRequest = GDAPI.Network.WebRequest;

namespace GDAPI.Tests.Network
{
    public class NetworkClientTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        public void TestEnqueueRequest(int times)
        {
            for (var i = 0; i < times; i++)
            {
                var req = new TestWebRequest(WebRequestMethod.Post, new { idx = i });
                NetworkClient.Enqueue(req);
            }

            for (var i = 0; i < times; i++)
            {
                var res = NetworkClient.Dequeue<TestWebResult>();
                Assert.IsNotNull(res);
                Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
                Assert.AreEqual(i.ToString(), res.Data.form.idx);
            }
        }
        
        public class TestWebRequest : WebRequest
        {
            public TestWebRequest(WebRequestMethod method, object properties = null)
                : base(method, properties!)
            {
            }

            public override Uri FullPath => new Uri($"https://httpbin.org/{Method.ToString().ToLower()}");
            protected override string Path => String.Empty;
            protected override object DefaultProperties => new { };
        }

        public class TestWebResult : WebResult
        {
            public dynamic Data { get; private set; }

            public override void ParseWebResponse(string response)
            {
                Data = JsonConvert.DeserializeObject<ExpandoObject>(response, new ExpandoObjectConverter());
                Console.WriteLine(response);
            }
        }
    }
}