using System.Runtime.Serialization;

namespace WebApp.Models
{
    /// <summary>
    /// Класс Mail.
    /// </summary>
    [DataContract]
    public class Mail
    {
        /// <summary>
        /// Тема сообщения.
        /// </summary>
        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Содержание сообщения.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Id отправителя.
        /// </summary>
        [DataMember(Name = "senderId")]
        public string SenderId { get; set; }

        /// <summary>
        /// Id получателя.
        /// </summary>
        [DataMember(Name = "receiverId")]
        public string ReceiverId { get; set; }

        /// <summary>
        /// Конструктор класса Mail.
        /// </summary>
        /// <param name="subject">Темма сообщения</param>
        /// <param name="message">Содержание сообщения</param>
        /// <param name="senderId">id отправителя</param>
        /// <param name="receiverId">id получателя</param>
        public Mail(string subject, string message, string senderId, string receiverId)
        {
            Subject = subject;
            Message = message;
            SenderId = senderId;
            ReceiverId = receiverId;
        }
    }
}
