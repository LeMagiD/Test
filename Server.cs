using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Tamon_Testat
{

    public class Server
    {

        private TcpListener tcpListener;
        private TcpClient tcpClient;
        private NetworkStream netStream;
        private StreamWriter streamWrite;
        private StreamReader streamRead;

        public void TcpServer_Start(Gui gui)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 8080);
            tcpListener = new TcpListener(iPEndPoint);

            tcpListener.Start();

            gui.PrintConnecting();

            tcpClient = tcpListener.AcceptTcpClient();

            gui.UpdateConnected();

            netStream = tcpClient.GetStream();
            streamWrite = new StreamWriter(netStream);
            streamRead = new StreamReader(netStream);

            // Send initial message to client
            streamWrite.WriteLine("Fight");
            streamWrite.Flush();

            return;
            //// Starte den Thread für die empfangenen Daten
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
        public void EndServer()
        {
            streamRead.Close();
            streamWrite.Close();
            tcpClient.Close();
            netStream.Close();
        }
    }
}
