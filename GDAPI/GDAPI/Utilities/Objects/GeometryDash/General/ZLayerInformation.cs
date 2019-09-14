using GDAPI.Utilities.Enumerations.GeometryDash;
using static GDAPI.Utilities.Functions.GeometryDash.ValueGenerator;

namespace GDAPI.Utilities.Objects.GeometryDash.General
{
    /// <summary>Contains detailed information about a Z Layer.</summary>
    public struct ZLayerInformation
    {
        /// <summary>The relative position of the Z Layer (top or bottom).</summary>
        public ZLayerPosition Position;
        /// <summary>The layer.</summary>
        public int Layer;

        /// <summary>Initializes a new instance of the <seealso cref="ZLayerInformation"/> struct.</summary>
        /// <param name="position">The relative position of the Z Layer (top or bottom).</param>
        /// <param name="layer">The layer.</param>
        public ZLayerInformation(ZLayerPosition position, int layer)
        {
            Position = position;
            Layer = layer;
        }
        /// <summary>Initializes a new instance of the <seealso cref="ZLayerInformation"/> struct.</summary>
        /// <param name="zLayer">The <seealso cref="ZLayer"/> whose information to retrieve.</param>
        public ZLayerInformation(ZLayer zLayer)
        {
            Position = GetZLayerPosition(zLayer);
            Layer = (zLayer - ZLayer.B1) / 2;
            if (Position == ZLayerPosition.Bottom)
                Layer++;
        }

        /// <summary>Generates the <seealso cref="ZLayer"/> that this information matches.</summary>
        public ZLayer ToZLayer() => GenerateZLayer(Position, Layer);
    }
}
