using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base {
    protected virtual void Init() {
        Access.UI.SetCanvas(gameObject, false);
    }
}