using System;
using System.IO;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using log4net;

namespace GU.Net
{
	/// <summary>
	/// Contains helper methods for making Simple Http Requests
	/// </summary>
	public sealed class HttpRequestHelper
	{
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private HttpRequestHelper()
		{}

		/// <summary>
		/// Makes a simple HTTP request to the specified URL using the 
		/// method provided and passing the specified data.  Returns a 
		/// string containing the response.
		/// </summary>
		/// <remarks>
		/// Method typically will be either "POST" or "GET".  In the case of 
		/// "GET", data is passed on the query string as name/value paires.
		/// In the case of POST, the data posted in the form data headers.
		/// </remarks>
		/// <param name="url">Url to make the request</param>
		/// <param name="method"></param>
		/// <param name="data"></param>
		/// <returns>The reply from the web server.</returns>
		public static string GetResponseText(Uri url, string method, NameValueCollection data)
		{
			using(Stream responseStream = GetResponseStream(url, method, data))
			{
				using(StreamReader reader = new StreamReader(responseStream))
				{
					return reader.ReadToEnd();
				}
			}
		}		

		/// <summary>
		/// Makes a simple HTTP request to the specified URL using the 
		/// method provided and returns a string containing the response.
		/// </summary>
		/// <remarks>
		/// Method typically will be either "POST" or "GET".
		/// </remarks>
		/// <param name="url">Url to make the request</param>
		/// <param name="method"></param>
		/// <returns>The reply from the web server.</returns>
		public static string GetResponseText(Uri url, string method)
		{
			return GetResponseText(url, method, null);
		}

		/// <summary>
		/// Returns an XmlReader used for efficiently reading the XML Response.
		/// </summary>
		/// <remarks>Assumes an XML response. Will throw exception otherwise.</remarks>
		/// <param name="url"></param>
		/// <param name="method"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static XmlReader GetResponseXmlReader(Uri url, string method, NameValueCollection data)
		{
			
			using(Stream responseStream = GetResponseStream(url, method, data))
			{
				return new XmlTextReader(responseStream);
			}
		}

		/// <summary>
		/// Returns an XPathNavigator used to navigate the XML Response.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="method"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static XPathNavigator GetResponseXPathNavigator(Uri url, string method, NameValueCollection data)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(GetResponseXmlReader(url, method, data));
			return doc.CreateNavigator();
		}

		// Utility. Builds a query string or form post data.
		static string TranslateToRequestData(NameValueCollection data)
		{
			if(data == null)
				return string.Empty;

			StringBuilder requestData = new StringBuilder();	
			foreach(string key in data.Keys)
			{
				requestData.Append(key);
				requestData.Append("=");
				requestData.Append(data[key]);
				requestData.Append("&");
			}
			//Remove last &
			return GU.Text.StringHelper.Left(requestData.ToString(), requestData.Length - 1);
		}

		// Utility method to get the Response as a stream.
		static Stream GetResponseStream(Uri url, string method, NameValueCollection data)
		{
			Log.Info("Making Request for " + url);
			string requestData = TranslateToRequestData(data);

			method = method.ToUpper();

			if(method == "GET")
				url = new Uri(url.ToString() + "?" + requestData);

			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			request.Method = method;

			if(method == "POST")
			{
				request.AllowAutoRedirect = false;
				request.KeepAlive = true;
				request.ContentType = "application/x-www-form-urlencoded";

				Log.Info("Posting " + requestData);
				byte[] requestDataBuffer = Encoding.ASCII.GetBytes(requestData);
				request.ContentLength = requestDataBuffer.Length;
			
				using(Stream requestStream = request.GetRequestStream())
				{
					requestStream.Write(requestDataBuffer, 0, requestDataBuffer.Length);
				}
			}
			
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			
			return response.GetResponseStream();
		}
	}
}
