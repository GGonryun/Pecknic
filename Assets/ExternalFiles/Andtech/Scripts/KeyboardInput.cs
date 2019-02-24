using UnityEngine;
using UnityEngine.Events;

namespace Andtech.Prototyping {

	/// <summary>
	/// Invokes actions when a button is activated.
	/// </summary>
	public class KeyboardInput : MonoBehaviour {
		public KeyCode keyCode;

		public UnityEvent onDown;
		public UnityEvent onPressed;
		public UnityEvent onUp;

		#region MONOBEHAVIOUR
		protected virtual void Reset() {
			keyCode = KeyCode.Space;
		}

		protected virtual void Update() {
			if (Input.GetKeyDown(keyCode))
				onDown.Invoke();
			else if (Input.GetKey(keyCode))
				onPressed.Invoke();
			else if (Input.GetKeyUp(keyCode))
				onUp.Invoke();
		}
		#endregion MONOBEHAVIOUR
	}
}
