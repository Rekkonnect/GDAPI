using GDAPI.Application.Editors.Actions;
using GDAPI.Application.Editors.Actions.EditorActions;
using GDAPI.Application.Editors.Actions.LevelActions;
using GDAPI.Application.Editors.Delegates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GDAPI.Enumerations;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash;
using GDAPI.Objects.GeometryDash.ColorChannels;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;
using GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.Triggers;
using GDAPI.Objects.IDMigration;
using static GDAPI.Objects.General.SourceTargetRange;
using static System.Math;

namespace GDAPI.Application.Editors
{
    /// <summary>The editor which edits a level.</summary>
    public class Editor
    {
        private bool swipe, gridSnap, freeMove, dualLayerMode;
        private double gridSize = 30, zoom = 1;

        private UndoRedoSystem editorActions = new UndoRedoSystem();
        private UndoRedoSystem levelActions = new UndoRedoSystem();

        #region Constants
        /// <summary>The big movement step in units.</summary>
        public const double BigMovementStep = 150;
        /// <summary>The normal movement step in units.</summary>
        public const double NormalMovementStep = 30;
        /// <summary>The small movement step in units.</summary>
        public const double SmallMovementStep = 2;
        /// <summary>The tiny movement step in units.</summary>
        public const double TinyMovementStep = 0.5;
        #endregion

        #region Level
        /// <summary>The currently edited level.</summary>
        public Level Level;
        /// <summary>The currently selected objects.</summary>
        public LevelObjectCollection SelectedObjects = new LevelObjectCollection();
        /// <summary>The objects that are copied into the clipboard and can be pasted.</summary>
        public LevelObjectCollection ObjectClipboard = new LevelObjectCollection();
        #endregion

        #region Editor Function Toggles
        /// <summary>Indicates whether the Swipe option is enabled or not.</summary>
        public bool Swipe
        {
            get => swipe;
            set => SetSwipe(value, true);
        }
        /// <summary>Indicates whether the Grid Snap option is enabled or not.</summary>
        public bool GridSnap
        {
            get => gridSnap;
            set => SetGridSnap(value, true);
        }
        /// <summary>Indicates whether the Free Move option is enabled or not.</summary>
        public bool FreeMove
        {
            get => freeMove;
            set => SetFreeMove(value, true);
        }
        #endregion

        #region Editor Preferences
        /// <summary>The customly defined movement steps in the editor.</summary>
        public List<double> CustomMovementSteps;
        /// <summary>The customly defined rotation steps in the editor.</summary>
        public List<double> CustomRotationSteps;
        #endregion

        #region Camera
        /// <summary>The size of each grid block in the editor.</summary>
        public double GridSize
        {
            get => gridSize;
            set => SetGridSize(value, true);
        }
        /// <summary>The camera zoom in the editor.</summary>
        public double Zoom
        {
            get => zoom;
            set => SetZoom(value, true);
        }

        /// <summary>Gets or sets a value indicating whether the editor is in dual layer mode.</summary>
        public bool DualLayerMode
        {
            get => dualLayerMode;
            set => SetDualLayerMode(value);
        }
        #endregion

        #region Events
        #region Editor Actions
        /// <summary>Occurs when the swipe option has been changed, including the new status.</summary>
        public event SwipeChangedHandler SwipeChanged;
        /// <summary>Occurs when the grid snap option has been changed, including the new status.</summary>
        public event GridSnapChangedHandler GridSnapChanged;
        /// <summary>Occurs when the free move option has been changed, including the new status.</summary>
        public event FreeMoveChangedHandler FreeMoveChanged;
        /// <summary>Occurs when the grid size has been changed, including the old and the new values.</summary>
        public event GridSizeChangedHandler GridSizeChanged;
        /// <summary>Occurs when the grid size has been changed, including the old and the new values.</summary>
        public event ZoomChangedHandler ZoomChanged;
        /// <summary>Occurs when the dual layer mode has been changed, including the new status.</summary>
        public event DualLayerModeChangedHandler DualLayerModeChanged;
        #endregion

        #region Level Actions
        // TODO: Create delegates
        /// <summary>Occurs when new objects have been added to the selection list.</summary>
        public event Action<LevelObjectCollection> SelectedObjectsAdded;
        /// <summary>Occurs when new objects have been removed from the selection list.</summary>
        public event Action<LevelObjectCollection> SelectedObjectsRemoved;
        /// <summary>Occurs when all objects have been deselected.</summary>
        public event AllObjectsDeselectedHandler AllObjectsDeselected;

        /// <summary>Occurs when objects have been created.</summary>
        public event Action<LevelObjectCollection> ObjectsCreated;
        /// <summary>Occurs when objects have been deleted.</summary>
        public event Action<LevelObjectCollection> ObjectsDeleted;

        /// <summary>Occurs when objects have been moved.</summary>
        public event MovedObjectsHandler ObjectsMoved;
        /// <summary>Occurs when objects have been rotated.</summary>
        public event RotatedObjectsHandler ObjectsRotated;
        /// <summary>Occurs when objects have been scaled.</summary>
        public event ScaledObjectsHandler ObjectsScaled;
        /// <summary>Occurs when objects have been flipped horizontally.</summary>
        public event FlippedObjectsHorizontallyHandler ObjectsFlippedHorizontally;
        /// <summary>Occurs when objects have been flipped vertically.</summary>
        public event FlippedObjectsVerticallyHandler ObjectsFlippedVertically;

        /// <summary>Occurs when objects have been copied to the clipboard.</summary>
        public event ObjectsCopiedHandler ObjectsCopied;
        /// <summary>Occurs when objects have been pasted from the clipboard.</summary>
        public event ObjectsPastedHandler ObjectsPasted;
        /// <summary>Occurs when objects have been ViPriNized.</summary>
        public event ObjectsCopyPastedHandler ObjectsCopyPasted;

        /// <summary>Occurs when a color channel has been changed.</summary>
        public event ColorChangedHandler ColorChanged;
        /// <summary>Occurs when a color channel's Blending property has been changed.</summary>
        public event BlendingChangedHandler BlendingChanged;

        /// <summary>Occurs when group IDs have been added to objects.</summary>
        public event GroupIDsAddedHandler GroupIDsAdded;
        /// <summary>Occurs when group IDs have been removed from objects.</summary>
        public event GroupIDsRemovedHandler GroupIDsRemoved;

        /// <summary>Occurs when the main color ID of a number of objects has been changed.</summary>
        public event MainColorIDsChangedHandler MainColorIDsChanged;
        /// <summary>Occurs when the detail color ID of a number of objects has been changed.</summary>
        public event DetailColorIDsChangedHandler DetailColorIDsChanged;
        #endregion
        #endregion

        #region Event Functions
        // Signatures and final invocation statements were macro-generated
        // TODO: Add support for the new object properties in 2.2
        #region Editor Actions
        /// <summary>Triggers the <seealso cref="SwipeChanged"/> event.</summary>
        /// <param name="value">The value that was set to Swipe.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnSwipeChanged(bool value, bool registerUndoable = true)
        {
            if (registerUndoable)
                editorActions.AddTemporaryAction(new SwipeChanged(value, SetSwipe));
            SwipeChanged?.Invoke(value);
        }
        /// <summary>Triggers the <seealso cref="GridSnapChanged"/> event.</summary>
        /// <param name="value">The value that was set to Grid Snap.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnGridSnapChanged(bool value, bool registerUndoable = true)
        {
            if (registerUndoable)
                editorActions.AddTemporaryAction(new GridSnapChanged(value, SetGridSnap));
            GridSnapChanged?.Invoke(value);
        }
        /// <summary>Triggers the <seealso cref="FreeMoveChanged"/> event.</summary>
        /// <param name="value">The value that was set to Free Move.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnFreeMoveChanged(bool value, bool registerUndoable = true)
        {
            if (registerUndoable)
                editorActions.AddTemporaryAction(new FreeMoveChanged(value, SetFreeMove));
            FreeMoveChanged?.Invoke(value);
        }
        /// <summary>Triggers the <seealso cref="ZoomChanged"/> event.</summary>
        /// <param name="oldValue">The old value of Zoom.</param>
        /// <param name="newValue">The value that was set to Zoom.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnZoomChanged(double oldValue, double newValue, bool registerUndoable = true)
        {
            if (registerUndoable)
                editorActions.AddTemporaryAction(new ZoomChanged(newValue, oldValue, SetZoom));
            ZoomChanged?.Invoke(oldValue, newValue);
        }
        /// <summary>Triggers the <seealso cref="GridSizeChanged"/> event.</summary>
        /// <param name="oldValue">The old value of Grid Size.</param>
        /// <param name="newValue">The value that was set to Grid Size.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnGridSizeChanged(double oldValue, double newValue, bool registerUndoable = true)
        {
            if (registerUndoable)
                editorActions.AddTemporaryAction(new GridSizeChanged(newValue, oldValue, SetGridSize));
            GridSizeChanged?.Invoke(oldValue, newValue);
        }
        /// <summary>Triggers the <seealso cref="DualLayerModeChanged"/> event.</summary>
        /// <param name="value">The value that was set to Dual Layer Mode.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnDualLayerModeChanged(bool value, bool registerUndoable = true)
        {
            if (registerUndoable)
                editorActions.AddTemporaryAction(new DualLayerModeChanged(value, SetFreeMove));
            DualLayerModeChanged?.Invoke(value);
        }
        #endregion

