using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Architecture.AssetsManagment
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> completedCache;
        private readonly Dictionary<string, List<AsyncOperationHandle>> handles;

        public AssetProvider()
        {
            completedCache = new Dictionary<string, AsyncOperationHandle>(20);
            handles = new Dictionary<string, List<AsyncOperationHandle>>(20);

            Addressables.InitializeAsync();
        }
        public Task<GameObject> Instantiate(string adress) =>
            Addressables.InstantiateAsync(adress).Task;

        public Task<GameObject> Instantiate(string adress, Vector3 at) =>
            Addressables.InstantiateAsync(adress, at, Quaternion.identity).Task;

        public async Task<T> LoadAsync<T>(AssetReference assetReference) where T : class
        {
            if (completedCache.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(assetReference),
                cacheKey: assetReference.AssetGUID);
        }

        public async Task<T> LoadAsync<T>(string adress) where T : class
        {
            if (completedCache.TryGetValue(adress, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(adress),
                cacheKey: adress);
        }

        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in handles.Values)
                foreach (AsyncOperationHandle handle in resourceHandles)
                    Addressables.Release(handle);

            completedCache.Clear();
            handles.Clear();
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle =>
                                completedCache[cacheKey] = completeHandle;

            AddHandle(cacheKey, handle);

            return await handle.Task;
        }

        private void AddHandle<T>(string cacheKey, AsyncOperationHandle<T> handle) where T : class
        {
            if (!handles.TryGetValue(cacheKey, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                handles[cacheKey] = resourceHandles;

                resourceHandles.Add(handle);
            }
        }
    }
}
