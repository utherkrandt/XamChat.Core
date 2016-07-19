using System;
using System.Threading.Tasks;
using XamChat.Data;

namespace XamChat.Service
{
	public interface IWebService
	{
		Task<User> Login (string username, string password);
		Task<User> Register(User user);
		Task<User[]> GetFriends (long userId);
		Task<User> AddFriend(User user);
		Task<Conversation[]> GetConversation (long userId);
		Task<Message[]> GetMessages(long conversationId);
		Task<Message> SendMessage (Message message);
		Task<User[]> GetFriendsNotInConversation(long userId);
	}
}

