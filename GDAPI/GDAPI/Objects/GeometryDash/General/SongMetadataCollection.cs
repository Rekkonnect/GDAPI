using System.Collections.Generic;
using System.Text;

namespace GDAPI.Objects.GeometryDash.General
{
    /// <summary>Contains multiple song metadata information.</summary>
    public class SongMetadataCollection : List<SongMetadata>
    {
        /// <summary>Initializes a new instance of the <seealso cref="SongMetadataCollection"/> class.</summary>
        public SongMetadataCollection() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="SongMetadataCollection"/> class.</summary>
        /// <param name="l">The list to initialize this <seealso cref="SongMetadataCollection"/> from.</param>
        public SongMetadataCollection(List<SongMetadata> l) : base(l) { }

        /// <summary>Returns the <seealso cref="string"/> representation of the <seealso cref="SongMetadataCollection"/>.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("<d>");
            for (int i = 0; i < Count; i++)
                result = result.Append($"<k>{this[i].ID}</k><d>{this[i]}</d>");
            return result.Append("</d>").ToString();
        }
    }
}
