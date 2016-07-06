using System;
using System.Linq;
using XamChat.Data;
using System.Collections.Generic;

namespace XamChat.Service
{
	public static class FakeData
	{
		private static List<User> _users= new List<User>{
			new User {Id = 1, Username = "User 1"},
			new User {Id = 2, Username = "User 2"},
			new User {Id = 3, Username = "User 3"},
			new User {Id = 4, Username = "User 4"},
			new User {Id = 5, Username = "User 5"},

		};

		private static List<Conversation> _conversations= new List<Conversation>{
			new Conversation {Id = 1, UserId = 1,  Username = "User 2"},
			new Conversation {Id = 2, UserId = 2,  Username = "User 1"},
			new Conversation {Id = 3, UserId = 3,  Username = "User 5"},
			new Conversation {Id = 4, UserId = 4,  Username = "User 4"},
			new Conversation {Id = 5, UserId = 5,  Username = "User 3"},
			new Conversation {Id = 6, UserId = 1,  Username = "User 5"},

		};


		private static List<Message> _messages= new List<Message>{
			new Message {Id = 1, ConversationId = 1, UserId = 1,  Username = "User 1", Text ="Text 11"},
			new Message {Id = 2, ConversationId = 1, UserId = 2,  Username = "User 1", Text ="Text 12"},
			new Message {Id = 3, ConversationId = 1, UserId = 1,  Username = "User 1", Text ="Text 13"},
			new Message {Id = 4, ConversationId = 1, UserId = 2,  Username = "User 1", Text ="Text 14"},
			new Message {Id = 5, ConversationId = 1, UserId = 1,  Username = "User 1", Text ="Text 15"},
			new Message {Id = 6, ConversationId = 2,UserId = 2,  Username = "User 2" , Text ="Text 21"},
			new Message {Id = 7, ConversationId = 2,UserId = 2,  Username = "User 2" , Text ="Text 22"},
			new Message {Id = 8, ConversationId = 2,UserId = 2,  Username = "User 2" , Text ="Text 23"},
			new Message {Id = 9, ConversationId = 2,UserId = 2,  Username = "User 2" , Text ="Text 24"},
			new Message {Id = 10, ConversationId = 2,UserId = 2,  Username = "User 2" , Text ="Text 25"},
			new Message {Id = 11, ConversationId = 3,UserId = 3,  Username = "User 3" , Text ="Text 31"},
			new Message {Id = 12, ConversationId = 4,UserId = 4,  Username = "User 4" , Text ="Text 41"},
			new Message {Id = 13, ConversationId = 5,UserId = 5,  Username = "User 5" , Text ="Text 51"},


		};


		static FakeData ()
		{
		}

		public static User GetUser(int i)
		{
			if (i > _users.Count)
				return _users [0];
			else
				return _users[i];
		}

		public static List<User> GetAllUsers()
		{
			return _users;
		}

		public static void AddFriends(User user)
		{
			var id  = _users.Max(x=> x.Id) +1;
			_users.Add(new User{ Id = id, Username = user.Username, Password = user.Password});
		}

		public static List<Conversation> GetAllConversations()
		{
			return _conversations;
		}

		public static List<Message> GetAllMessages()
		{
			return _messages;
		}

		public static List<Conversation> GetConversationByID(long conversatonId)
		{
			return _conversations.Where (x=> x.Id.Equals(conversatonId)).ToList();
		}

		public static List<Conversation> GetConversationByUser(long userId)
		{
			return _conversations.Where (x=> x.UserId.Equals(userId)).ToList();
		}

		public static List<Message> GetMessageByUserAndConversation(long userId, long conversationId)
		{
			return _messages.Where (x=> x.UserId.Equals(userId) && x.ConversationId.Equals(conversationId)).ToList();
		}

				public static Message AddMessage(Message message)
		{
			var id = _messages.Max (x => x.Id)+ 1;
					_messages.Add (new Message
						{
						Id = id, 
						ConversationId = message.ConversationId, 
						UserId = message.UserId, 
						Username = message.Username, 
						Text = message.Text 
						});
					return _messages.First(x=> x.Id.Equals(id));
		}
	}
}

