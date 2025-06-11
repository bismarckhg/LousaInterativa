using System;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms; // Though not directly used in final path, kept if needed later
using System.Drawing; // For AppSettings if it were used directly here

namespace LousaInterativa
{
    public static class SettingsManager
    {
        private static string SettingsFilePath { get; }

        static SettingsManager()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            // Using a generic company name placeholder as per plan, can be changed.
            string companyFolderPath = Path.Combine(appDataPath, "LousaInterativaCompany");
            string appFolderPath = Path.Combine(companyFolderPath, "LousaInterativaApp");

            // Ensure the directory exists; CreateDirectory handles nested creation.
            Directory.CreateDirectory(appFolderPath);

            SettingsFilePath = Path.Combine(appFolderPath, "settings.xml");
        }

        public static void SaveSettings(AppSettings settings)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                using (FileStream fs = new FileStream(SettingsFilePath, FileMode.Create))
                {
                    serializer.Serialize(fs, settings);
                }
            }
            catch (Exception ex)
            {
                // Optional: Log the exception (e.g., Console.WriteLine, or a proper logger)
                // For now, rethrow or handle as appropriate for the application design.
                // In many client apps, silently failing to save might be undesirable.
                // However, for this subtask, a simple propagation or console log is okay.
                Console.WriteLine($"Error saving settings: {ex.Message}");
                // Depending on requirements, might re-throw: throw;
            }
        }

        public static AppSettings LoadSettings()
        {
            if (!File.Exists(SettingsFilePath))
            {
                return new AppSettings(); // Return default settings if file doesn't exist
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                using (FileStream fs = new FileStream(SettingsFilePath, FileMode.Open))
                {
                    object deserializedObject = serializer.Deserialize(fs);
                    if (deserializedObject is AppSettings settings)
                    {
                        return settings;
                    }
                    else
                    {
                        // Log error: Unexpected type deserialized
                        Console.WriteLine("Error loading settings: Deserialized object is not of type AppSettings.");
                        return new AppSettings(); // Fallback to default
                    }
                }
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                Console.WriteLine($"Error loading settings: {ex.Message}. Returning default settings.");
                return new AppSettings(); // Fallback to default settings on error
            }
        }
    }
}
