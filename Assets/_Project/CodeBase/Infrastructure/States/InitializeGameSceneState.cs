using _Project.CodeBase.UI.Services;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.States
{
    public class InitializeGameSceneState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windowService;

        public InitializeGameSceneState(
            IUIFactory uiFactory,
            IWindowService windowService)
        {
            _uiFactory = uiFactory;
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
            var menu = _uiFactory.CreateMenuWindow();
            var table = _uiFactory.CreateTableWindow();
            _uiFactory.SetupWindowButtons(_windowService);
            table.gameObject.SetActive(false);
        }
        
        public void Exit() { }
    }
}