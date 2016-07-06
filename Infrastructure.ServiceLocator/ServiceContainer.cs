using System;
using System.Collections.Generic;

namespace Infrastructure.ServiceLocator
{
	public class ServiceContainer: IServiceContainer
	{
		private static readonly ServiceContainer _Instance = new ServiceContainer();
		public static ServiceContainer Instance 
		{
			get
			{
				return _Instance;
			}
		}

		private static readonly Dictionary<Type,Lazy<object>> _services;

		static ServiceContainer ()
		{
			_services = new Dictionary<Type, Lazy<object>> (); 
		}

		private object ResolveService (Type type)
		{
			Lazy<object> serviceToResolve = null;
			if (_services.TryGetValue (type, out serviceToResolve)) 
			{
				return serviceToResolve.Value;
			} else {
				throw new ArgumentException ("Cannot resolve this service: "+ type.Name);
			}
		}

		#region IServiceContainer implementation
		public T Resolve<T> ()
		{
			return (T)ResolveService (typeof(T));
		}


		public void Register<T> (Func<T> function)
		{
			if (_services.ContainsKey(typeof(T))) 
			{
				throw new InvalidOperationException("This service already registered");
			}
			else
			{
				_services [typeof(T)] = new Lazy<object> (()=>function());
			}
		}

		#endregion
	}
}

