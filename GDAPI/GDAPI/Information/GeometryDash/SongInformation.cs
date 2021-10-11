using GDAPI.Objects.GeometryDash.General;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using static GDAPI.Objects.GeometryDash.General.SongMetadata;

namespace GDAPI.Information.GeometryDash
{
    /// <summary>Contains useful information about songs.</summary>
    public static class SongInformation
    {
        /// <summary>The names of the offical songs.</summary>
        public static readonly ImmutableArray<string> OfficialSongNames = GetAttributedFields<TitleAttribute>();
        /// <summary>The names of the offical songs' artists.</summary>
        public static readonly ImmutableArray<string> OfficialArtistNames = GetAttributedFields<TitleAttribute>();

        [Title]
        private const string
            StereoMadness = "Stereo Madness",
            BackOnTrack = "Back On Track",
            Polargeist = "Polargeist",
            DryOut = "Dry Out",
            BaseAfterBase = "Base After Base",
            CantLetGo = "Can't Let Go",
            Jumper = "Jumper",
            TimeMachine = "Time Machine",
            Cycles = "Cycles",
            xStep = "xStep",
            Clutterfunk = "Clutterfunk",
            TheoryOfEverything = "Theory of Everything",
            ElectromanAdventures = "Electroman Adventures",
            Clubstep = "Clubstep",
            Electrodynamix = "Electrodynamix",
            HexagonForce = "Hexagon Force",
            BlastProcessing = "Blast Processing",
            TheoryOfEverything2 = "Theory of Everything 2",
            GeometricalDominator = "Geometrical Dominator",
            Deadlocked = "Deadlocked",
            Fingerbang = "Fingerbang";

        [Artist]
        private const string
            ForeverBound = "ForeverBound",
            DJVI = "DJVI",
            Step = "Step",
            Waterflame = "Waterflame",
            DjNate = "Dj-Nate",
            F777 = "F-777",
            MDK = "MDK";

        /// <summary>The song metadata of the offical songs.</summary>
        public static readonly SongMetadata[] OfficialSongMetadata =
        {
            CreateOfficialSongMetadata(ForeverBound, StereoMadness),
            CreateOfficialSongMetadata(DJVI, BackOnTrack),
            CreateOfficialSongMetadata(Step, Polargeist),
            CreateOfficialSongMetadata(DJVI, DryOut),
            CreateOfficialSongMetadata(DJVI, BaseAfterBase),
            CreateOfficialSongMetadata(DJVI, CantLetGo),
            CreateOfficialSongMetadata(Waterflame, Jumper),
            CreateOfficialSongMetadata(Waterflame, TimeMachine),
            CreateOfficialSongMetadata(DJVI, Cycles),
            CreateOfficialSongMetadata(DJVI, xStep),
            CreateOfficialSongMetadata(Waterflame, Clutterfunk),
            CreateOfficialSongMetadata(DjNate, TheoryOfEverything),
            CreateOfficialSongMetadata(Waterflame, ElectromanAdventures),
            CreateOfficialSongMetadata(DjNate, Clubstep),
            CreateOfficialSongMetadata(DjNate, Electrodynamix),
            CreateOfficialSongMetadata(Waterflame, HexagonForce),
            CreateOfficialSongMetadata(Waterflame, BlastProcessing),
            CreateOfficialSongMetadata(DjNate, TheoryOfEverything2),
            CreateOfficialSongMetadata(Waterflame, GeometricalDominator),
            CreateOfficialSongMetadata(F777, Deadlocked),
            CreateOfficialSongMetadata(MDK, Fingerbang),
        };

        private static ImmutableArray<string> GetAttributedFields<TAttribute>()
            where TAttribute : Attribute
        {
            return typeof(SongInformation).GetFields()
                .Where(f => f.GetCustomAttributes<TAttribute>().Any())
                .Select(f => f.GetRawConstantValue() as string)
                .ToImmutableArray();
        }

        [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
        private sealed class TitleAttribute : Attribute { }
        [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
        private sealed class ArtistAttribute : Attribute { }
    }
}