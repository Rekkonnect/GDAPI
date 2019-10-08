using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Pulse trigger.</summary>
    [ObjectID(TriggerType.Pulse)]
    public class PulseTrigger : Trigger, IHasTargetGroupID, IHasTargetColorID, IHasCopiedColorID, IHasColor
    {
        private byte red = 255, green = 255, blue = 255;
        private short targetID, copiedColorID;
        private float fadeIn, hold, fadeOut;

        /// <summary>The Object ID of the Pulse trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Pulse;

        /// <summary>Determines whether this Pulse trigger is targeting a color channel.</summary>
        public bool IsTargetingColorChannel => PulseTargetType == PulseTargetType.ColorChannel;
        public bool IsTargetingGroup => PulseTargetType == PulseTargetType.Group;

        /// <summary>The target ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID, 0)]
        public int TargetID
        {
            get => targetID;
            set => targetID = (short)value;
        }
        /// <summary>The target Group ID of the trigger.</summary>
        public int TargetGroupID
        {
            get => IsTargetingGroup ? targetID : 0;
            set => SetTargetID(PulseTargetType.Group, value);
        }
        /// <summary>The target Color ID of the trigger.</summary>
        public int TargetColorID
        {
            get => IsTargetingColorChannel ? targetID : 0;
            set => SetTargetID(PulseTargetType.ColorChannel, value);
        }
        /// <summary>The red part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Red, 255)]
        public int Red
        {
            get => red;
            set => red = (byte)value;
        }
        /// <summary>The green part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Green, 255)]
        public int Green
        {
            get => green;
            set => green = (byte)value;
        }
        /// <summary>The blue part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Blue, 255)]
        public int Blue
        {
            get => blue;
            set => blue = (byte)value;
        }
        /// <summary>The Fade In property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.FadeIn, 0d)]
        public double FadeIn
        {
            get => fadeIn;
            set => fadeIn = (float)value;
        }
        /// <summary>The Hold property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Hold, 0d)]
        public double Hold
        {
            get => hold;
            set => hold = (float)value;
        }
        /// <summary>The Fade Out property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.FadeOut, 0d)]
        public double FadeOut
        {
            get => fadeOut;
            set => fadeOut = (float)value;
        }
        /// <summary>The copied Color ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.CopiedColorID, 0)]
        public int CopiedColorID
        {
            get => copiedColorID;
            set => copiedColorID = (short)value;
        }
        /// <summary>The Pulse Mode of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.PulseMode, PulseMode.Color)]
        public PulseMode PulseMode { get; set; }
        /// <summary>The Pulse Target Type of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetType, PulseTargetType.ColorChannel)]
        public PulseTargetType PulseTargetType { get; set; }
        /// <summary>The Main Only property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.MainOnly, false)]
        public bool MainOnly
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Detail Only property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.DetailOnly, false)]
        public bool DetailOnly
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }
        /// <summary>The Exclusive property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Exclusive, false)]
        public bool Exclusive
        {
            get => TriggerBools[5];
            set => TriggerBools[5] = value;
        }
        /// <summary>The HSV of the trigger (as a string for the gamesave).</summary>
        [ObjectStringMappable(ObjectParameter.CopiedColorHSVValues, HSVAdjustment.DefaultHSVString)]
        public string HSV
        {
            get => HSVAdjustment.ToString();
            set => HSVAdjustment = HSVAdjustment.Parse(value);
        }

        /// <summary>The HSV adjustment of the copied color of the trigger.</summary>
        public HSVAdjustment HSVAdjustment { get; set; } = new HSVAdjustment();

        /// <summary>Initializes a new instance of the <seealso cref="PulseTrigger"/> class.</summary>
        public PulseTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="PulseTrigger"/> class.</summary>
        /// <param name="fadeIn">The Fade In property of the trigger.</param>
        /// <param name="hold">The Hold property of the trigger.</param>
        /// <param name="fadeOut">The Fade Out property of the trigger.</param>
        /// <param name="targetID">The target ID of the trigger.</param>
        /// <param name="pulseTargetType">The Pulse Target Type of the trigger.</param>
        /// <param name="pulseMode">The Pulse Mode of the trigger.</param>
        public PulseTrigger(double fadeIn, double hold, double fadeOut, int targetID, PulseTargetType pulseTargetType = PulseTargetType.ColorChannel, PulseMode pulseMode = PulseMode.Color)
            : base()
        {
            FadeIn = fadeIn;
            Hold = hold;
            FadeOut = fadeOut;
            if (pulseTargetType == PulseTargetType.ColorChannel)
                TargetColorID = targetID;
            else
                TargetGroupID = targetID;
            PulseTargetType = pulseTargetType;
            PulseMode = pulseMode;
        }

        /// <summary>Sets the target ID to the value if the current Pulse trigger is targeting the specified target type.</summary>
        /// <param name="type">The target type this Pulse trigger must be targeting for the target ID to be changed.</param>
        /// <param name="value">The value to set as target ID.</param>
        public void SetTargetID(PulseTargetType type, int value)
        {
            if (PulseTargetType == type)
                targetID = (short)value;
        }

        /// <summary>Returns a clone of this <seealso cref="PulseTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new PulseTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as PulseTrigger;
            c.targetID = targetID;
            c.red = red;
            c.green = green;
            c.blue = blue;
            c.fadeIn = fadeIn;
            c.hold = hold;
            c.fadeOut = fadeOut;
            c.copiedColorID = copiedColorID;
            c.PulseMode = PulseMode;
            c.PulseTargetType = PulseTargetType;
            c.HSVAdjustment = HSVAdjustment;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as PulseTrigger;
            return base.EqualsInherited(other)
                && targetID == z.targetID
                && red == z.red
                && green == z.green
                && blue == z.blue
                && fadeIn == z.fadeIn
                && hold == z.hold
                && fadeOut == z.fadeOut
                && copiedColorID == z.copiedColorID
                && PulseMode == z.PulseMode
                && PulseTargetType == z.PulseTargetType
                && HSVAdjustment == z.HSVAdjustment;
        }
    }
}
