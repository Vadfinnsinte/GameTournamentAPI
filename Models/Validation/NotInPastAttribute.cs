using System.ComponentModel.DataAnnotations;

namespace GameTournamentAPI.Models.Validation
{
    public class NotInPastAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not DateTime date)
                return false;

            return date >= DateTime.Now;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} can not be in the past.";
        }
    }
}
