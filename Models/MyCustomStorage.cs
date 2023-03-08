using _2FAService.Extensions;

namespace _2FAService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class MyCustomStorage : IDisposable
    {
        private static MyCustomStorage _instance = null;
        private IStorageProvider _currentProvider;
        private List<AuthModel> _models = null;
        private MyCustomStorage(IStorageProvider? provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            _currentProvider = provider;
            _models = _currentProvider.ReadAll();
        }

        public static MyCustomStorage Instance(IStorageProvider? provider = null)
        {
            if (_instance == null) return _instance = new MyCustomStorage(provider);
            return _instance;

        }

        public void SaveEntry(AuthModel model)
        {

            var me = this._models.FirstOrDefault(s => s.PhoneNumber.Equals(model.PhoneNumber));
            if (me == null)
            {
                this._models.Add(model);
                this._currentProvider.Save(_models);
            }
            else
            {
                _models.Remove(me);

                me.CurrentCodeRequestCount += 1;
                _models.Add(me);
            }


        }

        public void RemoveEntry(string phoneNumber)
        {
            var model = this._models.FirstOrDefault(s => s.PhoneNumber.Equals(phoneNumber));
            if (model != null)
            {
                _models.Remove(model);
                this._currentProvider.Save(_models);
            }
        }

        public AuthModel GetEntry(string phoneNumber)
        {
            var model = this._models.FirstOrDefault(s => s.PhoneNumber.Equals(phoneNumber));
            return model;
        }

        public void Dispose()
        {
            this._currentProvider.Save(_models);
        }
    }
}
