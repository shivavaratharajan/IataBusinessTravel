using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Provider.NDCApi
{
    public static class XmlUtil
    {
        #region Constants and Fields

        /// <summary>
        /// The serializers.
        /// </summary>
        private static readonly Dictionary<string, XmlSerializer> SerializerDictionary =
            new Dictionary<string, XmlSerializer>();

        /// <summary>
        /// The settings.
        /// </summary>
        private static readonly XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
        private static readonly XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="XmlUtil"/> class.
        /// </summary>
        static XmlUtil()
        {
            xmlWriterSettings.OmitXmlDeclaration = true;
            xmlWriterSettings.Encoding = Encoding.UTF8;
            xmlWriterSettings.Indent = false;
            xmlWriterSettings.NamespaceHandling = NamespaceHandling.OmitDuplicates;
            xmlSerializerNamespaces.Add("", "");
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get xml.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The get xml.
        /// </returns>
        public static string GetXml(object data)
        {
            string xml = string.Empty;

            var xmls = new XmlSerializer(data.GetType());
            using (var ms = new MemoryStream())
            {
                var settings = new XmlWriterSettings();
                settings.Encoding = Encoding.UTF8;
                settings.Indent = true;
                settings.IndentChars = "\t";
                settings.NewLineChars = Environment.NewLine;
                settings.ConformanceLevel = ConformanceLevel.Document;

                using (XmlWriter writer = XmlWriter.Create(ms, settings))
                {
                    xmls.Serialize(writer, data);
                }

                xml = Encoding.UTF8.GetString(ms.ToArray(), 0, ms.ToArray().Length);
            }

            return xml;
        }

        /// <summary>
        /// The convert request to xml string.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The convert request to xml string.
        /// </returns>
        public static string Serialize<T>(T input)
        {
            string xmlObject;
            string inputName = input.GetType().FullName;
            XmlSerializer xmlSerializer = null;

            //Lock is applied only for Dictionary modification. This may cause dirty read but the scope is very less
            if (!SerializerDictionary.TryGetValue(inputName, out xmlSerializer))
            {
                xmlSerializer = new XmlSerializer(input.GetType(), "");

                lock (SerializerDictionary)
                {
                    SerializerDictionary[inputName] = xmlSerializer;
                }
            }

            using (var ms = new MemoryStream())
            {

                using (var writer = new StreamWriter(ms))
                using (var xmlWriter = XmlWriter.Create(writer, xmlWriterSettings))
                {
                    xmlSerializer.Serialize(xmlWriter, input, xmlSerializerNamespaces);
                }

                // As by default stream writer uses UTF8Encoding we use the same to retrieve the serliazed xml as string
                xmlObject = new UTF8Encoding().GetString(ms.ToArray());
            }

            return xmlObject;
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <typeparam name="T">
        /// Type T
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T Deserialize<T>(string input)
                where T : class
        {
            var ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (var sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        /// <summary>
        /// The get xml string from object.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="nameSpace">
        /// The name space.
        /// </param>
        /// <returns>
        /// The get xml string from object.
        /// </returns>


        public static string GetXmlStringFromObject(object obj)
        {
            var writerSettings = new XmlWriterSettings();
            writerSettings.OmitXmlDeclaration = true;
            writerSettings.Indent = false;

            var serializer = new XmlSerializer(obj.GetType());

            using (var stringWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stringWriter, writerSettings))
                {
                    serializer.Serialize(writer, obj);

                    return stringWriter.ToString();
                }
            }
        }

        public static string getXMLStringFromObject(object obj, string nameSpace)
        {
            string res = "<?xml version='1.0' encoding='utf-8'?><Null />";
            string xmlStartTag = "?>";
            if (obj != null)
            {
                if (obj is string)
                {
                    res = obj.ToString();
                    if (!res.Contains("<?xml"))
                    {
                        res = string.Format("{0}\n{1}", "<?xml version='1.0' encoding='utf-8'?>", obj);
                    }
                }
                else
                {
                    var ns = new XmlSerializerNamespaces();
                    if (!string.IsNullOrEmpty(nameSpace))
                    {
                        ns.Add("acc", nameSpace);
                    }

                    var xSer = new XmlSerializer(obj.GetType());
                    using (var memStr = new MemoryStream())
                    {
                        using (var strWri = new XmlTextWriter(memStr, Encoding.UTF8))
                        {
                            xSer.Serialize(strWri, obj, ns);
                            res = UTF8ByteArrayToString(memStr.ToArray());

                            // System.Text.ASCIIEncoding.UTF8.GetString(memStr.ToArray());
                            if (res.Contains(xmlStartTag))
                            {
                                res = res.Substring(res.IndexOf(xmlStartTag) + xmlStartTag.Length);
                            }

                            strWri.Close();
                        }
                    }
                }
            }

            return res.Replace("<?xml version='1.0' encoding='utf-8'?>", string.Empty);
        }

        #endregion


        #region Methods

        /// <summary>
        /// The ut f 8 byte array to string.
        /// </summary>
        /// <param name="characters">
        /// The characters.
        /// </param>
        /// <returns>
        /// The ut f 8 byte array to string.
        /// </returns>
        private static string UTF8ByteArrayToString(byte[] characters)
        {
            var encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return constructedString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return mso.ToArray();
            }
        }
        #endregion
    }
}
