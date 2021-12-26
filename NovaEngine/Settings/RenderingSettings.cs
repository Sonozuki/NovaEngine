using NovaEngine.Logging;
using NovaEngine.Rendering;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NovaEngine.Settings
{
    /// <summary>The application settings related to rendering.</summary>
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
            set => _SampleCount = (SampleCount)Math.Clamp((int)value, 0, (int)MaxSampleCount);
        }

        /// <summary>The singleton <see cref="RenderingSettings"/> instance.</summary>
        public static RenderingSettings Instance { get; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Initialises the class.</summary>
        static RenderingSettings()
        {
            RenderingSettings? instance = new();

            // deserialise file if it already exists
            if (File.Exists(Constants.RenderingSettingsFilePath))
            {
                try { instance = JsonSerializer.Deserialize<RenderingSettings>(File.ReadAllText(Constants.RenderingSettingsFilePath)); }
                catch { instance = null; }

                if (instance == null)
                {
                    Logger.LogError($"Failed to deserialise {nameof(RenderingSettings)}, reverting to default settings.");
                    Logger.LogError($"The invalid settings file has been moved to: \"{Constants.InvalidRenderingSettingsFilePath}\".");
                    File.Move(Constants.RenderingSettingsFilePath, Constants.InvalidRenderingSettingsFilePath, true);
                    instance = new();
                }
            }

            Instance = instance;

            Logger.LogDebug($"{nameof(RenderingSettings)}:\n{JsonSerializer.Serialize(Instance, new JsonSerializerOptions() { WriteIndented = true })}");

            // save settings, this adds any missing properties as well as creating the settings file if it doesn't exist
            Save();
        }

        /// <summary>Saves the rendering settings.</summary>
        public static void Save()
        {
            // ensure directory exists before attempting to reserialise settings
            var directoryName = new FileInfo(Constants.RenderingSettingsFilePath).DirectoryName!;
            Directory.CreateDirectory(directoryName);

            // serialise settings
            try { File.WriteAllText(Constants.RenderingSettingsFilePath, JsonSerializer.Serialize(Instance, new JsonSerializerOptions() { WriteIndented = true })); }
            catch { Logger.LogError($"Failed to serialise {nameof(RenderingSettings)}, settings won't persist between sessions."); }
        }
    }
}