        #region Level Actions
        /// <summary>Triggers the <seealso cref="SelectedObjectsAdded"/> event.</summary>
        /// <param name="objects">The objects that were added to the selection list.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnSelectedObjectsAdded(LevelObjectCollection objects, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new SelectedObjectsAdded(objects, SelectObjects, DeselectObjects));
            SelectedObjectsAdded?.Invoke(objects);
        }
        /// <summary>Triggers the <seealso cref="SelectedObjectsRemoved"/> event.</summary>
        /// <param name="objects">The objects that were removed from the selection list.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnSelectedObjectsRemoved(LevelObjectCollection objects, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new SelectedObjectsRemoved(objects, DeselectObjects, SelectObjects));
            SelectedObjectsRemoved?.Invoke(objects);
        }
        /// <summary>Triggers the <seealso cref="AllObjectsDeselected"/> event.</summary>
        /// <param name="previousSelection">The previous selection before deselecting all the objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnAllObjectsDeselected(LevelObjectCollection previousSelection, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new AllObjectsDeselected(previousSelection, DeselectAll, SelectObjects));
            AllObjectsDeselected?.Invoke(previousSelection);
        }
        /// <summary>Triggers the <seealso cref="ObjectsCreated"/> event.</summary>
        /// <param name="objects">The objects that were added to the level.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsCreated(LevelObjectCollection objects, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new ObjectsCreated(objects, AddObjects, RemoveObjects));
            ObjectsCreated?.Invoke(objects);
        }
        /// <summary>Triggers the <seealso cref="ObjectsDeleted"/> event.</summary>
        /// <param name="objects">The objects that were removed from the level.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsDeleted(LevelObjectCollection objects, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new ObjectsDeleted(objects, RemoveObjects, AddObjects));
            ObjectsDeleted?.Invoke(objects);
        }
        /// <summary>Triggers the <seealso cref="ObjectsMoved"/> event.</summary>
        /// <param name="objects">The objects that were moved.</param>
        /// <param name="offset">The difference of the previous and the new locations of the objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsMoved(LevelObjectCollection objects, Point offset, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new ObjectsMoved(objects, offset, Move));
            ObjectsMoved?.Invoke(objects, offset);
        }
        /// <summary>Triggers the <seealso cref="ObjectsRotated"/> event.</summary>
        /// <param name="objects">The objects that were rotated.</param>
        /// <param name="offset">The difference of the previous and the new rotations of the objects.</param>
        /// <param name="centralPoint">The central point that was taken into account while rotating all objects. If <see langword="null"/>, the objects were individually rotated.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsRotated(LevelObjectCollection objects, double offset, Point? centralPoint, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new ObjectsRotated(objects, offset, centralPoint, Rotate));
            ObjectsRotated?.Invoke(objects, offset, centralPoint);
        }
        /// <summary>Triggers the <seealso cref="ObjectsScaled"/> event.</summary>
        /// <param name="objects">The objects that were rotated.</param>
        /// <param name="scaling">The ratio of the previous and the new scalings of the objects, e.g. if the new scaling is 8x and the old one was 2x, this value will equal to 4, since 8 / 2 = 4.</param>
        /// <param name="centralPoint">The central point that was taken into account while rotating all objects. If <see langword="null"/>, the objects were individually rotated.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsScaled(LevelObjectCollection objects, double scaling, Point? centralPoint, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new ObjectsRotated(objects, scaling, centralPoint, Scale));
            ObjectsScaled?.Invoke(objects, scaling, centralPoint);
        }
        /// <summary>Triggers the <seealso cref="ObjectsFlippedHorizontally"/> event.</summary>
        /// <param name="objects">The objects that were flipped horizontally.</param>
        /// <param name="centralPoint">The central point that was taken into account while flipping all objects horizontally. If <see langword="null"/>, the objects were individually flipped.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsFlippedHorizontally(LevelObjectCollection objects, Point? centralPoint, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new ObjectsFlippedHorizontally(objects, centralPoint, FlipHorizontally));
            ObjectsFlippedHorizontally?.Invoke(objects, centralPoint);
        }
        /// <summary>Triggers the <seealso cref="ObjectsFlippedVertically"/> event.</summary>
        /// <param name="objects">The objects that were flipped vertically.</param>
        /// <param name="centralPoint">The central point that was taken into account while flipping all objects vertically. If <see langword="null"/>, the objects were individually flipped.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsFlippedVertically(LevelObjectCollection objects, Point? centralPoint, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new ObjectsFlippedVertically(objects, centralPoint, FlipHorizontally));
            ObjectsFlippedVertically?.Invoke(objects, centralPoint);
        }
        /// <summary>Triggers the <seealso cref="ObjectsCopied"/> event.</summary>
        /// <param name="newObjects">The objects that were copied and added to the clipboard.</param>
        /// <param name="oldObjects">The objects that were previously in the clipboard.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsCopied(LevelObjectCollection newObjects, LevelObjectCollection oldObjects, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new ObjectsCopied(newObjects, oldObjects, Copy));
            ObjectsCopied?.Invoke(newObjects, oldObjects);
        }
        /// <summary>Triggers the <seealso cref="ObjectsPasted"/> event.</summary>
        /// <param name="objects">The new objects that were pasted.</param>
        /// <param name="centralPoint">The central point that was taken into account while pasting all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsPasted(LevelObjectCollection objects, Point centralPoint, bool registerUndoable = true)
        {
            // This will not select the previously selected objects, if any; a workaround must be implemented
            // Preferably the workaround involves implementing something that registers both the deselection and the pasting using the multiple action toggle
            if (registerUndoable)
                levelActions.AddTemporaryAction(new ObjectsPasted(objects, centralPoint, Paste, RemoveObjects));
            ObjectsPasted?.Invoke(objects, centralPoint);
        }
        /// <summary>Triggers the <seealso cref="ObjectsCopyPasted"/> event.</summary>
        /// <param name="newObjects">The objects that were pasted.</param>
        /// <param name="oldObjects">The objects that were copied.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnObjectsCopyPasted(LevelObjectCollection newObjects, LevelObjectCollection oldObjects, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new ObjectsCopyPasted(newObjects, oldObjects, CopyPaste, UndoCopyPaste));
            ObjectsCopyPasted?.Invoke(newObjects, oldObjects);
        }

        /// <summary>Triggers the <seealso cref="ColorChanged"/> event.</summary>
        /// <param name="affectedObjects">The objects that were affected from this change.</param>
        /// <param name="colorID">The color ID that was changed.</param>
        /// <param name="oldColor">The old color of the color channel.</param>
        /// <param name="newColor">The new color of the color channel.</param>
        /// <param name="colorChannel">The color channel instance that was changed.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnColorChanged(LevelObjectCollection affectedObjects, int colorID, Color oldColor, Color newColor, ColorChannel colorChannel, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new ColorChanged(colorID, newColor, oldColor, ChangeColorChannelColor));
            ColorChanged?.Invoke(affectedObjects, colorID, oldColor, newColor, colorChannel);
        }
        /// <summary>Triggers the <seealso cref="BlendingChanged"/> event.</summary>
        /// <param name="affectedObjects">The objects that were affected from this change.</param>
        /// <param name="colorID">The color ID whose Blending property was changed.</param>
        /// <param name="colorChannel">The color channel instance that was changed.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnBlendingChanged(LevelObjectCollection affectedObjects, int colorID, ColorChannel colorChannel, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new BlendingChanged(colorID, ChangeColorChannelBlending));
            BlendingChanged?.Invoke(affectedObjects, colorID, colorChannel);
        }

        /// <summary>Triggers the <seealso cref="GroupIDsAdded"/> event.</summary>
        /// <param name="objects">The objects whose group IDs were changed.</param>
        /// <param name="groupIDs">The group IDs that were added.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnGroupIDsAdded(LevelObjectCollection objects, IEnumerable<int> groupIDs, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new GroupIDsAdded(objects, groupIDs, AddGroupIDs, RemoveGroupIDs));
            GroupIDsAdded?.Invoke(objects, groupIDs);
        }
        /// <summary>Triggers the <seealso cref="GroupIDsRemoved"/> event.</summary>
        /// <param name="objects">The objects whose group IDs were changed.</param>
        /// <param name="groupIDs">The group IDs that were removed.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnGroupIDsRemoved(LevelObjectCollection objects, IEnumerable<int> groupIDs, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new GroupIDsAdded(objects, groupIDs, RemoveGroupIDs, AddGroupIDs));
            GroupIDsRemoved?.Invoke(objects, groupIDs);
        }

        /// <summary>Triggers the <seealso cref="MainColorIDsChanged"/> event.</summary>
        /// <param name="objects">The objects whose main color ID was changed.</param>
        /// <param name="colorIDDifference">The color ID difference (new - old).</param>
        /// <param name="colorChannel">The color channel information of the new main color ID.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnMainColorIDsChanged(LevelObjectCollection objects, int colorIDDifference, ColorChannel colorChannel, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new MainColorIDsChanged(objects, colorIDDifference, colorChannel.ColorChannelID, ChangeMainColorID));
            MainColorIDsChanged?.Invoke(objects, colorIDDifference, colorChannel);
        }
        /// <summary>Triggers the <seealso cref="DetailColorIDsChanged"/> event.</summary>
        /// <param name="objects">The objects whose detail color ID was changed.</param>
        /// <param name="colorIDDifference">The color ID difference (new - old).</param>
        /// <param name="colorChannel">The color channel information of the new detail color ID.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void OnDetailColorIDsChanged(LevelObjectCollection objects, int colorIDDifference, ColorChannel colorChannel, bool registerUndoable = true)
        {
            if (registerUndoable)
                levelActions.AddTemporaryAction(new DetailColorIDsChanged(objects, colorIDDifference, colorChannel.ColorChannelID, ChangeMainColorID));
            DetailColorIDsChanged?.Invoke(objects, colorIDDifference, colorChannel);
        }
        #endregion
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <seealso cref="Editor"/> class.</summary>
        /// <param name="level">The level to edit. Upon initializing this instance, the level's cached level string data is cleared.</param>
        public Editor(Level level)
        {
            (Level = level)?.ClearCachedLevelStringData();
        }
        #endregion

        #region Object Selection
        /// <summary>Selects an object.</summary>
        /// <param name="obj">The object to add to the selection.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SelectObject(GeneralObject obj, bool registerUndoable = true)
        {
            if (!Swipe)
                DeselectAll();
            SelectedObjects.Add(obj);
            OnSelectedObjectsAdded(new LevelObjectCollection(obj), registerUndoable);
        }
        /// <summary>Selects a number of objects.</summary>
        /// <param name="objects">The objects to add to the selection.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SelectObjects(LevelObjectCollection objects, bool registerUndoable = true)
        {
            if (!Swipe)
                DeselectAll();
            SelectedObjects.AddRange(objects);
            OnSelectedObjectsAdded(objects, registerUndoable);
        }
        /// <summary>Deselects an object.</summary>
        /// <param name="obj">The object to remove from the selection.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void DeselectObject(GeneralObject obj, bool registerUndoable = true)
        {
            SelectedObjects.Remove(obj);
            OnSelectedObjectsRemoved(new LevelObjectCollection(obj), registerUndoable);
        }
        /// <summary>Deselects a number of objects.</summary>
        /// <param name="objects">The objects to remove from the selection.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void DeselectObjects(LevelObjectCollection objects, bool registerUndoable = true)
        {
            if (objects == SelectedObjects) // Micro-optimization
                DeselectAll(registerUndoable);
            else
            {
                SelectedObjects.RemoveRange(objects);
                OnSelectedObjectsRemoved(objects, registerUndoable);
            }
        }
        /// <summary>Inverts the current selection.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void InvertSelection(bool registerUndoable = true)
        {
            var newObjects = Level.LevelObjects.Clone().RemoveRange(SelectedObjects);
            DeselectAll(registerUndoable);
            SelectObjects(newObjects, registerUndoable);
        }
        /// <summary>Selects a number of objects based on a condition.</summary>
        /// <param name="predicate">The predicate to determine the objects to select.</param>
        /// <param name="appendToSelection">Determines whether the new objects will be appended to the already existing selection.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SelectObjects(Predicate<GeneralObject> predicate, bool appendToSelection = true, bool registerUndoable = true)
        {
            // Maybe there's a better way to take care of that but let it be like that for the time being
            levelActions.MultipleActionToggle = true;
            if (!appendToSelection)
                DeselectAll(registerUndoable);
            SelectObjects(GetObjects(predicate), registerUndoable);
            levelActions.MultipleActionToggle = false;
        }
        /// <summary>Selects all objects.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SelectAll(bool registerUndoable = true) => SelectObjects(Level.LevelObjects, registerUndoable);
        /// <summary>Deselects all objects.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void DeselectAll(bool registerUndoable = true)
        {
            var previousSelection = SelectedObjects.Clone();
            SelectedObjects.Clear();
            OnAllObjectsDeselected(previousSelection, registerUndoable);
        }
        #endregion

        #region Object Filtering
        /// <summary>Returns the object within a rectangular range.</summary>
        /// <param name="startingX">The X of the starting point.</param>
        /// <param name="startingY">The Y of the starting point.</param>
        /// <param name="endingX">The X of the ending point.</param>
        /// <param name="endingY">The Y of the ending point.</param>
        public LevelObjectCollection GetObjectsWithinRange(double startingX, double startingY, double endingX, double endingY)
        {
            // Fix starting/ending points to avoid having to fix in the editor itself
            if (startingX > endingX)
                Swap(ref startingX, ref endingX);
            if (startingY > endingY)
                Swap(ref startingY, ref endingY);
            var result = new LevelObjectCollection();
            foreach (var o in Level.LevelObjects)
                if (o.IsWithinRange(startingX, startingY, endingX, endingY))
                    result.Add(o);
            return result;
        }
        /// <summary>Returns all the objects that are in a specific layer.</summary>
        /// <param name="EL">The editor layer which contains the objects to retrieve.</param>
        public LevelObjectCollection GetObjectsByLayer(int EL)
        {
            if (EL == -1) // Indicates the All layer
                return Level.LevelObjects;
            var result = new LevelObjectCollection();
            foreach (var o in Level.LevelObjects)
                if (o.EL1 == EL || o.EL2 == EL)
                    result.Add(o);
            return result;
        }
        /// <summary>Returns a collection of objects based on a predicate.</summary>
        /// <param name="predicate">The predicate to determine the resulting object collection.</param>
        public LevelObjectCollection GetObjects(Predicate<GeneralObject> predicate)
        {
            var result = new LevelObjectCollection();
            foreach (var o in Level.LevelObjects)
                if (predicate(o))
                    result.Add(o);
            return result;
        }
        #endregion

        #region Object Editing
        // TODO: Figure out a performant way to improve the copy-pasted code model
        private void PerformAction(bool individually, Action action, Action nonIndividualAction, Action eventToInvoke)
        {
            if (individually)
            {
                action();
                eventToInvoke();
            }
            else
                nonIndividualAction();
        }
        #region Object Movement
        #region Differential Movement (Move*)
        /// <summary>Moves the selected objects by an amount on the X axis.</summary>
        /// <param name="x">The offset of X to move the objects by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveX(double x, bool registerUndoable = true) => MoveX(SelectedObjects, x, registerUndoable);
        /// <summary>Moves the specified object by an amount on the X axis.</summary>
        /// <param name="obj">The object to move.</param>
        /// <param name="x">The offset of X to move the object by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveX(GeneralObject obj, double x, bool registerUndoable = true) => MoveX(new LevelObjectCollection(obj), x, registerUndoable);
        /// <summary>Moves the specified objects by an amount on the X axis.</summary>
        /// <param name="objects">The objects to move.</param>
        /// <param name="x">The offset of X to move the objects by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveX(LevelObjectCollection objects, double x, bool registerUndoable = true)
        {
            if (x != 0)
            {
                foreach (var o in objects)
                    o.X += x;
                OnObjectsMoved(objects, new Point(x, 0), registerUndoable);
            }
        }
        /// <summary>Moves the selected objects by an amount on the Y axis.</summary>
        /// <param name="y">The offset of Y to move the objects by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveY(double y, bool registerUndoable = true) => MoveY(SelectedObjects, y, registerUndoable);
        /// <summary>Moves the specified object by an amount on the Y axis.</summary>
        /// <param name="obj">The object to move.</param>
        /// <param name="y">The offset of Y to move the object by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveY(GeneralObject obj, double y, bool registerUndoable = true) => MoveY(new LevelObjectCollection(obj), y, registerUndoable);
        /// <summary>Moves the specified objects by an amount on the Y axis.</summary>
        /// <param name="objects">The objects to move.</param>
        /// <param name="y">The offset of Y to move the objects by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveY(LevelObjectCollection objects, double y, bool registerUndoable = true)
        {
            if (y != 0)
            {
                foreach (var o in objects)
                    o.Y += y;
                OnObjectsMoved(objects, new Point(0, y), registerUndoable);
            }
        }
        /// <summary>Moves the selected objects by an amount.</summary>
        /// <param name="x">The offset of X to move the objects by.</param>
        /// <param name="y">The offset of Y to move the objects by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Move(double x, double y, bool registerUndoable = true) => Move(new Point(x, y), registerUndoable);
        /// <summary>Moves the specified object by an amount.</summary>
        /// <param name="obj">The object to move.</param>
        /// <param name="x">The offset of X to move the object by.</param>
        /// <param name="y">The offset of Y to move the object by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Move(GeneralObject obj, double x, double y, bool registerUndoable = true) => Move(new LevelObjectCollection(obj), new Point(x, y), registerUndoable);
        /// <summary>Moves the specified objects by an amount.</summary>
        /// <param name="objects">The objects to move.</param>
        /// <param name="x">The offset of X to move the objects by.</param>
        /// <param name="y">The offset of Y to move the objects by.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Move(LevelObjectCollection objects, double x, double y, bool registerUndoable = true) => Move(objects, new Point(x, y), registerUndoable);
        /// <summary>Moves the selected objects by an amount.</summary>
        /// <param name="p">The point indicating the movement of the objects across the field.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Move(Point p, bool registerUndoable = true) => Move(SelectedObjects, p, registerUndoable);
        /// <summary>Moves the specified object by an amount.</summary>
        /// <param name="obj">The object to move.</param>
        /// <param name="p">The point indicating the movement of the object across the field.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Move(GeneralObject obj, Point p, bool registerUndoable = true) => Move(new LevelObjectCollection(obj), p, registerUndoable);
        /// <summary>Moves the specified objects by an amount.</summary>
        /// <param name="objects">The objects to move.</param>
        /// <param name="p">The point indicating the movement of the objects across the field.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Move(LevelObjectCollection objects, Point p, bool registerUndoable = true)
        {
            if (p.Y == 0)
                MoveX(p.X);
            else if (p.X == 0)
                MoveY(p.Y);
            else
            {
                foreach (var o in objects)
                {
                    o.X += p.X;
                    o.Y += p.Y;
                }
                OnObjectsMoved(objects, p, registerUndoable);
            }
        }
        #endregion
        #region Absolute Movement (MoveTo*)
        /// <summary>Moves the selected objects to a location in the X axis.</summary>
        /// <param name="x">The X location to move the object to.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveToX(double x, bool registerUndoable = true) => MoveToX(SelectedObjects, x, registerUndoable);
        /// <summary>Moves the specified object to a location in the X axis.</summary>
        /// <param name="obj">The object to move.</param>
        /// <param name="x">The X location to move the object to.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveToX(GeneralObject obj, double x, bool registerUndoable = true)
        {
            if (x != 0)
                MoveX(obj, x - obj.X, registerUndoable);
        }
        /// <summary>Moves the specified objects to a location in the X axis.</summary>
        /// <param name="objects">The objects to move.</param>
        /// <param name="x">The X location to move the object to.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveToX(LevelObjectCollection objects, double x, bool registerUndoable = true)
        {
            if (x != 0)
            {
                levelActions.MultipleActionToggle = true;
                foreach (var o in objects)
                    MoveX(o, x - o.X, registerUndoable);
                levelActions.MultipleActionToggle = false;
            }
        }
        /// <summary>Moves the selected objects to a location in the Y axis.</summary>
        /// <param name="y">The Y location to move the object to.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveToY(double y, bool registerUndoable = true) => MoveToX(SelectedObjects, y, registerUndoable);
        /// <summary>Moves the specified object to a location in the Y axis.</summary>
        /// <param name="objects">The objects to move.</param>
        /// <param name="y">The Y location to move the object to.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveToY(LevelObjectCollection objects, double y, bool registerUndoable = true)
        {
            if (y != 0)
            {
                levelActions.MultipleActionToggle = true;
                foreach (var o in objects)
                    MoveY(o, y - o.Y, registerUndoable);
                levelActions.MultipleActionToggle = false;
            }
        }
        /// <summary>Moves the selected objects to a location.</summary>
        /// <param name="p">The point indicating the movement of the objects across the field.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveTo(Point p, bool registerUndoable = true) => MoveTo(SelectedObjects, p, registerUndoable);
        /// <summary>Moves the specified objects by an amount.</summary>
        /// <param name="objects">The objects to move.</param>
        /// <param name="p">The point indicating the movement of the objects across the field.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void MoveTo(LevelObjectCollection objects, Point p, bool registerUndoable = true)
        {
            if (p.Y == 0)
                MoveToX(p.X);
            else if (p.X == 0)
                MoveToY(p.Y);
            else
            {
                levelActions.MultipleActionToggle = true;
                foreach (var o in objects)
                    Move(o, p - o.Location, registerUndoable);
                levelActions.MultipleActionToggle = false;
            }
        }
        #endregion
        #endregion
        #region Object Rotation
        // The rotation direction is probably wrongly documented; take a look at it in another branch
        /// <summary>Rotates the selected objects by an amount.</summary>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="individually">Determines whether the objects will be only rotated individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Rotate(double rotation, bool individually, bool registerUndoable = true) => Rotate(SelectedObjects, rotation, individually, registerUndoable);
        /// <summary>Rotates the specified objects by an amount.</summary>
        /// <param name="objects">The objects to rotate.</param>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="individually">Determines whether the objects will be only rotated individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Rotate(LevelObjectCollection objects, double rotation, bool individually, bool registerUndoable = true)
        {
            if (individually)
            {
                foreach (var o in objects)
                    o.Rotation += rotation;
                OnObjectsRotated(objects, rotation, null, registerUndoable);
            }
            else
                Rotate(objects, rotation, GetMedianPoint(), registerUndoable);
        }
        /// <summary>Rotates the selected objects by an amount based on a specific central point.</summary>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="center">The central point to take into account while rotating all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Rotate(double rotation, Point center, bool registerUndoable = true) => Rotate(SelectedObjects, rotation, center, registerUndoable);
        /// <summary>Rotates the specified objects by an amount based on a specific central point.</summary>
        /// <param name="objects">The objects to rotate.</param>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="center">The central point to take into account while rotating all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Rotate(LevelObjectCollection objects, double rotation, Point center, bool registerUndoable = true)
        {
            foreach (var o in objects)
            {
                o.Rotation += rotation;
                o.Location = o.Location.Rotate(center, rotation);
            }
            OnObjectsRotated(objects, rotation, center, registerUndoable);
        }
        /// <summary>Rotates the selected objects by an amount based on a specific central point.</summary>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="center">The central point to take into account while rotating all objects. If <see langword="null"/>, the objects will be individually rotated.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Rotate(double rotation, Point? center, bool registerUndoable = true) => Rotate(SelectedObjects, rotation, center, registerUndoable);
        /// <summary>Rotates the specified objects by an amount based on a specific central point.</summary>
        /// <param name="objects">The objects to rotate.</param>
        /// <param name="rotation">The rotation to apply to all the objects. Positive is counter-clockwise (CCW), negative is clockwise (CW).</param>
        /// <param name="center">The central point to take into account while rotating all objects. If <see langword="null"/>, the objects will be individually rotated.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Rotate(LevelObjectCollection objects, double rotation, Point? center, bool registerUndoable = true)
        {
            if (center == null)
                Rotate(objects, rotation, true, registerUndoable);
            else
                Rotate(objects, rotation, center.Value, registerUndoable);
        }
        #endregion
        #region Object Scaling
        /// <summary>Scales the selected objects by an amount.</summary>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="individually">Determines whether the objects will be only scaled individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Scale(double scaling, bool individually, bool registerUndoable = true) => Scale(SelectedObjects, scaling, individually, registerUndoable);
        /// <summary>Scales the specified objects by an amount.</summary>
        /// <param name="objects">The objects to scale.</param>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="individually">Determines whether the objects will be only scaled individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Scale(LevelObjectCollection objects, double scaling, bool individually, bool registerUndoable = true)
        {
            if (individually)
            {
                foreach (var o in objects)
                    o.Scaling *= scaling;
                OnObjectsScaled(objects, scaling, null, registerUndoable);
            }
            else
                Scale(objects, scaling, GetMedianPoint(), registerUndoable);
        }
        /// <summary>Scales the selected objects by an amount based on a specific central point.</summary>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="center">The central point to take into account while scaling all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Scale(double scaling, Point center, bool registerUndoable = true) => Scale(SelectedObjects, scaling, center, registerUndoable);
        /// <summary>Scales the specified objects by an amount based on a specific central point.</summary>
        /// <param name="objects">The objects to scale.</param>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="center">The central point to take into account while scaling all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Scale(LevelObjectCollection objects, double scaling, Point center, bool registerUndoable = true)
        {
            foreach (var o in objects)
            {
                o.Scaling *= scaling;
                o.Location = (center - o.Location) * scaling + center;
            }
            OnObjectsScaled(objects, scaling, center, registerUndoable);
        }
        /// <summary>Scales the selected objects by an amount based on a specific central point.</summary>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="center">The central point to take into account while scaling all objects. If <see langword="null"/>, the objects will be individually scaled.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Scale(double scaling, Point? center, bool registerUndoable = true) => Scale(SelectedObjects, scaling, center, registerUndoable);
        /// <summary>Scales the specified objects by an amount based on a specific central point.</summary>
        /// <param name="objects">The objects to scale.</param>
        /// <param name="scaling">The scaling to apply to all the objects.</param>
        /// <param name="center">The central point to take into account while scaling all objects. If <see langword="null"/>, the objects will be individually scaled.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Scale(LevelObjectCollection objects, double scaling, Point? center, bool registerUndoable = true)
        {
            if (center == null)
                Scale(objects, scaling, true, registerUndoable);
            else
                Scale(objects, scaling, center.Value, registerUndoable);
        }
        #endregion
        #region Object Flipping
        #region Flip Horizontally
        /// <summary>Flips the selected objects horizontally.</summary>
        /// <param name="individually">Determines whether the objects will be only flipped individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipHorizontally(bool individually, bool registerUndoable = true) => FlipHorizontally(SelectedObjects, individually, registerUndoable);
        /// <summary>Flips the selected objects horizontally.</summary>
        /// <param name="objects">The objects to flip horizontally.</param>
        /// <param name="individually">Determines whether the objects will be only flipped individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipHorizontally(LevelObjectCollection objects, bool individually, bool registerUndoable = true)
        {
            if (individually)
            {
                foreach (var o in objects)
                    o.FlippedHorizontally = !o.FlippedHorizontally;
                OnObjectsFlippedHorizontally(objects, null, registerUndoable);
            }
            else
                FlipHorizontally(objects, GetMedianPoint(), registerUndoable);
        }
        /// <summary>Flips the selected objects horizontally based on a specific central point.</summary>
        /// <param name="center">The central point to take into account while flipping all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipHorizontally(Point center, bool registerUndoable = true) => FlipHorizontally(SelectedObjects, center, registerUndoable);
        /// <summary>Flips the selected objects horizontally based on a specific central point.</summary>
        /// <param name="objects">The objects to flip horizontally.</param>
        /// <param name="center">The central point to take into account while flipping all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipHorizontally(LevelObjectCollection objects, Point center, bool registerUndoable = true)
        {
            foreach (var o in objects)
            {
                o.FlippedHorizontally = !o.FlippedHorizontally;
                o.X = 2 * center.X - o.X;
            }
            OnObjectsFlippedHorizontally(objects, center, registerUndoable);
        }
        /// <summary>Flips the selected objects horizontally based on a specific central point.</summary>
        /// <param name="center">The central point to take into account while flipping all objects. If <see langword="null"/>, the objects will be individually flipped.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipHorizontally(Point? center, bool registerUndoable = true) => FlipHorizontally(SelectedObjects, center, registerUndoable);
        /// <summary>Flips the selected objects horizontally based on a specific central point.</summary>
        /// <param name="objects">The objects to flip horizontally.</param>
        /// <param name="center">The central point to take into account while flipping all objects. If <see langword="null"/>, the objects will be individually flipped.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipHorizontally(LevelObjectCollection objects, Point? center, bool registerUndoable = true)
        {
            if (center == null)
                FlipHorizontally(objects, true, registerUndoable);
            else
                FlipHorizontally(objects, center.Value, registerUndoable);
        }
        #endregion
        #region Flip Vertically
        /// <summary>Flips the selected objects vertically.</summary>
        /// <param name="individually">Determines whether the objects will be only flipped individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipVertically(bool individually, bool registerUndoable = true) => FlipHorizontally(SelectedObjects, individually, registerUndoable);
        /// <summary>Flips the selected objects vertically.</summary>
        /// <param name="objects">The objects to flip vertically.</param>
        /// <param name="individually">Determines whether the objects will be only flipped individually.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipVertically(LevelObjectCollection objects, bool individually, bool registerUndoable = true)
        {
            if (individually)
            {
                foreach (var o in objects)
                    o.FlippedVertically = !o.FlippedVertically;
                OnObjectsFlippedVertically(objects, null, registerUndoable);
            }
            else
                FlipVertically(objects, GetMedianPoint(), registerUndoable);
        }
        /// <summary>Flips the selected objects vertically based on a specific central point.</summary>
        /// <param name="center">The central point to take into account while flipping all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipVertically(Point center, bool registerUndoable = true) => FlipVertically(SelectedObjects, center, registerUndoable);
        /// <summary>Flips the selected objects vertically based on a specific central point.</summary>
        /// <param name="objects">The objects to flip vertically.</param>
        /// <param name="center">The central point to take into account while flipping all objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipVertically(LevelObjectCollection objects, Point center, bool registerUndoable = true)
        {
            foreach (var o in objects)
            {
                o.FlippedVertically = !o.FlippedVertically;
                o.Y = 2 * center.Y - o.Y;
            }
            OnObjectsFlippedVertically(objects, center, registerUndoable);
        }
        /// <summary>Flips the selected objects vertically based on a specific central point.</summary>
        /// <param name="center">The central point to take into account while flipping all objects. If <see langword="null"/>, the objects will be individually flipped.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipVertically(Point? center, bool registerUndoable = true) => FlipVertically(SelectedObjects, center, registerUndoable);
        /// <summary>Flips the selected objects vertically based on a specific central point.</summary>
        /// <param name="objects">The objects to flip vertically.</param>
        /// <param name="center">The central point to take into account while flipping all objects. If <see langword="null"/>, the objects will be individually flipped.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void FlipVertically(LevelObjectCollection objects, Point? center, bool registerUndoable = true)
        {
            if (center == null)
                FlipVertically(objects, true, registerUndoable);
            else
                FlipVertically(objects, center.Value, registerUndoable);
        }
        #endregion
        #endregion
        #endregion

        #region Editor Functions
        /// <summary>Adds an object to the level.</summary>
        /// <param name="obj">The object to add to the level.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void AddObject(GeneralObject obj, bool registerUndoable = true)
        {
            Level.LevelObjects.Add(obj);
            SelectObject(obj, false);
            OnObjectsCreated(new LevelObjectCollection(obj), registerUndoable);
        }
        /// <summary>Adds a collection of objects to the level.</summary>
        /// <param name="objects">The objects to add to the level.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void AddObjects(LevelObjectCollection objects, bool registerUndoable = true)
        {
            Level.LevelObjects.AddRange(objects);
            SelectObjects(objects, false);
            OnObjectsCreated(objects, registerUndoable);
        }
        /// <summary>Removes an object from the level.</summary>
        /// <param name="obj">The object to remove from the level.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void RemoveObject(GeneralObject obj, bool registerUndoable = true)
        {
            DeselectObject(obj, false);
            Level.LevelObjects.Remove(obj);
            OnObjectsDeleted(new LevelObjectCollection(obj), registerUndoable);
        }
        /// <summary>Removes the currently selected objects from the level.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void RemoveObjects(bool registerUndoable = true) => RemoveObjects(SelectedObjects, registerUndoable);
        /// <summary>Removes a collection of objects from the level.</summary>
        /// <param name="objects">The objects to remove from the level.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void RemoveObjects(LevelObjectCollection objects, bool registerUndoable = true)
        {
            DeselectObjects(objects, false);
            Level.LevelObjects.RemoveRange(objects);
            OnObjectsDeleted(objects, registerUndoable);
        }

        /// <summary>Copies the selected objects and add them to the clipboard.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Copy(bool registerUndoable = true) => Copy(SelectedObjects, registerUndoable);
        /// <summary>Copies the specified objects and add them to the clipboard.</summary>
        /// <param name="objects">The objects to copy to the clipboard.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Copy(LevelObjectCollection objects, bool registerUndoable = true)
        {
            var old = ObjectClipboard.Clone();
            ObjectClipboard = new LevelObjectCollection();
            ObjectClipboard.AddRange(objects);
            OnObjectsCopied(ObjectClipboard, old, registerUndoable);
        }
        /// <summary>Pastes the copied objects on the clipboard provided a central position to place them.</summary>
        /// <param name="center">The central position which will determine the position of the pasted objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Paste(Point center, bool registerUndoable = true) => Paste(ObjectClipboard, center, registerUndoable);
        /// <summary>Pastes the specified objects provided a central position to place them.</summary>
        /// <param name="objects">The objects to paste to the specified central position.</param>
        /// <param name="center">The central position which will determine the position of the pasted objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void Paste(LevelObjectCollection objects, Point center, bool registerUndoable = true)
        {
            if (objects.Count == 0)
                return;
            var distance = center - GetMedianPoint(objects);
            var newObjects = objects.Clone();
            foreach (var o in newObjects)
                o.Location += distance;
            Level.LevelObjects.AddRange(newObjects);
            SelectObjects(newObjects, false);
            OnObjectsPasted(newObjects, center, registerUndoable);
        }
        /// <summary>ViPriNizes all the selected objects.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void CopyPaste(bool registerUndoable = true) => CopyPaste(SelectedObjects, registerUndoable);
        /// <summary>ViPriNizes all the selected objects.</summary>
        /// <param name="objects">The objects to ViPriNize.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void CopyPaste(LevelObjectCollection objects, bool registerUndoable = true)
        {
            var cloned = objects.Clone();
            Level.LevelObjects.AddRange(cloned);
            OnObjectsCopyPasted(cloned, SelectedObjects, registerUndoable);
        }
        /// <summary>Undoes the ViPriNization and selects the previously selected objects.</summary>
        /// <param name="newObjects">The objects that were ViPriNized.</param>
        /// <param name="newObjects">The previous selection before the ViPriNization.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        private void UndoCopyPaste(LevelObjectCollection newObjects, LevelObjectCollection oldObjects, bool registerUndoable = true)
        {
            RemoveObjects(newObjects, registerUndoable);
            SelectObjects(oldObjects, registerUndoable);
        }

        /// <summary>Changes a color channel's color.</summary>
        /// <param name="colorChannelID">The color channel ID to change the color of.</param>
        /// <param name="newColor">The new color to apply to that color channel.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void ChangeColorChannelColor(int colorChannelID, Color newColor, bool registerUndoable = true)
        {
            var colorChannel = Level.ColorChannels[colorChannelID];
            var oldColor = colorChannel.Color;
            var objects = Level.LevelObjects.GetObjectsByColorID(colorChannelID);
            colorChannel.Color = newColor;
            OnColorChanged(objects, colorChannelID, oldColor, newColor, colorChannel, registerUndoable);
        }
        /// <summary>Changes a color channel's Blending.</summary>
        /// <param name="colorChannelID">The color channel ID to change the Blending of.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void ChangeColorChannelBlending(int colorChannelID, bool registerUndoable = true)
        {
            var colorChannel = Level.ColorChannels[colorChannelID];
            colorChannel.Blending = !colorChannel.Blending;
            var objects = Level.LevelObjects.GetObjectsByColorID(colorChannelID);
            OnBlendingChanged(objects, colorChannelID, colorChannel, registerUndoable);
        }

        // The *CommonGroupIDs functions serve the purpose of only changing the group IDs on objects that have the same group IDs from the provided enumerable
        // That means only the groups that objects do not have will be added and only groups the objects do have will be removed
        // Maybe these comments are misleading, but at least the functions work(?)
        /// <summary>Adjusts the provided objects' main color ID by a specified value.</summary>
        /// <param name="objects">The objects whose main color ID to change. All objects must have the same color ID.</param>
        /// <param name="groupIDs">The group IDs to add to all the objects (assuming none of the objects belong any of the groups).</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void AddCommonGroupIDs(LevelObjectCollection objects, IEnumerable<int> groupIDs, bool registerUndoable = true)
        {
            var g = groupIDs.Cast<short>();
            foreach (var o in objects)
                o.AddGroupIDs(g);
            OnGroupIDsAdded(objects, groupIDs, registerUndoable);
        }
        /// <summary>Adjusts the provided objects' main color ID by a specified value.</summary>
        /// <param name="objects">The objects whose main color ID to change. All objects must have the same color ID.</param>
        /// <param name="groupIDs">The group IDs to add to all the objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void AddGroupIDs(LevelObjectCollection objects, IEnumerable<int> groupIDs, bool registerUndoable = true)
        {
            var d = new Dictionary<IEnumerable<short>, LevelObjectCollection>();
            var g = groupIDs.Cast<short>();
            foreach (var o in objects)
                foreach (var kvp in d)
                    if (o.ExcludeExistingGroupIDs(g).SequenceEqual(kvp.Key))
                        kvp.Value.Add(o);
                    else
                        d.Add(g, new LevelObjectCollection(o));
            foreach (var kvp in d)
                AddCommonGroupIDs(kvp.Value, kvp.Key.Cast<int>(), registerUndoable);
        }
        /// <summary>Adjusts the provided objects' main color ID by a specified value.</summary>
        /// <param name="objects">The objects whose main color ID to change. All objects must have the same color ID.</param>
        /// <param name="groupIDs">The group IDs to remove from all the objects (assuming all of the objects belong to all the specified groups).</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void RemoveCommonGroupIDs(LevelObjectCollection objects, IEnumerable<int> groupIDs, bool registerUndoable = true)
        {
            var g = groupIDs.Cast<short>();
            foreach (var o in objects)
                o.RemoveGroupIDs(g);
            OnGroupIDsRemoved(objects, groupIDs, registerUndoable);
        }
        /// <summary>Adjusts the provided objects' main color ID by a specified value.</summary>
        /// <param name="objects">The objects whose main color ID to change. All objects must have the same color ID.</param>
        /// <param name="groupIDs">The group IDs to remove from all the objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void RemoveGroupIDs(LevelObjectCollection objects, IEnumerable<int> groupIDs, bool registerUndoable = true)
        {
            var d = new Dictionary<IEnumerable<short>, LevelObjectCollection>();
            var g = groupIDs.Cast<short>();
            foreach (var o in objects)
                foreach (var kvp in d)
                    if (o.GetCommonGroupIDs(g).SequenceEqual(kvp.Key))
                        kvp.Value.Add(o);
                    else
                        d.Add(g, new LevelObjectCollection(o));
            foreach (var kvp in d)
                RemoveCommonGroupIDs(kvp.Value, kvp.Key.Cast<int>(), registerUndoable);
        }

        /// <summary>Adjusts the provided objects' main color ID by a specified value.</summary>
        /// <param name="objects">The objects whose main color ID to change. All objects must have the same color ID.</param>
        /// <param name="colorIDDifference">The color ID difference (new - old).</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void ChangeMainColorID(LevelObjectCollection objects, int colorIDDifference, bool registerUndoable = true)
        {
            int? newColorID = null;
            foreach (var o in objects)
                if (newColorID == null)
                    newColorID = o.Color1ID += colorIDDifference;
                else if (newColorID.Value != (o.Color1ID += colorIDDifference))
                    throw new ArgumentException("Not all objects have the same main color ID.");
            if (newColorID != null)
                OnMainColorIDsChanged(objects, colorIDDifference, Level.ColorChannels[newColorID.Value], registerUndoable);
        }
        /// <summary>Adjusts the provided objects' main color ID by a specified value.</summary>
        /// <param name="objects">The objects whose main color ID to change. All objects must have the same color ID.</param>
        /// <param name="colorIDDifference">The color ID difference (new - old).</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void ChangeDetailColorID(LevelObjectCollection objects, int colorIDDifference, bool registerUndoable = true)
        {
            int? newColorID = null;
            foreach (var o in objects)
                if (newColorID == null)
                    newColorID = o.Color1ID += colorIDDifference;
                else if (newColorID.Value != (o.Color2ID += colorIDDifference))
                    throw new ArgumentException("Not all objects have the same detail color ID.");
            if (newColorID != null)
                OnDetailColorIDsChanged(objects, colorIDDifference, Level.ColorChannels[newColorID.Value], registerUndoable);
        }
        /// <summary>Sets the provided objects' main color ID to the specified value.</summary>
        /// <param name="objects">The objects whose main color ID to change.</param>
        /// <param name="colorID">The color ID to set as main color ID on the specified objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetMainColorID(LevelObjectCollection objects, int colorID, bool registerUndoable = true)
        {
            var d = objects.GetMainColorIDObjectDictionary();
            foreach (var k in d)
                ChangeMainColorID(objects, colorID - k.Key, registerUndoable);
        }
        /// <summary>Sets the provided objects' detail color ID to the specified value.</summary>
        /// <param name="objects">The objects whose detail color ID to change.</param>
        /// <param name="colorID">The color ID to set as detail color ID on the specified objects.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetDetailColorID(LevelObjectCollection objects, int colorID, bool registerUndoable = true)
        {
            var d = objects.GetDetailColorIDObjectDictionary();
            foreach (var k in d)
                ChangeDetailColorID(objects, colorID - k.Key, registerUndoable);
        }

        /// <summary>Resets the unused color channels.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void ResetUnusedColors(bool registerUndoable = true)
        {
            HashSet<int> usedIDs = new HashSet<int>();
            foreach (var o in Level.LevelObjects)
            {
                usedIDs.Add(o.Color1ID);
                usedIDs.Add(o.Color2ID);
            }
            HashSet<int> copiedIDs = new HashSet<int>();
            for (int i = 0; i < 1000; i++)
                copiedIDs.Add(Level.ColorChannels[i].CopiedColorID);
            foreach (var c in copiedIDs)
                usedIDs.Add(c);
            levelActions.MultipleActionToggle = true;
            for (int i = 1; i < 1000; i++)
                if (!usedIDs.Contains(i))
                    Level.ColorChannels[i].Reset();
            levelActions.MultipleActionToggle = false;
            if (registerUndoable)
                levelActions.RegisterActions("Reset unused Color IDs");
        }
        /// <summary>Resets the Group IDs of the objects that are not targeted by any trigger.</summary>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void ResetUnusedGroupIDs(bool registerUndoable = true)
        {
            HashSet<int> usedIDs = new HashSet<int>();
            foreach (var o in Level.LevelObjects)
            {
                switch (o)
                {
                    case PulseTrigger p:
                        if (p.PulseTargetType == PulseTargetType.Group)
                            usedIDs.Add(p.TargetGroupID);
                        break;
                    case PickupItem i:
                        if (i.PickupMode == PickupItemPickupMode.ToggleTriggerMode)
                            usedIDs.Add(i.TargetGroupID);
                        break;
                    case IHasTargetGroupID t:
                        usedIDs.Add(t.TargetGroupID);
                        break;
                    case IHasSecondaryGroupID s:
                        usedIDs.Add(s.SecondaryGroupID);
                        break;
                }
            }
            levelActions.MultipleActionToggle = true;
            foreach (var o in Level.LevelObjects)
                for (int i = 0; i < o.GroupIDs.Length; i++)
                    if (!usedIDs.Contains(o.GetGroupID(i)))
                        o.SetGroupID(i, 0); // TODO: Raise events
            levelActions.MultipleActionToggle = false;
            if (registerUndoable)
                levelActions.RegisterActions("Reset unused Group IDs");
        }

        /// <summary>Snaps all the triggers to the level's guidelines.</summary>
        /// <param name="maxDifference">The max difference between the time of the trigger and the guideline in milliseconds.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SnapTriggersToGuidelines(double maxDifference = 100, bool registerUndoable = true)
        {
            if (Level.Guidelines.Count == 0)
                return;
            var segments = Level.SpeedSegments;
            var triggers = new List<Trigger>(); // Contains the information of the triggers and their X positions
            foreach (Trigger t in Level.LevelObjects)
                if (!t.TouchTriggered)
                    triggers.Add(t);
            levelActions.MultipleActionToggle = true;
            foreach (var t in triggers)
            {
                double time = segments.ConvertXToTime(t.X);
                int index = Level.Guidelines.GetFirstIndexAfterTimeStamp(time);
                double a = index > 0 ? Level.Guidelines[index - 1].TimeStamp : double.NegativeInfinity;
                double b = Level.Guidelines[index].TimeStamp;
                if (time - a <= maxDifference)
                    MoveToX(t, segments.ConvertTimeToX(a));
                else if (b - time <= maxDifference)
                    MoveToX(t, segments.ConvertTimeToX(b));
            }
            levelActions.MultipleActionToggle = false;
            if (registerUndoable)
                levelActions.RegisterActions("Snap triggers to guidelines");
        }
        #endregion

        #region Editor Camera
        /// <summary>Reduces the grid size by setting it to half the original amount.</summary>
        public void ReduceGridSize() => GridSize /= 2;
        /// <summary>Increases the grid size by setting it to double the original amount.</summary>
        public void IncreaseGridSize() => GridSize *= 2;
        /// <summary>Reduces the camera zoom in the editor by 0.1x.</summary>
        public void ReduceZoom()
        {
            if (Zoom > 0.1)
                Zoom -= 0.1;
        }
        /// <summary>Increases the camera zoom in the editor by 0.1x.</summary>
        public void IncreaseZoom()
        {
            if (Zoom < 4.9) // Good threshold?
                Zoom += 0.1;
        }
        #endregion

        #region ID Migration
        /// <summary>The ID migration info of this editor instance that will be used when performing ID migration operations.</summary>
        public readonly IDMigrationInfo IDMigrationInfo = new IDMigrationInfo();

        public event Action IDMigrationOperationInitialized;
        public event ProgressReporter IDMigrationProgressReported;
        public event Action IDMigrationOperationCompleted;

        /// <summary>The steps of the Group ID migration mode.</summary>
        public List<SourceTargetRange> GroupSteps => GetIDMigrationSteps(IDMigrationMode.Groups);
        /// <summary>The steps of the Color ID migration mode.</summary>
        public List<SourceTargetRange> ColorSteps => GetIDMigrationSteps(IDMigrationMode.Colors);
        /// <summary>The steps of the Item ID migration mode.</summary>
        public List<SourceTargetRange> ItemSteps => GetIDMigrationSteps(IDMigrationMode.Items);
        /// <summary>The steps of the Block ID migration mode.</summary>
        public List<SourceTargetRange> BlockSteps => GetIDMigrationSteps(IDMigrationMode.Blocks);

        /// <summary>Gets or sets the currently selected ID migration mode.</summary>
        public IDMigrationMode SelectedIDMigrationMode
        {
            get => IDMigrationInfo.SelectedIDMigrationMode;
            set => IDMigrationInfo.SelectedIDMigrationMode = value;
        }

        /// <summary>Gets the currently selected mode's <seealso cref="IDMigrationModeInfo"/> object.</summary>
        public IDMigrationModeInfo CurrentlySelectedIDMigrationModeInfo => IDMigrationInfo.CurrentlySelectedIDMigrationModeInfo;
        /// <summary>Gets or sets the currently selected ID migration steps.</summary>
        public List<SourceTargetRange> CurrentlySelectedIDMigrationSteps
        {
            get => IDMigrationInfo.CurrentlySelectedIDMigrationSteps;
            set => IDMigrationInfo.CurrentlySelectedIDMigrationSteps = value;
        }

        /// <summary>Performs the ID migration for the currently selected mode.</summary>
        public void PerformMigration()
        {
            switch (SelectedIDMigrationMode)
            {
                case IDMigrationMode.Groups:
                    PerformGroupIDMigration();
                    break;
                case IDMigrationMode.Colors:
                    PerformColorIDMigration();
                    break;
                case IDMigrationMode.Items:
                    PerformItemIDMigration();
                    break;
                case IDMigrationMode.Blocks:
                    PerformBlockIDMigration();
                    break;
                default:
                    throw new InvalidOperationException("My disappointment is immeasurable and my day is ruined.");
            }
        }
        /// <summary>Gets the <seealso cref="IDMigrationModeInfo"/> object for a specified mode.</summary>
        /// <param name="mode">The mode to get the <seealso cref="IDMigrationModeInfo"/> object for.</param>
        public IDMigrationModeInfo GetIDMigrationModeInfo(IDMigrationMode mode) => IDMigrationInfo[mode];
        /// <summary>Gets the ID migration steps for a specified mode.</summary>
        /// <param name="mode">The mode to get the steps for.</param>
        public List<SourceTargetRange> GetIDMigrationSteps(IDMigrationMode mode) => IDMigrationInfo[mode].Steps;

        /// <summary>Adds a new ID migration step to the currently selected mode's ranges.</summary>
        /// <param name="range">The ID migration step to add to the currently selected ID migration.</param>
        public void AddIDMigrationStep(SourceTargetRange range) => IDMigrationInfo.AddIDMigrationStep(range);
        /// <summary>Adds a range of new ID migration steps to the currently selected mode's ranges.</summary>
        /// <param name="ranges">The ID migration steps to add to the currently selected ID migration.</param>
        public void AddIDMigrationSteps(List<SourceTargetRange> ranges) => IDMigrationInfo.AddIDMigrationSteps(ranges);
        /// <summary>Removes a new ID migration step from the currently selected mode's ranges.</summary>
        /// <param name="range">The ID migration step to remove from the currently selected ID migration.</param>
        public void RemoveIDMigrationStep(SourceTargetRange range) => IDMigrationInfo.RemoveIDMigrationStep(range);
        /// <summary>Removes a range of new ID migration steps from the currently selected mode's ranges.</summary>
        /// <param name="ranges">The ID migration steps to remove from the currently selected ID migration.</param>
        public void RemoveIDMigrationSteps(List<SourceTargetRange> ranges) => IDMigrationInfo.RemoveIDMigrationSteps(ranges);
        // TODO: Add cloning method

        /// <summary>Saves the current ID migration steps to the associated file, if not <see langword="null"/> and returns whether the steps were saved.</summary>
        public bool SaveCurrentIDMigrationSteps()
        {
            bool hasAssociatedFile = CurrentlySelectedIDMigrationModeInfo.FileName != null;
            if (hasAssociatedFile)
                SaveCurrentIDMigrationSteps(CurrentlySelectedIDMigrationModeInfo.FileName, false);
            return hasAssociatedFile;
        }
        /// <summary>Saves the current ID migration steps to a specified file.</summary>
        /// <param name="fileName">The name of the file to save the current ID migration steps.</param>
        /// <param name="associateFile">Determines whether the file name will be associated with the currently selected ID migration steps.</param>
        public void SaveCurrentIDMigrationSteps(string fileName, bool associateFile = true)
        {
            SaveIDMigrationSteps(fileName, CurrentlySelectedIDMigrationSteps);
            if (associateFile)
                CurrentlySelectedIDMigrationModeInfo.FileName = fileName;
        }
        /// <summary>Saves the specified ID migration steps to a specified file.</summary>
        /// <param name="fileName">The name of the file to save the specified ID migration steps.</param>
        /// <param name="steps">The steps to save.</param>
        public void SaveIDMigrationSteps(string fileName, List<SourceTargetRange> steps) => File.WriteAllLines(fileName, ConvertRangesToStringArray(steps));
        /// <summary>Loads the ID migration steps from a specified file and replaces the currently selected ID migration steps with the loaded ones.</summary>
        /// <param name="fileName">The name of the file to load the ID migration steps from.</param>
        public void LoadIDMigrationSteps(string fileName)
        {
            CurrentlySelectedIDMigrationSteps = LoadRangesFromStringArray(File.ReadAllLines(fileName));
            IDMigrationInfo.CurrentlySelectedIDMigrationModeInfo.FileName = fileName;
        }

        // This was copied from a private feature code of EffectSome
        // TODO: Add undo/redo action after reworking the undo/redo system to use classes instead of functions
        /// <summary>Performs a group ID migration with the currently loaded ID migration steps for the group mode.</summary>
        public void PerformGroupIDMigration() => PerformGroupIDMigration(GroupSteps);
        /// <summary>Performs a group ID migration given the provided ID migration steps.</summary>
        /// <param name="ranges">The ID migration steps to execute to perform the group ID migration.</param>
        public void PerformGroupIDMigration(List<SourceTargetRange> ranges) => PerformIDMigration(ranges, AdjustGroups);
        /// <summary>Performs a color ID migration with the currently loaded ID migration steps for the color mode.</summary>
        public void PerformColorIDMigration() => PerformColorIDMigration(ColorSteps);
        /// <summary>Performs a color ID migration given the provided ID migration steps.</summary>
        /// <param name="ranges">The ID migration steps to execute to perform the color ID migration.</param>
        public void PerformColorIDMigration(List<SourceTargetRange> ranges) => PerformIDMigration(ranges, AdjustColors);
        /// <summary>Performs an item ID migration with the currently loaded ID migration steps for the item mode.</summary>
        public void PerformItemIDMigration() => PerformItemIDMigration(ItemSteps);
        /// <summary>Performs an item ID migration given the provided ID migration steps.</summary>
        /// <param name="ranges">The ID migration steps to execute to perform the item ID migration.</param>
        public void PerformItemIDMigration(List<SourceTargetRange> ranges) => PerformIDMigration(ranges, AdjustItems);
        /// <summary>Performs a block ID migration with the currently loaded ID migration steps for the block mode.</summary>
        public void PerformBlockIDMigration() => PerformBlockIDMigration(BlockSteps);
        /// <summary>Performs a block ID migration given the provided ID migration steps.</summary>
        /// <param name="ranges">The ID migration steps to execute to perform the block ID migration.</param>
        public void PerformBlockIDMigration(List<SourceTargetRange> ranges) => PerformIDMigration(ranges, AdjustBlocks);

        private void PerformIDMigration(List<SourceTargetRange> ranges, Action<GeneralObject, SourceTargetRange> adjustmentFunction)
        {
            IDMigrationOperationInitialized?.Invoke();
            for (int i = 0; i < ranges.Count; i++)
            {
                for (int j = 0; j < Level.LevelObjects.Count; j++)
                    adjustmentFunction(Level.LevelObjects[j], ranges[i]);
                IDMigrationProgressReported?.Invoke(i + 1, ranges.Count);
            }
            IDMigrationOperationCompleted?.Invoke();
        }

        // Perhaps experiment with creating a class that performs the adjustment using appropriate interfaces

        private void AdjustGroups(GeneralObject o, SourceTargetRange r)
        {
            int d = r.Difference;

            var groups = o.GroupIDs;
            if (groups != null)
                for (int g = 0; g < groups.Length; g++)
                    if (r.IsWithinSourceRange(groups[g]))
                        o.AdjustGroupID(g, d);

            if (o is IHasTargetGroupID t && r.IsWithinSourceRange(t.TargetGroupID))
                t.TargetGroupID += d;
            if (o is IHasSecondaryGroupID s && r.IsWithinSourceRange(s.SecondaryGroupID))
                s.SecondaryGroupID += d;
        }
        private void AdjustColors(GeneralObject o, SourceTargetRange r)
        {
            int d = r.Difference;

            if (r.IsWithinSourceRange(o.Color1ID))
                o.Color1ID += d;
            if (r.IsWithinSourceRange(o.Color2ID))
                o.Color2ID += d;

            if (o is IHasTargetColorID t && r.IsWithinSourceRange(t.TargetColorID))
                t.TargetColorID += d;
            if (o is IHasCopiedColorID c && r.IsWithinSourceRange(c.CopiedColorID))
                c.CopiedColorID += d;
        }
        private void AdjustItems(GeneralObject o, SourceTargetRange r)
        {
            int d = r.Difference;

            if (o is IHasPrimaryItemID p && r.IsWithinSourceRange(p.PrimaryItemID))
                p.PrimaryItemID += d;
            if (o is IHasTargetItemID t && r.IsWithinSourceRange(t.TargetItemID))
                t.TargetItemID += d;
        }
        private void AdjustBlocks(GeneralObject o, SourceTargetRange r)
        {
            int d = r.Difference;

            if (o is IHasPrimaryBlockID p && r.IsWithinSourceRange(p.PrimaryBlockID))
                p.PrimaryBlockID += d;
            if (o is IHasSecondaryBlockID s && r.IsWithinSourceRange(s.SecondaryBlockID))
                s.SecondaryBlockID += d;
        }
        #endregion

        #region Other
        /// <summary>Saves this level to the database.</summary>
        /// <param name="database">The database this level should be saved to.</param>
        /// <param name="index">The index at which the level will be saved in the database.</param>
        public void Save(Database database, int index)
        {
            database.UserLevels[index] = Level;
            database.WriteLevelData();
        }
        #endregion

        // TODO: Add functions to do lots of stuff

        #region Undo/Redo
        /// <summary>Undoes a number of actions from the level actions undo-redo system.</summary>
        /// <param name="count">The number of actions to undo.</param>
        public void UndoLevelActions(int count = 1) => Undo(levelActions, count);
        /// <summary>Redoes a number of actions from the level actions undo-redo system.</summary>
        /// <param name="count">The number of actions to redo.</param>
        public void RedoLevelActions(int count = 1) => Redo(levelActions, count);
        /// <summary>Undoes a number of actions from the editor actions undo-redo system.</summary>
        /// <param name="count">The number of actions to undo.</param>
        public void UndoEditorActions(int count = 1) => Undo(editorActions, count);
        /// <summary>Redoes a number of actions from the editor actions undo-redo system.</summary>
        /// <param name="count">The number of actions to redo.</param>
        public void RedoEditorActions(int count = 1) => Redo(editorActions, count);
        #endregion

        #region Property Setters
        /// <summary>Sets the value of Swipe. This function's purpose is to provide the option to not register the action as undoable.</summary>
        /// <param name="value">The value to set to Swipe.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetSwipe(bool value, bool registerUndoable = true) => Set(ref swipe, value, OnSwipeChanged, registerUndoable);
        /// <summary>Sets the value of Grid Snap. This function's purpose is to provide the option to not register the action as undoable.</summary>
        /// <param name="value">The value to set to Grid Snap.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetGridSnap(bool value, bool registerUndoable = true) => Set(ref gridSnap, value, OnGridSnapChanged, registerUndoable);
        /// <summary>Sets the value of Free Move. This function's purpose is to provide the option to not register the action as undoable.</summary>
        /// <param name="value">The value to set to Free Move.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetFreeMove(bool value, bool registerUndoable = true) => Set(ref freeMove, value, OnFreeMoveChanged, registerUndoable);
        /// <summary>Sets the value of Zoom. This function's purpose is to provide the option to not register the action as undoable.</summary>
        /// <param name="value">The value to set to Zoom.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetZoom(double value, bool registerUndoable = true) => Set(ref zoom, value, OnZoomChanged, registerUndoable);
        /// <summary>Sets the value of Grid Size. This function's purpose is to provide the option to not register the action as undoable.</summary>
        /// <param name="value">The value to set to Grid Size.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetGridSize(double value, bool registerUndoable = true) => Set(ref gridSize, value, OnGridSizeChanged, registerUndoable);
        /// <summary>Sets the value of Dual Layer Mode. This function's purpose is to provide the option to not register the action as undoable.</summary>
        /// <param name="value">The value to set to Dual Layer Mode.</param>
        /// <param name="registerUndoable">Determines whether the events will be invoked. Defaults to <see langword="true"/> and must be set to <see langword="false"/> during undo/redo to avoid endless invocation.</param>
        public void SetDualLayerMode(bool value, bool registerUndoable = true) => Set(ref dualLayerMode, value, OnDualLayerModeChanged, registerUndoable);
        #endregion

        #region Private Methods
        /// <summary>Gets the median point of all the selected objects.</summary>
        private Point GetMedianPoint()
        {
            Point result = new Point();
            foreach (var o in SelectedObjects)
                result += o.Location;
            return result / SelectedObjects.Count;
        }
        /// <summary>Gets the median point of the specified objects.</summary>
        private Point GetMedianPoint(LevelObjectCollection objects)
        {
            Point result = new Point();
            foreach (var o in objects)
                result += o.Location;
            return result / objects.Count;
        }
        private void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        private void Undo(UndoRedoSystem system, int count = 1) => system.Undo(count);
        private void Redo(UndoRedoSystem system, int count = 1) => system.Redo(count);

        private void Set(ref bool field, bool value, ValueChangedHandler<bool> handler, bool registerUndoable = true)
        {
            if (value == field)
                return;
            handler(field = value, registerUndoable);
        }
        private void Set(ref double field, double value, OldNewValueChangedHandler<double> handler, bool registerUndoable = true)
        {
            if (value == field)
                return;
            double old = field;
            handler(old, field = value, registerUndoable);
        }

        private static string GetAppropriateForm(int count, string thing) => $"{count} {thing}{(count != 1 ? "" : "s")}";
        #endregion
    }

    #region Delegates
    #region Editor Actions
    /// <summary>Represents a function that contains information about changing the state of Swipe.</summary>
    /// <param name="value">The new value of the Dual Layer Mode.</param>
    public delegate void SwipeChangedHandler(bool value);
    /// <summary>Represents a function that contains information about changing the state of Grid Snap.</summary>
    /// <param name="value">The new value of the Dual Layer Mode.</param>
    public delegate void GridSnapChangedHandler(bool value);
    /// <summary>Represents a function that contains information about changing the state of Free Move.</summary>
    /// <param name="value">The new value of the Dual Layer Mode.</param>
    public delegate void FreeMoveChangedHandler(bool value);
    /// <summary>Represents a function that contains information about changing the state of Dual Layer Mode.</summary>
    /// <param name="value">The new value of the Dual Layer Mode.</param>
    public delegate void DualLayerModeChangedHandler(bool value);
    /// <summary>Represents a function that contains information about changing the grid size.</summary>
    /// <param name="oldValue">The old value of the grid size.</param>
    /// <param name="newValue">The new value of the grid size.</param>
    public delegate void GridSizeChangedHandler(double oldValue, double newValue);
    /// <summary>Represents a function that contains information about changing the camera zoom.</summary>
    /// <param name="oldValue">The old value of the camera zoom.</param>
    /// <param name="newValue">The new value of the camera zoom.</param>
    public delegate void ZoomChangedHandler(double oldValue, double newValue);
    #endregion

    #region Level Actions
    /// <summary>Represents a function that contains information about deselecting all objects.</summary>
    /// <param name="previousSelection">The objects that were previously selected.</param>
    public delegate void AllObjectsDeselectedHandler(LevelObjectCollection previousSelection);
    /// <summary>Represents a function that contains information about an object movement action.</summary>
    /// <param name="objects">The objects that were moved.</param>
    /// <param name="offset">The offset of the movement function.</param>
    public delegate void MovedObjectsHandler(LevelObjectCollection objects, Point offset);
    /// <summary>Represents a function that contains information about an object rotation action.</summary>
    /// <param name="objects">The objects that were rotated.</param>
    /// <param name="offset">The offset of the rotation function.</param>
    /// <param name="centralPoint">The central point of the rotation (<see langword="null"/> to indicate an individual rotation).</param>
    public delegate void RotatedObjectsHandler(LevelObjectCollection objects, double offset, Point? centralPoint);
    /// <summary>Represents a function that contains information about an object scaling action.</summary>
    /// <param name="objects">The objects that were scaled.</param>
    /// <param name="scaling">The offset of the scaling function.</param>
    /// <param name="centralPoint">The central point of the scaling (<see langword="null"/> to indicate an individual scaling).</param>
    public delegate void ScaledObjectsHandler(LevelObjectCollection objects, double scaling, Point? centralPoint);
    /// <summary>Represents a function that contains information about a horizontal object flipping action.</summary>
    /// <param name="objects">The objects that were flipped horizontally.</param>
    /// <param name="centralPoint">The central point of the horizontal flipping (<see langword="null"/> to indicate an individual horizontal flipping).</param>
    public delegate void FlippedObjectsHorizontallyHandler(LevelObjectCollection objects, Point? centralPoint);
    /// <summary>Represents a function that contains information about a vertical object flipping action.</summary>
    /// <param name="objects">The objects that were flipped vertically.</param>
    /// <param name="centralPoint">The central point of the vertical flipping (<see langword="null"/> to indicate an individual vertical flipping).</param>
    public delegate void FlippedObjectsVerticallyHandler(LevelObjectCollection objects, Point? centralPoint);
    /// <summary>Represents a function that contains information about an object copy action.</summary>
    /// <param name="newObjects">The new objects that were copied.</param>
    /// <param name="oldObjects">The old copied objects.</param>
    public delegate void ObjectsCopiedHandler(LevelObjectCollection newObjects, LevelObjectCollection oldObjects);
    /// <summary>Represents a function that contains information about an object paste action.</summary>
    /// <param name="objects">The objects that were pasted.</param>
    /// <param name="centralPoint">The central point of the pasted objects.</param>
    public delegate void ObjectsPastedHandler(LevelObjectCollection objects, Point centralPoint);
    /// <summary>Represents a function that contains information about an object copy-paste action.</summary>
    /// <param name="newObjects">The new copies of the original objects.</param>
    /// <param name="oldObjects">The original objects that were copied.</param>
    public delegate void ObjectsCopyPastedHandler(LevelObjectCollection newObjects, LevelObjectCollection oldObjects);

    #region Color Channels
    // TODO: Consider implementing more handler delegates, events and On*Changed functions to support the ResetUnusedColors and potentially more functions
    /// <summary>Represents a function that contains information about a color ID change action including the affected objects.</summary>
    /// <param name="affectedObjects">The objects that were affected from this change.</param>
    /// <param name="colorID">The color ID that was changed.</param>
    /// <param name="oldColor">The old color of the color channel.</param>
    /// <param name="newColor">The new color of the color channel.</param>
    /// <param name="colorChannel">The color channel instance that was changed.</param>
    // TODO: Consider changing this in the future so that only one Color parameter is parsed indicating the difference between the two colors (would require a rework in the implementation of the Color)
    public delegate void ColorChangedHandler(LevelObjectCollection affectedObjects, int colorID, Color oldColor, Color newColor, ColorChannel colorChannel);
    /// <summary>Represents a function that contains information about a color ID blending change action including the affected objects.</summary>
    /// <param name="affectedObjects">The objects that were affected from this change.</param>
    /// <param name="colorID">The color ID that was changed.</param>
    /// <param name="colorChannel">The color channel instance that was changed.</param>
    public delegate void BlendingChangedHandler(LevelObjectCollection affectedObjects, int colorID, ColorChannel colorChannel);
    #endregion

    #region Groups
    /// <summary>Represents a function that contains information about a group ID addition action.</summary>
    /// <param name="objects">The objects whose group IDs were changed.</param>
    /// <param name="groupIDs">The group IDs that were added.</param>
    public delegate void GroupIDsAddedHandler(LevelObjectCollection objects, IEnumerable<int> groupIDs);
    /// <summary>Represents a function that contains information about a group ID removal action.</summary>
    /// <param name="objects">The objects whose group IDs were changed.</param>
    /// <param name="groupIDs">The group IDs that were removed.</param>
    public delegate void GroupIDsRemovedHandler(LevelObjectCollection objects, IEnumerable<int> groupIDs);
    #endregion

    #region Colors
    /// <summary>Represents a function that contains information about a main color ID change action including the affected objects.</summary>
    /// <param name="objects">The objects whose main Color ID was changed.</param>
    /// <param name="colorIDDifference">The color ID difference (new - old).</param>
    /// <param name="colorChannel">The color channel instance that contains the color information of the newly set main color ID.</param>
    public delegate void MainColorIDsChangedHandler(LevelObjectCollection objects, int colorIDDifference, ColorChannel colorChannel);
    /// <summary>Represents a function that contains information about a detail color ID change action including the affected objects.</summary>
    /// <param name="objects">The objects whose detail Color ID was changed.</param>
    /// <param name="colorIDDifference">The color ID difference (new - old).</param>
    /// <param name="colorChannel">The color channel instance that contains the color information of the newly set detail color ID.</param>
    public delegate void DetailColorIDsChangedHandler(LevelObjectCollection objects, int colorIDDifference, ColorChannel colorChannel);
    #endregion
    #endregion
    #endregion
}
