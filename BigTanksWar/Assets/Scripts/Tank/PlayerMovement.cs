using UnityEngine;
using System.Collections;

namespace Complete
{
	public class PlayerMovement : TankMovement
	{
		//for rotate of enemy
		float timeUpdate = 0;

		private bool isLeft = false, isRight = false, isTop = false, isBottom = false;
		public GameObject[] enemies;
		Rigidbody[] r_enemies;

		void Start ()
		{
			enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			r_enemies = new Rigidbody[enemies.Length];
			for (int i = 0; i < enemies.Length; i++) {
				r_enemies [i] = (Rigidbody)enemies [i].GetComponent<Rigidbody> ();
			}
		}
		// Update is called once per frame
		void Update ()
		{
			
			playerTurn ();


			for (int i = 0; i < enemies.Length; i++) {
				float dis = distance (new Vector2 (transform.position.x, transform.position.z),
					            new Vector2 (enemies [i].transform.position.x, enemies [i].transform.position.z));
				if (dis < 20)
					enemies [i].transform.LookAt (this.transform);
				else
					enemyTurn (r_enemies [i],i);
			}

			timeUpdate += Time.deltaTime;
			EngineAudio ();
		

		}

		void FixedUpdate ()
		{
			playerMovement ();
			for (int i = 0; i < enemies.Length; i++) {
				enemyMovement (r_enemies [i], enemies [i],i);
			}
		}

		private void enemyMovement (Rigidbody rigid, GameObject obj,int position)
		{
			// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
			if (timeUpdate > 2.5 && timeUpdate < 5) {
				Vector3 movement = new Vector3 (0, 0, 0);
				if (position == 0) {
					movement = obj.transform.forward * Random.Range (5, 60) * Time.deltaTime;
				} else if(position ==1){
					movement = obj.transform.forward * Random.Range (1, 30) * Time.deltaTime;
				}

				rigid.MovePosition (rigid.position + movement);
			} else if (timeUpdate > 5) {
				timeUpdate = 0;
			}
		}

		private void enemyTurn (Rigidbody rigidbody,int position)
		{
			// Determine the number of degrees to be turned based on the input, speed and time between frames.
			float turn = 0;
			Quaternion turnRotation;
			if (timeUpdate > 0.55 && timeUpdate < 0.95) {
				if(position == 0){
					turn = (Random.Range (2, 5) + m_TurnSpeed) * Time.deltaTime;
				}
				else if(position == 1){
					turn = (Random.Range (-2, 2) + m_TurnSpeed) * Time.deltaTime;
				}
			} else if (timeUpdate > 4 && timeUpdate < 4.5) {
				if(position == 0){
					turn = (Random.Range (-3, 1) + m_TurnSpeed) * Time.deltaTime;
				}
				else if(position == 1){
					turn = (Random.Range (3, 6) + m_TurnSpeed) * Time.deltaTime;
				}
			} else
				turn = 0;
			// Make this into a rotation in the y axis.
			turnRotation = Quaternion.Euler (0f, turn, 0f);

			// Apply this rotation to the rigidbody's rotation.
			rigidbody.MoveRotation (rigidbody.rotation * turnRotation);
		}

		public void turnLeft ()
		{
			isLeft = true;
		}

		public void turnRight ()
		{
			isRight = true;
		}

		public void goUp ()
		{
			isTop = true;
		}

		public void goBack ()
		{
			isBottom = true;
		}

		public void notAction ()
		{
			isLeft = false;
			isRight = false;
			isBottom = false;
			isTop = false;
		}


		private void playerMovement ()
		{
			// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
			Vector3 movement = new Vector3 (0, 0, 0);
			if (isTop) {
				movement = transform.forward * m_Speed * Time.deltaTime;
			} else if (isBottom) {
				movement = -transform.forward * m_Speed * Time.deltaTime;
			}

			// Apply this movement to the rigidbody's position.
			m_Rigidbody.MovePosition (m_Rigidbody.position + movement);
		}

		private void playerTurn ()
		{
			// Determine the number of degrees to be turned based on the input, speed and time between frames.
			float turn;
			if (isLeft) {
				turn = -m_TurnSpeed * Time.deltaTime;
			} else if (isRight) {
				turn = m_TurnSpeed * Time.deltaTime;
			} else
				turn = 0;

			// Make this into a rotation in the y axis.
			Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

			// Apply this rotation to the rigidbody's rotation.
			m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
		}
	}
}
