using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Licznik.Models;

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

                XmlElement Rnode = doc.CreateElement("R");
                Rnode.InnerText = c.r.ToString();
                counterNode.AppendChild(Rnode);

                XmlElement Gnode = doc.CreateElement("G");
                Gnode.InnerText = c.g.ToString();
                counterNode.AppendChild(Gnode);

                XmlElement Bnode = doc.CreateElement("B");
                Bnode.InnerText = c.b.ToString();
                counterNode.AppendChild(Bnode);

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
                        int r = 0, g = 0, b = 0;
                        int.TryParse(counterNode.SelectSingleNode("R")?.InnerText, out r);
                        int.TryParse(counterNode.SelectSingleNode("G")?.InnerText, out g);
                        int.TryParse(counterNode.SelectSingleNode("B")?.InnerText, out b);
                        counters.Add(new Counter(value, name, initialValue, r, g, b));
                    }
                }

                return (counters, index);
            }
            else
            {
                return (new List<Counter> { new Counter(0, "counter1", 0, 0, 255) }, 1);
            }
        }
    }
}
