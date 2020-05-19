using GDAPI.Objects.GeometryDash.GamesaveStrings;

namespace GDAPI.Tests.Objects.GeometryDash.GamesaveStrings
{
    public class LevelDataStringTests : GamesaveStringTestsBase<LevelDataString>
    {
        protected override string FileType => "CCLocalLevels";

        protected override LevelDataString GetNewInstance(string s) => new LevelDataString(s);
    }
}
