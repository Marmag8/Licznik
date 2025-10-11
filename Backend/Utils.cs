using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Licznik.Backend
{
    internal class Utils
    {
        public static void ToXML(List<Counter> counters, int index)
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "licznik");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            String path = Path.Combine(dir, "counters.xml");

            if (File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
            }

            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(xmlDeclaration);

            XmlElement root = doc.CreateElement("Counters");
            doc.AppendChild(root);

            XmlElement indexNode = doc.CreateElement("Index");
            indexNode.InnerText = index.ToString();
            root.AppendChild(indexNode);

            foreach (Counter c in counters)
            {
                XmlElement counterNode = doc.CreateElement("Counter");

                XmlElement nameNode = doc.CreateElement("Name");
                nameNode.InnerText = c.name;
                counterNode.AppendChild(nameNode);

                XmlElement valueNode = doc.CreateElement("Value");
                valueNode.InnerText = c.count.ToString();
                counterNode.AppendChild(valueNode);

                XmlElement initialValueNode = doc.CreateElement("InitialValue");
                initialValueNode.InnerText = c.initialCount.ToString();
                counterNode.AppendChild(initialValueNode);

                root.AppendChild(counterNode);
            }

            doc.Save(path);
        }

        public static (List<Counter> counters, int index) FromXML()
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "licznik");
            string path = Path.Combine(dir, "counters.xml");

            if (File.Exists(path))
            {
                var counters = new List<Counter>();
                int index = 1;

                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlNode? root = doc.SelectSingleNode("Counters");
                if (root != null)
                {
                    XmlNode? indexNode = root.SelectSingleNode("Index");
                    if (indexNode != null && int.TryParse(indexNode.InnerText, out int parsedIndex))
                        index = parsedIndex;

                    foreach (XmlNode counterNode in root.SelectNodes("Counter")!)
                    {
                        string name = counterNode.SelectSingleNode("Name")?.InnerText ?? "counter1";
                        int value = 0;
                        int.TryParse(counterNode.SelectSingleNode("Value")?.InnerText, out value);
                        int initialValue = 0;
                        int.TryParse(counterNode.SelectSingleNode("InitialValue")?.InnerText, out initialValue);
                        counters.Add(new Counter(value, name, initialValue));
                    }
                }

                return (counters, index);
            }
            else
            {
                return (new List<Counter> { new Counter(0, "counter1") }, 1);
            }
        }
    }
}
