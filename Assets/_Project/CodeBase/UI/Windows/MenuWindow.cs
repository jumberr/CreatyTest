using _Project.CodeBase.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows
{
    public class MenuWindow : WindowBase
    {
        [SerializeField] private TableWindow _table;
        [SerializeField] private GameObject _parent;
        [SerializeField] private TMP_InputField _playerInput;
        [SerializeField] private Button _startButton;
        [SerializeField] private GameObject _error;

        private Validator _validator;
        private int? _validatedNumber;

        protected override void Initialize()
        {
            _validator = new Validator();
            _playerInput.onEndEdit.AddListener(OnEndEditing);
            _startButton.onClick.AddListener(GenerateTable);
        }

        protected override void OnDisable()
        {
            _playerInput.onEndEdit.RemoveAllListeners();
            _startButton.onClick.RemoveAllListeners();
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

        private void GenerateTable()
        {
            if (_validatedNumber != null)
            {
                //_table.GenerateTable(_validatedNumber);
                DisableMenu();
            }
        }

        public void EnableMenu() => 
            _parent.SetActive(true);

        private void DisableMenu() => 
            _parent.SetActive(false);
    }
}
