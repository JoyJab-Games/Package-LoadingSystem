using System.Collections;
using UnityEngine;

namespace JoyJab.LoadingSystem {
    public class FadeToBlack : MonoBehaviour {
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 1f;
        
        public static float FadeDuration => _instance._fadeDuration;
        private static FadeToBlack _instance;

        private Coroutine _fadeCoroutine;

        private void Start() {
            if (_instance != null) {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            _canvasGroup.alpha = 0f;
            _fadeCoroutine = StartCoroutine(FadeToValue(1f));
        }
        
        public static void FadeOut() {
            if (_instance == null) return;
            _instance.FadeOutLocal();
        }

        private void FadeOutLocal() {
            if(_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = StartCoroutine(FadeToValue(0f));
        }

        private IEnumerator FadeToValue(float target) {
            float start = _canvasGroup.alpha;
            float time = 0f;
            while (time < _fadeDuration) {
                _canvasGroup.alpha = Mathf.Lerp(start, target, time / _fadeDuration);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            _canvasGroup.alpha = target;
        }
        
    }
}