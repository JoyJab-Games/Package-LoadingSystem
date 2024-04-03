using UnityEngine;

namespace JoyJab.LoadingSystem {
    public static class CachedStaticInstance<T> where T : MonoBehaviour {
        public static T Value {
            get {
                if (_instanceCache != null) return _instanceCache;
                GameObject source = new GameObject(nameof(T));
                Object.DontDestroyOnLoad(source);
                _instanceCache = source.AddComponent<T>();
                return _instanceCache;
            }
        }
        private static T _instanceCache;
    }
}