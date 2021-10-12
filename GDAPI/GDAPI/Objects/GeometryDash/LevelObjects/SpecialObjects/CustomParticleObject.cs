using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.General;
using static GDAPI.Objects.General.SymmetricalRangeMethods;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a custom particle object.</summary>
    [FutureProofing("2.2")]
    [ObjectID(SpecialObjectType.CustomParticleObject)]
    public class CustomParticleObject : ConstantIDSpecialObject
    {
        /// <summary>Represents the infinite duration constant for the Duration property.</summary>
        public const double InfiniteDuration = -1;

        #region Default Constants
        // The following are constants because they are subject to change
        private const int defaultMaxParticles = 30;
        private const int defaultEmission = 30;

        private const double defaultDuration = InfiniteDuration;

        private const int defaultStartSize = 2;
        private const int defaultStartSizeAdjustment = 1;
        private const int defaultEndSize = 1;
        private const int defaultEndSizeAdjustment = 1;

        private const double defaultLifetime = 1;
        private const double defaultLifetimeAdjustment = 0.3f;
        private const double defaultAngle = -90;
        private const double defaultAngleAdjustment = 90;
        private const double defaultSpeed = 29; // one would assume the default is 30
        private const double defaultSpeedAdjustment = 0;

        private const double defaultColorComponent = 1;
        private const double defaultColorAdjustmentComponent = 0;
        #endregion

        private byte maxParticles = defaultMaxParticles;
        private byte emission = defaultEmission;
        private byte texture;
        private float duration = (float)defaultDuration;
        private Point posVar, gravity;
        private SymmetricalRange<int> startSize = new(defaultStartSize, defaultStartSizeAdjustment);
        private SymmetricalRange<int> endSize = new(defaultEndSize, defaultEndSizeAdjustment);
        private SymmetricalRange<int> startSpin = new(0, 0);
        private SymmetricalRange<int> endSpin = new(0, 0);
        private SymmetricalRange<float> lifetime = new((float)defaultLifetime, (float)defaultLifetimeAdjustment);
        private SymmetricalRange<float> angle = new((float)defaultAngle, (float)defaultAngleAdjustment);
        private SymmetricalRange<float> speed = new((float)defaultSpeed, (float)defaultSpeedAdjustment);
        private SymmetricalRange<float> accelRad = new(0, 0);
        private SymmetricalRange<float> accelTan = new(0, 0);
        private SymmetricalRange<float> fadeIn = new(0, 0);
        private SymmetricalRange<float> fadeOut = new(0, 0);
        private SymmetricalRange<Color> start = new(Color.White, Color.Zero);
        private SymmetricalRange<Color> end = new(Color.White, Color.Zero);

        /// <summary>The object ID of the custom particle object.</summary>
        public override int ConstantObjectID => (int)SpecialObjectType.CustomParticleObject;

        #region Motion
        // TODO: Figure out what this does
        /// <summary>The property 1 of the custom particles.</summary>
        [ObjectStringMappable(ObjectProperty.Property1, CustomParticleProperty1.Gravity)]
        public CustomParticleProperty1 Property1 { get; set; }
        /// <summary>The maximum number of particles that will be alive simultaneously.</summary>
        [ObjectStringMappable(ObjectProperty.MaxParticles, defaultMaxParticles)]
        public int MaxParticles
        {
            get => maxParticles;
            set => UpdateLinkedProperties(ref emission, maxParticles = (byte)value);
        }
        /// <summary>The duration of the particle creation.</summary>
        [ObjectStringMappable(ObjectProperty.CustomParticleDuration, defaultDuration)]
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The lifetime of the particle creation.</summary>
        [ObjectStringMappable(ObjectProperty.Lifetime, defaultLifetime)]
        public double Lifetime
        {
            get => lifetime.MiddleValue;
            set => lifetime.MiddleValue = (float)value;
        }
        /// <summary>The Lifetime +- property.</summary>
        [ObjectStringMappable(ObjectProperty.LifetimeAdjustment, defaultLifetimeAdjustment)]
        public double LifetimeAdjustment
        {
            get => lifetime.MaximumDistance;
            set => lifetime.MaximumDistance = (float)value;
        }
        /// <summary>The Emission property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectProperty.Emission, defaultEmission)]
        public int Emission
        {
            get => emission;
            set => UpdateLinkedProperties(ref maxParticles, emission = (byte)value);
        }
        /// <summary>The angle of the particles and the center.</summary>
        [ObjectStringMappable(ObjectProperty.Angle, defaultAngle)]
        public double Angle
        {
            get => angle.MiddleValue;
            set => angle.MiddleValue = (float)value;
        }
        /// <summary>The Angle +- property.</summary>
        [ObjectStringMappable(ObjectProperty.AngleAdjustment, defaultAngleAdjustment)]
        public double AngleAdjustment
        {
            get => angle.MaximumDistance;
            set => angle.MaximumDistance = (float)value;
        }
        /// <summary>The speed at which the particles move.</summary>
        [ObjectStringMappable(ObjectProperty.CustomParticleSpeed, defaultSpeed)]
        public double Speed
        {
            get => speed.MiddleValue;
            set => speed.MiddleValue = (float)value;
        }
        /// <summary>The Speed +- property.</summary>
        [ObjectStringMappable(ObjectProperty.SpeedAdjustment, defaultSpeedAdjustment)]
        public double SpeedAdjustment
        {
            get => speed.MaximumDistance;
            set => speed.MaximumDistance = (float)value;
        }
        /// <summary>The PosVarX property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectProperty.PosVarX, 0d)]
        public double PosVarX
        {
            get => posVar.X;
            set => posVar.X = (float)value;
        }
        /// <summary>The PosVarY +- property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectProperty.PosVarY, 0d)]
        public double PosVarY
        {
            get => posVar.Y;
            set => posVar.Y = (float)value;
        }
        /// <summary>The GravityX property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectProperty.GravityX, 0d)]
        public double GravityX
        {
            get => gravity.X;
            set => gravity.X = (float)value;
        }
        /// <summary>The GravityY +- property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectProperty.GravityY, 0d)]
        public double GravityY
        {
            get => gravity.Y;
            set => gravity.Y = (float)value;
        }
        /// <summary>The AccelRad property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectProperty.AccelRad, 0d)]
        public double AccelRad
        {
            get => accelRad.MiddleValue;
            set => accelRad.MiddleValue = (float)value;
        }
        /// <summary>The AccelRad +- property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectProperty.AccelRadAdjustment, 0d)]
        public double AccelRadAdjustment
        {
            get => accelRad.MaximumDistance;
            set => accelRad.MaximumDistance = (float)value;
        }
        /// <summary>The AccelTan property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectProperty.AccelTan, 0d)]
        public double AccelTan
        {
            get => accelTan.MiddleValue;
            set => accelTan.MiddleValue = (float)value;
        }
        /// <summary>The AccelTan +- property (unknown functionality).</summary>
        [ObjectStringMappable(ObjectProperty.AccelTanAdjustment, 0d)]
        public double AccelTanAdjustment
        {
            get => accelTan.MaximumDistance;
            set => accelTan.MaximumDistance = (float)value;
        }
        #endregion
        #region Visual
        /// <summary>The size during the start of the particle's life.</summary>
        [ObjectStringMappable(ObjectProperty.StartSize, defaultStartSize)]
        public int StartSize
        {
            get => startSize.MiddleValue;
            set => startSize.MiddleValue = value;
        }
        /// <summary>The StartSize +- property.</summary>
        [ObjectStringMappable(ObjectProperty.StartSizeAdjustment, defaultStartSizeAdjustment)]
        public int StartSizeAdjustment
        {
            get => startSize.MaximumDistance;
            set => startSize.MaximumDistance = value;
        }
        /// <summary>The size during the end of the particle's life.</summary>
        [ObjectStringMappable(ObjectProperty.EndSize, defaultEndSize)]
        public int EndSize
        {
            get => endSize.MiddleValue;
            set => endSize.MiddleValue = value;
        }
        /// <summary>The EndSize +- property.</summary>
        [ObjectStringMappable(ObjectProperty.EndSizeAdjustment, defaultEndSizeAdjustment)]
        public int EndSizeAdjustment
        {
            get => endSize.MaximumDistance;
            set => endSize.MaximumDistance = value;
        }
        /// <summary>The rotation during the start of the particle's life.</summary>
        [ObjectStringMappable(ObjectProperty.StartSpin, 0)]
        public int StartSpin
        {
            get => startSpin.MiddleValue;
            set => startSpin.MiddleValue = value;
        }
        /// <summary>The StartSpin +- property.</summary>
        [ObjectStringMappable(ObjectProperty.StartSpinAdjustment, 0)]
        public int StartSpinAdjustment
        {
            get => startSpin.MaximumDistance;
            set => startSpin.MaximumDistance = value;
        }
        /// <summary>The rotation during the end of the particle's life.</summary>
        [ObjectStringMappable(ObjectProperty.EndSpin, 0)]
        public int EndSpin
        {
            get => endSpin.MiddleValue;
            set => endSpin.MiddleValue = value;
        }
        /// <summary>The EndSpin +- property.</summary>
        [ObjectStringMappable(ObjectProperty.EndSpinAdjustment, 0)]
        public int EndSpinAdjustment
        {
            get => endSpin.MaximumDistance;
            set => endSpin.MaximumDistance = value;
        }
        /// <summary>The alpha value of the color during the start of the particle's life.</summary>
        [ObjectStringMappable(ObjectProperty.StartA, defaultColorComponent)]
        public double StartA
        {
            get => start.MiddleValue.A;
            set => start.MiddleValue.A = (float)value;
        }
        /// <summary>The Start_A +- property.</summary>
        [ObjectStringMappable(ObjectProperty.StartAAdjustment, defaultColorAdjustmentComponent)]
        public double StartAAdjustment
        {
            get => start.MaximumDistance.A;
            set => start.MaximumDistance.A = (float)value;
        }
        /// <summary>The red value of the color during the start of the particle's life.</summary>
        [ObjectStringMappable(ObjectProperty.StartR, defaultColorComponent)]
        public double StartR
        {
            get => start.MiddleValue.R;
            set => start.MiddleValue.R = (float)value;
        }
        /// <summary>The Start_R +- property.</summary>
        [ObjectStringMappable(ObjectProperty.StartRAdjustment, defaultColorAdjustmentComponent)]
        public double StartRAdjustment
        {
            get => start.MaximumDistance.R;
            set => start.MaximumDistance.R = (float)value;
        }
        /// <summary>The green value of the color during the start of the particle's life.</summary>
        [ObjectStringMappable(ObjectProperty.StartG, defaultColorComponent)]
        public double StartG
        {
            get => start.MiddleValue.G;
            set => start.MiddleValue.G = (float)value;
        }
        /// <summary>The Start_G +- property.</summary>
        [ObjectStringMappable(ObjectProperty.StartGAdjustment, defaultColorAdjustmentComponent)]
        public double StartGAdjustment
        {
            get => start.MaximumDistance.G;
            set => start.MaximumDistance.G = (float)value;
        }
        /// <summary>The blue value of the color during the start of the particle's life.</summary>
        [ObjectStringMappable(ObjectProperty.StartB, defaultColorComponent)]
        public double StartB
        {
            get => start.MiddleValue.B;
            set => start.MiddleValue.B = (float)value;
        }
        /// <summary>The Start_B +- property.</summary>
        [ObjectStringMappable(ObjectProperty.StartBAdjustment, defaultColorAdjustmentComponent)]
        public double StartBAdjustment
        {
            get => start.MaximumDistance.B;
            set => start.MaximumDistance.B = (float)value;
        }
        /// <summary>The alpha value of the color during the end of the particle's life.</summary>
        [ObjectStringMappable(ObjectProperty.EndA, defaultColorComponent)]
        public double EndA
        {
            get => end.MiddleValue.A;
            set => end.MiddleValue.A = (float)value;
        }
        /// <summary>The End_A +- property.</summary>
        [ObjectStringMappable(ObjectProperty.EndAAdjustment, defaultColorAdjustmentComponent)]
        public double EndAAdjustment
        {
            get => end.MaximumDistance.A;
            set => end.MaximumDistance.A = (float)value;
        }
        /// <summary>The red value of the color during the end of the particle's life.</summary>
        [ObjectStringMappable(ObjectProperty.EndR, defaultColorComponent)]
        public double EndR
        {
            get => end.MiddleValue.R;
            set => end.MiddleValue.R = (float)value;
        }
        /// <summary>The End_R +- property.</summary>
        [ObjectStringMappable(ObjectProperty.EndRAdjustment, defaultColorAdjustmentComponent)]
        public double EndRAdjustment
        {
            get => end.MaximumDistance.R;
            set => end.MaximumDistance.R = (float)value;
        }
        /// <summary>The green value of the color during the end of the particle's life.</summary>
        [ObjectStringMappable(ObjectProperty.EndG, defaultColorComponent)]
        public double EndG
        {
            get => end.MiddleValue.G;
            set => end.MiddleValue.G = (float)value;
        }
        /// <summary>The End_G +- property.</summary>
        [ObjectStringMappable(ObjectProperty.EndGAdjustment, defaultColorAdjustmentComponent)]
        public double EndGAdjustment
        {
            get => end.MaximumDistance.G;
            set => end.MaximumDistance.G = (float)value;
        }
        /// <summary>The blue value of the color during the end of the particle's life.</summary>
        [ObjectStringMappable(ObjectProperty.EndB, defaultColorComponent)]
        public double EndB
        {
            get => end.MiddleValue.B;
            set => end.MiddleValue.B = (float)value;
        }
        /// <summary>The End_B +- property.</summary>
        [ObjectStringMappable(ObjectProperty.EndBAdjustment, defaultColorAdjustmentComponent)]
        public double EndBAdjustment
        {
            get => end.MaximumDistance.B;
            set => end.MaximumDistance.B = (float)value;
        }
        #endregion
        #region Extra
        /// <summary>The grouping of the custom particles.</summary>
        [ObjectStringMappable(ObjectProperty.Grouping, CustomParticleGrouping.Free)]
        public CustomParticleGrouping Grouping { get; set; }
        /// <summary>The Fade In property.</summary>
        [ObjectStringMappable(ObjectProperty.CustomParticleFadeIn, 0d)]
        public double FadeIn
        {
            get => fadeIn.MiddleValue;
            set => fadeIn.MiddleValue = (float)value;
        }
        /// <summary>The Fade In +- property.</summary>
        [ObjectStringMappable(ObjectProperty.FadeInAdjustment, 0d)]
        public double FadeInAdjustment
        {
            get => fadeIn.MaximumDistance;
            set => fadeIn.MaximumDistance = (float)value;
        }
        /// <summary>The Fade Out property.</summary>
        [ObjectStringMappable(ObjectProperty.CustomParticleFadeOut, 0d)]
        public double FadeOut
        {
            get => fadeOut.MiddleValue;
            set => fadeOut.MiddleValue = (float)value;
        }
        /// <summary>The Fade Out +- property.</summary>
        [ObjectStringMappable(ObjectProperty.FadeOutAdjustment, 0d)]
        public double FadeOutAdjustment
        {
            get => fadeOut.MaximumDistance;
            set => fadeOut.MaximumDistance = (float)value;
        }
        /// <summary>Represents the Additive property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectProperty.Additive, false)]
        public bool Additive
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }
        /// <summary>Represents the Start Size = End property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectProperty.StartSizeEqualsEnd, false)]
        public bool StartSizeEqualsEnd
        {
            get => SpecialObjectBools[1];
            set => SpecialObjectBools[1] = value;
        }
        /// <summary>Represents the Start Spin = End property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectProperty.StartSpinEqualsEnd, false)]
        public bool StartSpinEqualsEnd
        {
            get => SpecialObjectBools[2];
            set => SpecialObjectBools[2] = value;
        }
        /// <summary>Represents the Start Radius = End property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectProperty.StartRadiusEqualsEnd, false)]
        public bool StartRadiusEqualsEnd
        {
            get => SpecialObjectBools[3];
            set => SpecialObjectBools[3] = value;
        }
        /// <summary>Represents the Start Rotation Is Dir property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectProperty.StartRotationIsDir, false)]
        public bool StartRotationIsDir
        {
            get => SpecialObjectBools[4];
            set => SpecialObjectBools[4] = value;
        }
        /// <summary>Represents the Dynamic Rotation property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectProperty.DynamicRotation, false)]
        public bool DynamicRotation
        {
            get => SpecialObjectBools[5];
            set => SpecialObjectBools[5] = value;
        }
        /// <summary>Represents the Use Object Color property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectProperty.UseObjectColor, false)]
        public bool UseObjectColor
        {
            get => SpecialObjectBools[6];
            set => SpecialObjectBools[6] = value;
        }
        /// <summary>Represents the Uniform Object Color property of the custom particle object.</summary>
        [ObjectStringMappable(ObjectProperty.UniformObjectColor, false)]
        public bool UniformObjectColor
        {
            get => SpecialObjectBools[7];
            set => SpecialObjectBools[7] = value;
        }
        #endregion

        /// <summary>The texture of the particles.</summary>
        [ObjectStringMappable(ObjectProperty.Texture, 0)]
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
