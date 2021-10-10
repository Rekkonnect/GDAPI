using GDAPI.Functions.Extensions;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace GDAPI.Functions.GeometryDash
{
    public static class Gamesave
    {
        public const string DefaultLevelString = "kS38,1_40_2_125_3_255_11_255_12_255_13_255_4_-1_6_1000_7_1_15_1_18_0_8_1|1_0_2_102_3_255_11_255_12_255_13_255_4_-1_6_1001_7_1_15_1_18_0_8_1|1_0_2_102_3_255_11_255_12_255_13_255_4_-1_6_1009_7_1_15_1_18_0_8_1|1_255_2_255_3_255_11_255_12_255_13_255_4_-1_6_1002_5_1_7_1_15_1_18_0_8_1|1_255_2_0_3_0_11_255_12_255_13_255_4_-1_6_1005_5_1_7_1_15_1_18_0_8_1|1_255_2_255_3_255_11_255_12_255_13_255_4_-1_6_1006_5_1_7_1_15_1_18_0_8_1|,kA13,0,kA15,0,kA16,0,kA14,,kA6,0,kA7,0,kA17,0,kA18,0,kS39,0,kA2,0,kA3,0,kA8,0,kA4,0,kA9,0,kA10,0,kA11,0;";
        public const string LevelDataStart = "<?xml version=\"1.0\"?><plist version=\"1.0\" gjver=\"2.0\"><dict><k>LLM_01</k><d><k>_isArr</k><t />";
        public const string LevelDataEnd = "</d><k>LLM_02</k><i>33</i></dict></plist>";
        public const string DefaultLevelData = LevelDataStart + LevelDataEnd;
        // TODO: Change to interpolated constant string once that feature is supported

        public static bool GetBoolKeyValue(string level, int key)
        {
            return level.Find($"<k>k{key}</k>") > -1;
        }
        public static int GetGuidelineStringStartIndex(string ls)
        {
            return ls.Find("kA14,") + 5;
        }
        public static string GetGuidelineString(string ls)
        {
            int guidelinesStartIndex = ls.Find("kA14,") + 5;
            int guidelinesEndIndex = ls.Find(",kA6");
            int guidelinesLength = guidelinesEndIndex - guidelinesStartIndex;
            return ls.Substring(guidelinesStartIndex, guidelinesLength);
        }

        public static string GetKeyValue(string level, int key, string valueType)
        {
            string startKeyString = $"<k>k{key}</k><{valueType}>";
            string endKeyString = $"</{valueType}>";

            int propertyStartIndex, propertyEndIndex, propertyLength;
            propertyStartIndex = level.Find(startKeyString, 0, level.Length - 1) + startKeyString.Length;
            propertyEndIndex = level.Find(endKeyString, propertyStartIndex, level.Length - 1);
            propertyLength = propertyEndIndex - propertyStartIndex;
            if (propertyStartIndex == startKeyString.Length - 1)
                throw new KeyNotFoundException();
            return level.Substring(propertyStartIndex, propertyLength);
        }
        public static string GetLevelString(string level)
        {
            try
            {
                return GetKeyValue(level, 4, "s");
            }
            catch (KeyNotFoundException)
            {
                return "";
            }
        }
        public static string GetObjectString(string ls)
        {
            try
            {
                return ls.Substring(ls.IndexOf(';') + 1);
            }
            catch
            {
                return "";
            }
        }
        public static string RemoveLevelIndexKey(string level)
        {
            string result = level;
            int keyIndex = result.Find("<k>k_");
            if (keyIndex > -1)
            {
                int endIndex = result.Find("</k>", keyIndex, result.Length) + 4;
                result = result.Remove(keyIndex, endIndex);
            }
            return result;
        }
        public static string TryGetKeyValue(string level, int key, string valueType, string defaultValueOnException)
        {
            string startKeyString = $"<k>k{key}</k><{valueType}>";
            string endKeyString = $"</{valueType}>";
            int parameterStartIndex = level.Find(startKeyString) + startKeyString.Length;
            int parameterEndIndex = level.Find(endKeyString, parameterStartIndex, level.Length - 1);
            int parameterLength = parameterEndIndex - parameterStartIndex;
            if (parameterStartIndex == startKeyString.Length - 1)
                return defaultValueOnException;
            else
                return level.Substring(parameterStartIndex, parameterLength);
        }
    }
}