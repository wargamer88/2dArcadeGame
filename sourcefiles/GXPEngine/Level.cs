using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GXPEngine
{
    class Level : GameObject
    {
        #region local class variables

        int _levelWidth;
        int _levelHeight;

        #endregion

        public Level(string sLevel)
        {
            string level = XMLreader(sLevel);
            //int[,] levelArray = LevelArrayBuilder(level);
            //BuildGameLevel(levelArray);
        }

        public string XMLreader(string slevel)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"levels\" + slevel);

            XmlElement root = doc.DocumentElement;
            if (root.HasAttribute("width"))
            {
                _levelWidth = Convert.ToInt16(root.GetAttribute("width"));
            }
            if (root.HasAttribute("height"))
            {
                _levelHeight = Convert.ToInt16(root.GetAttribute("height"));
            }

            string level = "";

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
        {
                level = node.InnerText;
            }

            return level;

        }
    }
}
