using System;
using System.Collections.Generic;

namespace _Project.CodeBase.Logic.Core
{
    public class Board : IBoard
    {
        private readonly List<List<Cell>> _cells = new List<List<Cell>>();
        private readonly int _players;

        public Board(int players) => 
            _players = players;

        public void AddList(List<Cell> list) => 
            _cells.Add(list);
        
        public void UpdateCellState(Cell cell)
        {
            FindPreviousCells(cell, _players, out var prevCell1, out var prevCell2);
            
            if (prevCell1 != null && prevCell2 != null)
                if (!CheckMatchEnded(prevCell1, prevCell2)) return;
            
            if (cell.State == State.Empty) 
                cell.ChangeState(State.Wait);
            else
            {
                var opponent = FindPair(cell, _players);
                if (opponent.State == State.Wait && cell.State == State.Wait)
                {
                    cell.ChangeState(State.Win);
                    opponent.ChangeState(State.Lose);
                }
            }
        }

        public void CleanUp() => 
            _cells.Clear();

        private Cell FindPair(Cell cell, int players)
        {
            var x = (int)Math.Log(players, 2);
            var i = cell.Index.Item1;
            var j = cell.Index.Item2;
            
            if (i == x)
                return _cells[x - 1][0];
            if (i == x - 1)
                return _cells[x][0];
            
            return j % 2 == 0 
                ? _cells[i][j + 1] 
                : _cells[i][j - 1];
        }

        private void FindPreviousCells(Cell cell, int players, out Cell prevCell1, out Cell prevCell2)
        {
            var i = cell.Index.Item1;
            var j = cell.Index.Item2;

            if (i == 0 || i == _cells.Count - 1)
            {
                prevCell1 = null;
                prevCell2 = null;
                return;
            }

            var listIndex = i < (int)Math.Log(players, 2) 
                ? i - 1 
                : i + 1;

            prevCell1 = _cells[listIndex][2 * j];
            prevCell2 = _cells[listIndex][2 * j + 1];
        }

        private bool CheckMatchEnded(Cell cell1, Cell cell2) => 
            (cell1.State == State.Win || cell1.State == State.Lose) 
            && (cell2.State == State.Win || cell2.State == State.Lose);
    }
}