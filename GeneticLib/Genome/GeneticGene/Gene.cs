using System;

namespace GeneticLib.Genome.GeneticGene
{
	public class Gene : IEquatable<Gene>
	{
		public ICloneable Value { get; protected set; }

		public Gene(ICloneable value)
		{
			Value = value;
		}

		public Gene(Gene other)
		{
			Value = other.Value.Clone() as ICloneable;
		}

		public bool Equals(Gene other)
		{
			return Value.Equals(other.Value);
		}

		public override int GetHashCode()
		{
			if (Value == null)
				return 0;

			return Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = (Gene)obj;
			return Value.Equals(other.Value);
		}

		public override string ToString()
		{
			return Value == null ? String.Empty : "G: " + Value.ToString();
		}
	}
}
