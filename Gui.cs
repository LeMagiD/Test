using GpioHat;
using System;
using System.Threading;

namespace Tamon_Testat
{

    public class Gui
    {

        public static int ownMonsterId = 0;
        public static Monster enemymonster = null;

        public static bool server = false;

        public void ClearScreen()
        {
            Console.Clear();
        }

        public void FieldEdge()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("=====================================================");
            Console.SetCursorPosition(0, 10);
            Console.Write("=====================================================");
        }

        public void Joystick(bool b)
        {
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(5, 4);
            Console.Write("-------");
            Console.SetCursorPosition(5, 5);
            Console.Write("|  o  |");
            Console.SetCursorPosition(5, 6);
            Console.Write("-------");
            if (b)
            {
                Console.SetCursorPosition(7, 3);
                Console.Write("[1]");
                Console.SetCursorPosition(1, 5);
                Console.Write("[3]");
                Console.SetCursorPosition(13, 5);
                Console.Write("[4]");
                Console.SetCursorPosition(7, 7);
                Console.Write("[2]");
            }
            else
            {
                Console.SetCursorPosition(5, 7);
                Console.Write("[Center]");
            }
        }

        public void PrintMenuNr(string one, string two, string three, string four)
        {
            Console.SetCursorPosition(20, 3);
            Console.Write($"[1]  {one}");
            Console.SetCursorPosition(20, 4);
            Console.Write($"[2]  {two}");
            Console.SetCursorPosition(20, 5);
            Console.Write($"[3]  {three}");
            Console.SetCursorPosition(20, 6);
            Console.Write($"[4]  {four}");
        }

        public void StartScreen()
        {
            Console.Clear();
            FieldEdge();
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("START SCREEN");
            Joystick(true);
            PrintMenuNr("Play as HOST", "Play", "Credits", "Exit");
            while (Program.Butt == JoystickButtons.None || Program.Butt == JoystickButtons.Center) {; ; }
            switch (Program.Butt)
            {

                case JoystickButtons.Up:
                    server = true;
                    PrintPlayHost(server);
                    break;
                case JoystickButtons.Down:
                    server = false;
                    PrintPlayHost(server);
                    break;
                case JoystickButtons.Left:
                    PrintCredits();
                    break;
                case JoystickButtons.Right:
                    PrintEXIT();
                    break;
                default:
                    Console.SetCursorPosition(10, 12);
                    Console.Write(" ERROR ");
                    break;
            }
        }

        public void PrintPlayHost(bool server)
        {

            Console.Clear();
            FieldEdge();
            Joystick(false);
            Console.SetCursorPosition(1, 1);
            if (server)
            {
                Console.WriteLine("PLAY as HOST");
                Console.SetCursorPosition(20, 4);
                Console.Write("hostname: eee-02004.simple.intern");
                Console.SetCursorPosition(20, 5);
                Console.Write("port    : 13");
            }
            else
            {
                Console.WriteLine("PLAY");
                Console.SetCursorPosition(20, 4);
                Console.Write("hostname: eee-02146.simple.intern");
                Console.SetCursorPosition(20, 5);
                Console.Write("port    : 13");
            }
            while (Program.Butt != JoystickButtons.Center) {; ; }

            Thread.Sleep(1000);
        }

        public void PrintConnecting()
        {
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("Connecting to Enemy");
            Console.SetCursorPosition(20, 4);
            Console.Write("Waiting for client to connect...");
            Console.SetCursorPosition(20, 5);
            Console.Write("                       ");
        }

        public void UpdateConnected()
        {
            Console.SetCursorPosition(20, 5);
            Console.Write("Connected to Enemy!!");
            Thread.Sleep(2000);
        }

        private void PrintCredits()
        {
            Console.Clear();
            FieldEdge();
            Console.SetCursorPosition(1, 1);
            Console.Write("Credits");
            Console.SetCursorPosition(10, 4);
            Console.Write("Denis Ameti:  ");
            Console.SetCursorPosition(25, 4);
            Console.Write("[XXXXXXXX  ]");
            Console.SetCursorPosition(2, 6);
            Console.Write("Marcel Stübi:  ");
            Console.SetCursorPosition(17, 6);
            Console.Write("[XXXXXXXXXX]");

            while (Program.Butt != JoystickButtons.Center) {; ; }
            StartScreen();
        }

