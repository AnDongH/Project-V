using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component {
    protected static T _instance;

    public static T Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<T>();

                if (_instance == null) {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake() {

    }
}

public class DontDestroySingleton<T> : Singleton<T> where T : Component {

    protected override void Awake() {
        if (_instance == null) {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else {
            _instance = this as T;
            Destroy(gameObject);
        }
    }
}

public class NormalSingleton<T> : Singleton<T> where T : Component {

    protected override void Awake() {
        _instance = this as T;
    }
}
