
using System;

namespace TrainingTask.ApplicationCore.Validation
{
    public static class Validator
    {
        public static bool NameIsValid(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            return true;
        }
        public static bool LengthIsValid(string str, int maxLength)
        {
            if (str == null || str.Length > maxLength)
                return false;
            return true;
        }

        public static bool DateIsValid(DateTime date)
        {
            if (date == null)
                return false;
            return true;
        }
    }
}
