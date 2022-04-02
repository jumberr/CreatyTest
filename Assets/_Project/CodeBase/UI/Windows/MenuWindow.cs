using _Project.CodeBase.Logic;
using TMPro;
using UnityEngine;

namespace _Project.CodeBase.UI.Windows
{
    public class MenuWindow : WindowBase
    {
        [SerializeField] private TMP_InputField _playerInput;
        [SerializeField] private GameObject _error;

        private int _validatedNumber;

        protected override void OnAwake() { }

        protected override void OnDisable() => 
            _playerInput.onEndEdit.RemoveAllListeners();

        protected override void Initialize() => 
            _playerInput.onEndEdit.AddListener(OnEndEditing);

        public bool GenerateTable(out int value)
        {
            value = 0;
            if (_validatedNumber == -1) return false;
            value = _validatedNumber;
            return true;
        }

        private void OnEndEditing(string str)
        {
            if (Validator.Validate(str, out var number))
            {
                _validatedNumber = number;
                _error.SetActive(false);
            }
            else
            {
                _validatedNumber = -1;
                _error.SetActive(true);
            }
        }
    }
}
