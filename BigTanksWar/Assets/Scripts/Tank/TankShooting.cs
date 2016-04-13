using UnityEngine;
using UnityEngine.UI;

	public class TankShooting : MonoBehaviour {
		public Rigidbody m_Shell;
		// Prefab of the shell.
		public Transform m_FireTransform;
		// A child of the tank that displays the current launch force.

		public float m_RangeFire = 30f;
		// The force given to the shell if the fire button is held for the max charge time.
		
		protected void Fire() {
			if(!GameController.isPause){
					// Create an instance of the shell and store a reference to it's rigidbody.
					Rigidbody shellInstance =
						Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation)as Rigidbody;

					// Set the shell's velocity to the launch force in the fire position's forward direction.
					shellInstance.velocity = m_RangeFire * m_FireTransform.forward;

			}
		}
	}
