using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Functions.Extensions;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects;
using static GDAPI.Information.GeometryDash.LevelObjectInformation;
using static System.Convert;

namespace GDAPI.Functions.GeometryDash
{
    public static class Gamesave
    {
        private const string DefaultLevelString = "kS38,1_40_2_125_3_255_11_255_12_255_13_255_4_-1_6_1000_7_1_15_1_18_0_8_1|1_0_2_102_3_255_11_255_12_255_13_255_4_-1_6_1001_7_1_15_1_18_0_8_1|1_0_2_102_3_255_11_255_12_255_13_255_4_-1_6_1009_7_1_15_1_18_0_8_1|1_255_2_255_3_255_11_255_12_255_13_255_4_-1_6_1002_5_1_7_1_15_1_18_0_8_1|1_255_2_0_3_0_11_255_12_255_13_255_4_-1_6_1005_5_1_7_1_15_1_18_0_8_1|1_255_2_255_3_255_11_255_12_255_13_255_4_-1_6_1006_5_1_7_1_15_1_18_0_8_1|,kA13,0,kA15,0,kA16,0,kA14,,kA6,0,kA7,0,kA17,0,kA18,0,kS39,0,kA2,0,kA3,0,kA8,0,kA4,0,kA9,0,kA10,0,kA11,0;";
        public const string LevelDataStart = "<?xml version=\"1.0\"?><plist version=\"1.0\" gjver=\"2.0\"><dict><k>LLM_01</k><d><k>_isArr</k><t />";
        public const string LevelDataEnd = "</d><k>LLM_02</k><i>33</i></dict></plist>";
        public const string DefaultLevelData = "<?xml version=\"1.0\"?><plist version=\"1.0\" gjver=\"2.0\"><dict><k>LLM_01</k><d><k>_isArr</k><t /></d><k>LLM_02</k><i>33</i></dict></plist>";

        /// <summary>Returns the decrypted version of the gamesave after checking whether the gamesave is encrypted or not. Returns true if the gamesave is encrypted; otherwise false.</summary>
        /// <param name="decrypted">The string to return the decrypted gamesave.</param>
        /// <returns>Returns true if the gamesave is encrypted; otherwise false.</returns>
        public static bool TryDecryptGamesave(string gamesave, out string decrypted)
        {
            bool isEncrypted = CheckIfGamesaveIsEncrypted(gamesave);
            decrypted = isEncrypted ? GDGamesaveDecrypt(gamesave) : gamesave;
            return isEncrypted;
        }
        /// <summary>Returns the decrypted version of the gamesave after checking whether the gamesave is encrypted or not asynchronously. Returns a tuple containing the result of the operation.</summary>
        /// <param name="gamesave">The original gamesave string to try to decrypt.</param>
        /// <returns>Returns a (bool, string), where the bool is equal to true if the gamesave is encrypted; otherwise false, and the string is equal to the final decrypted form of the gamesave.</returns>
        public static async Task<(bool, string)> TryDecryptGamesaveAsync(string gamesave)
        {
            bool isEncrypted = CheckIfGamesaveIsEncrypted(gamesave);
            string decrypted = isEncrypted ? GDGamesaveDecrypt(gamesave) : gamesave;
            return (isEncrypted, decrypted);
        }
        public static bool CheckIfGamesaveIsEncrypted(string gamesave)
        {
            int checks = 0;
            string[] tests = { "<k>bgVolume</k>", "<k>sfxVolume</k>", "<k>playerUDID</k>", "<k>playerName</k>", "<k>playerUserID</k>" };
            for (int i = 0; i < tests.Length; i++)
                if (gamesave.Contains(tests[i]))
                    checks++;
            return checks != tests.Length;
        }

        /// <summary>Returns the decrypted version of the level data after checking whether the level data is encrypted or not. Returns true if the level data is encrypted; otherwise false.</summary>
        /// <param name="decrypted">The string to return the decrypted level data.</param>
        /// <returns>Returns true if the level data is encrypted; otherwise false.</returns>
        public static bool TryDecryptLevelData(string levelData, out string decrypted)
        {
            bool isEncrypted = CheckIfLevelDataIsEncrypted(levelData);
            decrypted = isEncrypted ? GDGamesaveDecrypt(levelData) : levelData;
            return isEncrypted;
        }
        /// <summary>Returns the decrypted version of the level data after checking whether the level data is encrypted or not asynchronously. Returns a tuple containing the result of the operation.</summary>
        /// <param name="levelData">The original level data string to try to decrypt.</param>
        /// <returns>Returns a (bool, string), where the bool is equal to true if the level data is encrypted; otherwise false, and the string is equal to the final decrypted form of the level data.</returns>
        public static async Task<(bool, string)> TryDecryptLevelDataAsync(string levelData)
        {
            bool isEncrypted = CheckIfLevelDataIsEncrypted(levelData);
            string decrypted = isEncrypted ? GDGamesaveDecrypt(levelData) : levelData;
            return (isEncrypted, decrypted);
        }
        public static bool CheckIfLevelDataIsEncrypted(string levelData)
        {
            string test = "<?xml version=\"1.0\"?><plist version=\"1.0\" gjver=\"2.1\">";
            return !levelData.Contains(test);
        }

