using _Project.CodeBase.Infrastructure.AssetManagement;
using _Project.CodeBase.Infrastructure.StaticData;
using _Project.CodeBase.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _staticDataService;

        private GameObject HeroGameObject { get; set; }


        public GameFactory(
            IAssetProvider assetProvider,
            IUIFactory uiFactory,
            IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
        }

        public void Cleanup()
        {
            _assetProvider.CleanUp();
        }

        public void WarmUp() { }
    }
}