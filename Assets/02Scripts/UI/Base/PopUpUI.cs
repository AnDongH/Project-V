

/// <summary>
/// PopUp UI Base
/// If you want to create a popup UI canvas, you need to inherit from this class.
/// </summary>
public class PopUpUI : BaseUI {
    protected virtual void Init() {
        Access.UIM.SetCanvas(gameObject, true);
    }
    protected virtual void ClosePopUpUI() {
        Access.UIM.ClosePopupUI(this);
    }

}