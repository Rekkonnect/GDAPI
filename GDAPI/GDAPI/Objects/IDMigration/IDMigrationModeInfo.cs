using System.Collections.Generic;
using GDAPI.Objects.General;

namespace GDAPI.Objects.IDMigration
{
    /// <summary>Contains information about an ID migration mode.</summary>
    public class IDMigrationModeInfo
    {
        /// <summary>The name of the file that is currently associated to this ID migration's step list.</summary>
        public string FileName { get; set; }

        /// <summary>The step list of this ID migration.</summary>
        public List<SourceTargetRange> Steps = new();
    }
}
