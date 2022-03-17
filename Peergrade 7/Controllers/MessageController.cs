using Microsoft.AspNetCore.Mvc;
using Peergrade_7.Intruments;
using Peergrade_7.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Peergrade_7.Controllers
{
    /// <summary>
    /// Класс контроллера.
    /// </summary>
    [Route("/api/[controller]")]
    public class MessageController : Controller
    {
        private static List<User> _users = new();
        private static List<Mail> _mails = new();

        private const string MailPath = "Mails.json";
        private const string UserPath = "Users.json";

        private static readonly Random Random = new Random();


        /// <summary>
        /// Генерация пользователей.
        /// </summary>
        /// <returns>Состояние вызова</returns>
        [HttpPost("Inititialize users")]
        public IActionResult Post()
        {
            try
            {
                InitializeUsers();
                Serializer.SerializeData(_users, UserPath);
                Serializer.SerializeData(_mails, MailPath);
                return Ok(_users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Получение информации о всех пользователях.
        /// </summary>
        /// <returns>Список пользователей</returns>
        [HttpGet("Get all users")]
        public IEnumerable<User> GetAllUsers() => _users;


        /// <summary>
        /// Получение информации о пользователе по адресу почты.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Состояние вызова</returns>
        [HttpGet("Get by email", Name = "GetF")]
        public IActionResult GetUserByEmail([Required] string email)
        {
            var user = _users.SingleOrDefault(person => person.Email.Equals(email));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        /// <summary>
        /// Получение списка всех писем.
        /// </summary>
        /// <returns>Состояние вызова</returns>
        [HttpGet("Get all messages")]
        public IEnumerable<Mail> GetMails() => _mails;

        /// <summary>
        /// Получение списка сообщений из диалога двух пользователей.
        /// </summary>
        /// <param name="senderId">id отправителя</param>
        /// <param name="receiverId">id получателя</param>
        /// <returns>Список сообщений</returns>
        [HttpGet("Get by SenderId and ReceiverId")]
        public IActionResult GetBySenderAndReceiverId([Required] string senderId,
            [Required] string receiverId)
        {
            var listOfMails = new List<Mail>();

            foreach (var mail in _mails)
            {
                if (mail.ReceiverId.Equals(receiverId) && mail.SenderId.Equals(senderId))
                {
                    listOfMails.Add(mail);
                }
            }

            if (listOfMails.Count == 0)
            {
                return NotFound();
            }

            return Ok(listOfMails);
        }

        /// <summary>
        /// Получение всех писем отправителя.
        /// </summary>
        /// <param name="senderId">id отправителя</param>
        /// <returns>Список сообщений</returns>
        [HttpGet("Get By SenderId")]
        public IActionResult GetBySenderId([Required] string senderId)
        {
            var listOfMails = new List<Mail>();

            foreach (var mail in _mails)
            {
                if (mail.SenderId.Equals(senderId))
                {
                    listOfMails.Add(mail);
                }
            }

            if (listOfMails.Count == 0)
            {
                return NotFound();
            }

            return Ok(listOfMails);
        }

        /// <summary>
        /// Получение сообщений получателя.
        /// </summary>
        /// <param name="receiverId">id получателя</param>
        /// <returns>Список  сообшений</returns>
        [HttpGet("Get By ReceiverId")]
        public IActionResult GetByReceiverId([Required] string receiverId)
        {
            var listOfMails = new List<Mail>();

            foreach (var mail in _mails)
            {
                if (mail.ReceiverId.Equals(receiverId))
                {
                    listOfMails.Add(mail);
                }
            }

            if (listOfMails.Count == 0)
            {
                return NotFound();
            }

            return Ok(listOfMails);
        }

        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="email">Почта пользователя</param>
        /// <returns>Статус вызова</returns>
        [HttpPost("Add new user")]
        public IActionResult PostNewUser([Required] string userName, [Required] string email)
        {
            try
            {
                var newUser = new User(userName, email);
                if (userName == null || email == null)
                {
                    return NoContent();
                }

                _users.Add(newUser);
                GenerateMails(newUser);
                Serializer.SerializeData(_users, UserPath);
                Serializer.SerializeData(_mails, MailPath);
                return Ok(newUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Добавление нового сообщения.
        /// </summary>
        /// <param name="subject">Тема письма</param>
        /// <param name="message">Содержание сообщения</param>
        /// <param name="senderId">id отправителя</param>
        /// <param name="receiverId">id получателя</param>
        /// <returns>Статус вызова</returns>
        [HttpPost("Add new message")]
        public IActionResult PostNewMessage([Required] string subject, [Required] string message,
            [Required] string senderId, [Required] string receiverId)
        {
            try
            {
                var newMessage = new Mail(subject, message, senderId, receiverId);
                if (subject == null || message == null || senderId == null || receiverId == null)
                {
                    return NoContent();
                }

                if (!_users.Contains(_users.FirstOrDefault(person => person.Email.Equals(senderId))) ||
                    !_users.Contains(_users.FirstOrDefault(person => person.Email.Equals(receiverId))))
                {
                    return BadRequest();
                }

                _mails.Add(newMessage);
                Serializer.SerializeData(_mails, MailPath);
                return Ok(newMessage);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Получение определенного кол-ва пользователей.
        /// </summary>
        /// <param name="offset">Начальный индекс</param>
        /// <param name="limit">Кол-во пользователей для вывода</param>
        /// <returns>Список пользователей из выбранного интервала</returns>
        [HttpGet("Get users partly")]
        public IActionResult GetUsersPartly([Required] int offset, [Required] int limit)
        {
            List<User> partOfUsers = new List<User>();
            if (offset < 0 || limit <= 0)
            {
                return BadRequest();
            }

            for (int i = offset; i < offset + limit; i++)
            {
                if (i >= _users.Count)
                {
                    break;
                }

                partOfUsers.Add(_users[i]);
            }

            return Ok(partOfUsers);
        }

        /// <summary>
        /// Считывание данных о пользователях и их сообщениях из
        /// файла .json.
        /// </summary>
        /// <returns>Статус вызова</returns>
        [HttpGet("Get data from file")]
        public IActionResult GetDataFromFile()
        {
            try
            {
                Serializer.DeserializeData(ref _users, ref _mails);
                return Ok(_users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Иницииализация пользователей.
        /// </summary>
        private void InitializeUsers()
        {
            _users.Clear();
            var length = Random.Next(5, 11);
            for (int index = 0; index < length; index++)
            {
                _users.Add(new User(TextGenerator.GenerateUserData().Item1, TextGenerator.GenerateUserData().Item2));
            }

            for (int indexOfUser = 0; indexOfUser < _users.Count; indexOfUser++)
            {
                GenerateMails(_users[indexOfUser]);
            }
        }

        /// <summary>
        /// Генерация писем пользователя.
        /// </summary>
        /// <param name="user">ВЫбранный пользователь</param>
        private void GenerateMails(User user)
        {
            var length = Random.Next(2, 6);

            for (int indexOfMail = 0; indexOfMail < length; indexOfMail++)
            {
                _mails.Add(new Mail(TextGenerator.GenerateMessageText(), TextGenerator.GenerateMessageText(),
                    _users[Random.Next(0, _users.Count)].Email, user.Email));
            }
        }
    }
}