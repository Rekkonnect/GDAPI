using GDAPI.Attributes;
using GDAPI.Objects.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.Triggers;

namespace GDAPI.Enumerations.GeometryDash
{
    /// <summary>This enumeration provides values for the properties of a <see cref="LevelObject"/>.</summary>
    public enum ObjectProperty
    {
        /// <summary>Represents the ID of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyIntType]
        ObjectID = 1,
        /// <summary>Represents the X location of the <see cref="LevelObject"/> in units.</summary>
        [ObjectPropertyDoubleType]
        X = 2,
        /// <summary>Represents the Y location of the <see cref="LevelObject"/> in units.</summary>
        [ObjectPropertyDoubleType]
        Y = 3,
        /// <summary>Represents a value determining whether the <see cref="LevelObject"/> is flipped horizontally.</summary>
        [ObjectPropertyBoolType]
        FlippedHorizontally = 4,
        /// <summary>Represents a value determining whether the <see cref="LevelObject"/> is flipped vertically.</summary>
        [ObjectPropertyBoolType]
        FlippedVertically = 5,
        /// <summary>Represents the rotation of the <see cref="LevelObject"/> in degrees (positive is clockwise).</summary>
        [ObjectPropertyDoubleType]
        Rotation = 6,
        /// <summary>Represents the Red value of the color in the trigger.</summary>
        [ObjectPropertyIntType]
        Red = 7,
        /// <summary>Represents the Green value of the color in the trigger.</summary>
        [ObjectPropertyIntType]
        Green = 8,
        /// <summary>Represents the Blue value of the color in the trigger.</summary>
        [ObjectPropertyIntType]
        Blue = 9,
        /// <summary>Represents the duration of the effect of the trigger.</summary>
        [ObjectPropertyDoubleType]
        Duration = 10,
        /// <summary>Represents the Touch Triggered value of the trigger.</summary>
        [ObjectPropertyBoolType]
        TouchTriggered = 11,
        /// <summary>Represents the ID value of the Secret Coin.</summary>
        [ObjectPropertyIntType]
        SecretCoinID = 12,
        /// <summary>Represents the checked property of the special object.</summary>
        [ObjectPropertyBoolType]
        SpecialObjectChecked = 13,
        /// <summary>Represents the Tint Ground property of the BG Color trigger (discarded from 2.1 onwards).</summary>
        [ObjectPropertyBoolType]
        TintGround = 14,
        /// <summary>Represents the Player Color 1 property of the Color trigger.</summary>
        [ObjectPropertyBoolType]
        PlayerColor1 = 15,
        /// <summary>Represents the Player Color 2 property of the Color trigger.</summary>
        [ObjectPropertyBoolType]
        PlayerColor2 = 16,
        /// <summary>Represents the Blending property of the Color trigger.</summary>
        [ObjectPropertyBoolType]
        Blending = 17,
        /// <summary>Unknown feature with ID 18.</summary>
        UnknownFeature18 = 18,
        /// <summary>Unknown feature with ID 19.</summary>
        UnknownFeature19 = 19,
        /// <summary>Represents the Editor Layer 1 value of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyIntType]
        EL1 = 20,
        /// <summary>Represents the Main Color Channel value of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyIntType]
        Color1ID = 21,
        /// <summary>Represents the Detail Color Channel value of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyIntType]
        Color2ID = 22,
        /// <summary>Represents the Target Color ID property of the Color trigger.</summary>
        [ObjectPropertyIntType]
        TargetColorID = 23,
        /// <summary>Represents the Z Layer value of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyIntType]
        ZLayer = 24,
        /// <summary>Represents the Z Order value of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyIntType]
        ZOrder = 25,
        /// <summary>Unknown feature with ID 26.</summary>
        UnknownFeature26 = 26,
        /// <summary>Unknown feature with ID 27.</summary>
        UnknownFeature27 = 27,
        /// <summary>Represents the Offset X value of the <seealso cref="MoveTrigger"/> and <seealso cref="CameraOffsetTrigger"/> in units.</summary>
        [ObjectPropertyDoubleType]
        OffsetX = 28,
        /// <summary>Represents the Offset Y value of the <seealso cref="MoveTrigger"/> and <seealso cref="CameraOffsetTrigger"/> in units.</summary>
        [ObjectPropertyDoubleType]
        OffsetY = 29,
        /// <summary>Represents the Easing mode value of the trigger.</summary>
        [ObjectPropertyEasingType]
        Easing = 30,
        /// <summary>Represents the text of the text object encrypted in Base 64.</summary>
        [ObjectPropertyStringType]
        TextObjectText = 31,
        /// <summary>Represents the scaling of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyDoubleType]
        Scaling = 32,
        /// <summary>Unknown feature with ID 33.</summary>
        UnknownFeature33 = 33,
        /// <summary>Represents the Group Parent property of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyBoolType]
        GroupParent = 34,
        /// <summary>Represents the opacity value of the trigger.</summary>
        [ObjectPropertyDoubleType]
        Opacity = 35,
        /// <summary>Unknown feature with ID 36. It has been found as true on most special objects, and some general objects. Its usage must be investigated.</summary>
        [ObjectPropertyBoolType]
        UnknownFeature36 = 36,
        /// <summary>Unknown feature with ID 37.</summary>
        UnknownFeature37 = 37,
        /// <summary>Unknown feature with ID 38.</summary>
        UnknownFeature38 = 38,
        /// <summary>Unknown feature with ID 39.</summary>
        UnknownFeature39 = 39,
        /// <summary>Unknown feature with ID 40.</summary>
        UnknownFeature40 = 40,
        /// <summary>Represents the Color 1 HSV enabled value of the object.</summary>
        [ObjectPropertyBoolType]
        Color1HSVEnabled = 41,
        /// <summary>Represents the Color 2 HSV enabled value of the object.</summary>
        [ObjectPropertyBoolType]
        Color2HSVEnabled = 42,
        /// <summary>Represents the Color 1 HSV values of the object.</summary>
        [ObjectPropertyHSVAdjustmentType]
        Color1HSV = 43,
        /// <summary>Represents the Color 2 HSV values of the object.</summary>
        [ObjectPropertyHSVAdjustmentType]
        Color2HSV = 44,
        /// <summary>Represents the Fade In value of the Pulse trigger.</summary>
        [ObjectPropertyDoubleType]
        FadeIn = 45,
        /// <summary>Represents the Hold value of the Pulse trigger.</summary>
        [ObjectPropertyDoubleType]
        Hold = 46,
        /// <summary>Represents the Fade Out value of the Pulse trigger.</summary>
        [ObjectPropertyDoubleType]
        FadeOut = 47,
        /// <summary>Represents the Pulse Mode value of the Pulse trigger.</summary>
        [ObjectPropertyPulseModeType]
        PulseMode = 48,
        /// <summary>Represents the Copied Color HSV values of the trigger.</summary>
        [ObjectPropertyHSVAdjustmentType]
        CopiedColorHSVValues = 49,
        /// <summary>Represents the Copied Color ID value of the trigger.</summary>
        [ObjectPropertyIntType]
        CopiedColorID = 50,
        /// <summary>Represents the Target Group ID value of the trigger.</summary>
        [ObjectPropertyIntType]
        TargetGroupID = 51,
        /// <summary>Represents the Target Type value of the Pulse trigger.</summary>
        [ObjectPropertyPulseTargetTypeType]
        TargetType = 52,
        /// <summary>Unknown feature with ID 53.</summary>
        UnknownFeature53 = 53,
        /// <summary>Represents the value for the distance of the yellow teleportation portal from the blue teleportation portal.</summary>
        [ObjectPropertyDoubleType]
        YellowTeleportationPortalDistance = 54,
        /// <summary>Unknown feature with ID 55.</summary>
        UnknownFeature55 = 55,
        /// <summary>Represents the Activate Group property of the trigger.</summary>
        [ObjectPropertyBoolType]
        ActivateGroup = 56,
        /// <summary>Represents the assigned Group IDs of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyIntArrayType]
        GroupIDs = 57,
        /// <summary>Represents the Lock To Player X property of the Move trigger.</summary>
        [ObjectPropertyBoolType]
        LockToPlayerX = 58,
        /// <summary>Represents the Lock To Player Y property of the Move trigger.</summary>
        [ObjectPropertyBoolType]
        LockToPlayerY = 59,
        /// <summary>Represents the Copy Opacity property of the trigger.</summary>
        [ObjectPropertyBoolType]
        CopyOpacity = 60,
        /// <summary>Represents the Editor Layer 2 value of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyIntType]
        EL2 = 61,
        /// <summary>Represents the Spawn Triggered value of the trigger.</summary>
        [ObjectPropertyBoolType]
        SpawnTriggered = 62,
        /// <summary>Represents the Delay in the Spawn Trigger.</summary>
        [ObjectPropertyDoubleType]
        SpawnDelay = 63,
        /// <summary>Represents the Don't Fade property of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyBoolType]
        DontFade = 64,
        /// <summary>Represents the Main Only property of the Pulse trigger.</summary>
        [ObjectPropertyBoolType]
        MainOnly = 65,
        /// <summary>Represents the Detail Only property of the Pulse trigger.</summary>
        [ObjectPropertyBoolType]
        DetailOnly = 66,
        /// <summary>Represents the Don't Enter property of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyBoolType]
        DontEnter = 67,
        /// <summary>Represents the Degrees value of the Rotate trigger.</summary>
        [ObjectPropertyIntType]
        Degrees = 68,
        /// <summary>Represents the Times 360 value of the Rotate trigger.</summary>
        [ObjectPropertyIntType]
        Times360 = 69,
        /// <summary>Represents the Lock Object Rotation property of the Rotate trigger.</summary>
        [ObjectPropertyBoolType]
        LockObjectRotation = 70,
        /// <summary>Represents the Follow Group ID value of the Follow Trigger.</summary>
        [ObjectPropertyIntType]
        FollowGroupID = 71,
        /// <summary>Represents the Target Pos Group ID value of the Move Trigger.</summary>
        [ObjectPropertyIntType]
        TargetPosGroupID = 71,
        /// <summary>Represents the Center Group ID value of the Rotate Trigger.</summary>
        [ObjectPropertyIntType]
        CenterGroupID = 71,
        /// <summary>Represents the secondary Group ID value of some triggers.</summary>
        [ObjectPropertyIntType]
        SecondaryGroupID = 71,
        /// <summary>Represents the X Mod of value the Follow Trigger.</summary>
        [ObjectPropertyDoubleType]
        XMod = 72,
        /// <summary>Represents the Y Mod of value the Follow Trigger.</summary>
        [ObjectPropertyDoubleType]
        YMod = 73,
        /// <summary>Represents a value in the Follow trigger whose use is unknown.</summary>
        UnknownFollowTriggerFeature = 74,
        /// <summary>Represents the Strength value of the Shake trigger.</summary>
        [ObjectPropertyDoubleType]
        Strength = 75,
        /// <summary>Represents the Animation ID value of the Animate trigger.</summary>
        [ObjectPropertyIntType]
        AnimationID = 76,
        /// <summary>Represents the Count value of the Pickup trigger or Pickup Item.</summary>
        [ObjectPropertyIntType]
        Count = 77,
        /// <summary>Represents the Subtract Count property of the Pickup trigger or Pickup Item.</summary>
        [ObjectPropertyBoolType]
        SubtractCount = 78,
        /// <summary>Represents the Pickup Mode value of the Pickup Item.</summary>
        [ObjectPropertyPickupItemPickupModeType]
        PickupMode = 79,
        /// <summary>Represents the Target Item ID value, or the assigned Item ID value of the Pickup trigger or Pickup Item respectively.</summary>
        [ObjectPropertyIntType]
        ItemID = 80,
        /// <summary>Represents the Block ID value of the Collision block <see cref="LevelObject"/>.</summary>
        [ObjectPropertyIntType]
        BlockID = 80,
        /// <summary>Represents the Block A ID value of the Collision trigger.</summary>
        [ObjectPropertyIntType]
        BlockAID = 80,
        /// <summary>Represents the Hold Mode value of the Touch trigger.</summary>
        [ObjectPropertyBoolType]
        HoldMode = 81,
        /// <summary>Represents the Toggle Mode value of the Touch trigger.</summary>
        [ObjectPropertyTouchToggleModeType]
        ToggleMode = 82,
        /// <summary>Unknown feature with ID 83.</summary>
        UnknownFeature83 = 83,
        /// <summary>Represents the Interval value of the Shake trigger.</summary>
        [ObjectPropertyDoubleType]
        Interval = 84,
        /// <summary>Represents the Easing Rate value of the trigger.</summary>
        [ObjectPropertyDoubleType]
        EasingRate = 85,
        /// <summary>Represents the Exclusive property of the Pulse trigger.</summary>
        [ObjectPropertyBoolType]
        Exclusive = 86,
        /// <summary>Represents the Multi Trigger property of the trigger.</summary>
        [ObjectPropertyBoolType]
        MultiTrigger = 87,
        /// <summary>Represents the Comparison value of the Instant Count trigger.</summary>
        [ObjectPropertyInstantCountComparisonType]
        Comparison = 88,
        /// <summary>Represents the Dual Mode property of the Touch trigger.</summary>
        [ObjectPropertyBoolType]
        DualMode = 89,
        /// <summary>Represents the Speed value of the Follow Player Y trigger.</summary>
        [ObjectPropertyDoubleType]
        Speed = 90,
        /// <summary>Represents the Delay of the Follow Player Y Trigger.</summary>
        [ObjectPropertyDoubleType]
        FollowDelay = 91,
        /// <summary>Represents the Offset value of the Follow Player Y trigger.</summary>
        [ObjectPropertyDoubleType]
        YOffset = 92,
        /// <summary>Represents the Trigger On Exit property of the Collision trigger.</summary>
        [ObjectPropertyBoolType]
        TriggerOnExit = 93,
        /// <summary>Represents the Dynamic Block property of the Collision block.</summary>
        [ObjectPropertyBoolType]
        DynamicBlock = 94,
        /// <summary>Represents the Block B ID of the Collision trigger.</summary>
        [ObjectPropertyIntType]
        BlockBID = 95,
        /// <summary>Determines whether the glow of the <see cref="LevelObject"/> is disabled or not.</summary>
        [ObjectPropertyBoolType]
        DisableGlow = 96,
        /// <summary>Represents the custom rotation speed of the rotating <see cref="LevelObject"/> in degrees per second.</summary>
        [ObjectPropertyDoubleType]
        CustomRotationSpeed = 97,
        /// <summary>Determines whether the rotation of the rotating <see cref="LevelObject"/> is disabled or not.</summary>
        [ObjectPropertyBoolType]
        DisableRotation = 98,
        /// <summary>Represents the Multi Activate property of the Count trigger.</summary>
        [ObjectPropertyBoolType]
        MultiActivate = 99,
        /// <summary>Determines whether the Use Target of the Move trigger is enabled or not.</summary>
        [ObjectPropertyBoolType]
        EnableUseTarget = 100,
        /// <summary>Represents the coordinates that the <see cref="LevelObject"/> will follow the <see cref="LevelObject"/> in the Target Pos Group ID.</summary>
        [ObjectPropertyTargetPosCoordinatesType]
        TargetPosCoordinates = 101,
        /// <summary>Determines whether the Spawn trigger will be disabled while playtesting in the editor. (Currently dysfunctional as of 2.103)</summary>
        [ObjectPropertyBoolType]
        EditorDisable = 102,
        /// <summary>Determines whether the <see cref="LevelObject"/> is only enabled in High Detail Mode.</summary>
        [ObjectPropertyBoolType]
        HighDetail = 103,
        /// <summary>Unknown feature with ID 104.</summary>
        UnknownFeature104 = 104,
        /// <summary>Represents the Max Speed property in the <seealso cref="FollowPlayerYTrigger"/>, affecting the max speed at which the objects can be moved from the player's Y movement.</summary>
        [ObjectPropertyDoubleType]
        MaxSpeed = 105,
        /// <summary>Determines whether the animated <see cref="LevelObject"/> will randomly start.</summary>
        [ObjectPropertyBoolType]
        RandomizeStart = 106,
        /// <summary>Represents the animation speed of the animated <see cref="LevelObject"/>.</summary>
        [ObjectPropertyDoubleType]
        AnimationSpeed = 107,
        /// <summary>Represents the linked Group ID of the <see cref="LevelObject"/>.</summary>
        [ObjectPropertyIntType]
        LinkedGroupID = 108,

