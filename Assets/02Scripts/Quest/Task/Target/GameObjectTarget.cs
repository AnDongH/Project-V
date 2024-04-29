using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Target/GameObject", fileName = "Target_")]
public class GameObjectTarget : TaskTarget
{
    [SerializeField]
    private GameObject value;

    public override object Value => value;

    public override bool IsEqual(object target)
    {
        var targetAsGameObject = target as GameObject;
        if (targetAsGameObject == null) return false;

        /*
            The value will contain a prefab, 
            but target can be either a prefab or an object from the game scene, 
            so you determine whether they share the same name. 
            However, this approach can cause issues 
            if object names are changed or 
            if unusual objects are given identical names. 
            Therefore, adjustments are needed.
            please tell me an idea
        */
        return targetAsGameObject.name.Contains(value.name);
    }
}
