namespace _Project.CodeBase.Logic
{
    public class Validator
    {
        public bool Validate(string text, out int num)
        {
            num = 0;
            if (!int.TryParse(text, out var number)) return false;
            if (number <= 0 || number % 2 != 0) return false;
            num = number;
            return true;
        }
    }
}