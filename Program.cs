using GpioHat;
using System;
using System.Threading;

namespace Tamon_Testat {

    public class Program {

        public static JoystickButtons Butt = JoystickButtons.None;

        static void Main( string[] args ) {
            Game game = new Game();
            #region Wait for Debugger
            if ( args.Length > 0 && args[ 0 ] == "--debug" ) {
                Console.WriteLine( "Waiting for debugger ..." );
                while ( !System.Diagnostics.Debugger.IsAttached ) {
                    System.Threading.Thread.Sleep( 500 );
                }
            }
            #endregion

            Raspberry raspi = Raspberry.Instance;
            raspi.Joystick.JoystickChanged += JoystickChangedCallback;

            Console.WriteLine( "Welcome to TAMON" );
            Thread.Sleep( 1000 );
            Console.Clear();

            // RUN() hier
            game.Run();
            // debugg
            Console.ReadKey();
        }

        private static void JoystickChangedCallback( object sender, JoystickEventsArgs e ) {
            if ( e.Buttons != JoystickButtons.None ) {
                Butt = e.Buttons;
            }
        }

    }
}