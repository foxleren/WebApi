using System.Runtime.Serialization;

namespace Peergrade_7.Models
{
    [DataContract]
    public class Mail
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "senderId")]
        public string SenderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "receiverId")]
        public string ReceiverId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="senderId"></param>
        /// <param name="receiverId"></param>
        public Mail(string subject, string message, string senderId, string receiverId)
        {
            Subject = subject;
            Message = message;
            SenderId = senderId;
            ReceiverId = receiverId;
        }
    }
}
