using System.Collections.Generic;

namespace _Project.CodeBase.Logic.Core
{
    public interface IBoard
    {
        void AddList(List<Cell> list);
        void UpdateCellState(Cell cell);
        void CleanUp();
    }
}