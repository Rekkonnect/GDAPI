using GDAPI.Objects.GeometryDash.GamesaveStrings;

namespace GDAPI.Tests.Objects.GeometryDash.GamesaveStrings
{
    public class LevelStringTests : GamesaveStringTestsBase<LevelString>
    {
        protected override string FileType => "LevelString";

        protected override LevelString GetNewInstance(string s) => new(s);
    }
}
