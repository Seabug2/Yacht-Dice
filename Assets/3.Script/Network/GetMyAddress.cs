using System.Net;
using System.Net.Sockets;

public class GetMyAddress
{
    public static string GetLocalIPv4()
    {
        string localIP = string.Empty;
        foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
}
