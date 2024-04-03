using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

namespace JoyJab.LoadingSystem {
    public class LoadingManager : MonoBehaviour {

        public static event Action OnLoadingStarted;
        private static LoadingManager _instance => CachedStaticInstance<LoadingManager>.Value;
        
        private const string LoadingSceneName = "LoadingScreen";

        public static bool IsLoading => _loadCoroutine != null;
        private static Coroutine _loadCoroutine;
        
        public static void LoadScene(string sceneName, float delay = 0) {
            if (_loadCoroutine != null) {
                Debug.LogError("Already loading a scene");
                return;
            }
            _loadCoroutine = _instance.StartCoroutine(LoadSceneRoutine(sceneName, delay));
        }
        
        public static IEnumerator LoadSceneRoutine(string sceneName, float delay = 0) {
            try { OnLoadingStarted?.Invoke(); }
            catch (Exception e) { Debug.LogError(e); }
            yield return new WaitForSecondsRealtime(delay);
            Scene oldScene = SceneManager.GetActiveScene();
            yield return SwitchToLoadingScreen();
            yield return LoadNewScene(sceneName);
            yield return RemoveLoadingScreen(oldScene);
            _loadCoroutine = null;
        }

        private static IEnumerator SwitchToLoadingScreen() {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive);
            asyncOperation.allowSceneActivation = true;
            yield return new WaitUntil(() => asyncOperation.isDone);
        }

        private static IEnumerator LoadNewScene(string sceneName) {
            float startTime = Time.time;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncOperation.allowSceneActivation = false;
            yield return new WaitUntil(() => asyncOperation.progress >= 0.9f);
            while (Time.time - startTime < FadeToBlack.FadeDuration) yield return null;
            asyncOperation.allowSceneActivation = true;
            yield return new WaitUntil(() => asyncOperation.isDone);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

        private static IEnumerator RemoveLoadingScreen(Scene oldScene) {
            SceneManager.UnloadSceneAsync(oldScene);
            FadeToBlack.FadeOut();
            yield return new WaitForSeconds(FadeToBlack.FadeDuration);
            SceneManager.UnloadSceneAsync(LoadingSceneName);
        }
    }
}