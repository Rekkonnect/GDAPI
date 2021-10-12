using GDAPI.Objects.GeometryDash.GamesaveStrings;

namespace GDAPI.Tests.Objects.GeometryDash.GamesaveStrings
{
    public class GamesaveStringTests : GamesaveStringTestsBase<GamesaveString>
    {
        protected override string FileType => "CCGameManager";

        protected override GamesaveString GetNewInstance(string s) => new(s);
    }
}
