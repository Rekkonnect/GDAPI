using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Information.GeometryDash;
using GDAPI.Utilities.Objects.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GDAPI.Utilities.Objects.General.SymmetricalRangeMethods;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a custom particle object.</summary>
    [FutureProofing("2.2")]
    [ObjectID(SpecialObjectType.CustomParticleObject)]
    public class CustomParticleObject : ConstantIDSpecialObject
    {
        /// <summary>Represents the infinite duration constant for the Duration property.</summary>
        public const double InfiniteDuration = -1;

        private byte maxParticles, emission, texture;
        private float duration;
        private Point posVar, gravity;
        private SymmetricalRange<int> startSize, endSize, startSpin, endSpin;
        private SymmetricalRange<float> lifetime, angle, speed, accelRad, accelTan, fadeIn, fadeOut;
        private SymmetricalRange<Color> start, end;

        /// <summary>The object ID of the custom particle object.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)SpecialObjectType.CustomParticleObject;

        #region Motion
        // TODO: Figure out what this does
        /// <summary>The property 1 of the custom particles.</summary>
        [ObjectStringMappable(ObjectParameter.Property1)]
        public CustomParticleProperty1 Property1 { get; set; }
        /// <summary>The maximum number of particles that will be alive simultaneously.</summary>
        [ObjectStringMappable(ObjectParameter.MaxParticles)]
        public int MaxParticles
        {
            get => maxParticles;
            set => UpdateLinkedProperties(ref emission, maxParticles = (byte)value);
        }
        /// <summary>The duration of the particle creation.</summary>
        [ObjectStringMappable(ObjectParameter.CustomParticleDuration)]
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The lifetime of the particle creation.</summary>
        [ObjectStringMappable(ObjectParameter.Lifetime)]
        public double Lifetime
        {
            get => lifetime.MiddleValue;
            set => lifetime.MiddleValue = (float)value;
        }
        /// <summary>The Lifetime +- property.</summary>
        [ObjectStringMappable(ObjectParameter.LifetimeAdjustment)]
        public double LifetimeAdjustment
        {
            get => lifetime.MaximumDistance;
            set => lifetime.MaximumDistance = (float)value;
        }
        /// <summary>The Emission property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectParameter.Emission)]
        public int Emission
        {
            get => emission;
            set => UpdateLinkedProperties(ref maxParticles, emission = (byte)value);
        }
        /// <summary>The angle of the particles and the center.</summary>
        [ObjectStringMappable(ObjectParameter.Angle)]
        public double Angle
        {
            get => angle.MiddleValue;
            set => angle.MiddleValue = (float)value;
        }
        /// <summary>The Angle +- property.</summary>
        [ObjectStringMappable(ObjectParameter.AngleAdjustment)]
        public double AngleAdjustment
        {
            get => angle.MaximumDistance;
            set => angle.MaximumDistance = (float)value;
        }
        /// <summary>The speed at which the particles move.</summary>
        [ObjectStringMappable(ObjectParameter.CustomParticleSpeed)]
        public double Speed
        {
            get => speed.MiddleValue;
            set => speed.MiddleValue = (float)value;
        }
        /// <summary>The Speed +- property.</summary>
        [ObjectStringMappable(ObjectParameter.SpeedAdjustment)]
        public double SpeedAdjustment
        {
            get => speed.MaximumDistance;
            set => speed.MaximumDistance = (float)value;
        }
        /// <summary>The PosVarX property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectParameter.PosVarX)]
        public double PosVarX
        {
            get => posVar.X;
            set => posVar.X = (float)value;
        }
        /// <summary>The PosVarY +- property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectParameter.PosVarY)]
        public double PosVarY
        {
            get => posVar.Y;
            set => posVar.Y = (float)value;
        }
        /// <summary>The GravityX property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectParameter.GravityX)]
        public double GravityX
        {
            get => gravity.X;
            set => gravity.X = (float)value;
        }
        /// <summary>The GravityY +- property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectParameter.GravityY)]
        public double GravityY
        {
            get => gravity.Y;
            set => gravity.Y = (float)value;
        }
        /// <summary>The AccelRad property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectParameter.AccelRad)]
        public double AccelRad
        {
            get => accelRad.MiddleValue;
            set => accelRad.MiddleValue = (float)value;
        }
        /// <summary>The AccelRad +- property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectParameter.AccelRadAdjustment)]
        public double AccelRadAdjustment
        {
            get => accelRad.MaximumDistance;
            set => accelRad.MaximumDistance = (float)value;
        }
        /// <summary>The AccelTan property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectParameter.AccelTan)]
        public double AccelTan
        {
            get => accelTan.MiddleValue;
            set => accelTan.MiddleValue = (float)value;
        }
        /// <summary>The AccelTan +- property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectParameter.AccelTanAdjustment)]
        public double AccelTanAdjustment
        {
            get => accelTan.MaximumDistance;
            set => accelTan.MaximumDistance = (float)value;
        }
        #endregion
        #region Visual
        /// <summary>The size during the start of the particle's life.</summary>
        [ObjectStringMappable(ObjectParameter.StartSize)]
        public int StartSize
        {
            get => startSize.MiddleValue;
            set => startSize.MiddleValue = value;
        }
        /// <summary>The StartSize +- property.</summary>
        [ObjectStringMappable(ObjectParameter.StartSizeAdjustment)]
        public int StartSizeAdjustment
        {
            get => startSize.MaximumDistance;
            set => startSize.MaximumDistance = value;
        }
        /// <summary>The size during the end of the particle's life.</summary>
        [ObjectStringMappable(ObjectParameter.EndSize)]
        public int EndSize
        {
            get => endSize.MiddleValue;
            set => endSize.MiddleValue = value;
        }
        /// <summary>The EndSize +- property.</summary>
        [ObjectStringMappable(ObjectParameter.EndSizeAdjustment)]
        public int EndSizeAdjustment
        {
            get => endSize.MaximumDistance;
            set => endSize.MaximumDistance = value;
        }
        /// <summary>The rotation during the start of the particle's life.</summary>
        [ObjectStringMappable(ObjectParameter.StartSpin)]
        public int StartSpin
        {
            get => startSpin.MiddleValue;
            set => startSpin.MiddleValue = value;
        }
        /// <summary>The StartSpin +- property.</summary>
        [ObjectStringMappable(ObjectParameter.StartSpinAdjustment)]
        public int StartSpinAdjustment
        {
            get => startSpin.MaximumDistance;
            set => startSpin.MaximumDistance = value;
        }
        /// <summary>The rotation during the end of the particle's life.</summary>
        [ObjectStringMappable(ObjectParameter.EndSpin)]
        public int EndSpin
        {
            get => endSpin.MiddleValue;
            set => endSpin.MiddleValue = value;
        }
        /// <summary>The EndSpin +- property.</summary>
        [ObjectStringMappable(ObjectParameter.EndSpinAdjustment)]
        public int EndSpinAdjustment
        {
            get => endSpin.MaximumDistance;
            set => endSpin.MaximumDistance = value;
        }
        /// <summary>The alpha value of the color during the start of the particle's life.</summary>
        [ObjectStringMappable(ObjectParameter.StartA)]
        public double StartA
        {
            get => start.MiddleValue.A;
            set => start.MiddleValue.A = (float)value;
        }
        /// <summary>The Start_A +- property.</summary>
        [ObjectStringMappable(ObjectParameter.StartAAdjustment)]
        public double StartAAdjustment
        {
            get => start.MaximumDistance.A;
            set => start.MaximumDistance.A = (float)value;
        }
        /// <summary>The red value of the color during the start of the particle's life.</summary>
        [ObjectStringMappable(ObjectParameter.StartR)]
        public double StartR
        {
            get => start.MiddleValue.R;
            set => start.MiddleValue.R = (float)value;
        }
        /// <summary>The Start_R +- property.</summary>
        [ObjectStringMappable(ObjectParameter.StartRAdjustment)]
        public double StartRAdjustment
        {
            get => start.MaximumDistance.R;
            set => start.MaximumDistance.R = (float)value;
        }
        /// <summary>The green value of the color during the start of the particle's life.</summary>
        [ObjectStringMappable(ObjectParameter.StartG)]
        public double StartG
        {
            get => start.MiddleValue.G;
            set => start.MiddleValue.G = (float)value;
        }
        /// <summary>The Start_G +- property.</summary>
        [ObjectStringMappable(ObjectParameter.StartGAdjustment)]
        public double StartGAdjustment
        {
            get => start.MaximumDistance.G;
            set => start.MaximumDistance.G = (float)value;
        }
        /// <summary>The blue value of the color during the start of the particle's life.</summary>
        [ObjectStringMappable(ObjectParameter.StartB)]
        public double StartB
        {
            get => start.MiddleValue.B;
            set => start.MiddleValue.B = (float)value;
        }
        /// <summary>The Start_B +- property.</summary>
        [ObjectStringMappable(ObjectParameter.StartBAdjustment)]
        public double StartBAdjustment
        {
            get => start.MaximumDistance.B;
            set => start.MaximumDistance.B = (float)value;
        }
        /// <summary>The alpha value of the color during the end of the particle's life.</summary>
        [ObjectStringMappable(ObjectParameter.EndA)]
        public double EndA
        {
            get => end.MiddleValue.A;
            set => end.MiddleValue.A = (float)value;
        }
        /// <summary>The End_A +- property.</summary>
        [ObjectStringMappable(ObjectParameter.EndAAdjustment)]
        public double EndAAdjustment
        {
            get => end.MaximumDistance.A;
            set => end.MaximumDistance.A = (float)value;
        }
        /// <summary>The red value of the color during the end of the particle's life.</summary>
        [ObjectStringMappable(ObjectParameter.EndR)]
        public double EndR
        {
            get => end.MiddleValue.R;
            set => end.MiddleValue.R = (float)value;
        }
        /// <summary>The End_R +- property.</summary>
        [ObjectStringMappable(ObjectParameter.EndRAdjustment)]
        public double EndRAdjustment
        {
            get => end.MaximumDistance.R;
            set => end.MaximumDistance.R = (float)value;
        }
        /// <summary>The green value of the color during the end of the particle's life.</summary>
        [ObjectStringMappable(ObjectParameter.EndG)]
        public double EndG
        {
            get => end.MiddleValue.G;
            set => end.MiddleValue.G = (float)value;
        }
        /// <summary>The End_G +- property.</summary>
        [ObjectStringMappable(ObjectParameter.EndGAdjustment)]
        public double EndGAdjustment
        {
            get => end.MaximumDistance.G;
            set => end.MaximumDistance.G = (float)value;
        }
        /// <summary>The blue value of the color during the end of the particle's life.</summary>
        [ObjectStringMappable(ObjectParameter.EndB)]
        public double EndB
        {
            get => end.MiddleValue.B;
            set => end.MiddleValue.B = (float)value;
        }
        /// <summary>The End_B +- property.</summary>
        [ObjectStringMappable(ObjectParameter.EndBAdjustment)]
        public double EndBAdjustment
        {
            get => end.MaximumDistance.B;
            set => end.MaximumDistance.B = (float)value;
        }
        #endregion
        #region Extra
        /// <summary>The grouping of the custom particles.</summary>
        [ObjectStringMappable(ObjectParameter.Grouping)]
        public CustomParticleGrouping Grouping { get; set; }
        /// <summary>The Fade In property.</summary>
        [ObjectStringMappable(ObjectParameter.CustomParticleFadeIn)]
        public double FadeIn
        {
            get => fadeIn.MiddleValue;
            set => fadeIn.MiddleValue = (float)value;
        }
        /// <summary>The Fade In +- property.</summary>
        [ObjectStringMappable(ObjectParameter.FadeInAdjustment)]
        public double FadeInAdjustment
        {
            get => fadeIn.MaximumDistance;
            set => fadeIn.MaximumDistance = (float)value;
        }
        /// <summary>The Fade Out property.</summary>
        [ObjectStringMappable(ObjectParameter.CustomParticleFadeOut)]
        public double FadeOut
        {
            get => fadeOut.MiddleValue;
            set => fadeOut.MiddleValue = (float)value;
        }
        /// <summary>The Fade Out +- property.</summary>
        [ObjectStringMappable(ObjectParameter.FadeOutAdjustment)]
        public double FadeOutAdjustment
        {
            get => fadeOut.MaximumDistance;
            set => fadeOut.MaximumDistance = (float)value;
        }
        /// <summary>Represents the Additive property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectParameter.Additive)]
        public bool Additive
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }
        /// <summary>Represents the Start Size = End property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectParameter.StartSizeEqualsEnd)]
        public bool StartSizeEqualsEnd
        {
            get => SpecialObjectBools[1];
            set => SpecialObjectBools[1] = value;
        }
        /// <summary>Represents the Start Spin = End property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectParameter.StartSpinEqualsEnd)]
        public bool StartSpinEqualsEnd
        {
            get => SpecialObjectBools[2];
            set => SpecialObjectBools[2] = value;
        }
        /// <summary>Represents the Start Radius = End property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectParameter.StartRadiusEqualsEnd)]
        public bool StartRadiusEqualsEnd
        {
            get => SpecialObjectBools[3];
            set => SpecialObjectBools[3] = value;
        }
        /// <summary>Represents the Start Rotation Is Dir property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectParameter.StartRotationIsDir)]
        public bool StartRotationIsDir
        {
            get => SpecialObjectBools[4];
            set => SpecialObjectBools[4] = value;
        }
        /// <summary>Represents the Dynamic Rotation property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectParameter.DynamicRotation)]
        public bool DynamicRotation
        {
            get => SpecialObjectBools[5];
            set => SpecialObjectBools[5] = value;
        }
        /// <summary>Represents the Use Object Color property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectParameter.UseObjectColor)]
        public bool UseObjectColor
        {
            get => SpecialObjectBools[6];
            set => SpecialObjectBools[6] = value;
        }
        /// <summary>Represents the Uniform Object Color property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectParameter.UniformObjectColor)]
        public bool UniformObjectColor
        {
            get => SpecialObjectBools[7];
            set => SpecialObjectBools[7] = value;
        }
        #endregion

        /// <summary>The texture of the particles.</summary>
        [ObjectStringMappable(ObjectParameter.Texture)]
        public int Texture
        {
            get => texture;
            set => texture = (byte)value;
        }

        /// <summary>Initializes a new empty instance of the <seealso cref="CustomParticleObject"/> class. For internal use only.</summary>
        public CustomParticleObject() : base() { }

        /// <summary>Returns a clone of this <seealso cref="CustomParticleObject"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new CustomParticleObject());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as CustomParticleObject;
            c.Grouping = Grouping;
            c.Property1 = Property1;
            c.maxParticles = maxParticles;
            c.duration = duration;
            c.emission = emission;
            c.posVar = posVar;
            c.gravity = gravity;
            c.startSize = startSize;
            c.endSize = endSize;
            c.startSpin = startSpin;
            c.endSpin = endSpin;
            c.lifetime = lifetime;
            c.angle = angle;
            c.speed = speed;
            c.accelRad = accelRad;
            c.accelTan = accelTan;
            c.fadeIn = fadeIn;
            c.fadeOut = fadeOut;
            c.start = start;
            c.end = end;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as CustomParticleObject;
            return base.EqualsInherited(other)
                && Grouping == z.Grouping
                && Property1 == z.Property1
                && maxParticles == z.maxParticles
                && duration == z.duration
                && emission == z.emission
                && posVar == z.posVar
                && gravity == z.gravity
                && AreEqual(startSize, z.startSize)
                && AreEqual(endSize, z.endSize)
                && AreEqual(startSpin, z.startSpin)
                && AreEqual(endSpin, z.endSpin)
                && AreEqual(lifetime, z.lifetime)
                && AreEqual(angle, z.angle)
                && AreEqual(speed, z.speed)
                && AreEqual(accelRad, z.accelRad)
                && AreEqual(accelTan, z.accelTan)
                && AreEqual(fadeIn, z.fadeIn)
                && AreEqual(fadeOut, z.fadeOut)
                && AreEqual(start, z.start)
                && AreEqual(end, z.end);
        }

        // Updates the linked properties Max Particles and Emission
        private void UpdateLinkedProperties(ref byte assigned, byte value)
        {
            if (maxParticles < emission)
                assigned = value;
        }
    }
}
