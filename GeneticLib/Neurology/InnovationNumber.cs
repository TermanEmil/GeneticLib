using System;
namespace GeneticLib.Neurology
{
	public struct InnovationNumber
    {
		private int value;

		public static implicit operator InnovationNumber(int val)
		{
			return new InnovationNumber { value = val };
		}

		public static implicit operator int(InnovationNumber innovationNumber)
		{
			return innovationNumber.value;
		}

		public override string ToString()
		{
			return value.ToString();
		}
	}
}
