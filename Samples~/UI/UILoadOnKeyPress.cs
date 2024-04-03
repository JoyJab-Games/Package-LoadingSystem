using UnityEngine;
using UnityEngine.InputSystem;

namespace JoyJab.LoadingSystem {
    public class UILoadOnKeyPress : MonoBehaviour{

        [SerializeField] private InputAction _action;
        [SerializeField] private string _loadTarget;
        [SerializeField] private Optional<float> _delay;

        private void OnEnable() {
            _action.performed += DoTheLoad;
            _action.Enable();
        }

        private void OnDisable() {
            _action.performed -= DoTheLoad;
            _action.Dispose();
        }

        private void DoTheLoad(InputAction.CallbackContext obj) {
            _action.Disable();
            LoadingManager.LoadScene(_loadTarget, _delay.Enabled ? _delay.Value : 0);
        }
    }
}