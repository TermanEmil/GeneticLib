namespace GeneticLib.Randomness
{
    public interface IRandom
    {
        int Next();
        int Next(int maxValue);
        int Next(int minValue, int maxValue);
        
        double NextDouble();
		double NextDouble(double minValue, double maxValue);
    }
}