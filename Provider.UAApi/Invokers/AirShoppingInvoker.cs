using Provider.UAApi.Schema;
using Provider.UAApi.Schema.AirShopping;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Provider.UAApi.Invokers
{

    public class AirShoppingInvoker
    {
        #region Static Fields

        /// <summary>
        /// The deserializer.
        /// </summary>
        private static readonly XmlSerializer deserializer = new XmlSerializer(typeof(AirShoppingRS), new XmlRootAttribute("AirShoppingRS"));

        #endregion

        #region Fields

        /// <summary>
        ///     The encoding.
        /// </summary>
        private readonly UTF8Encoding encoding = new UTF8Encoding();

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets Endpoint.
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        ///     Gets or sets Endpoint.
        /// </summary>
        public string TxHeader { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The invoke.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceListRS"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public AirShoppingRS Invoke(AirShoppingRQ request)
        {
            try
            {

                var reqXml = CreateFmsSvcSoapHeaderAndBodyRequest(request).Replace("\"", "'").Replace(@"xmlns='http://farelogix.com/flx/AirShoppingRQ'", string.Empty);
                var req = CreateWebRequest(@"https://stg.farelogix.com/xmlts/sandboxdm-wsdl");
                req.AutomaticDecompression = DecompressionMethods.GZip;

                using (var strWri = new StreamWriter(req.GetRequestStream()))
                {
                    strWri.WriteLine(reqXml);
                    strWri.Close();
                }

                var st = new Stopwatch();
                st.Start();

                using (var response = req.GetResponse() as HttpWebResponse)
                {

                    st.Stop();
                    var elapsedMilliseconds = st.ElapsedMilliseconds;
                 
                    if (response != null)
                    {
                        using (var strRead = new StreamReader(response.GetResponseStream()))
                        {
                            //var xDoc = XDocument.Load(strRead);

                            XmlDocument xDoc = new XmlDocument();
                            xDoc.Load(strRead);

                            var responseContent = Read(xDoc);// xDoc.Descendants("FlxTransactionResponse").First().FirstNode.ToString();

                            AirShoppingRS flxFmsResponse;
                            responseContent = responseContent.Replace("\"", "'").Replace(@"xmlns='http://farelogix.com/flx/AirShoppingRS'", string.Empty);
                            using (var reader = new StringReader(responseContent))// new StreamReader(stream))
                            {
                                var ss = reader.ReadToEnd();
                            }

                            using (var reader = new StringReader(responseContent))// new StreamReader(stream))
                            {
                                flxFmsResponse = (AirShoppingRS)deserializer.Deserialize(reader);
                            }

                            return flxFmsResponse;
                        }
                    }
                    else
                    {
                        throw new Exception("No response reveived");
                    }
                }
            }
            catch (WebException wex)
            {

                var failureReason = string.Empty;
                using (var httpResponse = wex.Response as HttpWebResponse)
                {

                    if (httpResponse != null)
                    {
                        using (var responseStream = httpResponse.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (var reader = new StreamReader(responseStream))
                                {
                                    failureReason = reader.ReadToEnd();

                                }
                            }
                        }
                    }
                }
                
                throw;
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return null;
        }

        public static string Read(XmlDocument xdoc)
        {
            var reader = new XmlNodeReader(xdoc);

            //Parse the file and display each of the nodes.
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "FlxTransactionResponse":
                        return reader.ReadInnerXml();
                }
            }

            return string.Empty;
        }
        public static HttpWebRequest CreateWebRequest(string endPoint)
        {
            var request = WebRequest.Create(endPoint) as HttpWebRequest;
            request.Timeout = 10000000;
            request.Method = "POST";
            return request;
        }

        public static string CreateFmsSvcSoapHeaderAndBodyRequest(object request)
        {
            var bodyXMl = GetXml(request);
            string soapTemplate = ConfigManagerUtil.FlxRequestTemplate;
            var reqXml = soapTemplate.Replace("${ReqBody}", bodyXMl);

          
            return reqXml;
        }
        private static XmlSerializer RequestXmlSerliazer = new XmlSerializer(typeof(AirShoppingRQ), new XmlRootAttribute("AirShoppingRQ"));
        public static string GetXml(object data)
        {
            string xml = string.Empty;

            using (var ms = new MemoryStream())
            {
                var settings = new XmlWriterSettings();
                settings.Encoding = Encoding.UTF8;
                settings.ConformanceLevel = ConformanceLevel.Document;
                settings.OmitXmlDeclaration = true;

                using (XmlWriter writer = XmlWriter.Create(ms, settings))
                {
                    RequestXmlSerliazer.Serialize(writer, data);
                }

                xml = Encoding.UTF8.GetString(ms.ToArray(), 0, ms.ToArray().Length);
            }

            return xml;
        }
        #endregion
    }
}
