
/// <summary>
/// Scene UI Base
/// If you want to create a Scene UI canvas, you need to inherit from this class.
/// </summary>
public class SceneUI : BaseUI {
    protected virtual void Init() {
        Access.UIM.SetCanvas(gameObject, false);
    }
}