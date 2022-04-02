using _Project.CodeBase.Logic;
using TMPro;
using UnityEngine;

namespace _Project.CodeBase.UI.Windows
{
    public class MenuWindow : WindowBase
    {
        [SerializeField] private TMP_InputField _playerInput;
        [SerializeField] private GameObject _error;

        private Validator _validator;
        private int? _validatedNumber;

        protected override void OnAwake() { }

        protected override void Initialize()
        {
            _validator = new Validator();
            _playerInput.onEndEdit.AddListener(OnEndEditing);
        }

        protected override void OnDisable() => 
            _playerInput.onEndEdit.RemoveAllListeners();

        public bool GenerateTable(out int value)
        {
            value = 0;
            if (_validatedNumber == null) return false;
            value = (int) _validatedNumber;
            return true;
        }

        private void OnEndEditing(string str)
        {
            if (_validator.Validate(str, out var number))
            {
                _validatedNumber = number;
                _error.SetActive(false);
            }
            else
            {
                _validatedNumber = null;
                _error.SetActive(true);
            }
        }
    }
}
