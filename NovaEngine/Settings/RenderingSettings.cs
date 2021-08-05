using NovaEngine.Logging;
using NovaEngine.Maths;
using NovaEngine.Rendering;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NovaEngine.Settings
{
    /// <summary>The application settings related to rendering.</summary>
    /// <remarks>These are automatically saved to/loaded from the file: 'Documents/My Games/[Application Name]/Settings/RenderingSettings.json'.</remarks>
    public class RenderingSettings
    {
        /*********
        ** Fields
        *********/
        /// <summary>The current number of samples per pixel to use in multisample anti aliasing (MSAA).</summary>
        private SampleCount _SampleCount = SampleCount.Count8;


        /*********
        ** Accessors
        *********/
        /// <summary>The maximum number of samples per pixel that can be used in multisample anti aliasing (MSAA).</summary>
        [JsonIgnore]
        public SampleCount MaxSampleCount => RendererManager.CurrentRenderer.MaxSampleCount;

        /// <summary>The current number of samples per pixel to use in multisample anti aliasing (MSAA).</summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SampleCount SampleCount
        {
            get => _SampleCount;
            set => _SampleCount = (SampleCount)MathsHelper.Clamp((int)value, 0, (int)MaxSampleCount);
        }

        /// <summary>The singleton <see cref="RenderingSettings"/> instance.</summary>
        public static RenderingSettings Instance { get; }

        /// <summary>The settings file.</summary>
        private static string SettingsFileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", Program.Name, "Settings", "RenderingSettings.json");


        /*********
        ** Public Methods
        *********/
        /// <summary>Initialises the class.</summary>
        static RenderingSettings()
        {
            RenderingSettings? instance = new();

            // deserialise file if it already exists
            if (File.Exists(SettingsFileName))
            {
                try { instance = JsonSerializer.Deserialize<RenderingSettings>(File.ReadAllText(SettingsFileName)); }
                catch { instance = null; }

                if (instance == null)
                {
                    Console.WriteLine($"Failed to deserialise {nameof(RenderingSettings)}, reverting to default settings.");
                    instance = new();
                }
            }

            Instance = instance;

            // save settings, this adds any missing properties as well as creating the settings file if it doesn't exist
            Save();
        }

        /// <summary>Saves the rendering settings.</summary>
        public static void Save()
        {
            // ensure directory exists before attempting to reserialise settings
            var directoryName = new FileInfo(SettingsFileName).DirectoryName!;
            Directory.CreateDirectory(directoryName);

            // serialise settings
            try { File.WriteAllText(SettingsFileName, JsonSerializer.Serialize(Instance, new() { WriteIndented = true })); }
            catch { Logger.Log($"Failed to serialise {nameof(RenderingSettings)}, settings won't persist between sessions.", LogSeverity.Error); }
        }
    }
}
