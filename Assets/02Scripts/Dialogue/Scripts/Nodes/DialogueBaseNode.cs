using XNode;

namespace Dialogue {
	public abstract class DialogueBaseNode : Node {

		[Input] public DialogueBaseNode input;
		[Output] public DialogueBaseNode output;

		abstract public void Trigger();

		public override object GetValue(NodePort port) {
			return null;
		}
	}
}
