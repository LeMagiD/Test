using System;
using System.Collections.Generic;
using System.Threading;

namespace Tamon_Testat
{
    public class Game
    {
        public static string[] MonsterNames = { "Bob", "Stefan", "Ueli", "Ruedi" };
        public List<Monster> MonsterList { get; set; }
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
            MonsterList.Add(new Monster(MonsterNames[0], Element.normal, 100, NormalAttacks)); 
            MonsterList.Add(new Monster(MonsterNames[1], Element.fire, 120, FireAttacks));
            MonsterList.Add(new Monster(MonsterNames[2], Element.water, 120, WaterAttacks));
            MonsterList.Add(new Monster(MonsterNames[3], Element.grass, 120, GrassAttacks));
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
            if (new Random().Next(0, 101) > 95) return dmg * 5; // 5 times the damage if critical hit (5% chance)
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
            if (new Random().Next(0, 101) > 95) return damage * 5; // 5 times the damage if critical hit (5% chance)
            return damage;
        }

        private int CalculateHp(Monster monster, int dmg, int sRate)
        {
            monster.HP -= CalculateDmg(dmg, sRate);
            if (monster.HP <= 0)
            {
                monster.HP = 0;
                Console.WriteLine("your HP reached 0! you lost!");
                return monster.HP;
                // TODO - Streamwriter (oder so) wenn verloren, danach direkt beenden/wieder zum start.
                // womöglich auch ein boolschen Wert zurückgeben, welches aussagt das verloren
            }
            return monster.HP;
        }
        private void convertData(string recStr, Monster monster)
        {
            //  DONE - convert received Data to string, split it into a array and use the integers for the damage and success rate
            /*
             * string s = sr.ReadToEnd();
             * string [] str = s.Split(' ');
             * int damage = Int32.Parse(str[1]);
             * int success = int32.Parse(str[2]);
            */

            //Bsp
            string s = recStr;
            string[] str = s.Split(' ');
            int nr1 = Int32.Parse(str[1]);
            int nr2 = Int32.Parse(str[2]);
            int i = CalculateHp(monster, nr1, nr2);
            Console.WriteLine($"This is a test with the number {i}");
        }
        private void getEnemyTamon(string Id)
        {
            int monsterId = Int32.Parse(Id);
            Monster enemyMonster = MonsterList[monsterId];
            MonsterList.Add(enemyMonster);

            // TODO - change location of code below
/*            Console.WriteLine("Monster Index = " + MonsterList.IndexOf(MonsterList[0]));
            Console.WriteLine("Monster Name = " + MonsterList[0].Name);
            for(int i = 0; i < MonsterList[0].Moves.Count;i++)
            {
                Console.WriteLine($"Attack {i+1} = " + MonsterList[0].Moves[i].Name);
            }
            Console.WriteLine("Monster Moves = " + MonsterList[0].Moves[1].Name);

*/          
// DONE - Name und Hp erhalten
        }
/*        private string convertSentData(int id)
        {
            string s1 = MonsterList[id].Name;
            string s2 = MonsterList[id].HP.ToString();
            string s3 = MonsterList[id].Moves[0].Name;
            string data = s1 +" "+ s2;

            return string.Empty;
        }*/


        public void Run()
        {
            Console.WriteLine("Welcome to Tamon - Table Monsters!");
            convertData("Dmg 10 100 Acc", MonsterList[0]);
            Thread.Sleep(2000);
            getEnemyTamon("0");
            Thread.Sleep(2000);
            Gui gui = new Gui();
            gui.StartScreen();
            //TODO - GUI + TCP
        }

    }
}
