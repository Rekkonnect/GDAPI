using GDAPI.Application.NewGrounds;
using GDAPI.Objects.GeometryDash.General;
using NUnit.Framework;
using System.Threading;

namespace GDAPI.Tests.Application.NewGrounds
{
    public class NewGroundsTests
    {
        private static int[] usableSongs = new int[] { 905921, 911107, 910549, 901476, 885164 }; // on NG
        private static int[] unusableSongs = new int[] { 123657, 6554, 645654, 46587, 90154667 }; // not on NG

        private static int totalSongs = usableSongs.Length + unusableSongs.Length;

        [Test]
        public void GetSongMeta()
        {
            var usableMetas = new SongMetadataGetter[usableSongs.Length];
            var unusableMetas = new SongMetadataGetter[unusableSongs.Length];

            int retrievedSongs = 0;

            for (int i = 0; i < usableSongs.Length; i++)
                usableMetas[i] = new SongMetadataGetter(usableSongs[i], SongRetrievalComplete);
            for (int i = 0; i < unusableSongs.Length; i++)
                unusableMetas[i] = new SongMetadataGetter(unusableSongs[i], SongRetrievalComplete);

            while (retrievedSongs < totalSongs)
                Thread.Sleep(100);

            for (int i = 0; i < usableMetas.Length; i++)
                Assert.IsTrue(usableMetas[i].Result.ID == usableSongs[i], $"Unusable song detected: {usableMetas[i].Result.ID}");
            for (int i = 0; i < unusableMetas.Length; i++)
                Assert.IsTrue(unusableMetas[i].Result.ID == -1, $"Usable song detected: {unusableMetas[i].Result.ID}");

            void SongRetrievalComplete(SongMetadata metadata) => retrievedSongs++;
        }
    }
}
