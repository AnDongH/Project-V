using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogue {
    public abstract class DialogueAcceptionCondition : ScriptableObject {
        public abstract bool IsPass(DialogueGraph dialogue);
    }
}
