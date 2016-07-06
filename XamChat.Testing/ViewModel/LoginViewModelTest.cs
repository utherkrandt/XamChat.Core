using System;
using NUnit.Framework;
using XamChat.Service;
using Infrastructure.ServiceLocator;
using XamChat.ViewModels;
using System.Threading.Tasks;

namespace XamChat.Testing.ViewModel
{
	[TestFixture]
	public class LoginViewModelTest
	{
		private ISetting setting;
		private LoginViewModel loginViewModel;

		[SetUp]
		public void SetUp()
		{
			Test.SetUp();
			setting = ServiceContainer.Instance.Resolve<ISetting>();
			loginViewModel = new LoginViewModel();
			                          
		}

		[Test]
		public async Task LoginTest()
		{
			loginViewModel.Username = "TestUsername";
			loginViewModel.Password = "TestPassword";

			await loginViewModel.Login();

			Assert.IsNotNull(setting.User);
			Assert.AreEqual(loginViewModel.Username, setting.User.Username);
			Assert.AreEqual(loginViewModel.Password, setting.User.Password);

		}
	}
}

