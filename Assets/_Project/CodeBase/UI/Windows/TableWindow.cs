using System.Collections.Generic;
using _Project.CodeBase.UI.Elements;
using UnityEngine;

namespace _Project.CodeBase.UI.Windows
{
    public class TableWindow : WindowBase
    {
        [SerializeField] private Transform _boardParent;
        [SerializeField] private GameObject _bigContainer;
        [SerializeField] private GameObject _smallContainer;
        [SerializeField] private Cell _cell;
        
        private readonly List<List<Cell>> _cells = new List<List<Cell>>();
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
            Destroy(_leftHalf);
            Destroy(_rightHalf);
            _cells.Clear();
        }

        private void CreateCells(int players)
        {
            var half = players / 2;
            CreateLeftHalf(half);
            CreateRightHalf(half);
        }

        private void CreateLeftHalf(int startValue)
        {
            _leftHalf = Instantiate(_bigContainer, _boardParent);

            var index = startValue;
            var tmpList = new List<List<Cell>>();
            while (index >= 1)
            {
                var list = new List<Cell>();
                var smallContainer = Instantiate(_smallContainer, _leftHalf.transform);
                for (var i = index; i > 0; i--)
                {
                    var cell = Instantiate(_cell, smallContainer.transform);
                    list.Add(cell);
                }
                
                tmpList.Add(list);
                index /= 2;
            }

            _cells.AddRange(tmpList);
        }
        
        private void CreateRightHalf(int endValue)
        {
            _rightHalf = Instantiate(_bigContainer, _boardParent);

            var index = 1;
            var tmpList = new List<List<Cell>>();
            while (index <= endValue)
            {
                var list = new List<Cell>();
                var smallContainer = Instantiate(_smallContainer, _rightHalf.transform);
                for (var i = index; i > 0; i--)
                {
                    var cell = Instantiate(_cell, smallContainer.transform);
                    list.Add(cell);
                }
                
                tmpList.Add(list);
                index *= 2;
            }

            _cells.AddRange(tmpList);
        }
    }
}