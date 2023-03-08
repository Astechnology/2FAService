using Newtonsoft.Json;

namespace _2FAService.Extensions
{
    /// <summary>
    /// File Storage Provider
    /// </summary>
    public class FileStorageProvider : IStorageProvider
    {
        private string _fullPath = "";
        public FileStorageProvider(string path)
        {
            _fullPath = path;
            //check if the path exist 
            if (!File.Exists(_fullPath))
            {
                File.Create(_fullPath).Close();
            }
        }
        /// <summary>
        /// return the json serialized content 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public List<AuthModel> ReadAll()
        {
            if (File.Exists(_fullPath))
            {
                var content = File.ReadAllText(_fullPath);
                if (string.IsNullOrEmpty(content))
                    return new List<AuthModel>();

                //if the content have been encrypted,
                //be sure that you have decrypt it before cast to the list of model 

                var data = JsonConvert.DeserializeObject<List<AuthModel>>(content);

                return data;
            }
            else
                throw new FileNotFoundException(_fullPath);
        }

        public void Save(List<AuthModel> model)
        {
            var content = JsonConvert.SerializeObject(model);
            // you can encrypt the content if need before save in the file
            File.WriteAllText(_fullPath, content);
        }
    }
}
