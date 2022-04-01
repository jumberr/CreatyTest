using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.StaticData;
using _Project.CodeBase.UI.Elements;
using _Project.CodeBase.UI.Services.Windows;
using _Project.CodeBase.UI.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        private Transform _uiRoot;
        private MenuWindow _menuWindow;
        private TableWindow _tableWindow;
        
        public UIFactory(
            IAssetProvider assetProvider,
            IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public async UniTask CreateUIRoot()
        {
            var uiRoot = await _assetProvider.InstantiateAsync(AssetPath.UIRootPath);
            _uiRoot = uiRoot.transform;
        }

        public void CreateMenuWindow()
        {
            var prefab = _staticDataService.ForWindow(WindowId.Menu).Prefab;
            _menuWindow = Object.Instantiate(prefab, _uiRoot) as MenuWindow;
        }
        
        public void CreateTableWindow()
        {
            var prefab = _staticDataService.ForWindow(WindowId.Table).Prefab;
            _tableWindow = Object.Instantiate(prefab, _uiRoot) as TableWindow;
        }

        public void OpenMenuWindow() => 
            _menuWindow.gameObject.SetActive(true);

        public void OpenTableWindow() => 
            _tableWindow.gameObject.SetActive(true);

        public void SetupWindowButtons(IWindowService windowService, GameObject hud)
        {
            foreach (var button in hud.GetComponentsInChildren<OpenWindowButton>()) 
                button.Construct(windowService);
        }
    }
}