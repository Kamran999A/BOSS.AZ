using System;
using System.Text.RegularExpressions;
using Bossaz.ConsoleInterface;
using Bossaz.Entities;
using Bossaz.Enums;
using Bossaz.Exceptions;
using Bossaz.Logger;

namespace Bossaz.Helpers
{
    public static class UserHelper
    {
        public static string GetString(string errorMessage)
        {

            while (true)
            {
                Console.Write(">> ");
                var str = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(str))
                    return str;
                LoggerPublisher.OnLogError(errorMessage);
            }
        }
        public static dynamic GetNumeric(NumericTypes type)
        {
            switch (type)
            {
                case NumericTypes.INT:
                    {
                        while (true)
                        {
                            try
                            {
                                Console.Write(">> ");
                                var data = Convert.ToInt32(Console.ReadLine());
                                return data;
                            }
                            catch (Exception)
                            {
                                LoggerPublisher.OnLogError("Input must be numeric value!");
                            }
                        }
                    }
                case NumericTypes.DOUBLE:
                    {
                        while (true)
                        {
                            try
                            {
                                Console.Write(">> ");
                                var data = Convert.ToDouble(Console.ReadLine());
                                return data;
                            }
                            catch (Exception)
                            {
                                LoggerPublisher.OnLogError("Input must be numeric value!");
                            }
                        }
                    }
                default:
                    {
                        throw new InvalidOperationException("Invalid type!");
                    }
            }
        }
        public static string GenerateCode()
        {
            var random = new Random();

            return random.Next(1000, 9999).ToString();
        }
        public static void ValidateMail(string mail)
        {
            var mailComponents = mail.Split('@');
            if (mailComponents.Length == 2 && mailComponents[1].Contains("."))
                return;
            throw new MailException("Invalid mail format");
        }

        public static void ValidatePhone(string phone)
        {
            foreach (var character in phone)
            {
                if (char.IsLetter(character))
                    throw new InvalidPhoneFormat("Phone number can not contain any letter!");
            }
        }
        public static void ValidateLink(string link)
        {
            if (link.StartsWith("https://"))
                return;

            throw new InvalidLinkFormat("Link format is not valid!");
        }

        public static Guid InputGuid()
        {


            Console.WriteLine("Enter guid: ");
            while (true)
            {
                var str = GetString("Guid can not be empty!");

                if (Guid.TryParse(str, out Guid guid))
                    return guid;


                LoggerPublisher.OnLogError("Invalid guid format!");
            }

        }



        public static string InputCategory()
        {
            Console.Clear();
            Console.WriteLine("Category:");
            ConsoleScreen.PrintMenu(FileHelper.Categories, ConsoleColor.Blue);

            return FileHelper.Categories[ConsoleScreen.Input(FileHelper.Categories.Count) - 1];
        }

        public static string InputRegion()
        {
            Console.Clear();
            Console.WriteLine("Region:");
            ConsoleScreen.PrintMenu(FileHelper.Regions, ConsoleColor.Blue);

            return FileHelper.Regions[ConsoleScreen.Input(FileHelper.Regions.Count) - 1];
        }

        public static string InputEducation()
        {
            Console.Clear();
            Console.WriteLine("Education:");
            ConsoleScreen.PrintMenu(FileHelper.Educations, ConsoleColor.Blue);

            return FileHelper.Educations[ConsoleScreen.Input(FileHelper.Educations.Count) - 1];
        }

        public static string InputExperience()
        {
            Console.Clear();
            Console.WriteLine("Experience:");
            ConsoleScreen.PrintMenu(FileHelper.Experiences, ConsoleColor.Blue);

            return FileHelper.Experiences[ConsoleScreen.Input(FileHelper.Experiences.Count) - 1];
        }

        public static string InputSalary()
        {
            Console.Clear();
            Console.WriteLine("Salary: ");
            ConsoleScreen.PrintMenu(FileHelper.Salaries, ConsoleColor.Blue);

            return FileHelper.Salaries[ConsoleScreen.Input(FileHelper.Salaries.Count) - 1];
        }

        public static SalaryRange InputSalaryRange()
        {
            var salaryRange = new SalaryRange();

            Console.WriteLine("Salary range:");

            Console.WriteLine("From: ");
            salaryRange.From = GetNumeric(NumericTypes.INT);

            while (true)
            {
                Console.WriteLine("To: ");
                salaryRange.To = GetNumeric(NumericTypes.INT);

                if (salaryRange.To >= salaryRange.From)
                    break;


                LoggerPublisher.OnLogError("Max salary must be greater than min!");
            }

            return salaryRange;
        }

        public static int ParseSalary(string data)
        {
            return Convert.ToInt32(Regex.Match(data, @"\d+").Value);
        }
    }
}