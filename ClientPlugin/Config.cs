using ClientPlugin.Settings;
using ClientPlugin.Settings.Elements;
using Sandbox.Graphics.GUI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ClientPlugin
{
    public class Config : INotifyPropertyChanged
    {
        #region Options

        private string _previewImage = "";

        #endregion

        #region User interface

        // TODO: Settings dialog title
        public readonly string Title = "Menu Overlay Replacer";

        [Separator("Settings")]

        [Textbox(description: "Path to custom preview image")]
        public string PreviewImage
        {
            get => _previewImage;
            set => SetField(ref _previewImage, value);
        }
        
        
        [Button(description: "Update Preview Image")]
        public void ChangeOverlayImage()
        {
            Menu.Utils.ChangeImage(Current.PreviewImage);
        }
        
        [Button(description: "Disable Preview Image")]
        public void DisableOverlayImage()
        {
            Menu.Utils.ChangeImage(string.Empty);
            Current.PreviewImage = string.Empty;
        }
        
        [Button(description: "Reset Preview Image")]
        public void ResetOverlayImage()
        {
            Menu.Utils.ChangeImage(Menu.Utils.OriginalImage);
            Current.PreviewImage = Menu.Utils.OriginalImage;
        }

        #endregion

        #region Property change notification boilerplate

        public static readonly Config Default = new Config();
        public static readonly Config Current = ConfigStorage.Load();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}