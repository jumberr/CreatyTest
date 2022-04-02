using _Project.CodeBase.UI.Elements;

namespace _Project.CodeBase.Logic.Core
{
    public class Cell
    {
        private readonly IBoard _board;
        private readonly CellView _cellView;
        private readonly (int, int) _index;
        private State _state;

        public State State => _state;
        public (int, int) Index => _index;
        
        public Cell(IBoard board, CellView cellView, (int i, int j) index)
        {
            _board = board;
            _cellView = cellView;
            _index = index;
            _state = State.Empty;
        }

        public void OnClick() => 
            _board.UpdateCellState(this);

        public void ChangeState(State state)
        {
            _state = state;
            _cellView.ChangeColor(state);
        }
    }
}