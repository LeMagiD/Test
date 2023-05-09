using GpioHat;
using System;
using System.Threading;

namespace Tamon_Testat {

    public class Gui {

        public void ClearScreen() {
            Console.Clear();
        }

        public void FieldEdge() {
            Console.SetCursorPosition( 0, 0 );
            Console.Write( "=====================================================" );
            Console.SetCursorPosition( 0, 10 );
            Console.Write( "=====================================================" );
        }

        public void Joystick( bool b ) {
            Console.SetCursorPosition( 0, 0 );
            Console.SetCursorPosition( 5, 4 );
            Console.Write( "-------" );
            Console.SetCursorPosition( 5, 5 );
            Console.Write( "|  o  |" );
            Console.SetCursorPosition( 5, 6 );
            Console.Write( "-------" );
            if ( b ) {
                Console.SetCursorPosition( 7, 3 );
                Console.Write( "[1]" );
                Console.SetCursorPosition( 1, 5 );
                Console.Write( "[3]" );
                Console.SetCursorPosition( 13, 5 );
                Console.Write( "[4]" );
                Console.SetCursorPosition( 7, 7 );
                Console.Write( "[2]" );
            }
            else {
                Console.SetCursorPosition( 5, 7 );
                Console.Write( "[Center]" );
            }
        }

        public void PrintMenuNr( string one, string two, string three, string four ) {
            Console.SetCursorPosition( 20, 3 );
            Console.Write( $"[1]  {one}" );
            Console.SetCursorPosition( 20, 4 );
            Console.Write( $"[2]  {two}" );
            Console.SetCursorPosition( 20, 5 );
            Console.Write( $"[3]  {three}" );
            Console.SetCursorPosition( 20, 6 );
            Console.Write( $"[4]  {four}" );
        }

        public void StartScreen() {
            Console.Clear();
            FieldEdge();
            Console.SetCursorPosition( 1, 1 );
            Console.WriteLine( "START SCREEN" );
            Joystick( true );
            PrintMenuNr( "Play as HOST", "Play", "Credits", "Exit" );
            while ( Program.Butt == JoystickButtons.None || Program.Butt == JoystickButtons.Center ) {; ; }
            switch ( Program.Butt ) {

                case JoystickButtons.Up:
                    PrintPlayHost( true );
                    break;
                case JoystickButtons.Down:
                    PrintPlayHost( false );
                    break;
                case JoystickButtons.Left:
                    PrintCredits();
                    break;
                case JoystickButtons.Right:
                    PrintEXIT();
                    break;
                default:
                    Console.SetCursorPosition( 10, 12 );
                    Console.Write( " ERROR " );
                    break;
            }
        }

        public void PrintPlayHost( bool b ) {

            string tamon;       // ToDo von Z 109                                     
            Console.Clear();
            FieldEdge();
            Joystick( false );
            Console.SetCursorPosition( 1, 1 );
            if ( b ) {
                Console.WriteLine( "PLAY as HOST" );
                Console.SetCursorPosition( 20, 4 );
                Console.Write( "hostname: eee-02004.simple.intern" );
                Console.SetCursorPosition( 20, 5 );
                Console.Write( "port    : 13" );
            }
            else {
                Console.WriteLine( "PLAY" );
                Console.SetCursorPosition( 20, 4 );
                Console.Write( "hostname: eee-02146.simple.intern" );
                Console.SetCursorPosition( 20, 5 );
                Console.Write( "port    : 13" );
            }
            while ( Program.Butt != JoystickButtons.Center ) {; ; }

            tamon = PrintMonster();  // ToDo tamon besser mit object zuordnung
            Thread.Sleep( 1000 );
            // GameScreen( "Hit", "Tackle", "Scratch", "Bounce", tamon, "Denis" );          // main RUN!

        }

        private void PrintCredits() {
            Console.Clear();
            FieldEdge();
            Console.SetCursorPosition( 1, 1 );
            Console.SetCursorPosition( 10, 4 );
            Console.Write( "Denis Ameti:  " );
            Console.SetCursorPosition( 25, 4 );
            Console.Write( "[XXXXXXXX  ]" );
            Console.SetCursorPosition( 2, 6 );
            Console.Write( "Marcel Stübi:  " );
            Console.SetCursorPosition( 17, 6 );
            Console.Write( "[XXXXXXXXXX]" );

            while ( Program.Butt != JoystickButtons.Center ) {; ; }
            StartScreen();
        }

        private void PrintEXIT() {
            Console.Clear();
            FieldEdge();
            Console.SetCursorPosition( 1, 1 );
            Console.SetCursorPosition( 10, 4 );
            Console.Write( "crtl + C to exit" );
            while ( Program.Butt != JoystickButtons.Center ) {; ; }
            StartScreen();
        }

        // ToDo Monster List verbinden
        public string PrintMonster() {

            Console.Clear();
            FieldEdge();
            Console.SetCursorPosition( 1, 1 );
            Console.WriteLine( "Choose your TAMON" );
            Joystick( true );
            PrintMenuNr( Game.MonsterNames[ 0 ], Game.MonsterNames[ 1 ], Game.MonsterNames[ 2 ], Game.MonsterNames[ 3 ] );
            while ( Program.Butt == JoystickButtons.None || Program.Butt == JoystickButtons.Center ) {; ; }
            switch ( Program.Butt ) {

                case JoystickButtons.Up:
                    Console.SetCursorPosition( 10, 12 );
                    Console.Write( " Tamon 1 gewählt " );
                    return Game.MonsterNames[ 0 ];
                case JoystickButtons.Down:
                    Console.SetCursorPosition( 10, 12 );
                    Console.Write( " Tamon 2 gewählt " );
                    return Game.MonsterNames[ 1 ];
                case JoystickButtons.Left:
                    Console.SetCursorPosition( 10, 12 );
                    Console.Write( " Tamon 3 gewählt " );
                    return Game.MonsterNames[ 2 ];
                case JoystickButtons.Right:
                    Console.SetCursorPosition( 10, 12 );
                    Console.Write( " Tamon 4 gewählt " );
                    return Game.MonsterNames[ 3 ];
                default:
                    Console.SetCursorPosition( 10, 12 );
                    Console.Write( " ERROR " );
                    break;
            }
            Thread.Sleep( 2000 );
            return "No Monster error";
        }

        // ToDo Attacken
        public void GameScreen( string ownAtt1, string ownAtt2, string ownAtt3, string ownAtt4, string ownMon, string enemyMon ) {
            Console.Clear();
            FieldEdge();
            Joystick( true );
            PrintMenuNr( ownAtt1, ownAtt2, ownAtt3, ownAtt4 );
            Console.SetCursorPosition( 10, 12 );
            Console.Write( $"{enemyMon}:  " );
            Console.SetCursorPosition( 25, 12 );
            Console.Write( "[XXXXXXXXXX]" );
            Console.SetCursorPosition( 2, 15 );
            Console.Write( $"{ownMon}:  " );
            Console.SetCursorPosition( 17, 15 );
            Console.Write( "[XXXXXXXXXX]" );
        }

    }
}
