using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UI_PopUp : UI_Base {
    protected virtual void Init() {
        Access.UI.SetCanvas(gameObject, true);
    }
    protected virtual void ClosePopUpUI() {
        Access.UI.ClosePopupUI(this);
    }

}