using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using XamChat.Data;

namespace XamChat.Service
{
	public class FakeWebService:IWebService
	{
		public int SleepDuration {
			get;
			set;
		}



		public FakeWebService ()
		{
			SleepDuration = 1;
		}

		private Task Sleep()
		{
			return Task.Delay (SleepDuration);
		}

		private Task Sleep(int duration)
		{
			return Task.Delay (duration);
		}

		public async Task<User> Login (string username, string password)
		{
			await Sleep ();
			return FakeData.GetAllUsers().FirstOrDefault(x => x.Username.Equals(username));
		}

		public async Task<User> Register(User user)
		{
			await Sleep (2);
			return user;
		}

		public async Task<User[]> GetFriends (long userId)
		{
			await Sleep (2);
			return FakeData.GetAllUsers().Where(x=> !x.Id.Equals(userId)).ToArray();
		}

		public async Task<User> AddFriend(User user)
		{
			await Sleep (2);
			FakeData.AddFriends (user);
			return FakeData.GetAllUsers ().First (x => x.Username.Equals (user.Username));
		}

		public async Task<Conversation[]> GetConversation (long userId)
		{
			await Sleep(2);
			return FakeData.GetConversationByUser (userId).ToArray();

		}

		public async Task<Message[]> GetMessages(long conversationId)
		{
			await Sleep (3);
			return FakeData.GetAllMessages().Where(x=> x.ConversationId.Equals(conversationId)).ToArray();
		}

		public async Task<Message> SendMessage (Message message)
		{
			await Sleep (1);		 
			return FakeData.AddMessage (message);

		}


	}
}

