using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
	[CreateAssetMenu(menuName = "Dialogue/TalkerInfo")]
	public class TalkerInfo : ScriptableObject {
		[SerializeField] private Color textColor;
        [SerializeField] private string talkerName;

		public Color TextColor => textColor;
		public string TalkerName => talkerName;
	}
}