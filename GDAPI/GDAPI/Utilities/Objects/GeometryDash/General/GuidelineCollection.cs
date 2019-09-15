using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Convert;

namespace GDAPI.Utilities.Objects.GeometryDash.General
{
    /// <summary>Represents a collection of guidelines.</summary>
    public class GuidelineCollection : IEnumerable<Guideline>
    {
        private List<Guideline> guidelines;
        private Dictionary<GuidelineColor, int> colors;

        /// <summary>The count of the guideline collection.</summary>
        public int Count => guidelines.Count;
        /// <summary>The time stamps of the guidelines.</summary>
        public List<double> TimeStamps
        {
            get
            {
                var t = new List<double>();
                foreach (var g in guidelines)
                    t.Add(g.TimeStamp);
                return t;
            }
        }

        /// <summary>Retrieves the cached count of transparent guidelines in this collection.</summary>
        public int TransparentGuidelineCount => colors[GuidelineColor.Transparent];
        /// <summary>Retrieves the cached count of orange guidelines in this collection.</summary>
        public int OrangeGuidelineCount => colors[GuidelineColor.Orange];
        /// <summary>Retrieves the cached count of yellow guidelines in this collection.</summary>
        public int YellowGuidelineCount => colors[GuidelineColor.Yellow];
        /// <summary>Retrieves the cached count of green guidelines in this collection.</summary>
        public int GreenGuidelineCount => colors[GuidelineColor.Green];

        /// <summary>Retrieves a collection consisting of the transparent guidelines from this collection.</summary>
        public GuidelineCollection TransparentGuidelines => GetColorSpecificGuidelines(GuidelineColor.Transparent);
        /// <summary>Retrieves a collection consisting of the orange guidelines from this collection.</summary>
        public GuidelineCollection OrangeGuidelines => GetColorSpecificGuidelines(GuidelineColor.Orange);
        /// <summary>Retrieves a collection consisting of the yellow guidelines from this collection.</summary>
        public GuidelineCollection YellowGuidelines => GetColorSpecificGuidelines(GuidelineColor.Yellow);
        /// <summary>Retrieves a collection consisting of the green guidelines from this collection.</summary>
        public GuidelineCollection GreenGuidelines => GetColorSpecificGuidelines(GuidelineColor.Green);

        /// <summary>Initializes a new instance of the <seealso cref="GuidelineCollection"/> class.</summary>
        public GuidelineCollection() : this(new List<Guideline>()) { }
        /// <summary>Initializes a new instance of the <seealso cref="GuidelineCollection"/> class.</summary>
        /// <param name="g">The guidelines to create the collection out of.</param>
        public GuidelineCollection(IEnumerable<Guideline> g)
            : this(g, true) { }
        /// <summary>Initializes a new instance of the <seealso cref="GuidelineCollection"/> class.</summary>
        /// <param name="g">The guidelines to create the collection out of.</param>
        /// <param name="analyzeColors">Determines whether the guideline color counts will be cached.</param>
        private GuidelineCollection(IEnumerable<Guideline> g, bool analyzeColors)
        {
            guidelines = new List<Guideline>(g);
            if (analyzeColors)
                AnalyzeColors();
        }

