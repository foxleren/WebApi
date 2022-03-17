using Peergrade_7.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace Peergrade_7.Intruments
{
    /// <summary>
    /// Класс, реализующий сериализацию и десериализацию.
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Определение типа данных и вызов метода сериализации.
        /// </summary>
        /// <param name="objects">Список объектов для сериализации</param>
        /// <param name="path">Путь к файлу .json</param>
        /// <typeparam name="T">Тип принимаемых данных</typeparam>
        public static void SerializeData<T>(List<T> objects, string path)
        {
            try
            {
                if (objects[0] is User)
                {
                    List<User> list = (objects).Cast<User>().ToList();
                    list.Sort();
                    Serialize(list);
                }
                else
                {
                    List<Mail> list = (objects).Cast<Mail>().ToList();
                    Serialize(list);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Метод сериализации.
        /// </summary>
        /// <param name="list">Список объектов для сериализации</param>
        /// <typeparam name="T">Тип принимаемых данных</typeparam>
        private static void Serialize<T>(List<T> list)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<User>));

                using FileStream fs =
                    new FileStream(Environment.CurrentDirectory + $"{Path.DirectorySeparatorChar}Users.json",
                        FileMode.OpenOrCreate);
                serializer.WriteObject(fs, list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Метод десериализации.
        /// </summary>
        /// <param name="listOfUsers">Список пользователей</param>
        /// <param name="listOfMails">Список сообщений</param>
        public static void DeserializeData(ref List<User> listOfUsers, ref List<Mail> listOfMails)
        {
            using (var fs = new FileStream(
                       Environment.CurrentDirectory + $"{Path.DirectorySeparatorChar}Mails.json",
                       FileMode.Open, FileAccess.Read))
            {
                var mailFormatter = new DataContractJsonSerializer(typeof(List<Mail>));
                listOfMails = (List<Mail>) mailFormatter.ReadObject(fs);
            }

            using (var fs = new FileStream(
                       Environment.CurrentDirectory + $"{Path.DirectorySeparatorChar}Users.json", FileMode.Open,
                       FileAccess.Read))
            {
                var userFormatter = new DataContractJsonSerializer(typeof(List<User>));
                listOfUsers = (List<User>) userFormatter.ReadObject(fs);
            }

            if (listOfUsers != null)
            {
                listOfUsers.Sort((x, y) => String.Compare(x.Email, y.Email, StringComparison.Ordinal));
            }
        }
    }
}