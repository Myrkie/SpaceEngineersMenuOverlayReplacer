using System;
using System.Reflection;
using HarmonyLib;
using NLog;
using Sandbox.Game.Gui;
using Sandbox.Graphics.GUI;
using SpaceEngineers.Game.GUI;

namespace ClientPlugin.Menu
{
    public class Utils
    {
        public static string OriginalImage { get; private set; }
        public static void ChangeImage(string customImage)
        {
            AccessTools.Field(typeof(MyGuiScreenIntroVideo), "m_videoOverlay").SetValue(null, customImage);
        }
        
        public static void CaptureOriginalOverlay()
        {
            if (OriginalImage != null) return; 
            
            Plugin.WriteToPulsarLog("Capturing original image", LogLevel.Info);
            Type introVideoType = typeof(MyGuiScreenIntroVideo);
            FieldInfo field = introVideoType.GetField("m_videoOverlay", BindingFlags.NonPublic | BindingFlags.Static);
            OriginalImage = (string)field.GetValue(null);
            Plugin.WriteToPulsarLog($"Captured image at string path {OriginalImage}", LogLevel.Info);
        }
    }
}