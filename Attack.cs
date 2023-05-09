namespace Tamon_Testat
{
    public class Attack
    {
        public Element Element { get; }
        public int SuccessRate { get; }
        public string Name { get; }
        public int Damage { get; }


        public Attack(int damage, Element element, int successRate, string name)
        {
            Damage = damage;
            Element = element;
            SuccessRate = successRate;
            Name = name;
        }


    }





}
