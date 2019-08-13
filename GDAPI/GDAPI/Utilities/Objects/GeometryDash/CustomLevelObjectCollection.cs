using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash
{
    /// <summary>Represents a collection of <seealso cref="CustomLevelObject"/>s.</summary>
    public class CustomLevelObjectCollection : List<CustomLevelObject>
    {
        /// <summary>Initializes a new instance of the <seealso cref="CustomLevelObjectCollection"/> class.</summary>
        public CustomLevelObjectCollection() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="CustomLevelObjectCollection"/> class.</summary>
        /// <param name="l">The list to initialize this <seealso cref="CustomLevelObjectCollection"/> from.</param>
        public CustomLevelObjectCollection(List<CustomLevelObject> l) : base(l) { }

        /// <summary>Returns the <seealso cref="string"/> representation of the <seealso cref="CustomLevelObjectCollection"/>.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("<d>");
            for (int i = 0; i < Count; i++)
                result = result.Append($"<k>{-i}</k><s>").Append(this[i]).Append("</s>");
            return result.Append("</d>").ToString();
        }
    }
}
