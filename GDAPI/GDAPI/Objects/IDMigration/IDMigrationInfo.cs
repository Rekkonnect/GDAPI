using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.General;
using System.Collections.Generic;

namespace GDAPI.Objects.IDMigration
{
    /// <summary>Contains information about all ID migration modes.</summary>
    public class IDMigrationInfo
    {
        private readonly IDMigrationModeInfo[] modes = new IDMigrationModeInfo[4];

        private LevelObjectIDType selectedIDMigrationMode;

        /// <summary>Gets or sets the currently selected ID migration mode.</summary>
        public LevelObjectIDType SelectedIDMigrationMode
        {
            get => selectedIDMigrationMode;
            set => CurrentlySelectedIDMigrationModeInfo = this[selectedIDMigrationMode = value];
        }

        /// <summary>Gets the currently selected mode's <seealso cref="IDMigrationModeInfo"/> object.</summary>
        public IDMigrationModeInfo CurrentlySelectedIDMigrationModeInfo { get; private set; }
        /// <summary>Gets or sets the currently selected ID migration steps.</summary>
        public List<SourceTargetRange> CurrentlySelectedIDMigrationSteps
        {
            get => CurrentlySelectedIDMigrationModeInfo.Steps;
            set => CurrentlySelectedIDMigrationModeInfo.Steps = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="IDMigrationInfo"/> class.</summary>
        /// <param name="defaultMode">The default ID migration mode to initialize this ID migration info object with.</param>
        public IDMigrationInfo(LevelObjectIDType defaultMode = default)
        {
            InitializeModes();
            SelectedIDMigrationMode = defaultMode;
        }

        /// <summary>Adds a new ID migration step to the currently selected mode's ranges.</summary>
        /// <param name="range">The ID migration step to add to the currently selected ID migration.</param>
        public void AddIDMigrationStep(SourceTargetRange range) => CurrentlySelectedIDMigrationSteps.Add(range);
        /// <summary>Adds a range of new ID migration steps to the currently selected mode's ranges.</summary>
        /// <param name="ranges">The ID migration steps to add to the currently selected ID migration.</param>
        public void AddIDMigrationSteps(List<SourceTargetRange> ranges) => CurrentlySelectedIDMigrationSteps.AddRange(ranges);
        /// <summary>Removes a new ID migration step from the currently selected mode's ranges.</summary>
        /// <param name="range">The ID migration step to remove from the currently selected ID migration.</param>
        public void RemoveIDMigrationStep(SourceTargetRange range) => CurrentlySelectedIDMigrationSteps.Remove(range);
        /// <summary>Removes a range of new ID migration steps from the currently selected mode's ranges.</summary>
        /// <param name="ranges">The ID migration steps to remove from the currently selected ID migration.</param>
        public void RemoveIDMigrationSteps(List<SourceTargetRange> ranges)
        {
            foreach (var r in ranges)
                CurrentlySelectedIDMigrationSteps.Remove(r);
        }
        // TODO: Add cloning method

        /// <summary>Gets the <seealso cref="IDMigrationModeInfo"/> object of the specified mode.</summary>
        /// <param name="mode">The ID migration mode to get the <seealso cref="IDMigrationModeInfo"/> object of.</param>
        public IDMigrationModeInfo this[LevelObjectIDType mode] => modes[(int)mode];

        private void InitializeModes()
        {
            for (int i = 0; i < 4; i++)
                modes[i] = new IDMigrationModeInfo();
        }
    }
}
