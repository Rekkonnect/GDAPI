using GDAPI.Utilities.Objects.GeometryDash.LevelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.ObjectSets
{
    /// <summary>Represents a collection of objects in a 2D grid for pattern making.</summary>
    public class ObjectGrid
    {
        /// <summary>The 2D grid of objects in the collection in the form [row, column].</summary>
        public GeneralObject[,] Grid;

        /// <summary>The total rows of the <seealso cref="ObjectGrid"/>.</summary>
        public int Rows => Grid.GetLength(0);
        /// <summary>The total columns of the <seealso cref="ObjectGrid"/>.</summary>
        public int Columns => Grid.GetLength(1);

        /// <summary>Initializes a new instance of the <seealso cref="ObjectGrid"/> class from a single <seealso cref="GeneralObject"/>.</summary>
        /// <param name="g">The <seealso cref="GeneralObject"/> to create the <seealso cref="ObjectGrid"/> from.</param>
        public ObjectGrid(GeneralObject g)
        {
            Grid = new GeneralObject[1, 1];
            Grid[0, 0] = g;
        }

        /// <summary>Initializes a new instance of the <seealso cref="ObjectGrid"/> class from a 2D array of <seealso cref="GeneralObject"/>s.</summary>
        /// <param name="g">The <seealso cref="GeneralObject"/>s to create the <seealso cref="ObjectGrid"/> from.</param>
        public ObjectGrid(GeneralObject[,] g)
        {
            Grid = g;
        }
    }
}
