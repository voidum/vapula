using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCM.Net
{
    public class Response
    {
        private string GetHeader(string mime, int total, string status)
        {
            StringBuilder sbd = new StringBuilder();
            sbd.Append("HTTP/1.1 " + status + "\r\n");
            sbd.Append("Server: TCM Server\r\n");
            sbd.Append("Content-Type: " + mime + "; charset=utf-8\r\n");
            sbd.Append("Content-Length: " + total + "\r\n\r\n");
            return sbd.ToString();
        }
    }
}
