using UnityEngine;

	public class TankMovement : MonoBehaviour {

		public float m_Speed = 7f;
		// How fast the tank moves forward and back.
		public float m_TurnSpeed = 50f;
		// How fast the tank turns in degrees per second.
		
		public float m_PitchRange = 0.2f;
		// The amount by which the pitch of the engine noises can vary.

		//	private string m_TurnAxisName;
		// The name of the input axis for turning.
		protected Rigidbody m_Rigidbody;
		// Reference used to move the tank.

		private void Awake() {
			m_Rigidbody = GetComponent < Rigidbody > ();
		}

		private void OnEnable() {
			// When the tank is turned on, make sure it's not kinematic.
			if (m_Rigidbody != null)
				m_Rigidbody.isKinematic = false;
		}

		private void OnDisable() {
			// When the tank is turned off, set it to kinematic so it stops moving.
			if (m_Rigidbody != null)
				m_Rigidbody.isKinematic = true;
		}

		public float distance(Vector2 a, Vector2 b) {
			return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
		}
	}
