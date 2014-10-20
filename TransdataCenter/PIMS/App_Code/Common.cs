using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Security.Cryptography;

/// <summary>
///Common 的摘要说明
/// </summary>
public abstract class Common
{
    public static string SSOAddress()
    {
       // return "http://192.168.254.20/default.aspx";
        return "http://192.168.254.20:8080/default.aspx";
    }

    public static void RedirectToSSO(HttpResponse Response, int ErrorID)
    {
        Response.Redirect(SSOAddress() + "?AppID=102" + "&ErrorID=" + ErrorID);
    }

    public static string ArrayToString(int[] obj, string splitStr)
    {
        StringBuilder s = new StringBuilder();
        for (int i = 0; i < obj.Length; i++)
        {
            if (i > 0) s.Append(splitStr);
            s.Append(obj[i]);
        }
        return s.ToString();
    }
    public static StringBuilder ArrayToString(string[] obj, string splitStr)
    {
        StringBuilder s = new StringBuilder();
        for (int i = 0; i < obj.Length; i++)
        {
            if (i > 0) s.Append(splitStr);
            s.Append('"');
            s.Append(obj[i]);
            s.Append('"');
        }
        return s;
    }
    public static string PostDataToUrl(string url, string postData)
    {
        try
        {
            // Create a request for the URL.         
            WebRequest request = WebRequest.Create(url);
            // Convert postData to byteArray
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            Console.WriteLine(response.StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
        catch
        {
            return "";
        }
    }
}
