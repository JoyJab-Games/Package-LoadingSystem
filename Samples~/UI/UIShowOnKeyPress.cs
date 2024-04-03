using UnityEngine;
using UnityEngine.InputSystem;

namespace JoyJab.LoadingSystem {
    public class UIShowOnKeyPress : MonoBehaviour{

        [SerializeField] private InputAction _action;
        [SerializeField] private GameObject _open;

        private void OnEnable() {
            _action.performed += DoTheLoad;
            _action.Enable();
        }

        private void OnDisable() {
            _action.performed -= DoTheLoad;
            _action.Dispose();
        }

        private void DoTheLoad(InputAction.CallbackContext obj) {
            _open.SetActive(true);
        }
    }
}