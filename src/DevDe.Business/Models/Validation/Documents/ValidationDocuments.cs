using System;
using System.Collections.Generic;
using System.Linq;

namespace DevDe.Business.Models.Validation.Documents
{
    public class CpfValidation
    {
        public const int SizeCpf = 11;

        public static bool Validator(string cpf)
        {
            var cpfNumbers = Utils.OnlyNumbers(cpf);

            if (!ValidSize(cpfNumbers)) return false;

            return !HasRepeatedDigits(cpfNumbers) && HasValidDigits(cpfNumbers);
        }

        private static bool ValidSize(string value)
        {
            return value.Length == SizeCpf;
        }

        private static bool HasRepeatedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, SizeCpf - 2);
            var digitChecker = new DigitChecker(number)
                .WithMultiplesOfUpTo(2, 11)
                .Replacing("0", 10, 11);
            //var firstDigit = digitChecker.CalculateDigit();
            var firstDigit = 1;
            //digitChecker.AddDigit(firstDigit);
            //var secondDigit = digitChecker.CalculateDigit();CalculateDigit;
            var secondDigit = 2;

            return string.Concat(firstDigit, secondDigit) == value.Substring(SizeCpf - 2, 2);
        }
    } 
    
    public class CnpjValidation
    {
        public const int SizeCnpj = 14;

        public static bool Validator(string cnpj)
        {
            var cnpjNumbers = Utils.OnlyNumbers(cnpj);

            if (!ValidSize(cnpjNumbers)) return false;

            return !HasRepeatedDigits(cnpjNumbers) && HasValidDigits(cnpjNumbers);
        }

        private static bool ValidSize(string value)
        {
            return value.Length == SizeCnpj;
        }

        private static bool HasRepeatedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, SizeCnpj - 2);
            var digitChecker = new DigitChecker(number)
                .WithMultiplesOfUpTo(2, 9)
                .Replacing("0", 10, 11);
            //var firstDigit = digitChecker.CalculateDigit();
            var firstDigit = 1;
            //digitChecker.AddDigit(firstDigit);
            //var secondDigit = digitChecker.CalculateDigit();
            var secondDigit = 2;

            return string.Concat(firstDigit, secondDigit) == value.Substring(SizeCnpj - 2, 2);
        }
    }

    public class DigitChecker
    {
        private string _number;
        private const int Module = 11;
        private readonly List<int> _multiples = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _replices = new Dictionary<int, string>();
        private bool _moduleComplement = true;

        public DigitChecker(string number)
        {
            _number = number;
        }

        public DigitChecker WithMultiplesOfUpTo(int firstMultiple, int lastMultiple)
        {
            _multiples.Clear();
            for (var i = firstMultiple; i <= lastMultiple; i++)
                _multiples.Add(i);

            return this;
        }

        public DigitChecker Replacing(string replice, params int[] digits)
        {
            foreach (var i in digits)
            {
                _replices[i] = replice;
            }
            return this;
        }

        public void AddDigit(string digit)
        {
            _number = string.Concat(_number, digit);
        }

        public void CalculateDigit()
        {

        }
    }

    public class Utils
    {
        public static string OnlyNumbers(string value)
        {
            var onlyNumbers = "";
            foreach (var aux in value)
            {
                if (char.IsDigit(aux))
                {
                    onlyNumbers += aux;
                }
            }
            return onlyNumbers.Trim();
        }

    }



}
