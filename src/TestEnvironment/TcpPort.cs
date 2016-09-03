using System.Net;
using System.Net.Sockets;

namespace OwnApt.TestEnvironment
{
    public static class TcpPort
    {
        #region Public Methods

        public static int GetFreeTcpPort()
        {
            var l = new TcpListener(IPAddress.Loopback, 0);

            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();

            return port;
        }

        #endregion Public Methods
    }
}
