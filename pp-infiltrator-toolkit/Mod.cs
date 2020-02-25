using Harmony;
using PhoenixPointModLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace phoenix_point.mod.infiltrator_toolkit
{
    public class Mod : IPhoenixPointMod
    {
        private const string FILE_NAME = "pp-infiltrator-toolkit.properties";
        private const string CrossbowIsSilent = "CrossbowIsSilent";
        private static bool crossbowIsSilent;
        private static Dictionary<string, string> generationProperties = new Dictionary<string, string>();

        public ModLoadPriority Priority => ModLoadPriority.Normal;

        public void Initialize()
        {
            string manifestDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
               ?? throw new InvalidOperationException("Could not determine operating directory. Is your folder structure correct? " +
               "Try verifying game files in the Epic Games Launcher, if you're using it.");

            string filePath = manifestDirectory + "/" + FILE_NAME;
            if (File.Exists(filePath))
            {
                try
                {
                    foreach (string row in File.ReadAllLines(filePath))
                    {
                        if (row.StartsWith("#")) continue;
                        string[] data = row.Split('=');
                        if (data.Length == 2)
                        {
                            generationProperties.Add(data[0].Trim(), data[1].Trim());
                        }
                    }
                }
                catch (Exception e)
                {
                    FileLog.Log("Failed to read the configuration file (" + filePath + "): " + e.ToString());
                }
            }
            crossbowIsSilent = GetValue(CrossbowIsSilent, bool.Parse, true);

            HarmonyInstance.Create("phoenixpoint.InfiltratorToolkit").PatchAll(Assembly.GetExecutingAssembly());
        }

        public static T GetValue<T>(string key, Func<string, T> mapper, T defaultValue)
        {
            string propertyValue;
            if (generationProperties.TryGetValue(key, out propertyValue))
            {
                try
                {
                    return mapper.Invoke(propertyValue);
                }
                catch (Exception)
                {
                    FileLog.Log("Failed to parse value for key " + key + ": " + propertyValue);
                }
            }
            return defaultValue;
        }

        internal static bool IsCrossbowSilent()
        {
            return crossbowIsSilent;
        }
    }
}
