using GDAPI.Enumerations;
using GDAPI.Functions.General;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static System.IO.Path;

namespace GDAPI.Tests.Functions.General
{
    public class PathExpansionPackTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetCommonDirectory()
        {
            AreEqual(PathExpansionPack.ConcatenateDirectoryPath("C:", "users", "user"), PathExpansionPack.GetCommonDirectory(Combine("C:", "users", "user", "Desktop"), Combine("C:", "users", "user")));
            AreEqual(PathExpansionPack.ConcatenateDirectoryPath("C:", "users", "user"), PathExpansionPack.GetCommonDirectory(Combine("C:", "users", "user"), Combine("C:", "users", "user", "Desktop")));
            AreEqual(PathExpansionPack.ConcatenateDirectoryPath("C:", "users"), PathExpansionPack.GetCommonDirectory(Combine("C:", "users", "user", "Desktop"), Combine("C:", "users", "Rekkon")));
            AreEqual(PathExpansionPack.ConcatenateDirectoryPath("C:", "users"), PathExpansionPack.GetCommonDirectory(Combine("C:", "users", "user"), Combine("C:", "users", "Rekkon", "Desktop")));
        }

        [Test]
        public void DeterminePathItemType()
        {
            AreEqual(PathItemType.Directory, PathExpansionPack.DeterminePathItemType($"{Combine("C:", "users", "user", "Desktop")}{DirectorySeparatorChar}"));
            AreEqual(PathItemType.Volume, PathExpansionPack.DeterminePathItemType($"C{VolumeSeparatorChar}"));
            AreEqual(PathItemType.File, PathExpansionPack.DeterminePathItemType(Combine("C:", "users", "user", "Desktop", "Some file.txt")));
        }

        [Test]
        public void ConcatenateDirectoryPath()
        {
            AreEqual($"{Combine("C:", "users", "user")}{DirectorySeparatorChar}", PathExpansionPack.ConcatenateDirectoryPath("C:", "users", "user"));
        }

        [Test]
        public void GetPreviousPathDirectoryInNewPath()
        {
            AreEqual("Desktop", PathExpansionPack.GetPreviousPathDirectoryInNewPath(Combine("C:", "users", "user", "Desktop"), Combine("C:", "users", "user")));
            AreEqual("user", PathExpansionPack.GetPreviousPathDirectoryInNewPath(Combine("C:", "users", "user", "Desktop"), Combine("C:", "users")));
            AreEqual(null, PathExpansionPack.GetPreviousPathDirectoryInNewPath(Combine("C:", "users", "user"), Combine("C:", "users", "user", "Desktop")));
        }
    }
}
