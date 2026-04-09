using System;

namespace ConsoleApp46
{

    /// <summary>
    /// исключение, которое возникает при использовании недопустимого ключа
    /// </summary>
    public class InvalidKeyException : Exception
    {

        /// <summary>
        /// создает новый экземпляр класса InvalidKeyException с заданным сообщением об ошибке
        /// </summary>
        /// <param name="message">сообщение об ошибке</param>
        public InvalidKeyException(string message) : base(message)
        {
        }
    }
}
