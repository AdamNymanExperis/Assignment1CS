namespace ExpansionPack.Helper
{
    // The reasoning with this class is to take out Random() from the classes to make it possible to mock random in testing 
    public interface IRandom
    {
        public int Next(int maxValue);
    }
}