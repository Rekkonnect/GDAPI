using GDAPI.Application.Newgrounds;
using GDAPI.Objects.GeometryDash.General;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Tests.Application.Newgrounds
{
    public class NewgroundsTests
    {
        [Test]
        public void GetSongMeta()
        {
            var usableSongs = new string[] {"905921", "911107", "910549", "901476", "885164" }; // on Newgounds
            var unusableSongs = new string[] {"123657", "6554", "645654", "46587", "90154667" }; // not on Newgounds
            var usableMetas = new SongMetaGetter[usableSongs.Length];
            var unusableMetas = new SongMetaGetter[unusableSongs.Length];

            for (int i = 0; i < usableSongs.Length; i++)
            {
                usableMetas[i] = new SongMetaGetter(usableSongs[i]);
            }
            for (int i = 0; i < unusableSongs.Length; i++)
            {
                unusableMetas[i] = new SongMetaGetter(unusableSongs[i]);
            }

            var areAllUsableGetsFinished = false;
            var areAllUnusableGetsFinished = false;
            while (!areAllUsableGetsFinished)
            {
                for (int i = 0; i < usableMetas.Length; i++)
                {
                    if (!(usableMetas[i].Status >= TaskStatus.RanToCompletion))
                        break;
                    else if (i == usableMetas.Length - 1)
                        areAllUsableGetsFinished = true;
                }
            }
            while (!areAllUnusableGetsFinished)
            {
                for (int i = 0; i < unusableMetas.Length; i++)
                {
                    if (!(unusableMetas[i].Status >= TaskStatus.RanToCompletion))
                        break;
                    else if (i == unusableMetas.Length - 1)
                        areAllUnusableGetsFinished = true;
                }
            }
            
            for (int i = 0; i < usableMetas.Length; i++)
            {
                Assert.IsTrue(VerifyUsable(i, usableMetas[i].Result), $"index: {i}");
            }
            for (int i = 0; i < unusableMetas.Length; i++)
            {
                Assert.IsTrue(VerifyUnusable(i, unusableMetas[i].Result), $"index: {i}");
            }

            bool VerifyUsable(int index, SongMetadata song) => song.ID.ToString() == usableSongs[index] ? true : false;
            bool VerifyUnusable(int index, SongMetadata song) => song.ID == -1 ? true : false;
        }
    }
}
