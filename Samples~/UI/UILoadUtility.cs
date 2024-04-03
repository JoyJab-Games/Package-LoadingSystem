using UnityEngine;

namespace JoyJab.LoadingSystem {
    public class UILoadUtility : MonoBehaviour {
        
        public void LoadScene(string sceneName) {
            LoadingManager.LoadScene(sceneName);
        }
        
    }
}