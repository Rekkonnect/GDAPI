using System.Collections.Generic;
using System.Text;

namespace GDAPI.Objects.GeometryDash
{
    /// <summary>Contains the folder names of the gamesave.</summary>
    public class FolderNameCollection : Dictionary<int, string>
    {
        /// <summary>Initializes a new instance of the <seealso cref="FolderNameCollection"/> class.</summary>
        public FolderNameCollection() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="FolderNameCollection"/> class.</summary>
        /// <param name="folders">The folders to create this <seealso cref="FolderNameCollection"/> from.</param>
        public FolderNameCollection(Dictionary<int, string> folders) : base(folders) { }

        /// <summary>Creates the <seealso cref="string"/> representation of the <seealso cref="FolderNameCollection"/>.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("<d>");
            foreach (var k in this)
                if (k.Value.Length > 0)
                    result.Append($"<k>{k.Key}</k><s>{k.Value}</s>");
            return result.Append("</d>").ToString();
        }
    }
}
