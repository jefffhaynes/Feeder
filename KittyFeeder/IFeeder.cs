using System;
using System.Threading.Tasks;

namespace KittyFeeder
{
	public interface IFeeder
	{
		Task Feed();
	}
}

