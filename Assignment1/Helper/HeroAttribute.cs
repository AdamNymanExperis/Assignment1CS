namespace Assignment1.Helper
{
    public class HeroAttribute
    {
        private int strength;
        private int dexterity;
        private int intelligence;

        public HeroAttribute()
        {
            strength = 0;
            dexterity = 0;
            intelligence = 0;
        }

        public HeroAttribute(int strength, int dexterity, int intelligence)
        {
            this.strength = strength;
            this.dexterity = dexterity;
            this.intelligence = intelligence;
        }

        public int[] GetAttributes()
        {
            int[] attributes = { strength, dexterity, intelligence };
            return attributes;
        }

        public static HeroAttribute operator +(HeroAttribute lhs, HeroAttribute rhs)
        {
            return new HeroAttribute { strength = lhs.strength + rhs.strength, dexterity = lhs.dexterity + rhs.dexterity, intelligence = lhs.intelligence + rhs.intelligence };
        }

    }
}