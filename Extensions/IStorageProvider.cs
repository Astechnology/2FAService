namespace _2FAService.Extensions
{
    public interface IStorageProvider
    {
        void Save(List<AuthModel> model);
        List<AuthModel> ReadAll();
    }
}
