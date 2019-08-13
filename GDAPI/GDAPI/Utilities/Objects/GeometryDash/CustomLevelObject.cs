using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Functions.General;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;
using GDAPI.Utilities.Functions.Extensions;

namespace GDAPI.Utilities.Objects.GeometryDash
{
    /// <summary>Represents a custom level object.</summary>
    public class CustomLevelObject
    {
        /// <summary>The objects of the custom object.</summary>
        public LevelObjectCollection LevelObjects;
        
        /// <summary>Creates a new instance of the <seealso cref="CustomLevelObject"/> class from the specified list of objects.</summary>
        /// <param name="levelObjects">The objects this custom object has.</param>
        public CustomLevelObject(LevelObjectCollection levelObjects)
        {
            LevelObjects = levelObjects.Clone();
            if (LevelObjects.Count > 0)
            {
                double avgX = 0;
                double avgY = 0;
                for (int i = 0; i < LevelObjects.Count; i++)
                {
                    avgX += LevelObjects[i].X;
                    avgY += LevelObjects[i].Y;
                }
                avgX /= LevelObjects.Count;
                avgY /= LevelObjects.Count;
                for (int i = 0; i < LevelObjects.Count; i++)
                {
                    LevelObjects[i].X -= avgX;
                    LevelObjects[i].Y -= avgY;
                }
            }
        }
    }
}