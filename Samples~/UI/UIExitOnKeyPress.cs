using UnityEngine;
using UnityEngine.InputSystem;

namespace JoyJab.LoadingSystem {
    public class UIExitOnKeyPress : MonoBehaviour{

        [SerializeField] private InputAction _action;

        private void OnEnable() {
            _action.performed += DoTheLoad;
            _action.Enable();
        }

        private void OnDisable() {
            _action.performed -= DoTheLoad;
            _action.Dispose();
        }

        private void DoTheLoad(InputAction.CallbackContext obj) {
            Application.Quit();
        }
    }
}