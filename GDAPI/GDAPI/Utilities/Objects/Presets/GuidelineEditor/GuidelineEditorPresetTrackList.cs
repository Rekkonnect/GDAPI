using GDAPI.Utilities.Functions.Extensions;
using GDAPI.Utilities.Objects.General.Music;
using GDAPI.Utilities.Objects.General.TimingPoints;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Utilities.Objects.Presets.GuidelineEditor
{
    /// <summary>Contains a list of <seealso cref="GuidelineEditorPresetTrack"/>s.</summary>
    public class GuidelineEditorPresetTrackList : IEnumerable<GuidelineEditorPresetTrack>
    {
        private List<GuidelineEditorPresetTrack> tracks;

        /// <summary>Gets the unique patterns that are used in this track.</summary>
        public HashSet<GuidelineEditorPresetPattern> UniquePatterns
        {
            get
            {
                var result = new HashSet<GuidelineEditorPresetPattern>();
                foreach (var t in tracks)
                    foreach (var p in t.UniquePatterns)
                        result.Add(p);
                return result;
            }
        }

        /// <summary>Gets the current count of tracks in this track list.</summary>
        public int Count => tracks.Count;

        /// <summary>Creates a new empty instance of the <seealso cref="GuidelineEditorPresetTrackList"/> class.</summary>
        public GuidelineEditorPresetTrackList() { }
        /// <summary>Creates a new instance of the <seealso cref="GuidelineEditorPresetTrackList"/> class.</summary>
        /// <param name="presetTracks">The list of tracks that are contained.</param>
        public GuidelineEditorPresetTrackList(List<GuidelineEditorPresetTrack> presetTracks)
        {
            tracks = new List<GuidelineEditorPresetTrack>(presetTracks);
        }

        /// <summary>Adds a <seealso cref="GuidelineEditorPresetTrack"/> to the list.</summary>
        /// <param name="track">The <seealso cref="GuidelineEditorPresetTrack"/> to add to the list.</param>
        public void Add(GuidelineEditorPresetTrack track) => tracks.Add(track);
        /// <summary>Removes a <seealso cref="GuidelineEditorPresetTrack"/> from the list.</summary>
        /// <param name="track">The <seealso cref="GuidelineEditorPresetTrack"/> to remove from the list.</param>
        public void Remove(GuidelineEditorPresetTrack track) => tracks.Remove(track);

        /// <summary>Adds a range of <seealso cref="GuidelineEditorPresetTrack"/>s to the list.</summary>
        /// <param name="t">The range of <seealso cref="GuidelineEditorPresetTrack"/>s to add to the list.</param>
        public void AddRange(IEnumerable<GuidelineEditorPresetTrack> t) => tracks.AddRange(t);

        /// <summary>Clones this instance and returns the new instance.</summary>
        public GuidelineEditorPresetTrackList Clone() => new GuidelineEditorPresetTrackList(tracks);

        /// <summary>Unifies this list's tracks and returns a new track containing the events of all the tracks that were unified.</summary>
        public GuidelineEditorPresetTrack UnifyTracks()
        {
            var result = new GuidelineEditorPresetTrack();
            foreach (var t in tracks)
                foreach (var e in t)
                    result.Add(e);
            return result;
        }
        /// <summary>Gets all the events of this track list and generates a track list that contains the smallest number of tracks where the events are not overlapping in any track.</summary>
        /// <param name="timingPoints">The timing points based on which to compact the tracks.</param>
        public GuidelineEditorPresetTrackList CompactTracks(TimingPointList timingPoints)
        {
            var result = new GuidelineEditorPresetTrackList();
            var unifiedTracks = UnifyTracks();
            foreach (var e in unifiedTracks)
            {
                bool found = false;
                for (int i = 0; i < result.Count && !found; i++)
                    if (found = MeasuredTimePosition.CompareByAbsolutePosition(result.tracks[i].GetEnd(timingPoints), e.TimePosition) < 0)
                        result.tracks[i].Add(e);
                if (!found)
                    result.Add(new GuidelineEditorPresetTrack(e));
            }
            return result;
        }

        /// <summary>Gets or sets the <seealso cref="GuidelineEditorPresetTrack"/> of this list at the specified index.</summary>
        /// <param name="index">The index of the <seealso cref="GuidelineEditorPresetTrack"/> to get or set.</param>
        public GuidelineEditorPresetTrack this[int index]
        {
            get => tracks[index];
            set => tracks[index] = value;
        }

        public IEnumerator<GuidelineEditorPresetTrack> GetEnumerator() => tracks.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => tracks.GetEnumerator();

        /// <summary>Gets the string representation of this <seealso cref="GuidelineEditorPresetTrackList"/> that will be used in the preset data.</summary>
        public override string ToString()
        {
            var result = new StringBuilder();

            for (int i = 0; i < tracks.Count; i++)
                result.AppendLine($"Track {i}").AppendLine(tracks[i].ToString()).AppendLine();

            return result.RemoveLastIfEndsWith('\n').ToString();
        }
    }
}
