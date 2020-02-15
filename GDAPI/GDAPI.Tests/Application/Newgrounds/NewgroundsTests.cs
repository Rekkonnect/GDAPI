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
        public void GetSongMetadata()
        {
            var usableMetadatas = new SongMetadataGetter[usableSongs.Length];
            var unusableMetadatas = new SongMetadataGetter[unusableSongs.Length];

            int retrievedSongs = 0;

            for (int i = 0; i < usableSongs.Length; i++)
                usableMetadatas[i] = new SongMetadataGetter(usableSongs[i], SongRetrievalComplete);
            for (int i = 0; i < unusableSongs.Length; i++)
                unusableMetadatas[i] = new SongMetadataGetter(unusableSongs[i], SongRetrievalComplete);

            while (retrievedSongs < totalSongs)
                Thread.Sleep(100);

            for (int i = 0; i < usableMetadatas.Length; i++)
                Assert.IsTrue(usableMetadatas[i].Result.ID == usableSongs[i], $"Unusable song detected: {usableMetadatas[i].Result.ID}");
            for (int i = 0; i < unusableMetadatas.Length; i++)
                Assert.IsTrue(unusableMetadatas[i].Result.ID == -1, $"Usable song detected: {unusableMetadatas[i].Result.ID}");

            void SongRetrievalComplete(SongMetadata metadata) => retrievedSongs++;
        }
    }
}
