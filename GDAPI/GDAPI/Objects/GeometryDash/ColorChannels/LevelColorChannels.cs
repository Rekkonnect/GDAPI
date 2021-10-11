using GDAPI.Enumerations.GeometryDash;
using GDAPI.Functions.Extensions;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.IDTypes;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Objects.GeometryDash.ColorChannels
{
    /// <summary>Represents the color channels of a level.</summary>
    public class LevelColorChannels
    {
        private ColorChannel[] colors = new ColorChannel[5000];

        /// <summary>Gets or sets the color at the specified color channel ID.</summary>
        /// <param name="colorID">The color channel ID whose color to get or set.</param>
        public ColorChannel this[int colorID]
        {
            get
            {
                if (colors[colorID - 1] == null)
                    colors[colorID - 1] = new ColorChannel(colorID);
                return colors[colorID - 1];
            }
            set => colors[colorID - 1] = value;
        }
        /// <summary>Gets or sets the color at the specified special color channel ID.</summary>
        /// <param name="colorID">The special color channel ID whose color to get or set.</param>
        public ColorChannel this[ColorID colorID]
        {
            get => this[(int)colorID];
            set => this[(int)colorID] = value;
        }
        /// <summary>Gets or sets the color at the specified special color channel ID.</summary>
        /// <param name="colorID">The special color channel ID whose color to get or set.</param>
        public ColorChannel this[SpecialColorID colorID]
        {
            get => this[(int)colorID];
            set => this[(int)colorID] = value;
        }

        /// <summary>Clones this <seealso cref="LevelColorChannels"/> instance and returns the cloned instance.</summary>
        public LevelColorChannels Clone()
        {
            LevelColorChannels result = new LevelColorChannels();
            result.colors = colors.CopyArray();
            return result;
        }

        /// <summary>Gets a <seealso cref="Dictionary{TKey, TValue}"/> mapping every <seealso cref="ColorChannel"/> to the <seealso cref="ColorChannel"/>s that copy its color through the <seealso cref="ColorChannel.CopiedColorID"/> property.</summary>
        public Dictionary<ColorChannel, List<ColorChannel>> GetCopiedColorChannelReferenceDictionary()
        {
            var result = new Dictionary<ColorChannel, List<ColorChannel>>();

            for (int i = 1; i <= colors.Length; i++)
                result.Add(this[i], new());

            for (int i = 1; i <= colors.Length; i++)
            {
                var copied = this[i].CopiedColorID;
                if (copied is 0)
                    continue;
                result[this[copied]].Add(this[i]);
            }
            return result;
        }

        /// <summary>Parses the level color string into a <seealso cref="LevelColorChannels"/> object.</summary>
        /// <param name="colorChannels">The level color string to parse.</param>
        public static LevelColorChannels Parse(string colorChannels)
        {
            string[] split = colorChannels.Split('|');
            LevelColorChannels result = new LevelColorChannels();
            foreach (var s in split)
            {
                if (s.Length == 0)
                    continue;
                var c = ColorChannel.Parse(s);
                result[c.ColorChannelID] = c;
            }
            return result;
        }
        /// <summary>Gets the common colors between a number of levels.</summary>
        /// <param name="levels">The levels to get the common colors of.</param>
        public static LevelColorChannels GetCommonColors(Level[] levels)
        {
            LevelColorChannels result = levels[0].ColorChannels.Clone();
            for (int i = 0; i < levels.Length; i++)
                for (int j = 0; j < result.colors.Length; j++)
                    if (result[j] != levels[i].ColorChannels[j])
                        result[j] = default;
            return result;
        }

        /// <summary>Returns the string of the <seealso cref="LevelColorChannels"/>.</summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var c in colors)
            {
                if (c is null)
                    continue;
                result.Append($"{c}|");
            }
            return result.ToString();
        }
    }
}