        // Future-proofing
        #region General
        /// <summary>Represents the [unrevealed text box feature 115] property of the <seealso cref="LevelObject"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        UnrevealedTextBoxFeature115 = 115,
        /// <summary>Represents the Switch Player Direction property of the <see cref="OrbPad"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        SwitchPlayerDirection = 117,
        // Due to bad reservation habits, the new sneak peek's property IDs are offset starting at -200 for this category
        // However this does not really matter since the values are unused and only serve as future-proof reservations
        // The IDs will end up being discovered and used
        /// <summary>Represents whether the <see cref="LevelObject"/> will have any effects.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        NoEffects = 116,
        /// <summary>The Ice Block property of the <see cref="LevelObject"/> (probably for adventure mode).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        IceBlock = -201,
        /// <summary>The Non-Stick property of the <see cref="LevelObject"/> (probably for adventure mode).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        NonStick = -202,
        /// <summary>The Unstuckable(?) property of the <see cref="LevelObject"/> (probably for adventure mode).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        Unstuckable = -203,
        /// <summary>The [unreadable text 1] property of the <see cref="LevelObject"/> (probably for adventure mode).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        UnreadableProperty1 = -204,
        /// <summary>The [unreadable text 2] property of the <see cref="LevelObject"/> (probably for adventure mode).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        UnreadableProperty2 = -205,
        /// <summary>The transformation scaling X property of the <see cref="LevelObject"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        TransformationScalingX = -206,
        /// <summary>The transformation scaling Y property of the <see cref="LevelObject"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        TransformationScalingY = -207,
        /// <summary>The transformation scaling center X property of the <see cref="LevelObject"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        TransformationScalingCenterX = -208,
        /// <summary>The transformation scaling center Y property of the <see cref="LevelObject"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        TransformationScalingCenterY = -209,
        #endregion

