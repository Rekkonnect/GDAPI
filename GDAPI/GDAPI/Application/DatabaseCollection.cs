using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Application
{
    /// <summary>Represents a collection of databases.</summary>
    public class DatabaseCollection : List<Database>
    {
        /// <summary>Initializes a new instance of the <seealso cref="DatabaseCollection"/> class with the default database object.</summary>
        public DatabaseCollection() : base()
        {
            Add(new Database());
        }
    }
}
