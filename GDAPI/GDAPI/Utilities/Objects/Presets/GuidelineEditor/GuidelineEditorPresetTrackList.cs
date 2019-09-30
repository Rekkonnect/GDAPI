using GDAPI.Utilities.Functions.Extensions;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Utilities.Objects.Presets.GuidelineEditor
{
    /// <summary>Contains information about an event track in a <seealso cref="GuidelineEditorPresetPattern"/>.</summary>
    public class GuidelineEditorPresetTrackList
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

        /// <summary>Clones this instance and returns the new instance.</summary>
        public GuidelineEditorPresetTrackList Clone() => new GuidelineEditorPresetTrackList(tracks);

        /// <summary>Unifies this list's tracks and returns a new track containing the events of all the tracks that were unified.</summary>
        public GuidelineEditorPresetTrack UnifyTracks()
        {
            var result = new GuidelineEditorPresetTrack();
            foreach (var t in tracks)
                foreach (var e in t.Events)
                    result.Events.Add(e);
            return result;
        }

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
