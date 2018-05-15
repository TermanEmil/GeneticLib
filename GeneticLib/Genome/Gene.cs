using System;

namespace GeneticLib.Genome
{
	public struct Gene : IEquatable<Gene>
	{
		public object Value { get; private set; }

		public Gene(object value)
		{
			Value = value;
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

		public static bool operator ==(Gene first, Gene second)
		{
			return first.Equals(second);
		}

		public static bool operator !=(Gene first, Gene second)
		{
			return !(first == second);
		}
	}
}
