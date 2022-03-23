using System;

namespace WebApp.Instruments
{
    /// <summary>
    /// Класс генерации текста для имен пользователей и их сообщений.
    /// </summary>
    public static class TextGenerator
    {
        private static readonly Random Random = new Random();


        /// <summary>
        /// Метод генерации текста для сообщений.
        /// </summary>
        /// <returns>Сгенерированный текст</returns>
        public static string GenerateMessageText()
        {
            string text = "";

            for (int i = 0; i < Random.Next(5, 11); i++)
            {
                text += (char) Random.Next('a', 'z' + 1);
            }

            return text;
        }

        /// <summary>
        /// Метод генерации данных пользователя.
        /// </summary>
        /// <returns>Сгенерированное имя пользователя и его почтовый адрес</returns>
        public static (string, string) GenerateUserData()
        {
            return (GenerateUserName(), GenerateUserName() + "@gmail.com");
        }

        private static string GenerateUserName()
        {
            string userName = "";

            for (int i = 0; i < 5; i++)
            {
                if (0 <= i && i < 2)
                {
                    userName += (char) Random.Next('a', 'z' + 1);
                }
                else
                {
                    userName += Random.Next(0, 10);
                }
            }

            return userName;
        }
    }
}