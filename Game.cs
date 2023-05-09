using GpioHat;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Tamon_Testat
{
    public class Game
    {

        public static string[] MonsterNames = { "Bob", "Stefan", "Ueli", "Ruedi" };
        public static List<Monster> MonsterList { get; set; }
        public List<Attack> NormalAttacks { get; set; }
        public List<Attack> FireAttacks { get; set; }
        public List<Attack> WaterAttacks { get; set; }
        public List<Attack> GrassAttacks { get; set; }

        public Game()
        {
            MonsterList = new List<Monster>();
            NormalAttacks = new List<Attack>();
            FireAttacks = new List<Attack>();
            WaterAttacks = new List<Attack>();
            GrassAttacks = new List<Attack>();
            InitMonsters();
            InitAttacks();
        }
        private void InitMonsters()
        {
            MonsterList.Add(new Monster(MonsterNames[0], Element.normal, 100, NormalAttacks)); //TODO - how to integrate Attack into attacklist using this?
            MonsterList.Add(new Monster(MonsterNames[1], Element.fire, 120, FireAttacks));
            MonsterList.Add(new Monster(MonsterNames[2], Element.fire, 120, WaterAttacks));
            MonsterList.Add(new Monster(MonsterNames[3], Element.fire, 120, GrassAttacks));
        }

        private void InitAttacks()
        {
            Attack Slap = new Attack(12, Element.normal, 100, "Slap");
            Attack Punch = new Attack(16, Element.normal, 85, "Punch");
            Attack Headbutt = new Attack(16, Element.normal, 90, "Headbutt");
            Attack Strangle = new Attack(16, Element.normal, 99, "Strangle");
            NormalAttacks.Add(Slap);
            NormalAttacks.Add(Punch);
            NormalAttacks.Add(Headbutt);
            NormalAttacks.Add(Strangle);
            //NormalAttacks.Add(new Attack(69, Element.normal, 100, "bigPP")); // kompaktere Schreibeweise des oberen.

            Attack Inflame = new Attack(20, Element.fire, 65, "Inflame");
            Attack Magmaburst = new Attack(20, Element.fire, 50, "Magmaburst");
            Attack Flameslash = new Attack(20, Element.fire, 70, "Flameslash");
            Attack Firetornado = new Attack(20, Element.fire, 80, "Firetornado");
            FireAttacks.Add(Firetornado);
            FireAttacks.Add(Magmaburst);
            FireAttacks.Add(Flameslash);
            FireAttacks.Add(Inflame);

            Attack Waterboarding = new Attack(20, Element.water, 65, "Waterboarding");
            Attack Spit = new Attack(20, Element.water, 50, "Spit");
            Attack Surfer = new Attack(20, Element.water, 70, "Surfer");
            Attack Hotwater = new Attack(20, Element.water, 80, "Hotwater");
            WaterAttacks.Add(Waterboarding);
            WaterAttacks.Add(Spit);
            WaterAttacks.Add(Surfer);
            WaterAttacks.Add(Hotwater);

            Attack Leafblade = new Attack(20, Element.fire, 65, "Leafblade");
            Attack Mossovergrow = new Attack(20, Element.fire, 50, "Mossovergrow");
            Attack Woodslam = new Attack(20, Element.fire, 70, "Woodslam");
            Attack Pollenbreeze = new Attack(20, Element.fire, 80, "Pollenbreeze");
            GrassAttacks.Add(Leafblade);
            GrassAttacks.Add(Mossovergrow);
            GrassAttacks.Add(Woodslam);
            GrassAttacks.Add(Pollenbreeze);
        }
        #region calculation with class Attack & Monster
        private int CalculateDmgClass(Attack attack)
        {
            if (new Random().Next(0, 101) > attack.SuccessRate) { return 0; }
            float rand = new Random().NextSingle();
            int dmg = attack.Damage + (int)(rand * attack.Damage); // Damage*(1+Rand) | 0 <= Rand < 1
            if (new Random().Next(0, 101) > 95)
                return dmg * 5; // 5 times the damage if critical hit (5% chance)
            return dmg;
            // TODO - calculate Damage (critical, succesrate, maybe STAB/elemental damage for later)
        }
        private int CalculateHpClass(Monster monster, Attack attack)
        {
            int Hp = monster.HP - CalculateDmgClass(attack);
            return Hp;
        }
        #endregion

        /// <summary>
        /// Berechnet eingehender Schaden, nur für Funktion CalculateHp() gedacht
        /// </summary>
        /// <param name="dmg">Schaden des Gegners auf das eigene Monster</param>
        /// <param name="successrate">Erfolgrate der vom Gegner eingesetzten Attacke</param>
        /// <returns> Schaden der nach den Berechnungen verursacht wurde</returns>
        private int CalculateDmg(int dmg, int successrate)
        {
            if (new Random().Next(0, 101) > successrate)
            {
                Console.WriteLine("Missed the Attack!");
                return 0;
            }
            float rand = new Random().NextSingle();
            int damage = dmg + (int)(rand * dmg); // Damage*(1+Rand) | 0 <= Rand < 1
            if (new Random().Next(0, 101) > 95)
                return damage * 5; // 5 times the damage if critical hit (5% chance)
            return damage;
        }
        private int CalculateHp(Monster monster, int dmg, int sRate)
        {
            int damage = CalculateDmg(dmg, sRate);
            monster.HP -= damage;
            if (monster.HP <= 0)
            {
                monster.HP = 0;
                Console.WriteLine("your HP reached 0! you lost!");
                return damage;
                // TODO - Streamwriter (oder so) wenn verloren, danach direkt beenden/wieder zum start.
            }
            return damage; 
        }
        private void convertData(Gui gui ,string recStr, Monster ownmonster)
        {
            //  TODO - convert received Data to string, split it into a array and use the integers for the damage and success rate
            /*
             * string s = sr.ReadToEnd();
             * string [] str = s.Split(' ');
             * int damage = Int32.Parse(str[1]);
             * int success = int32.Parse(str[2]);
            */

            string s = recStr;
            string[] str = s.Split(' ');
            int AtkValue = Int32.Parse(str[1]);
            int successrate = Int32.Parse(str[3]);
            int damageDone = CalculateHp(ownmonster, AtkValue, successrate);

            string AtkName = str[2];
            string enemyHp = str[0];
            gui.UpdateGameScreen(AtkName, damageDone);
            
            
            // Reihenfolge string [ownHp, AtkValue, AtkName, successrate]
            //enemymonster hat mit "angriff" angegriffen
        }

        public string SendAttack(Monster monster, int move)
        {
            string ownHP = MonsterList[Gui.ownMonsterId].HP.ToString();
            string attackValue = MonsterList[Gui.ownMonsterId].Moves[move].Damage.ToString();
            string attackName = MonsterList[Gui.ownMonsterId].Moves[move].Name;
            string succRate = MonsterList[Gui.ownMonsterId].Moves[move].SuccessRate.ToString();
            return ownHP + " " + attackValue + " " + attackName + " " + succRate;
        }
        private void getEnemyTamon(string Id)   // get enemies monster data
        {
            int monsterId = Int32.Parse(Id);
            Monster enemyMonster = MonsterList[monsterId];
            MonsterList.Add(enemyMonster);

/*            Console.WriteLine("Monster Index = " + MonsterList.IndexOf(MonsterList[0]));
            Console.WriteLine("Monster Name = " + MonsterList[0].Name);
            for (int i = 0; i < MonsterList[0].Moves.Count; i++)
            {
                Console.WriteLine($"Attack {i} = " + MonsterList[0].Moves[i].Name);
            }
            Console.WriteLine("Monster Moves = " + MonsterList[0].Moves[1].Name);
*/
        }

        public void Run()
        {
            Server server = new Server();
            Client client = new Client();
            Gui gui = new Gui();
            gui.StartScreen();          // Prints Startscreen Menu and than individual Screen 1,2,3 or 4
                                        //    or stays in loop with options 3 or 4

/*            //used to test 
            Console.WriteLine("Welcome to Tamon - Table Monsters!");
            convertData(gui,"Dmg 10 100 AtkName", MonsterList[0]);
            Thread.Sleep(2000);
            getEnemyTamon("0");
            Thread.Sleep(2000);
            // end used to test*/


            if (Gui.server)
            {                     // init Server/Client depending on Menu path
                server.TcpServer_Start(gui);      // waits for connection
            }
            else
            {
                client.TcpClient_Start(gui);
            }

            string monsterId = gui.PrintMonster();  // Prints Monster-Choosing Menu and returns Id Nr as string
            string enemyId;

            if (Gui.server)
            {
                enemyId = server.ReceiveData();     // Server Pi waits for enemyId (string)
                getEnemyTamon(enemyId);
                server.SendData(monsterId);       // Server Pi sends to client ownMonsterId
                Thread.Sleep(1000);
                gui.GameScreen();                   // Setup Gamescreen with own Attacks and Tamon names
            }
            else
            {
                client.SendData(monsterId);       // Client Pi sends to client ownMonsterId
                enemyId = client.ReceiveData();     // Client Pi waits for enemyId (string)
                getEnemyTamon(enemyId);
                Thread.Sleep(1000);
                gui.GameScreen();                   // Setup Gamescreen with own Attacks and Tamon names
            }

            // Loop till one Tamon fainted => HP <= 0
            // Convertion needed!!

            //ToDo - Lebenspunkte und string // convert
            while (MonsterList[Gui.ownMonsterId].HP > 0 && MonsterList[4].HP > 0)
            {
                if (Gui.server)
                {
                    Program.Butt = JoystickButtons.None;
                    Thread.Sleep(1000);
                    convertData(gui,server.ReceiveData(),MonsterList[Gui.ownMonsterId]);

                    while (Program.Butt == JoystickButtons.None || Program.Butt == JoystickButtons.Center) {; ; }
                    switch (Program.Butt)
                    {

                        case JoystickButtons.Up:
                            server.SendData(SendAttack(MonsterList[Gui.ownMonsterId], 0));
                            break;
                        case JoystickButtons.Down:
                            server.SendData(SendAttack(MonsterList[Gui.ownMonsterId], 1));
                            break;
                        case JoystickButtons.Left:
                            server.SendData(SendAttack(MonsterList[Gui.ownMonsterId], 2));
                            break;
                        case JoystickButtons.Right:
                            server.SendData(SendAttack(MonsterList[Gui.ownMonsterId], 3));
                            break;
                        default:
                            server.SendData(SendAttack(MonsterList[Gui.ownMonsterId], 0));
                            break;
                    } 
                    //server.SendData( own HP, Att Value, Att Name, successRate );

                    //gui.UpdateGameScreen();
                }
                else
                {
                    Program.Butt = JoystickButtons.None;
                    Thread.Sleep(1000);

                    while (Program.Butt == JoystickButtons.None || Program.Butt == JoystickButtons.Center) {}
                    switch (Program.Butt)
                    {

                        case JoystickButtons.Up:
                            client.SendData(SendAttack(MonsterList[Gui.ownMonsterId], 0));
                            break;
                        case JoystickButtons.Down:
                            client.SendData(SendAttack(MonsterList[Gui.ownMonsterId], 1));
                            break;
                        case JoystickButtons.Left:
                            client.SendData(SendAttack(MonsterList[Gui.ownMonsterId], 2));
                            break;
                        case JoystickButtons.Right:
                            client.SendData(SendAttack(MonsterList[Gui.ownMonsterId], 3));
                            break;
                        default:
                            client.SendData(SendAttack(MonsterList[Gui.ownMonsterId], 0));
                            break;
                    } //server.SendData( own HP, Att Value, Att Name, successRate );
                    convertData(gui, client.ReceiveData(), MonsterList[Gui.ownMonsterId]);

                    //string HP, Att Value, Att Name = client.ReceiveData();
                    //gui.UpdateGameScreen();
                }
            }

            Thread.Sleep(3000);
            if (Gui.server)
            {
                server.EndServer();
            }
            else
            {
                client.EndCient();
            }
            gui.PrintEndScreen(MonsterList[Gui.ownMonsterId].HP);  // mit [Center] -> return Sartscreen
        }
    }
}
