using System;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using GDAPI.Enumerations;
using GDAPI.Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using WebRequest = GDAPI.Network.WebRequest;

namespace GDAPI.Tests.Network
{
    public class WebRequestTest
    {
        [TestCase(WebRequestMethod.Get)]
        [TestCase(WebRequestMethod.Delete)]
        [TestCase(WebRequestMethod.Post)]
        [TestCase(WebRequestMethod.Put)]
        [TestCase(WebRequestMethod.Patch)]
        public void TestMakeWebRequest(WebRequestMethod method)
        {
            var req = new TestWebRequest(method, new { text = "lol", boolean = true, number = 23 });
            var result = req.MakeRequest<TestWebResult>(new HttpClient());
            
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual($"https://httpbin.org/{method.ToString().ToLower()}", result.Data.url);

            if (method == WebRequestMethod.Post || method == WebRequestMethod.Put || method == WebRequestMethod.Patch)
            {
                Assert.AreEqual("lol", result.Data.form.text);
                Assert.AreEqual("23", result.Data.form.number);
                Assert.AreEqual("True", result.Data.form.boolean);
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