        #region Camera Offset Trigger
        #endregion

        #region Static Camera Trigger
        /// <summary>The Exit Static property of the <seealso cref="StaticCameraTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        ExitStatic = 110,
        #endregion

        #region End Trigger
        /// <summary>The Reversed property of the <seealso cref="EndTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        Reversed = 118,
        /// <summary>The Lock Y property of the <seealso cref="EndTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        LockY = 59, // This does not make sense but ok
        #endregion

        #region Random Trigger
        /// <summary>The Chance property of the <seealso cref="RandomTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        Chance = 10, // You must be kidding; using the duration property as a chance?
        // New sneak peek, new offset
        /// <summary>The Chance Lots property of the <seealso cref="RandomTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyChancePoolInfoType] // Assumed
        ChanceLots = -300,
        // There is a small *chance* RobTop actually uses a special way to store the chance lot per group
        // For instance, just like HSV, which is HaSaVaSCaVC
        // I would expect something like 1b10.2b20, where . is the separator between groups and b is the separator for the group and the chance lots
        // In the example above it means group 1 has 10 chance lots and group 2 has 20 chance lots
        /// <summary>The Chance Lot Groups property of the <seealso cref="RandomTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntArrayType]
        ChanceLotGroups = -301,
        #endregion

        #region Zoom Trigger
        /// <summary>The Zoom property of the <seealso cref="ZoomTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        Zoom = 109,
        #endregion

