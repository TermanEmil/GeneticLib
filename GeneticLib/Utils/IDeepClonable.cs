using System;
namespace GeneticLib.Utils
{
	public interface IDeepClonable<out T>
    {
		T Clone();
    }
}
