using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLSerialization
{
    static class ButtonActions
    {
        public static void SaveInXML(ref Player player, string fileName)
        {
            string fullPath = @"XMLSave\" + fileName + ".xml";
            //fullPath += DateTime.Now.ToString() + ".xml";
            //fullPath = fileName.Replace('/', '_');
            //fullPath = fileName.Replace(':', '_');
            //fullPath = fileName.Replace(' ', '_');
            FileStream writeStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None); // Create - переписывает файл автомвтически, не CreateNew
            XmlSerializer  XML = new XmlSerializer(typeof(Player));
            XML.Serialize(writeStream, player);


            writeStream.Close();
        }

        public static void LoadFromXML(ref Player player, string fileName)
        {
            string fullPath = @"XMLSave\" + fileName + ".xml";
            FileStream readStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.None);
            XmlSerializer XML = new XmlSerializer(typeof(Player));
            var obj = (Player)XML.Deserialize(readStream);

            player.Position = obj.Position;
            player.CurrentSpriteID = obj.CurrentSpriteID;
            player.IsMoveing = obj.IsMoveing;


            readStream.Close();
        }
    }
}
