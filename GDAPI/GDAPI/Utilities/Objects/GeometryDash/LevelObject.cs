using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Functions.General;
using GDAPI.Utilities.Functions.GeometryDash;
using GDAPI.Utilities.Information.GeometryDash;
using static GDAPI.Utilities.Information.GeometryDash.LevelObjectInformation;
using static System.Convert;
using GDAPI.Utilities.Functions.Extensions;

namespace GDAPI.Utilities.Objects.GeometryDash
{
    public class LevelObject
    {
        // TODO: Remove this fucking shit when properties are completely and properly refactored.

        public object[] Parameters = new object[ParameterCount + 1];
        
        #region Constructors
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the Object ID parameter set to 1.</summary>
        public LevelObject() { this[ObjectParameter.ID] = 1; }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID) { this[ObjectParameter.ID] = objID; }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID and location.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID, double x, double y)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location and flipped values.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="flippedHorizontally">The flipped horizontally value of the <see cref="LevelObject"/>.</param>
        /// <param name="flippedVertically">The flipped vertically value of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID, double x, double y, bool flippedHorizontally, bool flippedVertically)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.FlippedHorizontally] = flippedHorizontally;
            this[ObjectParameter.FlippedVertically] = flippedVertically;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location and rotation.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="rotation">The rotation of the <see cref="LevelObject"/> in degrees. Positive is clockwise.</param>
        public LevelObject(int objID, double x, double y, double rotation)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.Rotation] = rotation;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location, rotation and scaling.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="rotation">The rotation of the <see cref="LevelObject"/> in degrees. Positive is clockwise.</param>
        /// <param name="sacling">The scaling ratio of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID, double x, double y, double rotation, double scaling)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.Rotation] = rotation;
            this[ObjectParameter.Scaling] = scaling;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location, rotation and flipped values.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="flippedHorizontally">The flipped horizontally value of the <see cref="LevelObject"/>.</param>
        /// <param name="flippedVertically">The flipped vertically value of the <see cref="LevelObject"/>.</param>
        /// <param name="rotation">The rotation of the <see cref="LevelObject"/> in degrees. Positive is clockwise.</param>
        public LevelObject(int objID, double x, double y, bool flippedHorizontally, bool flippedVertically, double rotation)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.FlippedHorizontally] = flippedHorizontally;
            this[ObjectParameter.FlippedVertically] = flippedVertically;
            this[ObjectParameter.Rotation] = rotation;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location, rotation, scaling and flipped values.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="flippedHorizontally">The flipped horizontally value of the <see cref="LevelObject"/>.</param>
        /// <param name="flippedVertically">The flipped vertically value of the <see cref="LevelObject"/>.</param>
        /// <param name="rotation">The rotation of the <see cref="LevelObject"/> in degrees. Positive is clockwise.</param>
        /// <param name="scaling">The scaling ratio of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID, double x, double y, bool flippedHorizontally, bool flippedVertically, double rotation, double scaling)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.FlippedHorizontally] = flippedHorizontally;
            this[ObjectParameter.FlippedVertically] = flippedVertically;
            this[ObjectParameter.Rotation] = rotation;
            this[ObjectParameter.Scaling] = scaling;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location and Editor Layer 1.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="EL1">The Editor Layer 1 of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID, double x, double y, int EL1)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.EL1] = EL1;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location and Editor Layers.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="EL1">The Editor Layer 1 of the <see cref="LevelObject"/>.</param>
        /// <param name="EL2">The Editor Layer 2 of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID, double x, double y, int EL1, int EL2)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.EL1] = EL1;
            this[ObjectParameter.EL2] = EL2;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location and Group IDs.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="groupIDs">The Group IDs of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID, double x, double y, int[] groupIDs)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.GroupIDs] = groupIDs;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location, Group IDs and Editor Layers.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="groupIDs">The Group IDs of the <see cref="LevelObject"/>.</param>
        /// <param name="EL1">The Editor Layer 1 of the <see cref="LevelObject"/>.</param>
        /// <param name="EL2">The Editor Layer 2 of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID, double x, double y, int[] groupIDs, int EL1, int EL2)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.GroupIDs] = groupIDs;
            this[ObjectParameter.EL1] = EL1;
            this[ObjectParameter.EL2] = EL2;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location, Color IDs, Group IDs and Editor Layers.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="mainColor">The Main Color ID of the <see cref="LevelObject"/>.</param>
        /// <param name="detailColor">The Detail Color ID of the <see cref="LevelObject"/>.</param>
        /// <param name="groupIDs">The Group IDs of the <see cref="LevelObject"/>.</param>
        /// <param name="EL1">The Editor Layer 1 of the <see cref="LevelObject"/>.</param>
        /// <param name="EL2">The Editor Layer 2 of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID, double x, double y, int mainColor, int detailColor, int[] groupIDs, int EL1, int EL2)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.Color1] = mainColor;
            this[ObjectParameter.Color2] = detailColor;
            this[ObjectParameter.GroupIDs] = groupIDs;
            this[ObjectParameter.EL1] = EL1;
            this[ObjectParameter.EL2] = EL2;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location, Color IDs, Group IDs, Editor Layers and glow attibute.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="mainColor">The Main Color ID of the <see cref="LevelObject"/>.</param>
        /// <param name="detailColor">The Detail Color ID of the <see cref="LevelObject"/>.</param>
        /// <param name="groupIDs">The Group IDs of the <see cref="LevelObject"/>.</param>
        /// <param name="EL1">The Editor Layer 1 of the <see cref="LevelObject"/>.</param>
        /// <param name="EL2">The Editor Layer 2 of the <see cref="LevelObject"/>.</param>
        /// <param name="disableGlow">The Disable Glow value of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID, double x, double y, int mainColor, int detailColor, int[] groupIDs, int EL1, int EL2, bool disableGlow)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.Color1] = mainColor;
            this[ObjectParameter.Color2] = detailColor;
            this[ObjectParameter.GroupIDs] = groupIDs;
            this[ObjectParameter.EL1] = EL1;
            this[ObjectParameter.EL2] = EL2;
            this[ObjectParameter.DisableGlow] = disableGlow;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location, Color IDs, Group IDs, Editor Layers and glow attibute, Z Order and Z Layer.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="mainColor">The Main Color ID of the <see cref="LevelObject"/>.</param>
        /// <param name="detailColor">The Detail Color ID of the <see cref="LevelObject"/>.</param>
        /// <param name="groupIDs">The Group IDs of the <see cref="LevelObject"/>.</param>
        /// <param name="EL1">The Editor Layer 1 of the <see cref="LevelObject"/>.</param>
        /// <param name="EL2">The Editor Layer 2 of the <see cref="LevelObject"/>.</param>
        /// <param name="disableGlow">The Disable Glow value of the <see cref="LevelObject"/>.</param>
        /// <param name="ZOrder">The Z Order value of the <see cref="LevelObject"/>.</param>
        /// <param name="ZLayer">The Z Layer value of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID, double x, double y, int mainColor, int detailColor, int[] groupIDs, int EL1, int EL2, bool disableGlow, int ZOrder, int ZLayer)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.Color1] = mainColor;
            this[ObjectParameter.Color2] = detailColor;
            this[ObjectParameter.GroupIDs] = groupIDs;
            this[ObjectParameter.EL1] = EL1;
            this[ObjectParameter.EL2] = EL2;
            this[ObjectParameter.DisableGlow] = disableGlow;
            this[ObjectParameter.ZOrder] = ZOrder;
            this[ObjectParameter.ZLayer] = ZLayer;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class with the specified Object ID, location, Color IDs, Group IDs, Editor Layers and glow attibute, Z Order and Z Layer.</summary>
        /// <param name="objID">The Object ID of the <see cref="LevelObject"/>.</param>
        /// <param name="x">The X position of the <see cref="LevelObject"/>.</param>
        /// <param name="y">The Y position of the <see cref="LevelObject"/>.</param>
        /// <param name="mainColor">The Main Color ID of the <see cref="LevelObject"/>.</param>
        /// <param name="detailColor">The Detail Color ID of the <see cref="LevelObject"/>.</param>
        /// <param name="groupIDs">The Group IDs of the <see cref="LevelObject"/>.</param>
        /// <param name="EL1">The Editor Layer 1 of the <see cref="LevelObject"/>.</param>
        /// <param name="EL2">The Editor Layer 2 of the <see cref="LevelObject"/>.</param>
        /// <param name="disableGlow">The Disable Glow value of the <see cref="LevelObject"/>.</param>
        /// <param name="ZOrder">The Z Order value of the <see cref="LevelObject"/>.</param>
        /// <param name="ZLayer">The Z Layer value of the <see cref="LevelObject"/>.</param>
        public LevelObject(int objID, double x, double y, int mainColor, int detailColor, int[] groupIDs, int EL1, int EL2, bool disableGlow, int ZOrder, ZLayer ZLayer)
        {
            this[ObjectParameter.ID] = objID;
            this[ObjectParameter.UnknownFeature36] = ObjectLists.TriggerList.Contains(objID);
            this[ObjectParameter.X] = x;
            this[ObjectParameter.Y] = y;
            this[ObjectParameter.Color1] = mainColor;
            this[ObjectParameter.Color2] = detailColor;
            this[ObjectParameter.GroupIDs] = groupIDs;
            this[ObjectParameter.EL1] = EL1;
            this[ObjectParameter.EL2] = EL2;
            this[ObjectParameter.DisableGlow] = disableGlow;
            this[ObjectParameter.ZOrder] = ZOrder;
            this[ObjectParameter.ZLayer] = (int)ZLayer;
        }
        /// <summary>Creates an instance of the <see cref="LevelObject"/> class from the specified object string. Most parameters will be stored with their <see cref="string"/> representation, so it is recommended to convert them to their appropriate form before usage.</summary>
        /// <param name="objectString">The object string of the <see cref="LevelObject"/>.</param>
        public LevelObject(string objectString)
        {
            string[] parameters = objectString.Split(',').RemoveEmptyElements();
            for (int i = 0; i < parameters.Length; i += 2)
            {
                int param = ToInt32(parameters[i]);
                if (param == (int)ObjectParameter.GroupIDs)
                    this.Parameters[param] = parameters[i + 1].Split('.').ToInt32Array();
                if (param == (int)ObjectParameter.Color1HSVValues || param == (int)ObjectParameter.Color2HSVValues || param == (int)ObjectParameter.CopiedColorHSVValues)
                {
                    string[] HSVParams = parameters[i + 1].Split('a');
                    this.Parameters[param] = new object[] { ToInt32(HSVParams[0]), ToDouble(HSVParams[1]), ToDouble(HSVParams[2]), HSVParams[3] == "1", HSVParams[4] == "1" };
                }
                else
                    this.Parameters[param] = parameters[i + 1]; // Parse the parameters as their string representations, the parameters should be converted to their appropriate format before usage to prevent errors.
            }
        }
        // Add more constructors
        #endregion

        /// <summary>Get a specific parameter of an object from its name.</summary>
        /// <param name="p">The parameter to get.</param>
        /// <returns>An object with the parameter that was requested.</returns>
        public object this[ObjectParameter p]
        {
            get => Parameters[(int)p];
            set { Parameters[(int)p] = value; }
        }

        public bool IsWithinRange(float startingX, float startingY, float endingX, float endingY)
        {
            var x = (float)this[ObjectParameter.X];
            var y = (float)this[ObjectParameter.Y];
            return startingX <= x && endingX >= x && startingY <= y && endingY <= y;
        }

        /// <summary>Converts the <see cref="LevelObject"/> to its string representation in the gamesave.</summary>
        public override string ToString()
        {
            string objectString = "";
            for (int i = 1; i < ParameterCount; i++)
                if (Parameters[i] != null)
                {
                    string parameter = "";
                    if (i > 3)
                    {
                        if (Parameters[i] is int)
                        {
                            if ((int)Parameters[i] != 0)
                                parameter += i + "," + Parameters[i] + ",";
                        }
                        if (Parameters[i] is double)
                        {
                            if ((double)Parameters[i] != 0)
                                parameter += i + "," + Parameters[i] + ",";
                        }
                        else if (Parameters[i] is string)
                            parameter += i + "," + Parameters[i] + ",";
                        else if (Parameters[i] is bool)
                            parameter += i + "," + ToInt32(Parameters[i]) + ",";
                        else if (i == (int)ObjectParameter.GroupIDs)
                        {
                            if ((Parameters[i] as int[]).Length > 0)
                            {
                                parameter += i + ",";
                                for (int j = 0; j < (Parameters[i] as int[]).Length; j++)
                                    parameter += (Parameters[i] as int[])[j] + ".";
                                parameter = parameter.Remove(parameter.Length - 1);
                                parameter += ",";
                            }
                        }
                        else if (i == (int)ObjectParameter.Color1HSVValues || i == (int)ObjectParameter.Color2HSVValues || i == (int)ObjectParameter.CopiedColorHSVValues)
                        {
                            parameter += i + ",";
                            for (int j = 0; j < (Parameters[i] as Array).Length; j++)
                                parameter += ToDouble((Parameters[i] as object[])[j]) + "a";
                            parameter = parameter.Remove(parameter.Length - 1);
                            parameter += ",";
                        }
                    }
                    else
                        parameter += i + "," + Parameters[i] + ",";
                    objectString += parameter;
                }
            if (objectString.Length > 0)
                objectString = objectString.Remove(objectString.Length - 1);
            return objectString;
        }

        public static string GetObjectString(List<LevelObject> l)
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < l.Count; i++)
                s.Append(l[i].ToString() + ";");
            return s.ToString();
        }
    }
}