        /// <summary>Adds a <seealso cref="Guideline"/> to the <seealso cref="GuidelineCollection"/> and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="guideline">The guideline to add to the <seealso cref="GuidelineCollection"/>.</param>
        public GuidelineCollection Add(Guideline guideline)
        {
            guidelines.Add(guideline);
            colors[guideline.Color]++;
            return this;
        }
        /// <summary>Adds a collection of guidelines from the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="g">The guidelines to add.</param>
        public GuidelineCollection AddRange(List<Guideline> g)
        {
            guidelines.AddRange(g);
            foreach (var guideline in g)
                colors[guideline.Color]++;
            return this;
        }
        /// <summary>Adds a collection of guidelines from the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="guidelines">The guidelines to add.</param>
        public GuidelineCollection AddRange(GuidelineCollection guidelines) => AddRange(guidelines.guidelines);
        /// <summary>Inserts a <seealso cref="Guideline"/> into the <seealso cref="GuidelineCollection"/> at a specified index and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="index">The index to insert the <seealso cref="Guideline"/> at.</param>
        /// <param name="guideline">The guideline to insert into the <seealso cref="GuidelineCollection"/>.</param>
        public GuidelineCollection Insert(int index, Guideline guideline)
        {
            guidelines.Insert(index, guideline);
            colors[guideline.Color]++;
            return this;
        }
        /// <summary>Removes a <seealso cref="Guideline"/> from the <seealso cref="GuidelineCollection"/> and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="guideline">The guideline to remove from the <seealso cref="GuidelineCollection"/>.</param>
        public GuidelineCollection Remove(Guideline guideline)
        {
            guidelines.Remove(guideline);
            colors[guideline.Color]--;
            return this;
        }
        /// <summary>Removes the <seealso cref="Guideline"/> at the specified index from the <seealso cref="GuidelineCollection"/> and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="index">The index of the guideline to remove from the <seealso cref="GuidelineCollection"/>.</param>
        public GuidelineCollection RemoveAt(int index)
        {
            colors[guidelines[index].Color]--;
            guidelines.RemoveAt(index);
            return this;
        }
        /// <summary>Clears the <seealso cref="GuidelineCollection"/> and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="index">The index of the guideline to remove from the <seealso cref="GuidelineCollection"/>.</param>
        public GuidelineCollection Clear()
        {
            guidelines.Clear();
            InitializeColorDictionary();
            return this;
        }
        /// <summary>Adds a <seealso cref="Guideline"/> into the <seealso cref="GuidelineCollection"/> and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="timeStamp">The timestamp of the <seealso cref="Guideline"/>.</param>
        /// <param name="color">The color of the <seealso cref="Guideline"/>.</param>
        public GuidelineCollection Add(double timeStamp, double color) => Add(new Guideline(timeStamp, color));
        /// <summary>Inserts a <seealso cref="Guideline"/> into the <seealso cref="GuidelineCollection"/> at a specified index and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="index">The index to insert the <seealso cref="Guideline"/> at.</param>
        /// <param name="timeStamp">The timestamp of the <seealso cref="Guideline"/>.</param>
        /// <param name="color">The color of the <seealso cref="Guideline"/>.</param>
        public GuidelineCollection Insert(int index, double timeStamp, double color) => Insert(index, new Guideline(timeStamp, color));
        /// <summary>Removes the duplicated guidelines and returns the instance of the <seealso cref="GuidelineCollection"/>.</summary>
        public GuidelineCollection RemoveDuplicatedGuidelines()
        {
            var guidelines = new List<Guideline>();
            foreach (var g in this.guidelines)
                if (!guidelines.Contains(g))
                    guidelines.Add(g);
            this.guidelines = guidelines;
            AnalyzeColors();
            return this;
        }
        /// <summary>Returns the index of the first guideline whose time stamp is not greater than the provided time stamp.</summary>
        /// <param name="timeStamp">The time stamp to exceed.</param>
        public int GetFirstIndexAfterTimeStamp(double timeStamp)
        {
            int min = 0;
            int max = guidelines.Count;
            int mid = 0;
            while (min < max)
            {
                mid = (min + max) / 2;
                if (timeStamp == guidelines[mid].TimeStamp)
                    return mid;
                if (timeStamp < guidelines[mid].TimeStamp)
                    min = mid + 1;
                else
                    max = mid;
            }
            return mid;
        }

        public IEnumerator<Guideline> GetEnumerator()
        {
            foreach (var g in guidelines)
                yield return g;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #region Private stuff
        private void AnalyzeColors()
        {
            InitializeColorDictionary();
            for (int i = 0; i < guidelines.Count; i++)
                colors[guidelines[i].Color]++;
        }
        private void InitializeColorDictionary()
        {
            colors = new Dictionary<GuidelineColor, int>
            {
                { GuidelineColor.Transparent, 0 },
                { GuidelineColor.Orange, 0 },
                { GuidelineColor.Yellow, 0 },
                { GuidelineColor.Green, 0 },
            };
        }
        private GuidelineCollection GetColorSpecificGuidelines(GuidelineColor color)
        {
            var collection = new GuidelineCollection(guidelines.Where(g => g.Color == color), false);
            collection.colors[color] = colors[color];
            return collection;
        }

        private int FindIndexToInsertGuideline(Guideline g) => GetFirstIndexAfterTimeStamp(g.TimeStamp);
        #endregion

        /// <summary>Parses the guideline string into a <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="guidelineString">The guideline string to parse.</param>
        public static GuidelineCollection Parse(string guidelineString)
        {
            var guidelines = new GuidelineCollection();
            if (guidelineString != null && guidelineString != "")
            {
                guidelineString = guidelineString.Remove(guidelineString.Length - 1);
                string[] s = guidelineString.Split('~');
                for (int i = 0; i < s.Length; i += 2)
                    guidelines.Add(ToDouble(s[i]), ToDouble(s[i + 1]));
            }
            return guidelines;
        }

        /// <summary>Gets or sets the <seealso cref="Guideline"/> at a specified index.</summary>
        /// <param name="index">The index of the <seealso cref="Guideline"/> to get or set.</param>
        public Guideline this[int index]
        {
            get => guidelines[index];
            set => guidelines[index] = value;
        }

        /// <summary>Returns the guideline string of the <seealso cref="GuidelineCollection"/>.</summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var g in guidelines)
                result.Append($"{g}~");
            return result.ToString();
        }
    }
}
