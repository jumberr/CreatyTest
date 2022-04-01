using System.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}