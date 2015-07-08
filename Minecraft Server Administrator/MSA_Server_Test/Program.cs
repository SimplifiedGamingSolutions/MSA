using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MSA_Server_Test
{
    class Program
    {

        static void Main(string[] args)
        {

            #region Test HTTP Methods
            TestPlayerJoined("TestPlayer");
            Console.ReadKey();
            TestPlayerLeft("TestPlayer");
            Console.ReadKey();
            #endregion

            Console.ReadLine();
        }
        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        /// <summary>
        /// Test POST Method
        /// </summary>
        private static void TestPlayerJoined(string name)
        {
            Console.WriteLine("Testing POST Request");
            string strURL = "http://localhost:39640/PlayerJoined";

            // Create the xml document in a memory stream - Recommended       
            byte[] dataByte = Encoding.UTF8.GetBytes(name);

            HttpWebRequest POSTRequest = (HttpWebRequest)WebRequest.Create(strURL);
            //Method type
            POSTRequest.Method = "POST";
            // Data type - message body coming in xml
            POSTRequest.ContentType = "text/xml";
            POSTRequest.KeepAlive = false;
            POSTRequest.Timeout = 5000;
            //Content length of message body
            POSTRequest.ContentLength = dataByte.Length;

            // Get the request stream
            Stream POSTstream = POSTRequest.GetRequestStream();
            // Write the data bytes in the request stream
            POSTstream.Write(dataByte, 0, dataByte.Length);

            //Get response from server
            HttpWebResponse POSTResponse = (HttpWebResponse)POSTRequest.GetResponse();
            StreamReader reader = new StreamReader(POSTResponse.GetResponseStream(), Encoding.UTF8);
            Console.WriteLine("Response");
            Console.WriteLine(reader.ReadToEnd().ToString());
        }

        /// <summary>
        /// Test DELETE Method
        /// </summary>
        private static void TestPlayerLeft(string name)
        {
            string strURL = "http://localhost:39640/PlayerLeft";

            // Create the xml document in a memory stream - Recommended       
            byte[] dataByte = Encoding.UTF8.GetBytes(name);

            HttpWebRequest POSTRequest = (HttpWebRequest)WebRequest.Create(strURL);
            //Method type
            POSTRequest.Method = "POST";
            // Data type - message body coming in xml
            POSTRequest.ContentType = "text/xml";
            POSTRequest.KeepAlive = false;
            POSTRequest.Timeout = 5000;
            //Content length of message body
            POSTRequest.ContentLength = dataByte.Length;

            // Get the request stream
            Stream POSTstream = POSTRequest.GetRequestStream();
            // Write the data bytes in the request stream
            POSTstream.Write(dataByte, 0, dataByte.Length);

            //Get response from server
            HttpWebResponse POSTResponse = (HttpWebResponse)POSTRequest.GetResponse();
            StreamReader reader = new StreamReader(POSTResponse.GetResponseStream(), Encoding.UTF8);
            Console.WriteLine("Response");
            Console.WriteLine(reader.ReadToEnd().ToString());
        }
    }
}
