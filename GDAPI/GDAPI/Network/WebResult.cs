using System.Net;

namespace GDAPI.Network
{
    /// <summary>Base class for a web result.</summary>
    public abstract class WebResult
    {
        /// <summary>Parses the result and fills in the data from the result.</summary>
        /// <param name="response">The response to fill in the response from.</param>
        // TODO (nao): Maybe this shouldn't exist at all?
        //             Since both web responses and object format are fairly similar, we can re-use the object format parsing
        //             and use that instead of having to write this function for each web result.
        public abstract void ParseWebResponse(string response);
        
        /// <summary>Status code given by the server.</summary>
        public HttpStatusCode StatusCode { get; internal set; }
    }
}