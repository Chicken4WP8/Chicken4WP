using System.IO.IsolatedStorage;

namespace Chicken4WP.Services
{
    public class StorageService
    {
        private static readonly IsolatedStorageFile FileSystem = IsolatedStorageFile.GetUserStoreForApplication();

        static StorageService()
        {
        }
    }
}
