using SocialNW.BLL.Exceptions;
using SocialNW.BLL.Models;
using SocialNW.DAL.Entities;
using SocialNW.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNW.BLL.Services
{
    public class MessageService
    {
        IMessageRepository messageRepository;
        IUserRepository userRepository;
        public MessageService()
        {
            userRepository = new UserRepository();
            messageRepository = new MessageRepository();
        }

        public IEnumerable<Message> GetIncomingMessagesByUserId(int recipientId)
        {
            var messages = new List<Message>();

            messageRepository.FindByRecipientId(recipientId).ToList().ForEach(m =>
            {
                var senderUserEntity = userRepository.FindById(m.sender_id);
                var recipientUserEntity = userRepository.FindById(m.recipient_id);

                messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
            });

            return messages;
        }

        public IEnumerable<Message> GetOutcomingMessagesByUserId(int senderId)
        {
            var messages = new List<Message>();

            messageRepository.FindBySenderId(senderId).ToList().ForEach(m =>
            {
                var senderUserEntity = userRepository.FindById(m.sender_id);
                var recipientUserEntity = userRepository.FindById(m.recipient_id);

                messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
            });

            return messages;
        }

        public void SendMessage(MessageSendingData messageSendingData)
        {
            if (String.IsNullOrEmpty(messageSendingData.Content))
                throw new ArgumentNullException();

            if (messageSendingData.Content.Length > 5000)
                throw new ArgumentOutOfRangeException();

            var findUserEntity = this.userRepository.FindByEmail(messageSendingData.RecipientEmail);
            if (findUserEntity is null) throw new UserNotFoundException();

            var messageEntity = new MessageEntity()
            {
                content = messageSendingData.Content,
                sender_id = messageSendingData.SenderId,
                recipient_id = findUserEntity.id
            };

            if (this.messageRepository.Create(messageEntity) == 0)
                throw new Exception();
        }
    }
}
