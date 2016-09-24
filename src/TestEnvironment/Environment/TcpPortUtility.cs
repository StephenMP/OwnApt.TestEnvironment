using System.Net;
using System.Net.Sockets;

namespace OwnApt.TestEnvironment.Environment
{
    public static class TcpPortUtility
    {
        #region Public Methods

        public static int GetFreeTcpPort()
        {
            var portListener = new TcpListener(IPAddress.Loopback, 0);

            portListener.Start();
            var port = ((IPEndPoint)portListener.LocalEndpoint).Port;
            portListener.Stop();

            return port;
        }

        #endregion Public Methods
    }
}