        private void PrintEXIT()
        {
            Console.Clear();
            FieldEdge();
            Console.SetCursorPosition(1, 1);
            Console.SetCursorPosition(10, 4);
            Console.Write("crtl + C to exit");
            Console.SetCursorPosition(0, 50);
            while (Program.Butt != JoystickButtons.Center) {; ; }
            StartScreen();
        }

        public string PrintMonster()
        {

            Console.Clear();
            FieldEdge();
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("Choose your TAMON");
            Joystick(true);
            PrintMenuNr(Game.MonsterNames[0], Game.MonsterNames[1], Game.MonsterNames[2], Game.MonsterNames[3]);
            while (Program.Butt == JoystickButtons.None || Program.Butt == JoystickButtons.Center) {; ; }
            switch (Program.Butt)
            {

                case JoystickButtons.Up:
                    Console.SetCursorPosition(10, 12);
                    ownMonsterId = 0;
                    return "0";
                case JoystickButtons.Down:
                    Console.SetCursorPosition(10, 12);
                    ownMonsterId = 1;
                    return "1";
                case JoystickButtons.Left:
                    Console.SetCursorPosition(10, 12);
                    ownMonsterId = 2;
                    return "2";
                case JoystickButtons.Right:
                    Console.SetCursorPosition(10, 12);
                    ownMonsterId = 4;
                    return "3";
                default:
                    Console.SetCursorPosition(10, 12);
                    Console.Write(" ERROR ");
                    break;
            }
            Thread.Sleep(2000);
            return "9";
        }

        public void GameScreen()
        {
            Console.Clear();
            FieldEdge();
            Joystick(true);
            PrintMenuNr(Game.MonsterList[ownMonsterId].Moves[0].Name,
                            Game.MonsterList[ownMonsterId].Moves[1].Name,
                            Game.MonsterList[ownMonsterId].Moves[2].Name,
                            Game.MonsterList[ownMonsterId].Moves[3].Name);
            Console.SetCursorPosition(10, 12);
            Console.Write($"{Game.MonsterList[4].Name}:  ");
            Console.SetCursorPosition(25, 12);
            Console.Write("[XXXXXXXXXX]");
            Console.SetCursorPosition(2, 15);
            Console.Write($"{Game.MonsterNames[ownMonsterId]}:  ");
            Console.SetCursorPosition(17, 15);
            Console.Write("[XXXXXXXXXX]");
        }

        public void UpdateGameScreen(string move, int attackValue)
        {

            int i = (Game.MonsterList[4].HP / 10);
            int j = (Game.MonsterList[ownMonsterId].HP / 10);
            if (i > 10) i = 10; if (j > 10) j = 10;
            Console.SetCursorPosition((36 - (10 - i)), 12);
            for (int r = 0; r < i; r++) { Console.Write(" "); }
            Console.SetCursorPosition((28 - (10 - j)), 15);
            for (int r = 0; r < j; r++) { Console.Write(" "); }

            Console.SetCursorPosition(1, 17);
            Console.Write($"{Game.MonsterList[4].Name} used {move} and deals {attackValue} damage ");
        }

        public void PrintEndScreen(int i)
        {

            Console.Clear();
            FieldEdge();
            Console.SetCursorPosition(1, 1);
            Console.Write("Credits");
            Console.SetCursorPosition(10, 4);
            Console.Write("Denis Ameti:  ");
            Console.SetCursorPosition(25, 4);
            Console.Write("[XXXXXXXX  ]");
            Console.SetCursorPosition(2, 6);
            Console.Write("Marcel Stübi:  ");
            Console.SetCursorPosition(17, 6);
            Console.Write("[XXXXXXXXXX]");

            if (i > 0)
            {
                Console.SetCursorPosition(1, 12);
                Console.Write("YOU WIN");
            }
            else
            {
                Console.SetCursorPosition(1, 12);
                Console.Write("YOUR TAMON FAINTED");
            }
            while (Program.Butt != JoystickButtons.Center) {; ; }
            StartScreen();
        }
    }
}
