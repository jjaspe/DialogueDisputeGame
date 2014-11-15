using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;
using System.ServiceModel;
using System.Xml;

namespace DisputeCommon
{
    public class PropertyData
    {
        String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        double value;

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public static List<PropertyData> fromDictionary(Dictionary<string, double> dic)
        {
            if (dic == null)
                return null;
            List<PropertyData> list = new List<PropertyData>();
            foreach (var v in dic)
                list.Add(new PropertyData() { name = v.Key, value = v.Value });
            return list;
        }
        public static Dictionary<string, double> toDictionary(List<PropertyData> list)
        {
            if (list == null)
                return null;
            Dictionary<string, double> dic = new Dictionary<string, double>();
            foreach (var v in list)
                dic.Add(v.name, v.value);
            return dic;
        }
    }

    [DataContract]
    public class CharacterData
    {        
        Dictionary<String, double> myAttributes;
        public Dictionary<String, double> MyAttributes
        {
            get { return myAttributes; }
            set { myAttributes = value; }
        }
        [DataMember]
        public List<PropertyData> Attributes
        {
            get { return PropertyData.fromDictionary(myAttributes); }
            set { myAttributes = PropertyData.toDictionary(value); }
        }

        Dictionary<String, double> myStats;
        public Dictionary<String, double> MyStats
        {
            get { return myStats; }
            set { myStats = value; }
        }
        [DataMember]
        public List<PropertyData> Stats
        {
            get { return PropertyData.fromDictionary(myStats); }
            set { myStats = PropertyData.toDictionary(value); }
        }

        Dictionary<String, double> mySkills;
        public Dictionary<String, double> MySkills
        {
            get { return mySkills; }
            set { mySkills = value; }
        }
        [DataMember]
        public List<PropertyData> Skills
        {
            get { return PropertyData.fromDictionary(mySkills); }
            set { mySkills = PropertyData.toDictionary(value); }
        }
        String name;

        [DataMember]
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public CharacterData(Dictionary<String, double> atts, Dictionary<String, double> stats,
            Dictionary<String,double> skills,string _name)
        {
            myAttributes = atts;
            myStats = stats;
            mySkills = skills;
            name = _name;
        }
        public CharacterData()
        {
            name = "";
            myAttributes = new Dictionary<string, double>();
            mySkills = new Dictionary<string, double>();
            myStats = new Dictionary<string, double>();
        }
        public Double getStat(string name)
        {
            try
            {
                return myStats.First(n => n.Key.Equals(name)).Value;
            }
            catch (Exception e)
            {
                return Double.NaN;
            }
        }

        public Double getSkill(string name)
        {

            try
            {
                return mySkills.First(n => n.Key.Equals(name)).Value;
            }
            catch (Exception e)
            {
                
                return Double.NaN;
            }
        }

        public Double getAttribute(string name)
        {
            try
            {
                return myAttributes.First(n => n.Key.Equals(name)).Value;
            }
            catch (Exception e)
            {
                
                return Double.NaN;
            }
        }

        public Double getValue(string name)
        {
            Double result = getStat(name);
            if (Double.IsNaN(result))
                result = getSkill(name);
            if (Double.IsNaN(result))
                result = getAttribute(name);
            return result;
        }

        /// <summary>
        /// If property is not found, return NaN
        /// </summary>
        /// <param name="character"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public double getProperty(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName))
                return Double.NaN;
            if (this.MyStats.ContainsKey(propertyName))
                return this.MyStats[propertyName];
            else if (this.MyAttributes.ContainsKey(propertyName))
                return this.MyAttributes[propertyName];
            else if (this.MySkills.ContainsKey(propertyName))
                return this.MySkills[propertyName];
            else
                return Double.NaN;
        }

        static public object[] createXmlSavingObjects(CharacterData character)
        {
            return createSavingData(character.name, character.MyStats, character.mySkills, character.MyAttributes);
        }
        /// <summary>
        /// Creates data needed to save a character. Returns and object[] array containing the objects needed
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stats"></param>
        /// <param name="skills"></param>
        /// <param name="atts"></param>
        /// <returns>In this order:
        ///             XMLNode containing character data
        ///             String with name of character
        ///             XmlDocument holding the document creator
        /// </returns>
        static public object[] createSavingData(string name, Dictionary<String, Double> stats, Dictionary<String, Double>                                               skills,
             Dictionary<String, Double> atts)
        {
            XmlDocument creator = new XmlDocument();

            XmlNode charNode = creator.CreateElement("Character"),
                statsNode = creator.CreateElement("Stats"), skillsNode = creator.CreateElement("Skills"),
                attributesNode = creator.CreateElement("Attributes"), currentNode;
            XmlElement valueElement, nameElement, charNameElement = creator.CreateElement("Name");
            charNameElement.InnerText = name;

            if (stats != null)
            {
                foreach (KeyValuePair<String, Double> stat in stats)
                {
                    currentNode = creator.CreateElement("Stat");
                    valueElement = creator.CreateElement("Value");
                    valueElement.InnerText = stat.Value.ToString();
                    nameElement = creator.CreateElement("Name");
                    nameElement.InnerText = stat.Key;
                    currentNode.AppendChild(nameElement);
                    currentNode.AppendChild(valueElement);
                    statsNode.AppendChild(currentNode.Clone());
                }
            }

            if (skills != null)
            {
                foreach (KeyValuePair<String, Double> skill in skills)
                {
                    currentNode = creator.CreateElement("Skill");
                    valueElement = creator.CreateElement("Value");
                    valueElement.InnerText = skill.Value.ToString();
                    nameElement = creator.CreateElement("Name");
                    nameElement.InnerText = skill.Key;
                    currentNode.AppendChild(nameElement);
                    currentNode.AppendChild(valueElement);
                    skillsNode.AppendChild(currentNode.Clone());
                }
            }

            if (atts != null)
            {
                foreach (KeyValuePair<String, Double> attribute in atts)
                {
                    currentNode = creator.CreateElement("Attribute");
                    valueElement = creator.CreateElement("Value");
                    valueElement.InnerText = attribute.Value.ToString();
                    nameElement = creator.CreateElement("Name");
                    nameElement.InnerText = attribute.Key;
                    currentNode.AppendChild(nameElement);
                    currentNode.AppendChild(valueElement);
                    attributesNode.AppendChild(currentNode.Clone());
                }
            }

            charNode.AppendChild(charNameElement);
            charNode.AppendChild(statsNode);
            charNode.AppendChild(skillsNode);
            charNode.AppendChild(attributesNode);

            return new object[] { charNode, name, creator };
        }

    }
}
