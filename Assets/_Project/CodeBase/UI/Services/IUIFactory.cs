using _Project.CodeBase.UI.Services.Windows;
using _Project.CodeBase.UI.Windows;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.UI.Services
{
    public interface IUIFactory
    {
        UniTask CreateUIRoot();
        MenuWindow CreateMenuWindow();
        TableWindow CreateTableWindow();
        
        void OpenMenuWindow();
        void OpenTableWindow();
        void SetupWindowButtons(IWindowService windowService);
    }
}