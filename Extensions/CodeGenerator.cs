namespace _2FAService.Extensions
{
    public class CodeGenerator
    {
        public static string SmartAuthCode(int numSize = 3, int charSize = 3)
        {
            //const string chars = "092837465ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var nums = "0928374650123456789";
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var finalSize = numSize + charSize;

            var generatedCode = "";
            while (generatedCode.Length < finalSize)
            {
                const string jury = "0010101010101101010011";
                var juryDecision = jury[new Random(Guid.NewGuid().GetHashCode()).Next(jury.Length)];
                // 0: for num | 1: for char

                if (numSize == 0) juryDecision = '1';
                if (charSize == 0) juryDecision = '0';

                if (juryDecision == '0')
                {
                    // take num
                    var numIndex = new Random().Next(nums.Length);
                    generatedCode += nums[numIndex];
                    nums = nums.Remove(numIndex, 1);
                    numSize--;
                }
                else
                {
                    // take char
                    var charIndex = new Random().Next(chars.Length);
                    generatedCode += chars[charIndex];
                    chars = chars.Remove(charIndex, 1);
                    charSize--;
                }
            }

            return generatedCode;
        }

    }
}
