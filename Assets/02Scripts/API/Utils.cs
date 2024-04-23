using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


public class Utils {

    /// <summary>
    /// go�� �ڽ� ������Ʈ �߿��� �̸��� name�� �� �߿��� T������Ʈ ��������
    /// recursive�� true �� �� ��� �ڽ��� �� �Ⱦ��
    /// false�� �� ���� ù �ڽĸ� ����.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <param name="name"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : Object {
        if (go == null)
            return null;

        if (recursive == false) {
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
    /// go���� T ������Ʈ ��������
    /// ���� ���ٸ� �ٿ����� ��������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(GameObject go) where T : Component {
        T component = go.GetComponent<T>();
        if (component == null) component = go.AddComponent<T>();
        return component;
    }

    /// <summary>
    /// go�� �ڽ� ������Ʈ �߿��� �̸��� name�� �� �߿��� ���� ������Ʈ ��������
    /// recursive�� true �� �� ��� �ڽ��� �� �Ⱦ��
    /// false�� �� ���� ù �ڽĸ� ����.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="name"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false) {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null) return null;
        return transform.gameObject;
    }


}