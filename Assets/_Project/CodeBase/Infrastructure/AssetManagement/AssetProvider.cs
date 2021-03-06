using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

        public void Initialize() => 
            Addressables.InitializeAsync();

        public UniTask<GameObject> InstantiateAsync(string address) => 
            Addressables.InstantiateAsync(address).Task.AsUniTask();

        public UniTask<GameObject> InstantiateAsync(string address, Vector3 at) => 
            Addressables.InstantiateAsync(address, at, Quaternion.identity).Task.AsUniTask();

        public UniTask<GameObject> InstantiateAsync(string address, Transform at) => 
            Addressables.InstantiateAsync(address, at).Task.AsUniTask();

        public async UniTask<T> Load<T>(AssetReferenceGameObject assetReference) where T : class
        {
            if (_completedCache.TryGetValue(assetReference.AssetGUID, out var completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(assetReference),
                cacheKey: assetReference.AssetGUID);
        }

        public async UniTask<T> Load<T>(string address) where T : class
        {
            if (_completedCache.TryGetValue(address, out var completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(address),
                cacheKey: address);
        }

        public void CleanUp()
        {
            foreach (var resourceHandles in _handles.Values)
            foreach (var handle in resourceHandles) 
                Addressables.Release(handle);
            
            _completedCache.Clear();
            _handles.Clear();
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(key, out var resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle =>
                _completedCache[cacheKey] = completeHandle;

            AddHandle(cacheKey, handle);

            return await handle.Task;
        }
    }
}