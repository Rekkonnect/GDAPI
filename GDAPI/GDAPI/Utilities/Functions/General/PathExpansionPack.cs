using GDAPI.Utilities.Enumerations;
using GDAPI.Utilities.Functions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.IO.Path;
using static System.Math;
using static System.String;

namespace GDAPI.Utilities.Functions.General
{
    /// <summary>This limited edition expansion pack provides more useful functions for path handling.</summary>
    public static class PathExpansionPack
    {
        /// <summary>Fixes the path by making the directory separators consistent depending on the platform and returns the replaced string.</summary>
        /// <param name="path">The path to fix.</param>
        public static string FixPath(string path) => path.Replace('/', DirectorySeparatorChar).Replace('\\', DirectorySeparatorChar);
        /// <summary>Fixes the directory path by ensuring there is a directory separator character at the end of the string.</summary>
        /// <param name="dirPath">The directory path to fix.</param>
        public static string FixDirectoryPath(string dirPath)
        {
            if (dirPath.Length == 0)
                return dirPath;
            var result = FixPath(dirPath);
            if (!result.EndsWith(DirectorySeparatorChar)) 
                result += DirectorySeparatorChar;
            return result;
        }

        /// <summary>Adds the directory suffix to a path and returns the new string.</summary>
        /// <param name="path">The path to add the directory suffix to.</param>
        public static string AddDirectorySuffix(string path) => $@"{path}{DirectorySeparatorChar}";
        /// <summary>Aggregates two directory names. Both must include the directory separator character; the function does not take care of that.</summary>
        /// <param name="left">The left directory name. Must include the directory separator character.</param>
        /// <param name="right">The right directory name. Must include the directory separator character.</param>
        public static string AggregateDirectories(string left, string right) => $@"{left}{right}";

        /// <summary>Analyzes the provided path by splitting the individual item names by the directory separator character and returns the array of the names that form the path. The path is automatically converted to its appropriate platform-specific form.</summary>
        /// <param name="path">The path to analyze.</param>
        public static string[] AnalyzePath(string path) => FixPath(path).Split(DirectorySeparatorChar).RemoveEmptyElements();
        /// <summary>Returns a concatenated string version of the provided directory collection including the directory separator character.</summary>
        /// <param name="directories">The directories to concatenate.</param>
        public static string ConcatenateDirectoryPath(IEnumerable<string> directories) => $@"{directories.ToList().ConvertAll(AddDirectorySuffix).AggregateIfContains(AggregateDirectories)}";

        /// <summary>Gets the deepest common directory path between two paths.</summary>
        /// <param name="pathA">The first path to get the deepest common directory of.</param>
        /// <param name="pathB">The second path to get the deepest common directory of.</param>
        public static string GetCommonDirectory(string pathA, string pathB)
        {
            var splitA = AnalyzePath(pathA);
            var splitB = AnalyzePath(pathB);
            var result = new List<string>();
            int min = Min(splitA.Length, splitB.Length);
            for (int i = 0; i < min; i++)
                if (splitA[i] == splitB[i])
                    result.Add(splitA[i]);
            return result.Aggregate(AggregateDirectories);
        }
        // Fuck, this documentation seems confusing
        /// <summary>Gets the directory name of the directory in the previous path whose parent is the new path. For instance, <code>GetPreviousPathDirectoryInNewPath("C:\users\user\Desktop\", "C:\users\")</code> will return <code>"user"</code>.</summary>
        /// <param name="previousPath">The previous path.</param>
        /// <param name="newPath">The new path in which the resulting previous path's directory name is contained.</param>
        public static string GetPreviousPathDirectoryInNewPath(string previousPath, string newPath)
        {
            var splitPrevious = AnalyzePath(previousPath);
            var splitNew = AnalyzePath(newPath);
            if (splitNew.Length >= splitPrevious.Length)
                return null;

            int index = -1;
            while (++index < splitNew.Length)
                if (splitPrevious[index] != splitNew[index])
                    break;
            return splitPrevious[index];
        }
        /// <summary>Returns the name of the item, without taking its parent directory into consideration.</summary>
        /// <param name="path">The path to take its individual item name.</param>
        public static string GetIndividualItemName(string path) => AnalyzePath(path).Last();

        /// <summary>Returns the path item type of the provided path.</summary>
        /// <param name="path">The path to determine its path item type.</param>
        public static PathItemType DeterminePathItemType(string path)
        {
            if (EndsWithVolumeSeparator(path) || EndsWithVolumeSeparator(path.Remove(path.Length - 1)))
                return PathItemType.Volume;
            if (EndsWithDirectorySeparator(path) || IsNullOrWhiteSpace(GetExtension(path)))
                return PathItemType.Directory;
            return PathItemType.File;
        }

        /// <summary>Determines whether a path ends with the platform-specific directory separator character.</summary>
        /// <param name="path">The path to determine whether it ends with the platform-specific directory separator.</param>
        public static bool EndsWithDirectorySeparator(string path) => path.EndsWith(DirectorySeparatorChar);
        /// <summary>Determines whether a path ends with the platform-specific volume separator character.</summary>
        /// <param name="path">The path to determine whether it ends with the platform-specific volume separator.</param>
        public static bool EndsWithVolumeSeparator(string path) => path.EndsWith(VolumeSeparatorChar);
    }
}
