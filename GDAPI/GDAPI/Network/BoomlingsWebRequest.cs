using System;
using GDAPI.Enumerations;

namespace GDAPI.Network
{
    /// <summary>A web request targeted to the boomlings.com website.</summary>
    public abstract class BoomlingsWebRequest : WebRequest
    {
        /// <inheritdoc />
        protected BoomlingsWebRequest(object? properties = null)
            : base(WebRequestMethod.Post, properties)
        {
        }

        /// <inheritdoc />
        public override Uri FullPath => new Uri(String.Concat("http://www.boomlings.com/database/", Path, ".php"));


        /// <inheritdoc />
        protected override object DefaultProperties => new
        {
            secret = "Wmfd2893gb7"
        };
    }
}