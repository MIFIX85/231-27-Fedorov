using System;
using System.Text.RegularExpressions;
using StudentManager.Models;

namespace StudentManager.Utilities
{
    /// <summary>
    /// Класс для валидации данных студента
    /// </summary>
    public static class Validators
    {
        private const string EmailPattern = @"^[a-zA-Z0-9._%+-]{3,}@(yandex.ru|gmail.com|icloud.com)$";
        private const string PhonePattern = @"^\+7\d{3}\d{3}\d{2}\d{2}$";
        private static readonly DateTime MinBirthDate = new DateTime(1991, 12, 25);

        /// <summary>
        /// Проверяет корректность данных студента
        /// </summary>
        /// <param name="student">Объект студента для валидации</param>
        /// <exception cref="ArgumentException">Выбрасывается при некорректных данных</exception>
        public static void ValidateStudent(Student student)
        {
            ValidateSurname(student.Surname);
            ValidateBirthDate(student.BirthDate);
            ValidateEmail(student.Email);
            ValidatePhone(student.Phone);
        }

        private static void ValidateSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Фамилия обязательна");
        }

        private static void ValidateBirthDate(DateTime birthDate)
        {
            if (birthDate < MinBirthDate || birthDate > DateTime.Now)
                throw new ArgumentException("Некорректная дата рождения");
        }

        private static void ValidateEmail(string email)
        {
            if (!Regex.IsMatch(email, EmailPattern))
                throw new ArgumentException("Некорректный email");
        }

        private static void ValidatePhone(string phone)
        {
            if (!Regex.IsMatch(phone, PhonePattern))
                throw new ArgumentException("Некорректный телефон");
        }
    }
}