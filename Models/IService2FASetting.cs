namespace _2FAService.Models
{
    public interface IService2FASetting
    {

        TimeSpan CodeValidityDuration { get; set; }
        int CodeNumbersCount { get; set; }
        int NumberOfLettersInCode { get; set; }
        int NumberOfCodeRequestPerNumber { get; set; }
        string StoragePath { get; set; }
    }


}
