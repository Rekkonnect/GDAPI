using System;
using System.Collections.Generic;

namespace GDAPI.Application
{
    /// <summary>Represents a collection of databases.</summary>
    public class DatabaseCollection : List<Database>
    {
        /// <summary>Initializes a new instance of the <seealso cref="DatabaseCollection"/> class with the default database object.</summary>
        [Obsolete("Prefer the other constructor with the option to include a default instead.")]
        public DatabaseCollection()
            : this(true) { }

        /// <summary>
        /// Creates a new <see cref="DatabaseCollection"/> with the option to
        /// include the default game database.
        /// </summary>
        /// <param name="includeDefault">
        /// <see langword="true"/> to also include the default game database,
        /// otherwise <see langword="false"/>.
        /// </param>
        public DatabaseCollection(bool includeDefault)
        {
            if (includeDefault)
            {
                Add(new Database());
            }
        }

        public static DatabaseCollection InitializeWithoutDefault()
        {
            return new DatabaseCollection(false);
        }
    }
}
