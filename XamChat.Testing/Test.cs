using NUnit.Framework;
using System;
using Infrastructure.ServiceLocator;
using XamChat.Service;
namespace XamChat.Testing
{
	public static class Test
	{
		
		public static void SetUp()
		{
			ServiceContainer.Instance.Register<IWebService>(() => new FakeWebService());
			ServiceContainer.Instance.Register<ISetting>(() => new FakeSetting());
		}
	}
}

