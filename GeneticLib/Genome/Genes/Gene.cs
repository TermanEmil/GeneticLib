using System;
using GeneticLib.Utils;

namespace GeneticLib.Genome.Genes
{
	public class Gene : IEquatable<Gene>
	{
		public IDeepClonable<object> Value { get; protected set; }
		public virtual bool ExposedToMutations => true;

		public Gene(IDeepClonable<object> value)
		{
			this.Value = value;
		}

		public Gene(Gene other)
		{
			this.Value = other.Value.Clone() as IDeepClonable<object>;
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
