using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Tamon_Testat {

    public class Server {

        private TcpListener tcpListener;
        private TcpClient tcpClient;
        private NetworkStream netStream;
        private StreamWriter streamWrite;
        private StreamReader streamRead;

        public void TcpServer_Start() {
            IPEndPoint iPEndPoint = new IPEndPoint( IPAddress.Any, 8080 );
            tcpListener = new TcpListener( iPEndPoint );

            tcpListener.Start();

            Console.WriteLine( "Waiting for client to connect..." );

            tcpClient = tcpListener.AcceptTcpClient();
            Console.WriteLine( "Client connected: " + tcpClient.Client.RemoteEndPoint );

            netStream = tcpClient.GetStream();
            streamWrite = new StreamWriter( netStream );
            streamRead = new StreamReader( netStream );

            // Send initial message to client
            streamWrite.WriteLine( "Marcel Monster 69 Fire" );
            streamWrite.Flush();

            return;
            //// Starte den Thread für die empfangenen Daten
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