        /// <summary>Returns the decrypted version of the level string after checking whether the level string is encrypted or not. Returns true if the level string is encrypted; otherwise false.</summary>
        /// <param name="ls">The level string to try decrypting. If it is already encrypted, it will be returned in the <paramref name="decrypted"/> parameter.</param>
        /// <param name="decrypted">The string to return the decrypted level string.</param>
        /// <returns>Returns true if the level string is encrypted; otherwise false.</returns>
        public static bool TryDecryptLevelString(string ls, out string decrypted)
        {
            bool isEncrypted = true != CheckIfLevelStringIsEncrypted(ls);
            return isEncrypted;
        }
        /// <summary>Returns the decrypted version of the level string after checking whether the level string is encrypted or not asynchronously. Returns a tuple containing the result of the operation.</summary>
        /// <param name="ls">The original level string to try to decrypt</param>
        /// <returns>Returns a (bool, string), where the bool is equal to true if the level data is encrypted; otherwise false, and the string is equal to the final decrypted form of the level data.</returns>
        public static async Task<(bool, string)> TryDecryptLevelStringAsync(string ls)
        {
            bool isEncrypted = CheckIfLevelStringIsEncrypted(ls);
            string decrypted = isEncrypted ? DecryptLevelString(ls) : ls;
            return (isEncrypted, decrypted);
        }
        public static bool CheckIfLevelStringIsEncrypted(string ls)
        {
            int checks = 0;
            string[] tests = { "kA13,", "kA15,", "kA16,", "kA14,", "kA6," };
            for (int i = 0; i < tests.Length; i++)
                if (ls.Contains(tests[i]))
                    checks++;
            return checks != tests.Length;
        }
        public static string DecryptLevelString(string ls)
        {
            return GDLevelStringDecrypt(ls);
        }

