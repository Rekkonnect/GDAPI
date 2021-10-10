using System.Linq;

namespace GDAPI.Objects.GeometryDash.LevelObjects
{
    /// <summary>Represents a custom level object.</summary>
    public class CustomLevelObject
    {
        private LevelObjectCollection objects;

        /// <summary>The objects of the custom object.</summary>
        public LevelObjectCollection LevelObjects
        {
            get => objects;
            set
            {
                objects = value.Clone();
                if (!objects.Any())
                    return;
                
                double avgX = 0;
                double avgY = 0;
                for (int i = 0; i < objects.Count; i++)
                {
                    avgX += objects[i].X;
                    avgY += objects[i].Y;
                }
                avgX /= objects.Count;
                avgY /= objects.Count;
                for (int i = 0; i < objects.Count; i++)
                {
                    objects[i].X -= avgX;
                    objects[i].Y -= avgY;
                }
            }
        }
        
        /// <summary>Creates a new instance of the <seealso cref="CustomLevelObject"/> class from the specified list of objects.</summary>
        /// <param name="levelObjects">The objects this custom object has.</param>
        public CustomLevelObject(LevelObjectCollection levelObjects)
        {
            LevelObjects = levelObjects;
        }
    }
}