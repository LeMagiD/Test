using System.Collections.Generic;

namespace Tamon_Testat
{
    public class Monster
    {
        public int HP { get; set; }
        public  string Name { get; private set; }
        private int StartHp { get; }    // TODO - brauchts wahrscheinlich nicht
        public  Element Element { get; private set; }    // probably not used, since it is not used in the calculations
        public List<Attack> Moves { get; private set; }


        //Konstruktor
        public Monster(string name, Element element, int hp, List<Attack> moves)
        {
            HP = hp;
            Element = element;
            Name = name;
            Moves = moves;
        }







    }
}
