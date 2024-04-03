using UnityEngine;

namespace JoyJab.LoadingSystem {
    public class UIRotator : MonoBehaviour {
        
        [SerializeField] private float _speed = 1f;
        
        public void Update() {
            transform.Rotate(Vector3.forward, _speed * Time.unscaledDeltaTime);
        }
    }
}