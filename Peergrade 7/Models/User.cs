using System;
using System.Runtime.Serialization;

namespace Peergrade_7.Models
{
    [DataContract]
    public class User : IComparable<User>
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public User(string name, string email)
        {
            UserName = name;
            Email = email;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(User obj)
        {
            return String.Compare(obj.Email, this.Email, StringComparison.Ordinal);
        }
    }
}
