using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Objects.GeometryDash.General;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Color trigger.</summary>
    [ObjectID(TriggerType.Color)]
    public class ColorTrigger : Trigger, IHasTargetColorID, IHasCopiedColorID, IHasColor, IHasDuration
    {
        private byte red = 255, green = 255, blue = 255;
        private short targetColorID = 1, copiedColorID;
        private float duration = 0.5f, opacity = 1;

        /// <summary>The Object ID of the Color trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Color;

        /// <summary>Determines whether this color trigger has a constant target Color ID.</summary>
        public virtual bool HasConstantTargetColorID => false;

        /// <summary>The target Color ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetColorID)]
        public int TargetColorID
        {
            get => targetColorID;
            set
            {
                if (!HasConstantTargetColorID)
                    SetTargetColorID(value);
            }
        }
        /// <summary>The duration of the trigger's effect.</summary>
        [ObjectStringMappable(ObjectParameter.Duration)]
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The red part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Red)]
        public int Red
        {
            get => red;
            set => red = (byte)value;
        }
        /// <summary>The green part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Green)]
        public int Green
        {
            get => green;
            set => green = (byte)value;
        }
        /// <summary>The blue part of the color.</summary>
        [ObjectStringMappable(ObjectParameter.Blue)]
        public int Blue
        {
            get => blue;
            set => blue = (byte)value;
        }
        /// <summary>The Opacity property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Opacity)]
        public double Opacity
        {
            get => opacity;
            set => opacity = (float)value;
        }
        // IMPORTANT: The Player 1 and Player 2 setters are not implemented to avoid unnecessary assignments
        /// <summary>The Player 1 Color property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.SetColorToPlayerColor1)]
        public bool Player1Color
        {
            get => CopiedColorID == (int)SpecialColorID.P1;
            set { }
        }
        /// <summary>The Player 2 Color property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.SetColorToPlayerColor2)]
        public bool Player2Color
        {
            get => CopiedColorID == (int)SpecialColorID.P2;
            set { }
        }
        /// <summary>The copied Color ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.CopiedColorID)]
        public int CopiedColorID
        {
            get => copiedColorID;
            set => copiedColorID = (short)value;
        }
        /// <summary>The Blending property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Blending)]
        public bool Blending
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }
        /// <summary>The Copy Opacity property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.CopyOpacity)]
        public bool CopyOpacity
        {
            get => TriggerBools[4];
            set => TriggerBools[4] = value;
        }
        /// <summary>The Tint Ground property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TintGround)]
        public bool TintGround
        {
            get => TriggerBools[5];
            set => TriggerBools[5] = value;
        }
        /// <summary>The HSV of the trigger (as a string for the gamesave).</summary>
        [ObjectStringMappable(ObjectParameter.CopiedColorHSVValues)]
        public string HSV
        {
            get => HSVAdjustment.ToString();
            set => HSVAdjustment = HSVAdjustment.Parse(value);
        }

        /// <summary>The HSV adjustment of the copied color of the trigger.</summary>
        public HSVAdjustment HSVAdjustment { get; set; } = new HSVAdjustment();

        /// <summary>Initializes a new instance of the <seealso cref="ColorTrigger"/> class.</summary>
        public ColorTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="ColorTrigger"/> class.</summary>
        /// <param name="targetID">The target ID of the trigger.</param>
        public ColorTrigger(int targetID)
            : base()
        {
            TargetColorID = targetID;
        }
        /// <summary>Initializes a new instance of the <seealso cref="ColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetID">The target ID of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public ColorTrigger(double duration, int targetID, bool copyOpacity = false, bool tintGround = false)
            : this(targetID)
        {
            Duration = duration;
            TargetColorID = targetID;
            CopyOpacity = copyOpacity;
            TintGround = tintGround;
        }
        // Constructors like this are useless, so many fucking parameters are required

        /// <summary>Sets the target Color ID value. This function is <see langword="protected"/> so that special color triggers can initially set the target Color ID.</summary>
        /// <param name="value">The new value of the target Color ID.</param>
        protected void SetTargetColorID(int value) => targetColorID = (short)value;

        /// <summary>Returns a clone of this <seealso cref="ColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ColorTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as ColorTrigger;
            c.targetColorID = targetColorID;
            c.duration = duration;
            c.red = red;
            c.green = green;
            c.blue = blue;
            c.opacity = opacity;
            c.copiedColorID = copiedColorID;
            c.HSVAdjustment = HSVAdjustment;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as ColorTrigger;
            return base.EqualsInherited(other)
                && targetColorID == z.targetColorID
                && duration == z.duration
                && red == z.red
                && green == z.green
                && blue == z.blue
                && opacity == z.opacity
                && copiedColorID == z.copiedColorID
                && HSVAdjustment == z.HSVAdjustment;
        }
    }
}
