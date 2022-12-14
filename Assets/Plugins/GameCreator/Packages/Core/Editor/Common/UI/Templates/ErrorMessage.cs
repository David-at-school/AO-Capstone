using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Common
{
    public sealed class ErrorMessage : VisualElement
    {
        private const string USS_PATH = EditorPaths.COMMON + "UI/StyleSheets/ErrorMessage";
        
        private static readonly IIcon ICON = new IconErrorOutline(ColorTheme.Type.TextLight);

        // MEMBERS: -------------------------------------------------------------------------------

        private readonly Image m_Image;
        private readonly Label m_Label;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public string Text
        {
            get => this.m_Label.text;
            set => this.m_Label.text = value;
        }
        
        public Texture Image
        {
            get => this.m_Image.image;
            set => this.m_Image.image = value;
        }
        
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ErrorMessage(string message)
        {
            StyleSheet[] sheets = StyleSheetUtils.Load(USS_PATH);
            foreach (StyleSheet sheet in sheets)
            {
                this.styleSheets.Add(sheet);
            }

            this.m_Image = new Image { image = ICON.Texture };
            this.m_Label = new Label { text = message };
            
            this.Add(this.m_Image);
            this.Add(this.m_Label);
        }
    }
}
