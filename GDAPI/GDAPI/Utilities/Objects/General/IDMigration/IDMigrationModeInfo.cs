using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.General.IDMigration
{
    /// <summary>Contains information about an ID migration mode.</summary>
    public class IDMigrationModeInfo
    {
        /// <summary>The name of the file that is currently associated to this ID migration's step list.</summary>
        public string FileName { get; set; }

        /// <summary>The step list of this ID migration.</summary>
        public List<SourceTargetRange> Steps = new List<SourceTargetRange>();
    }
}
