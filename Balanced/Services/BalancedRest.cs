using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Balanced.Config;
using Balanced.Exceptions;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class BalancedRest
    {

        #region · Public Properties ·
        
       public string UserAgent
        {
            get
            {
                return string.Format("{0}/{1}", BalancedSettings.AcceptType, BalancedSettings.Version);
            }
        }

        #endregion
        
        #region · Public Methods ·

        public string Get(string uri)
        {
            return Request("GET", uri, null, null);
        }

        public string Get(string uri, NameValueCollection query)
        {
            return Request("GET", uri, query, null);
        }

        public string Post(string uri, JObject data)
        {
            return Request("POST", uri, null, data);
        }

        public string Put(string uri, JObject data)
        {
            return Request("PUT", uri, null, data);
        }

        public void Delete(string uri)
        {
            Request("DELETE", uri, null, null);
        }

        #endregion

        #region · Private Methods ·


        private string Request(string method, string relativeUri, NameValueCollection query, JObject data) 
        {
            var builder = new UriBuilder(BalancedSettings.BalancedUrl);

            if (query != null)
            {
                builder.Path = relativeUri;
                builder.Query = String.Join("&", (from object item in query select String.Concat(item, "=", HttpUtility.UrlEncode(query[item.ToString()]))).ToArray());
            }
            else
            {
                if (!relativeUri.Contains("?"))
                    builder.Path = relativeUri;
                else
                {
                    builder.Path = relativeUri.Substring(0, relativeUri.IndexOf('?'));
                    builder.Query = relativeUri.Substring(relativeUri.IndexOf('?'));
                }
            }
            var uri = builder.Uri;

            var req = (HttpWebRequest)WebRequest.Create(uri);
            req.UserAgent = UserAgent;
            req.Method = method;
            req.Accept = BalancedSettings.AcceptType;
            req.Headers.Add("Accept-Charset", BalancedSettings.Encoding.WebName);
            if (BalancedSettings.Secret != null) req.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(BalancedSettings.Secret + ":")));

            if (data != null)
            {
                req.ContentType = BalancedSettings.ContentType;
                byte[] content = BalancedSettings.Encoding.GetBytes(BalancedJsonSerializer.Serialize(data));
                req.ContentLength = content.Length;
                using (Stream stream = req.GetRequestStream())
                {
                    stream.Write(content, 0, content.Length);
                    stream.Close();
                }
            }

            try
            {
                using (var resp = req.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(resp.GetResponseStream()))
                    {
                        string body = reader.ReadToEnd();
                        if (body.Length == 0) return null;

                        return body;
                    }
                }
            }
            catch (WebException ex)
            {
                BalancedError error = CreateError(ex);  
                if (error == null)
                    throw;

                throw new BalancedException(error);
            }
        }



        private BalancedError CreateError(WebException ex)
        {
            if (ex.Status != WebExceptionStatus.ProtocolError || ex.Response == null)
            {
                return null;
            }
            var response = (HttpWebResponse)ex.Response;

            string body;
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    body = reader.ReadToEnd();
                }
            }
            if (response.ContentType != BalancedSettings.AcceptType || body.Length == 0) return null;

            if(body.Contains("errors"))
                return BalancedJsonSerializer.DeSerialize<BalancedError>(BalancedJsonSerializer.Serialize(JObject.Parse(body)["errors"][0]));
            
            return BalancedJsonSerializer.DeSerialize<BalancedError>(body);
        }

        #endregion

    }
}
