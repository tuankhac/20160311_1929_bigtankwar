using UnityEngine;
using System.Collections;

namespace Complete {
	public class PlayerMovement : TankMovement {
		//for rotate of enemy
		float timeUpdate = 0;

		private bool isLeft = false,
		isRight = false,
		isTop = false,
		isBottom = false;
		public GameObject[]enemies;
		Rigidbody[]r_enemies;

		private float m_MovementInputValue; // The current value of the movement input.
		private float m_TurnInputValue; // The current value of the turn input.
		private string m_MovementAxisName; // The name of the input axis for moving forward and back.
		private string m_TurnAxisName; // The name of the input axis for turning.
		public int m_PlayerNumber = 1; // Used to identify which tank belongs to which player.  This is set by this tank's manager.
		void Start() {
			// Also reset the input values.
			m_MovementInputValue = 0f;
			m_TurnInputValue = 0f;

			// The axes names are based on player number.
			m_MovementAxisName = "Vertical" + m_PlayerNumber;
			m_TurnAxisName = "Horizontal" + m_PlayerNumber;

			enemies = GameObject.FindGameObjectsWithTag("Enemy");
			r_enemies = new Rigidbody[enemies.Length];
			for (int i = 0; i < enemies.Length; i++) {
				r_enemies[i] = (Rigidbody)enemies[i].GetComponent < Rigidbody > ();
			}
		}
		// Update is called once per frame
		void Update() {
			// Store the value of both input axes.
			m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
			m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

			playerTurn();

			for (int i = 0; i < enemies.Length; i++) {
				float dis = distance(new Vector2(transform.position.x, transform.position.z),
						new Vector2(enemies[i].transform.position.x, enemies[i].transform.position.z));
				if (dis < 20)
					enemies[i].transform.LookAt(this.transform);
				else
					enemyTurn(r_enemies[i], i);
			}

			timeUpdate += Time.deltaTime;
			EngineAudio();

		}

		void FixedUpdate() {
			//keyboard test
			Move();
			Turn();

			playerMovement();
			for (int i = 0; i < enemies.Length; i++) {
				enemyMovement(r_enemies[i], enemies[i], i);
			}
		}

		private void Move() {
			// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
			Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

			// Apply this movement to the rigidbody's position.
			m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
		}

		private void Turn() {
			// Determine the number of degrees to be turned based on the input, speed and time between frames.
			float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

			// Make this into a rotation in the y axis.
			Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

			// Apply this rotation to the rigidbody's rotation.
			m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
		}

		private void enemyMovement(Rigidbody rigid, GameObject obj, int position) {
			// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
			if (timeUpdate > 2.5 && timeUpdate < 5) {
				Vector3 movement = new Vector3(0, 0, 0);

				if (position == 0) {
					movement = obj.transform.forward * Random.value / 15;
				} else if (position == 1) {
					movement = obj.transform.forward * Random.value / 9;
				} else if (position == 2) {
					movement = obj.transform.forward * Random.value / 8;
				} else if (position == 3) {
					movement = obj.transform.forward * Random.value / 13;
				} else {
					movement = obj.transform.forward * Random.value / 10;
				}

				rigid.MovePosition(rigid.position + movement);
			} else if (timeUpdate > 5) {
				timeUpdate = 0;
			}
		}

		private void enemyTurn(Rigidbody rigidbody, int position) {
			// Determine the number of degrees to be turned based on the input, speed and time between frames.
			float turn = 0;
			Quaternion turnRotation;
			if (timeUpdate > 0.55 && timeUpdate < 0.95) {
				if (position == 0) {
					turn = (Random.Range (1, 4) * m_TurnSpeed) * Time.deltaTime;
				} else if (position == 1) {
					turn = (Random.Range (-7, -4) * m_TurnSpeed) * Time.deltaTime;
				} else if (position == 2) {
					turn = (Random.Range (-4, -1) * m_TurnSpeed) * Time.deltaTime;
				} else {
					turn = (Random.Range (4, 8) * m_TurnSpeed) * Time.deltaTime;
				}
			} else if (timeUpdate > 10 && timeUpdate < 10.5) {
				if (position == 0) {
					turn = (Random.Range(-3, 1) * m_TurnSpeed) * Time.deltaTime;
				} else if (position == 1) {
					turn = (Random.Range(3, 6) * m_TurnSpeed) * Time.deltaTime;
				} else if (position == 2) {
					turn = (Random.Range(-5, 3) * m_TurnSpeed) * Time.deltaTime;
				}
			} else
				turn = 0;
			// Make this into a rotation in the y axis.
			turnRotation = Quaternion.Euler(0f, turn, 0f);

			// Apply this rotation to the rigidbody's rotation.
			rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
		}

		public void turnLeft() {
			isLeft = true;
		}

		public void turnRight() {
			isRight = true;
		}

		public void goUp() {
			isTop = true;
		}

		public void goBack() {
			isBottom = true;
		}

		public void notAction() {
			isLeft = false;
			isRight = false;
			isBottom = false;
			isTop = false;
		}

		private void playerMovement() {
			// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
			Vector3 movement = new Vector3(0, 0, 0);
			if (isTop) {
				movement = transform.forward * m_Speed * Time.deltaTime;
			} else if (isBottom) {
				movement = -transform.forward * m_Speed * Time.deltaTime;
			}

			// Apply this movement to the rigidbody's position.
			m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
		}

		private void playerTurn() {
			// Determine the number of degrees to be turned based on the input, speed and time between frames.
			float turn;
			if (isLeft) {
				turn = -m_TurnSpeed * Time.deltaTime;
			} else if (isRight) {
				turn = m_TurnSpeed * Time.deltaTime;
			} else
				turn = 0;

			// Make this into a rotation in the y axis.
			Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

			// Apply this rotation to the rigidbody's rotation.
			m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
		}
		private void OnTriggerEnter(Collider other) {
			if (other.gameObject.name == "Powerfull(Clone)") {
				other.gameObject.SetActive(false);
				Collider collider =  GameObject.Find ("Player").GetComponent<Collider> ();
				// Deal this damage to the tank.
				TankHealth targetHealth = collider.GetComponent<TankHealth>();
				float ad = -50f;
				targetHealth.TakeDamage(ad, collider);
			}
			//Debug.Log (other.gameObject.name);

		}
	}
}
