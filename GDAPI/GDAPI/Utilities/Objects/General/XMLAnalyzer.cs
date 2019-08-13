using GDAPI.Utilities.Functions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.General
{
    /// <summary>Represents an XML analyzer, containing functions to analyze an XML document and get its properties.</summary>
    public class XMLAnalyzer
    {
        // TODO: Add XML document validity checks
        /// <summary>The XML document.</summary>
        public string Document { get; }

        /// <summary>Initializes a new instance of the <seealso cref="XMLAnalyzer"/> class.</summary>
        /// <param name="document">The XML document.</param>
        public XMLAnalyzer(string document)
        {
            Document = document;
        }

        /// <summary>Analyzes the XML document and retrieves its information, invoking an <seealso cref="XMLEntryHandler"/> object for each entry.</summary>
        /// <param name="entryHandler">The method to invoke on each entry during the XML document's analysis.</param>
        public void AnalyzeXMLInformation(XMLEntryHandler entryHandler)
        {
            const string startKeyString = "<k>";
            const string endKeyString = "</k><";
            int IDStart, IDEnd;
            int valueTypeStart, valueTypeEnd;
            int valueStart, valueEnd;
            string valueType, value;

            for (int i = 0; i < Document.Length;)
            {
                IDStart = Document.Find(startKeyString, i, Document.Length) + startKeyString.Length;
                if (IDStart < startKeyString.Length)
                    break;
                IDEnd = Document.Find(endKeyString, IDStart, Document.Length);

                valueTypeStart = IDEnd + endKeyString.Length;
                valueTypeEnd = Document.Find(">", valueTypeStart, Document.Length);
                valueType = Document.Substring(valueTypeStart, valueTypeEnd - valueTypeStart);

                valueStart = valueTypeEnd + 1;
                valueEnd = valueType[valueType.Length - 1] != '/' ? Document.Find($"</{valueType}>", valueStart, Document.Length) : valueStart;
                value = Document.Substring(valueStart, valueEnd - valueStart);

                string s = Document.Substring(IDStart, IDEnd - IDStart);
                entryHandler?.Invoke(s, value, valueType);
                i = valueEnd;
            }
        }
    }

    /// <summary>Represents a method that handles an XML entry.</summary>
    /// <param name="key">The key of the XML entry.</param>
    /// <param name="value">The value of the XML entry.</param>
    /// <param name="valueType">The type of the value of the XML entry.</param>
    public delegate void XMLEntryHandler(string key, string value, string valueType);
}
