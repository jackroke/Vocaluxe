using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using Vocaluxe.Base;
using System.Collections;
using System.Xml;

namespace Vocaluxe.Menu
{
    class CThemeSettings : IMenuElement
    {
        private Dictionary<string,string> _ThemeSettings;
        private bool _ThemeLoaded;

        #region Constructors
        public CThemeSettings()
        {
            _ThemeLoaded = false;
            _ThemeSettings = new Dictionary<string, string>();
        }
        #endregion Constructors

        #region public
        public string GetValueOf(string key)
        {
            try
            {
                return _ThemeSettings[key];
            }
            catch (Exception)
            {
                CLog.LogError("Can't find Background Element \"" + key + "\" in Screen ");
                return "false";
            }
        }
        public bool LoadTheme(string XmlPath, string ElementName, XPathNavigator navigator, int SkinIndex)
        {
            try
            {
                _ThemeLoaded = true;
                if (navigator.MoveToFollowing("ThemeSettings", ""))
                {
                    if (navigator.HasChildren)
                    {
                        navigator.MoveToFirstChild();

                        string tag = String.Empty;
                        string value = String.Empty;
                        bool getNext = true;

                        while (getNext)
                        {
                            getNext &= CHelper.TryGetNextValueAndTagFromXML(navigator, ref tag, ref value);
                            _ThemeSettings[tag] = value;
                        }
                    }
                }   
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool SaveTheme(XmlWriter writer)
        {
            if (_ThemeLoaded)
            {
                writer.WriteStartElement("ThemeSettings");

                foreach(KeyValuePair<string,string> s in _ThemeSettings)
                {
                    if (!String.IsNullOrEmpty(s.Key) && !String.IsNullOrEmpty(s.Value))
                    {
                        writer.WriteComment("Value for <" + s.Key + ">");
                        writer.WriteElementString(s.Key, s.Value);
                    }
                }

                writer.WriteEndElement();
                return true;
            }
            return false;
        }

        public void UnloadTextures()
        {

        }

        public void LoadTextures()
        {
        }

        public void ReloadTextures() { }

        public void MoveElement(int stepX, int stepY) { }
        public void ResizeElement(int stepW, int stepH) { }

        #endregion public
    }
}
