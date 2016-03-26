using UnityEngine;
using System.Collections;
namespace Complete
{
	public class TankMovement: MonoBehaviour
	{
		public int m_PlayerNumber = 1;
		// Used to identify which tank belongs to which player.  This is set by this tank's manager.
		public float m_Speed = 12f;
		// How fast the tank moves forward and back.
		public float m_TurnSpeed = 180f;
		// How fast the tank turns in degrees per second.
		//public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
		//public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
		//public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
		///public float m_PitchRange = 0.2f;
		// The amount by which the pitch of the engine noises can vary.

		//for enemy
		//public Transform player, enemy1, enemy2, enemy3, enemy4;
		Vector3 pointLookAt;
		float timeUpdate = 0;
		private Generation gen;

		private string m_MovementAxisName;
		// The name of the input axis for moving forward and back.
		private string m_TurnAxisName;
		// The name of the input axis for turning.
		private Rigidbody m_Rigidbody;
		// Reference used to move the tank.
		private float m_MovementInputValue;
		// The current value of the movement input.
		private float m_TurnInputValue;
		// The current value of the turn input.
		private float m_OriginalPitch;
		// The pitch of the audio source at the start of the scene.

		private bool isLeft = false, isRight = false, isTop = false, isBottom = false;

		private void Awake ()
		{
			m_Rigidbody = GetComponent < Rigidbody > ();
		}


		private void OnEnable ()
		{
			// When the tank is turned on, make sure it's not kinematic.
			m_Rigidbody.isKinematic = false;

			// Also reset the input values.
			m_MovementInputValue = 0f;
			m_TurnInputValue = 0f;
		}


		private void OnDisable ()
		{
			// When the tank is turned off, set it to kinematic so it stops moving.
			m_Rigidbody.isKinematic = true;
		}


		private void Start ()
		{
			// The axes names are based on player number.
			m_MovementAxisName = "Vertical" + m_PlayerNumber;
			m_TurnAxisName = "Horizontal" + m_PlayerNumber;

			// Store the original pitch of the audio source.
			// m_OriginalPitch = m_MovementAudio.pitch;

			gen = FindObjectOfType (typeof(Generation)) as Generation;
		}

		private float distance (Vector2 a, Vector2 b)
		{
			return Mathf.Sqrt ((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
		}

		private void Update ()
		{
			// Store the value of both input axes.
			m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
			m_TurnInputValue = Input.GetAxis (m_TurnAxisName);

			EngineAudio ();

			//for rotate of enemy
			timeUpdate += Time.deltaTime;
			if (gen.player) {
				float dis = distance (new Vector2 (transform.position.x, transform.position.z), new Vector2 (gen.player.position.x, gen.player.position.z));
				if (dis < 15)
					this.transform.LookAt (gen.player);
				else
					enemyTurn ();

				if (timeUpdate > 6) {
					timeUpdate = 0;
					//timeWaitUpdate = 0;
				}
				enemyMovement ();
			}
			//if (gen.player)
			//	enemyMovement ();
		}

		IEnumerator WaitTime ()
		{
			yield
				return new WaitForSeconds (0.5f);
			pointLookAt = new Vector3 (Random.Range (170, 180), 0, Random.Range (170, 180));
			Debug.Log ("on here");
		}

		private void FixedUpdate ()
		{
			playerTurn ();
			playerMovement ();

		}

		public void enemyMovement ()
		{
			// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
			if (timeUpdate > 2.5 && timeUpdate < 5) {
				Vector3 movement = new Vector3 (0, 0, 0);

				if (gen.enemy1) {
					movement = transform.forward * Random.Range (1, 5) * Time.deltaTime;
				}
				if (gen.enemy2) {
					movement = transform.forward * Random.Range (5, 12) * Time.deltaTime;
				}

				m_Rigidbody.MovePosition (m_Rigidbody.position + movement);
			} else if (timeUpdate > 5) {
				timeUpdate = 0;
			}
		}

		private void EngineAudio ()
		{
			// If there is no input (the tank is stationary)...
			if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f) {
				// ... and if the audio source is currently playing the driving clip...
				//if (m_MovementAudio.clip == m_EngineDriving)
			//	 {
				// ... change the clip to idling and play it.
			//	     m_MovementAudio.clip = m_EngineIdling;
			//	      m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				//     m_MovementAudio.Play ();
				//  }
			} else {
				// Otherwise if the tank is moving and if the idling clip is currently playing...
				//  if (m_MovementAudio.clip == m_EngineIdling)
				//  {
				// ... change the clip to driving and play.
			    //   m_MovementAudio.clip = m_EngineDriving;
				//      m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				//     m_MovementAudio.Play();
				//  }
			}
		}

			

		public void setLeft ()
		{
			isLeft = true;
		}

		public void setRight ()
		{
			isRight = true;
		}

		public void setTop ()
		{
			isTop = true;
		}

		public void setBottom ()
		{
			isBottom = true;
		}

		public void notTurn ()
		{
			isLeft = false;
			isRight = false;
			isBottom = false;
			isTop = false;
		}


		public void playerMovement ()
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

		public void enemyTurn ()
		{
			// Determine the number of degrees to be turned based on the input, speed and time between frames.
			float turn = 0;
			if (timeUpdate > 0.55 && timeUpdate < 0.95) {
				if (gen.enemy1)
					turn = (Random.Range (0, 3) + m_TurnSpeed) * Time.deltaTime;
				else if (gen.enemy2)
					turn = (Random.Range (2, 5) + m_TurnSpeed) * Time.deltaTime;
			} else if (timeUpdate > 4 && timeUpdate < 4.5) {
				if (gen.enemy1)
					turn = (Random.Range (3, 5) + m_TurnSpeed) * Time.deltaTime;
				else if (gen.enemy2)
					turn = (Random.Range (-2, 1) + m_TurnSpeed) * Time.deltaTime;
			} else
				turn = 0;
			// Make this into a rotation in the y axis.
			Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

			// Apply this rotation to the rigidbody's rotation.
			m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
		}

		public void playerTurn ()
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