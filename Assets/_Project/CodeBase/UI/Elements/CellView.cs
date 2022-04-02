using _Project.CodeBase.Logic.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Elements
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        private Cell _cell;

        public void Construct(Cell cell)
        {
            _cell = cell;
            ChangeColor(_cell.State);
        }

        private void Start() => 
            _button.onClick.AddListener(OnClick);

        private void OnDestroy() => 
            _button.onClick.RemoveAllListeners();

        public void ChangeColor(State state)
        {
            var color = state switch
            {
                State.Empty => Color.gray,
                State.Wait => Color.blue,
                State.Lose => Color.black,
                State.Win => Color.yellow
            };
            _image.color = color;
        }

        private void OnClick() => 
            _cell.OnClick();
    }
}