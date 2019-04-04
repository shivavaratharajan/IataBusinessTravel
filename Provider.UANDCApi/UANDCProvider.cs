using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Provider.NDCApi
{
    public class NDCProvider
    {
               public R Invoke<T,R>(string request, string url= @"http://iata.api.mashery.com/kronos/ndc172api" ) where R :class
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                    return default(R);

                var httprequest = WebRequest.Create(url) as HttpWebRequest;
                httprequest.Timeout = 90000;
                httprequest.Method = "POST";
               // httprequest.AutomaticDecompression = DecompressionMethods.GZip;
                //httprequest.Headers.Add("Content-Encoding", "gzip");
                httprequest.Headers.Add("Content-Type", "application/xml");
                httprequest.Headers.Add("Authorization-Key", "4ef9nwtw4demuu58a6cyvdp6");
                //Authorization-Key: 

                //using (var gz = new GZipStream(httprequest.GetRequestStream(), CompressionMode.Compress))
                //{
                //    var strWri = new StreamWriter(gz, Encoding.ASCII);
                //    strWri.WriteLine(request);
                //    strWri.Close();
                //}

                using (var requestWriter = new StreamWriter(httprequest.GetRequestStream()))
                {
                    requestWriter.Write(request);
                    requestWriter.Flush();
                }

                var st = new Stopwatch();
                st.Start();

                var response = httprequest.GetResponse() as HttpWebResponse;

                if (response != null)
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var strRead = new StreamReader(responseStream))
                            {
                                var responseText = new StringBuilder();

                                while (!strRead.EndOfStream)
                                {
                                    var line = strRead.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(line))
                                    {
                                        responseText.AppendLine(line);
                                    }
                                }

                                return XmlUtil.Deserialize<R>(responseText.ToString());
                            }
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                bool throwError = true;
                var httpResponse = wex.Response as HttpWebResponse;

                if (httpResponse != null)
                {
                    using (var responseStream = httpResponse.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                var failureReason = reader.ReadToEnd();
                                throw new Exception("Web Exception occurred," + failureReason, wex);
                            }
                        }
                    }
                }
                if (throwError)
                    throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
    }
}