        public static LevelObjectCollection GetObjects(string objectString)
        {
            List<GeneralObject> objects = new List<GeneralObject>();
            while (objectString.Length > 0 && objectString[objectString.Length - 1] == ';')
                objectString = objectString.Remove(objectString.Length - 1);
            if (objectString.Length > 0)
            {
                string[][] objectProperties = objectString.Split(';').SplitAsJagged(',');
                for (int i = 0; i < objectProperties.Length; i++)
                {
                    try
                    {
                        var objectInfo = objectProperties[i];
                        var instance = GeneralObject.GetNewObjectInstance(ToInt16(objectInfo[1]));
                        objects.Add(instance); // Get IDs of the selected objects
                        for (int j = 3; j < objectInfo.Length; j += 2)
                        {
                            try
                            {
                                int propertyID = ToInt32(objectInfo[j - 1]);
                                switch (GetPropertyIDAttribute(propertyID))
                                {
                                    case IGenericAttribute<int> _:
                                        instance.SetPropertyWithID(propertyID, ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<bool> _:
                                        instance.SetPropertyWithID(propertyID, ToBoolean(ToInt32(objectInfo[j])));
                                        break;
                                    case IGenericAttribute<double> _:
                                        instance.SetPropertyWithID(propertyID, ToDouble(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<string> _:
                                        instance.SetPropertyWithID(propertyID, objectInfo[j]);
                                        break;
                                    case IGenericAttribute<HSVAdjustment> _:
                                        instance.SetPropertyWithID(propertyID, objectInfo[j].ToString());
                                        break;
                                    case IGenericAttribute<int[]> _:
                                        instance.SetPropertyWithID(propertyID, objectInfo[j].ToString().Split('.').ToInt32Array());
                                        break;
                                    case IGenericAttribute<Easing> _:
                                        instance.SetPropertyWithID(propertyID, (Easing)ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<InstantCountComparison> _:
                                        instance.SetPropertyWithID(propertyID, (InstantCountComparison)ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<PickupItemPickupMode> _:
                                        instance.SetPropertyWithID(propertyID, (PickupItemPickupMode)ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<PulseMode> _:
                                        instance.SetPropertyWithID(propertyID, (PulseMode)ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<PulseTargetType> _:
                                        instance.SetPropertyWithID(propertyID, (PulseTargetType)ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<TargetPosCoordinates> _:
                                        instance.SetPropertyWithID(propertyID, (TargetPosCoordinates)ToInt32(objectInfo[j]));
                                        break;
                                    case IGenericAttribute<TouchToggleMode> _:
                                        instance.SetPropertyWithID(propertyID, (TouchToggleMode)ToInt32(objectInfo[j]));
                                        break;
                                }
                            }
                            catch (FormatException) // If the property is not just a number; most likely a Start Pos object
                            {
                                // After logging the exceptions in the console, the exception is ignorable
                            }
                            catch (KeyNotFoundException e)
                            {
                                int propertyID = ToInt32(objectInfo[j - 1]);
                                if (propertyID == 36)
                                    continue;
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        // So far this only happens when attempting to abstractly create a yellow teleportation portal
                    }
                }
                objectProperties = null;
            }
            return new LevelObjectCollection(objects);
        }
        public static GeneralObject GetCommonAttributes(LevelObjectCollection list, short objectID)
        {
            GeneralObject common = GeneralObject.GetNewObjectInstance(objectID);

            for (int i = list.Count; i >= 0; i--)
                if (list[i].ObjectID != objectID)
                    list.RemoveAt(i);
            if (list.Count > 1)
            {
                var properties = common.GetType().GetProperties();
                foreach (var p in properties)
                    if (Attribute.GetCustomAttributes(p, typeof(ObjectStringMappableAttribute), false).Count() > 0)
                    {
                        var v = p.GetValue(list[0]);
                        bool isCommon = true;
                        foreach (var o in list)
                            if (isCommon = (p.GetValue(o) != v))
                                break;
                        if (isCommon)
                            p.SetValue(common, v);
                    }
                return common;
            }
            else if (list.Count == 1)
                return list[0];
            else
                return null;
        }
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
                return ls.Split(';').RemoveAt(0).Combine(";");
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
        
        public static string GetData(string path)
        {
            StreamReader sr = new StreamReader(path);
            string readfile = sr.ReadToEnd();
            sr.Close();
            return readfile;
        }

        public static byte[] Base64Decrypt(string encodedData)
        {
            while (encodedData.Length % 4 != 0)
                encodedData += "=";
            byte[] encodedDataAsBytes = FromBase64String(encodedData.Replace('-', '+').Replace('_', '/').Replace("\0", string.Empty));
            return encodedDataAsBytes;
        }
        public static string Base64Encrypt(byte[] decryptedData)
        {
            return ToBase64String(decryptedData);
        }
        public static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                var buffer = new byte[4096];
                int read;

                while ((read = zipStream.Read(buffer, 0, buffer.Length)) > 0)
                    resultStream.Write(buffer, 0, read);

                return resultStream.ToArray();
            }
        }
        public static byte[] Compress(byte[] data)
        {
            using (var decompressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(decompressedStream, CompressionMode.Compress))
            using (var resultStream = new MemoryStream())
            {
                var buffer = new byte[4096];
                int read;

                while ((read = zipStream.Read(buffer, 0, buffer.Length)) > 0)
                    resultStream.Write(buffer, 0, read);

                return resultStream.ToArray();
            }
        }
        public static string XORDecrypt(string text, int key)
        {
            byte[] result = new byte[text.Length];
            for (int c = 0; c < text.Length; c++)
                result[c] = (byte)((uint)text[c] ^ key);
            string dexored = Encoding.UTF8.GetString(result);
            return dexored;
        }
        public static string XOR11Decrypt(string text)
        {
            byte[] result = new byte[text.Length];
            for (int c = 0; c < text.Length - 1; c++)
                result[c] = (byte)((uint)text[c] ^ 11);
            string dexored = Encoding.UTF8.GetString(result);
            return dexored;
        }
        public static string GDGamesaveDecrypt(string data)
        {
            string xored = XOR11Decrypt(data); // Decrypt XOR ^ 11
            string replaced = xored.Replace('-', '+').Replace('_', '/').Replace("\0", string.Empty); // Replace characters
            byte[] gzipb64 = Decompress(Base64Decrypt(replaced)); // Decompress
            string result = Encoding.ASCII.GetString(gzipb64); // Change to string
            return result;
        }
        public static string GDLevelStringDecrypt(string ls)
        {
            string replaced = ls.Replace('-', '+').Replace('_', '/').Replace("\0", string.Empty); // Replace characters
            //string fixedBase64 = replaced.FixBase64String();
            byte[] gzipb64 = Base64Decrypt(replaced);
            if (replaced.StartsWith("H4sIAAAAAAAA"))
                gzipb64 = Decompress(gzipb64); // Decompress
            else throw new ArgumentException("The level string is not valid.");
            string result = Encoding.ASCII.GetString(gzipb64); // Change to string
            return result;
        }
    }
}
