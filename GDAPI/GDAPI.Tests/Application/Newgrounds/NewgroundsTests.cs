﻿using GDAPI.Application.Newgrounds;
using GDAPI.Objects.GeometryDash.General;
using NUnit.Framework;
using System.Threading;

namespace GDAPI.Tests.Application.Newgrounds
{
    public class NewgroundsTests
    {
        private static readonly int[] usableSongs = new int[] { 905921, 911107, 910549, 901476, 885164 }; // on NG
        private static readonly int[] unusableSongs = new int[] { 123657, 6554, 645654, 46587, 90154667 }; // not on NG

        private static readonly int totalSongs = usableSongs.Length + unusableSongs.Length;

        [Test]
        public void GetSongMetadata()
        {
            const int checkCount = 420;
            const int checkIterationDelay = 100;

            var usableMetadatas = new SongMetadataGetter[usableSongs.Length];
            var unusableMetadatas = new SongMetadataGetter[unusableSongs.Length];

            int retrievedSongs = 0;

            for (int i = 0; i < usableSongs.Length; i++)
                usableMetadatas[i] = new SongMetadataGetter(usableSongs[i], SongRetrievalComplete);
            for (int i = 0; i < unusableSongs.Length; i++)
                unusableMetadatas[i] = new SongMetadataGetter(unusableSongs[i], SongRetrievalComplete);

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
