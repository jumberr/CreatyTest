namespace _Project.CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory) => 
            _uiFactory = uiFactory;

        public void Open(WindowId id)
        {
            switch (id)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Menu:
                    _uiFactory.OpenMenuWindow();
                    break;
                case WindowId.Table:
                    _uiFactory.OpenTableWindow();
                    break;
            }
        }
    }
}