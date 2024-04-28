

/// <summary>
/// PopUp UI Base
/// If you want to create a popup UI canvas, you need to inherit from this class.
/// </summary>
public class PopUpUI : BaseUI {
    protected virtual void Init() {
        Access.UI.SetCanvas(gameObject, true);
    }
    protected virtual void ClosePopUpUI() {
        Access.UI.ClosePopupUI(this);
    }

}