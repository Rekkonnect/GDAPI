using static System.IO.File;

namespace GDAPI.Objects.Presets
{
    /// <summary>Represents a preset in the application.</summary>
    public abstract class Preset
    {
        /// <summary>Writes the contents of this preset to a file.</summary>
        /// <param name="path">The path of the file to write the contents of this preset.</param>
        public void SaveToFile(string path) => WriteAllText(path, ToString());
    }
}
