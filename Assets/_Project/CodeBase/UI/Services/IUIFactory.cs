using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.UI.Services
{
    public interface IUIFactory
    {
        UniTask CreateUIRoot();
        void CreateMenuWindow();
        void CreateTableWindow();
        
        void OpenMenuWindow();
        void OpenTableWindow();
        void SetupWindowButtons(IWindowService windowService, GameObject hud);
    }
}