        #region Custom Particle Object
        /// <summary>The grouping of the custom particles.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyCustomParticleGroupingType]
        Grouping = -108,
        /// <summary>The property 1 of the custom particles.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyCustomParticleProperty1Type]
        Property1 = -109, // TODO: Figure out what this does
        /// <summary>The maximum number of particles that will be alive simultaneously.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        MaxParticles = -110,
        /// <summary>The duration of the particle creation.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        CustomParticleDuration = -111, // Using already implemented Duration property?
        /// <summary>The lifetime of the particle creation.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        Lifetime = -112,
        /// <summary>The Lifetime +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        LifetimeAdjustment = -113,
        /// <summary>The Emission property (unknown functionality).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        Emission = -114,
        /// <summary>The angle of the particles and the center.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        Angle = -115,
        /// <summary>The Angle +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        AngleAdjustment = -116,
        /// <summary>The speed at which the particles move.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        CustomParticleSpeed = -117, // Using already implemented Speed property?
        /// <summary>The Speed +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        SpeedAdjustment = -118,
        /// <summary>The PosVarX property (unknown functionality).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        PosVarX = -119,
        /// <summary>The PosVarY +- property (unknown functionality).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        PosVarY = -120,
        /// <summary>The GravityX property (unknown functionality).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        GravityX = -121,
        /// <summary>The GravityY +- property (unknown functionality).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        GravityY = -122,
        /// <summary>The AccelRad property (unknown functionality).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        AccelRad = -123,
        /// <summary>The AccelRad +- property (unknown functionality).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        AccelRadAdjustment = -124,
        /// <summary>The AccelTan property (unknown functionality).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        AccelTan = -125,
        /// <summary>The AccelTan +- property (unknown functionality).</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        AccelTanAdjustment = -126,
        /// <summary>The size during the start of the particle's life.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        StartSize = -127,
        /// <summary>The StartSize +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        StartSizeAdjustment = -128,
        /// <summary>The size during the end of the particle's life.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        EndSize = -129,
        /// <summary>The EndSize +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        EndSizeAdjustment = -130,
        /// <summary>The rotation during the start of the particle's life.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        StartSpin = -131,
        /// <summary>The StartSpin +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        StartSpinAdjustment = -132,
        /// <summary>The rotation during the end of the particle's life.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        EndSpin = -133,
        /// <summary>The EndSpin +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        EndSpinAdjustment = -134,
        /// <summary>The alpha value of the color during the start of the particle's life.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        StartA = -135,
        /// <summary>The Start_A +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        StartAAdjustment = -136,
        /// <summary>The red value of the color during the start of the particle's life.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        StartR = -137,
        /// <summary>The Start_R +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        StartRAdjustment = -138,
        /// <summary>The green value of the color during the start of the particle's life.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        StartG = -139,
        /// <summary>The Start_G +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        StartGAdjustment = -140,
        /// <summary>The blue value of the color during the start of the particle's life.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        StartB = -141,
        /// <summary>The Start_B +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        StartBAdjustment = -142,
        /// <summary>The alpha value of the color during the end of the particle's life.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        EndA = -143,
        /// <summary>The End_A +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        EndAAdjustment = -144,
        /// <summary>The red value of the color during the end of the particle's life.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        EndR = -145,
        /// <summary>The End_R +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        EndRAdjustment = -146,
        /// <summary>The green value of the color during the end of the particle's life.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        EndG = -147,
        /// <summary>The End_G +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        EndGAdjustment = -148,
        /// <summary>The blue value of the color during the end of the particle's life.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        EndB = -149,
        /// <summary>The End_B +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        EndBAdjustment = -150,
        /// <summary>The Fade In property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        CustomParticleFadeIn = -151, // Using already implemented FadeIn property?
        /// <summary>The Fade In +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        FadeInAdjustment = -152,
        /// <summary>The Fade Out property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        CustomParticleFadeOut = -153, // Using already implemented FadeOut property?
        /// <summary>The Fade Out +- property.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        FadeOutAdjustment = -154,
        /// <summary>Represents the Additive property of the custom particle object.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        Additive = -155,
        /// <summary>Represents the Start Size = End property of the custom particle object.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        StartSizeEqualsEnd = -156,
        /// <summary>Represents the Start Spin = End property of the custom particle object.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        StartSpinEqualsEnd = -157,
        /// <summary>Represents the Start Radius = End property of the custom particle object.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        StartRadiusEqualsEnd = -158,
        /// <summary>Represents the Start Rotation Is Dir property of the custom particle object.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        StartRotationIsDir = -159,
        /// <summary>Represents the Dynamic Rotation property of the custom particle object.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        DynamicRotation = -160,
        /// <summary>Represents the Use Object Color property of the custom particle object.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        UseObjectColor = -161,
        /// <summary>Represents the Uniform Object Color property of the custom particle object.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        UniformObjectColor = -162,
        /// <summary>The texture of the particles.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyIntType]
        Texture = -163,
        #endregion

        #region Scale Trigger
        /// <summary>The Scale X property of the <seealso cref="ScaleTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        ScaleX = -164,
        /// <summary>The Scale Y property of the <seealso cref="ScaleTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyDoubleType]
        ScaleY = -165,
        /// <summary>The Lock Object Scale property of the <seealso cref="ScaleTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        LockObjectScale = -166,
        /// <summary>The Only Move Scale property of the <seealso cref="ScaleTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        OnlyMoveScale = -167,
        #endregion

        #region Move Trigger
        /// <summary>The Lock To Camera X property of the <seealso cref="MoveTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        LockToCameraX = -302,
        /// <summary>The Lock To Camera Y property of the <seealso cref="MoveTrigger"/>.</summary>
        [FutureProofing("2.2")]
        [ObjectPropertyBoolType]
        LockToCameraY = -303,
        #endregion
    }
}
