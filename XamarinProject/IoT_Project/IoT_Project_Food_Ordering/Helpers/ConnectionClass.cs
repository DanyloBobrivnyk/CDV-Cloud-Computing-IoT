using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace IoT_Project_Food_Ordering.Helpers
{
    public static class ConnectionClass
    {
        public static string connectionString = "https://dbobrivnykapi.azurewebsites.net";
        public static string GetResponseFromRequest(string url, ref HttpWebRequest request)
        {
            StringBuilder sb = new StringBuilder();
            Stream resStream = null;
            HttpWebResponse response = null;
            byte[] buf = new byte[8192];

            try
            {
                // execute the request
                response = (HttpWebResponse)request.GetResponse();

                // we will read data via the response stream
                resStream = response.GetResponseStream();
                string tempString = null;
                int count = 0;
                do
                {
                    // fill the buffer with data
                    count = resStream.Read(buf, 0, buf.Length);
                    // make sure we read some data
                    if (count != 0)
                    {
                        // translate from bytes to ASCII text
                        tempString = Encoding.ASCII.GetString(buf, 0, count);
                        // continue building the string
                        sb.Append(tempString);
                    }
                }
                while (count > 0); // any more data to read?
            }
            catch (Exception err)
            {
                String exc = err.Message;
            }
            finally
            {
                response.Close();
                resStream.Close();
            }

            return sb.ToString();
        }
    }   
}
