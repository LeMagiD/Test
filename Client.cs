using System.IO;
using System.Net.Sockets;

namespace Tamon_Testat
{

    public class Client
    {

        private TcpClient tcpClient;
        private NetworkStream netStream;
        private StreamWriter streamWrite;
        private StreamReader streamRead;

        public void TcpClient_Start(Gui gui)
        {
            string serverHostname = "eee-02004.simple.eee.intern"; // Replace with the IP address of the server
            int port = 8080;

            gui.PrintConnecting();

            tcpClient = new TcpClient(serverHostname, port);

            netStream = tcpClient.GetStream();
            streamWrite = new StreamWriter(netStream);
            streamRead = new StreamReader(netStream);

            // Wait for initial message from server
            string initialMessage = streamRead.ReadLine();
            gui.UpdateConnected();

            return;
            // Starte den Thread für die empfangenen Daten
            //Thread receiveThread = new Thread( new ThreadStart( ReceiveData ) );
            //receiveThread.Start();
        }

        public void SendData(string s)
        {
            streamWrite.WriteLine(s);
            streamWrite.Flush();
        }

        public string ReceiveData()
        {
            while (true)
            {
                string receivedData = streamRead.ReadLine();
                if (receivedData != null)
                {
                    return receivedData;
                }
            }
        }

        // End
        public void EndCient()
        {
            streamRead.Close();
            streamWrite.Close();
            netStream.Close();
            tcpClient.Close();
        }
    }
}
