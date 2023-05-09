using System;
using System.IO;
using System.Net.Sockets;

namespace Tamon_Testat {

    public class Client {

        private TcpClient tcpClient;
        private NetworkStream netStream;
        private StreamWriter streamWrite;
        private StreamReader streamRead;

        public void TcpClient_Start() {
            string serverHostname = "eee-02004.simple.eee.intern"; // Replace with the IP address of the server
            int port = 8080;

            Console.WriteLine( "Connecting to server..." );
            tcpClient = new TcpClient( serverHostname, port );

            netStream = tcpClient.GetStream();
            streamWrite = new StreamWriter( netStream );
            streamRead = new StreamReader( netStream );

            // Wait for initial message from server
            string initialMessage = streamRead.ReadLine();
            Console.WriteLine( "Initial message from server: " + initialMessage );

            return;
            // Starte den Thread für die empfangenen Daten
            //Thread receiveThread = new Thread( new ThreadStart( ReceiveData ) );
            //receiveThread.Start();
        }

        public void SendData() {
            Console.WriteLine( "Send Text: " );
            streamWrite.WriteLine( Console.ReadLine() );
            streamWrite.Flush();
        }

        public string ReceiveData() {
            while ( true ) {
                string receivedData = streamRead.ReadLine();
                if ( receivedData != null ) {
                    return receivedData;
                }

            }
        }
    }
}
