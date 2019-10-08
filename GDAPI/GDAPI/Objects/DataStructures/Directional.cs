using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Objects.DataStructures
{
    /// <summary>Represents a collection of objects in the 9 directions (top left, top center, top right, middle left, center, middle right, bottom left, bottom center, bottom right).</summary>
    /// <typeparam name="T">The type of the objects in the collection.</typeparam>
    public class Directional<T>
    {
        /// <summary>The items of the directional object stored in the form [y, x] from top left to bottom right.</summary>
        private readonly T[,] items = new T[3, 3];

        /// <summary>The item in the top left direction of the <seealso cref="Directional{T}"/>.</summary>
        public T TopLeft
        {
            get => this[Direction.TopLeft];
            set => this[Direction.TopLeft] = value;
        }
        /// <summary>The item in the top center direction of the <seealso cref="Directional{T}"/>.</summary>
        public T TopCenter
        {
            get => this[Direction.TopCenter];
            set => this[Direction.TopCenter] = value;
        }
        /// <summary>The item in the top right direction of the <seealso cref="Directional{T}"/>.</summary>
        public T TopRight
        {
            get => this[Direction.TopRight];
            set => this[Direction.TopRight] = value;
        }
        /// <summary>The item in the middle left direction of the <seealso cref="Directional{T}"/>.</summary>
        public T MiddleLeft
        {
            get => this[Direction.MiddleLeft];
            set => this[Direction.MiddleLeft] = value;
        }
        /// <summary>The item in the center of the <seealso cref="Directional{T}"/>.</summary>
        public T Center
        {
            get => this[Direction.Center];
            set => this[Direction.Center] = value;
        }
        /// <summary>The item in the middle right direction of the <seealso cref="Directional{T}"/>.</summary>
        public T MiddleRight
        {
            get => this[Direction.MiddleRight];
            set => this[Direction.MiddleRight] = value;
        }
        /// <summary>The item in the bottom left direction of the <seealso cref="Directional{T}"/>.</summary>
        public T BottomLeft
        {
            get => this[Direction.BottomLeft];
            set => this[Direction.BottomLeft] = value;
        }
        /// <summary>The item in the bottom center direction of the <seealso cref="Directional{T}"/>.</summary>
        public T BottomCenter
        {
            get => this[Direction.BottomCenter];
            set => this[Direction.BottomCenter] = value;
        }
        /// <summary>The item in the bottom right direction of the <seealso cref="Directional{T}"/>.</summary>
        public T BottomRight
        {
            get => this[Direction.BottomRight];
            set => this[Direction.BottomRight] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="Directional{T}"/> class.</summary>
        public Directional() { }

        /// <summary>Initializes a new instance of the <seealso cref="Directional{T}"/> class from a given array.</summary>
        /// <param name="array">The array to construct the directional from. The items have to be stored in the form of [y, x] from top left to bottom right.</param>
        public Directional(T[,] array)
        {
            if (array.GetLength(0) != 3 || array.GetLength(1) != 3)
                throw new Exception("The array has to be of size [3, 3].");
            items = array;
        }

        /// <summary>Gets or sets a value in this <seealso cref="Directional{T}"/> given a specific direction.</summary>
        /// <param name="d">The direction of the value.</param>
        public T this[Direction d]
        {
            get
            {
                var (x, y) = GetDirectionVector(d);
                return items[y, x];
            }
            set
            {
                var (x, y) = GetDirectionVector(d);
                items[y, x] = value;
            }
        }

        private (int x, int y) GetDirectionVector(Direction d) => ((int)d >> 2, (int)d & (1 << 4) - 1);
    }

    /// <summary>Represents a direction.</summary>
    public enum Direction
    {
        /// <summary>Represents the top direction.</summary>
        Top = 0,
        /// <summary>Represents the vertical middle.</summary>
        VerticalMiddle = 1 << 4,
        /// <summary>Represents the bottom direction.</summary>
        Bottom = 2 << 4,

        /// <summary>Represents the left direction.</summary>
        Left = 0,
        /// <summary>Represents the horizontal middle.</summary>
        HorizontalMiddle = 1,
        /// <summary>Represents the right direction.</summary>
        Right = 2,

        /// <summary>Represents the top left direction.</summary>
        TopLeft = Top | Left,
        /// <summary>Represents the top center direction.</summary>
        TopCenter = Top | HorizontalMiddle,
        /// <summary>Represents the top right direction.</summary>
        TopRight = Top | Right,
        /// <summary>Represents the middle left direction.</summary>
        MiddleLeft = VerticalMiddle | Left,
        /// <summary>Represents the center.</summary>
        Center = HorizontalMiddle | VerticalMiddle,
        /// <summary>Represents the middle right direction.</summary>
        MiddleRight = VerticalMiddle | Right,
        /// <summary>Represents the bottom left direction.</summary>
        BottomLeft = Bottom | Left,
        /// <summary>Represents the bottom center direction.</summary>
        BottomCenter = Bottom | HorizontalMiddle,
        /// <summary>Represents the bottom right direction.</summary>
        BottomRight = Bottom | Right,
    }
}