using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.Architecture.AssetsManagment
{
    public interface IAssetProvider
    {
        void CleanUp();
        Task<GameObject> Instantiate(string adress);
        Task<GameObject> Instantiate(string adress, Vector3 at);
        Task<T> LoadAsync<T>(AssetReference assetReference) where T : class;
        Task<T> LoadAsync<T>(string adress) where T : class;
    }
}