using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NovaEngine.Maths;
using NovaEngine.Rendering;
using System;
using System.IO;

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
        [JsonConverter(typeof(StringEnumConverter))]
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
                using (var streamReader = File.OpenText(SettingsFileName))
                using (var jsonReader = new JsonTextReader(streamReader))
                    try { instance = new JsonSerializer().Deserialize<RenderingSettings>(jsonReader); }
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
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            // serialise settings
            using (var streamWriter = new StreamWriter(SettingsFileName))
            using (var jsonWriter = new JsonTextWriter(streamWriter))
            {
                jsonWriter.Formatting = Formatting.Indented;
                new JsonSerializer().Serialize(jsonWriter, Instance);
            }
        }
    }
}
