namespace _2FAService.Models
{
    public class Service2FAsetting : IService2FASetting
    {
        public TimeSpan CodeValidityDuration { get; set; }
        public int CodeNumbersCount { get; set; }
        public int NumberOfLettersInCode { get; set; }
        public int NumberOfCodeRequestPerNumber { get; set; }
        public string StoragePath { get; set; }
    }


}
