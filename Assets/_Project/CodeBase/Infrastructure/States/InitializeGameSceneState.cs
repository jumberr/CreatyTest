using System.Threading.Tasks;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.StaticData;
using _Project.CodeBase.UI.Services;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.States
{
    public class InitializeGameSceneState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IWindowService _windowService;

        public InitializeGameSceneState(SceneLoader sceneLoader,
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            IStaticDataService staticDataService,
            IWindowService windowService)
        {
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
            _windowService = windowService;
        }

        public async void Enter() => 
            await InitializeGame();

        private async UniTask InitializeGame() => 
            await InitializeUI();

        private async UniTask InitializeUI()
        {
            await InitializeUIRoot();
            InitializeWindows();
        }

        private async UniTask InitializeUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private void InitializeWindows()
        {
            _uiFactory.CreateMenuWindow();
            _uiFactory.CreateTableWindow();
        }


        public void Exit() { }
    }
}