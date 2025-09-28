using System;
using System.Reflection;
using ClientPlugin.Settings;
using ClientPlugin.Settings.Layouts;
using HarmonyLib;
using Sandbox.Graphics.GUI;
using VRage.Plugins;

namespace ClientPlugin
{
    // ReSharper disable once UnusedType.Global
    public class Plugin : IPlugin, IDisposable
    {
        
        private static Action<string, NLog.LogLevel> PulsarLog;
        
        public static readonly string PluginName = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(
            Assembly.GetExecutingAssembly(), typeof(AssemblyTitleAttribute))).Title;
        public static void WriteToPulsarLog(string logMsg, NLog.LogLevel logLevel)
        {
            PulsarLog?.Invoke($"[{PluginName}] {logMsg}", logLevel);
        }
        
        // ReSharper disable once MemberCanBePrivate.Global
        public static Plugin Instance { get; private set; }
        private SettingsGenerator settingsGenerator;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public void Init(object gameInstance)
        {
            Menu.Utils.CaptureOriginalOverlay();
            Instance = this;
            Instance.settingsGenerator = new SettingsGenerator();

            // Harmony harmony = new Harmony(PluginName);

            Config.Current.ChangeOverlayImage();
        }

        public void Dispose()
        {
            // TODO: Save state and close resources here, called when the game exits (not guaranteed!)
            // IMPORTANT: Do NOT call harmony.UnpatchAll() here! It may break other plugins.

            Instance = null;
        }

        public void Update()
        {
            // TODO: Put your update code here. It is called on every simulation frame!
        }

        // ReSharper disable once UnusedMember.Global
        public void OpenConfigDialog()
        {
            Instance.settingsGenerator.SetLayout<Simple>();
            MyGuiSandbox.AddScreen(Instance.settingsGenerator.Dialog);
        }

        //TODO: Uncomment and use this method to load asset files
        /*public void LoadAssets(string folder)
        {

        }*/
    }
}