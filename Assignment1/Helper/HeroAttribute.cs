
using Assignment1.Enums;

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

        // returns a dictionary to keep the values encapsulated and still not using magic variables
        public Dictionary<AttributeType, int> GetAttributes()
        {
            var attributes = new Dictionary<AttributeType, int>{
                {AttributeType.Strength, strength},
                {AttributeType.Intelligence, intelligence},
                {AttributeType.Dexterity, dexterity},
            };
            return attributes;
        }

        public static HeroAttribute operator +(HeroAttribute lhs, HeroAttribute rhs)
        {
            return new HeroAttribute { strength = lhs.strength + rhs.strength, dexterity = lhs.dexterity + rhs.dexterity, intelligence = lhs.intelligence + rhs.intelligence };
        }

        public override bool Equals(object obj)
        {
            return obj is HeroAttribute attribute && 
                strength == attribute.strength &&
                dexterity == attribute.dexterity &&
                intelligence == attribute.intelligence;
        }

    }
}