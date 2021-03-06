using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.StaticData.UI;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetProvider;

        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public StaticDataService(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async UniTask LoadUIWindowConfig()
        {
            _windowConfigs = (await _assetProvider.Load<WindowStaticData>(StaticDataPath.WindowStaticData))
                .Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public WindowConfig ForWindow(WindowId windowId) => 
            _windowConfigs[windowId];
    }
}