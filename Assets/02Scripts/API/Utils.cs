using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


public static class Utils {

    /// <summary>
    /// Retrieve the component of type T from the child objects of GameObject that have the name 'name'.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <param name="name"></param>
    /// <param name="deepSearch"></param>
    /// <returns></returns>
    public static T FindChild<T>(GameObject go, string name = null, bool deepSearch = false) where T : Object {
        if (go == null)
            return null;

        if (deepSearch == false) {
            for (int i = 0; i < go.transform.childCount; i++) {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name) {
                    T componet = transform.GetComponent<T>();
                    if (componet != null)
                        return componet;
                }
            }
        }
        else {
            foreach (T component in go.GetComponentsInChildren<T>()) {
                if (string.IsNullOrEmpty(name) || component.name == name) {
                    return component;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Retrieve the GameObject from the child objects of cur GameObject that have the name 'name'.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="name"></param>
    /// <param name="deepSearch"></param>
    /// <returns></returns>
    public static GameObject FindChild(GameObject go, string name = null, bool deepSearch = false) {
        Transform transform = FindChild<Transform>(go, name, deepSearch);
        if (transform == null) return null;
        return transform.gameObject;
    }

    /// <summary>
    /// Retrieves the component of type <T>. If it doesn't exist, a new one is added and then returned.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(GameObject go) where T : Component {
        T component = go.GetComponent<T>();
        if (component == null) component = go.AddComponent<T>();
        return component;
    }

}