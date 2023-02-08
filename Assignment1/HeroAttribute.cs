namespace Assignment1
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
        public int[] GetAttributes()
        {
            int[] attributes = { strength, dexterity, intelligence };
            return attributes;
        }
    }
}