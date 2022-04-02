using System;
using System.Collections.Generic;
using _Project.CodeBase.Logic.Core;
using _Project.CodeBase.UI.Elements;
using UnityEngine;

namespace _Project.CodeBase.UI.Windows
{
    public class TableWindow : WindowBase
    {
        [SerializeField] private Transform _boardParent;
        [SerializeField] private GameObject _bigContainer;
        [SerializeField] private GameObject _smallContainer;
        [SerializeField] private CellView _cellViewPrefab;

        private IBoard _board;
        private GameObject _leftHalf;
        private GameObject _rightHalf;
        
        private int _players;

        public void Setup(int value)
        {
            _players = value;
            CreateCells(_players);
        }

        protected override void OnAwake() { }

        protected override void OnDisable()
        {
            _board?.CleanUp();
            Destroy(_leftHalf);
            Destroy(_rightHalf);
        }

        private void CreateCells(int players)
        {
            _board = new Board(players);
            
            CreateLeftHalf(players);
            CreateRightHalf(players);
        }

        private void CreateLeftHalf(int startValue)
        {
            _leftHalf = Instantiate(_bigContainer, _boardParent);

            var i = 0;
            var index = startValue / 2;
            while (index >= 1)
            {
                InitializeCellColumn(index, i, _leftHalf.transform);
                index /= 2;
                i++;
            }
        }

        private void CreateRightHalf(int players)
        {
            _rightHalf = Instantiate(_bigContainer, _boardParent);

            var i = (int)Math.Log(players, 2);
            var index = 1;
            while (index <= players / 2)
            {
                InitializeCellColumn(index, i, _rightHalf.transform);
                index *= 2;
                i++;
            }
        }

        private void InitializeCellColumn(int index, int i, Transform bigContainer)
        {
            var list = new List<Cell>();
            var smallContainer = Instantiate(_smallContainer, bigContainer);
            for (int k = index, j = 0; k > 0; k--, j++)
            {
                var cellView = Instantiate(_cellViewPrefab, smallContainer.transform);
                var cell = new Cell(_board, cellView, (i, j));

                cellView.Construct(cell);
                list.Add(cell);
            }
            
            _board.AddList(list);
        }
    }
}