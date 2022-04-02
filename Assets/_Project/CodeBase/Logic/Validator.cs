namespace _Project.CodeBase.Logic
{
    public class Validator
    {
        public bool Validate(string text, out int num)
        {
            num = 0;
            if (!int.TryParse(text, out var x)) return false;
            if (x < 2 || (x & (x - 1)) != 0) return false;
            num = x;
            return true;
        }
    }
}