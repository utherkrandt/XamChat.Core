using System;
using System.Collections.Generic;

namespace Infrastructure.ServiceLocator
{
	public interface IServiceContainer
	{
		T Resolve<T>();
		void Register<T> (Func<T> function);
	}
}

