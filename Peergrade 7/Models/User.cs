using System;
using System.Runtime.Serialization;

namespace Peergrade_7.Models
{
    /// <summary>
    /// Класс User.
    /// </summary>
    [DataContract]
    public class User : IComparable<User>
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// Почтовый адрес пользователя.
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Конструктор класса User.
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <param name="email">Почтовый адрес пользователя</param>
        public User(string name, string email)
        {
            UserName = name;
            Email = email;
        }

        /// <summary>
        /// Сравнение пользователей по почтовому адресу.
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public int CompareTo(User user)
        {
            return String.Compare(user.Email, Email, StringComparison.Ordinal);
        }
    }
}