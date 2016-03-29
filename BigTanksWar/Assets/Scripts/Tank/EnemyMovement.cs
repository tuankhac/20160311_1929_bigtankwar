using UnityEngine;
using System.Collections;

namespace Complete
{
	public class EnemyMovement : TankMovement
	{
		//for rotate of enemy
		float timeUpdate = 0;
		private Transform tplayer;

		public GameObject enemy1;
		PlayerMovement player;

		void Start ()
		{
			player = FindObjectOfType (typeof(PlayerMovement)) as PlayerMovement;
			tplayer = player.transform;
		}

		// Update is called once per frame
		void Update ()
		{
			float dis = distance (new Vector2 (transform.position.x, transform.position.z),
				            new Vector2 (tplayer.position.x, tplayer.position.z));
			if (dis < 20)
				this.transform.LookAt (tplayer);
			else {
				
				if (timeUpdate > 6) {
					timeUpdate = 0;
				}
				enemyTurn ();
				enemyMovement ();
				timeUpdate += Time.deltaTime;
			}
		}

		private void enemyTurn ()
		{
			// Determine the number of degrees to be turned based on the input, speed and time between frames.
			float turn = 0;
			if (timeUpdate > 0.55 && timeUpdate < 0.95) {
				turn = (Random.Range (2, 5) + m_TurnSpeed) * Time.deltaTime;
			} else if (timeUpdate > 4 && timeUpdate < 4.5) {
				turn = (Random.Range (-2, 1) + m_TurnSpeed) * Time.deltaTime;
			} else
				turn = 0;
			// Make this into a rotation in the y axis.
			Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

			// Apply this rotation to the rigidbody's rotation.
			m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
		}

		private void enemyMovement ()
		{
			// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
			if (timeUpdate > 2.5 && timeUpdate < 5) {
				Vector3 movement = new Vector3 (0, 0, 0);
				movement = transform.forward * Random.Range (5, 12) * Time.deltaTime;

				m_Rigidbody.MovePosition (m_Rigidbody.position + movement);
			} else if (timeUpdate > 5) {
				timeUpdate = 0;
			}
		}
	}
}
