using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Base Singleton
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

/// <summary>
/// This class provides a singleton pattern that is not destroyed even when switching scenes.
/// </summary>
/// <typeparam name="T"></typeparam>
public class DontDestroySingleton<T> : Singleton<T> where T : Component {

    protected override void Awake() {
        if (_instance == null) {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}

/// <summary>
/// This class provides a singleton pattern
/// </summary>
/// <typeparam name="T"></typeparam>
public class NormalSingleton<T> : Singleton<T> where T : Component {

    protected override void Awake() {
        _instance = this as T;
    }
}
