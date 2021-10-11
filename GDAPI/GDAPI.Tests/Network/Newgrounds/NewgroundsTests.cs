using System.Threading;
using GDAPI.Network.Newgrounds;
using GDAPI.Objects.GeometryDash.General;
using NUnit.Framework;

namespace GDAPI.Tests.Network.Newgrounds
{
    public class NewgroundsTests
    {
        private static int[] usableSongs = new int[] { 905921, 911107, 910549, 901476, 885164 }; // on NG
        private static int[] unusableSongs = new int[] { 123657, 6554, 645654, 46587, 90154667 }; // not on NG

        private static int totalSongs = usableSongs.Length + unusableSongs.Length;

        [Test]
        public void GetSongMetadata()
        {
            const int checkCount = 420;
            const int checkIterationDelay = 100;

            var usableMetadatas = new SongMetadataFetcher[usableSongs.Length];
            var unusableMetadatas = new SongMetadataFetcher[unusableSongs.Length];

            int retrievedSongs = 0;

            for (int i = 0; i < usableSongs.Length; i++)
                usableMetadatas[i] = new SongMetadataFetcher(usableSongs[i], SongRetrievalComplete);
            for (int i = 0; i < unusableSongs.Length; i++)
                unusableMetadatas[i] = new SongMetadataFetcher(unusableSongs[i], SongRetrievalComplete);

            for (int i = 0; i < checkCount && retrievedSongs < totalSongs; i++)
                Thread.Sleep(checkIterationDelay);

            if (retrievedSongs < totalSongs) // test was timed out
                Assert.Ignore($"The song metadata retrieval timed out after {checkCount * checkIterationDelay / 1000} seconds.");

            for (int i = 0; i < usableMetadatas.Length; i++)
                Assert.IsTrue(usableMetadatas[i].Result.ID == usableSongs[i], $"Unusable song detected: {usableMetadatas[i].Result.ID}");
            for (int i = 0; i < unusableMetadatas.Length; i++)
                Assert.IsTrue(unusableMetadatas[i].Result.ID == -1, $"Usable song detected: {unusableMetadatas[i].Result.ID}");

            void SongRetrievalComplete(SongMetadata metadata) => retrievedSongs++;
        }
    }
}
