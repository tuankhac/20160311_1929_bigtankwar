using UnityEngine;

public class PlayerMovement : TankMovement {
	public AudioSource m_MovementAudio; // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
	public AudioClip m_EngineIdling; // Audio to play when the tank isn't moving.
	public AudioClip m_EngineDriving; // Audio to play when the tank is moving.

	public string ePlayerZone = "";
	private bool isLeft = false,
	isRight = false,
	isTop = false,
	isBottom = false;

	private float m_MovementInputValue; // The current value of the movement input.
	private float m_TurnInputValue; // The current value of the turn input.
	private string m_MovementAxisName; // The name of the input axis for moving forward and back.
	private string m_TurnAxisName; // The name of the input axis for turning.

	void Start() {
		// Also reset the input values.
		m_MovementInputValue = 0f;
		m_TurnInputValue = 0f;

		// The axes names are based on player number.
		m_MovementAxisName = "Vertical1";
		m_TurnAxisName = "Horizontal1";

	}
	// Update is called once per frame
	void Update() {
		// Store the value of both input axes.
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

		playerTurn();
		EngineAudio();
		//keyboard test
		Move();
		Turn();

		playerMovement();
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
		if (other.gameObject.tag == "Powerfull") {
			Destroy (other.gameObject);
			Collider collider =  GameObject.Find ("Player").GetComponent<Collider> ();
			// Deal this damage to the tank.
			TankHealth targetHealth = collider.GetComponent<TankHealth>();
			float _add = -50f;
			targetHealth.TakeDamage(_add, collider);
		}
	}

	public void EngineAudio() {
		// Otherwise if the tank is moving and if the idling clip is currently playing...{
		// ... change the clip to driving and play.

		if (isLeft || isRight)
		{
			if (m_MovementAudio.clip == m_EngineDriving)
			{
				// ... change the clip to idling and play it.
				m_MovementAudio.clip = m_EngineIdling;
				//	m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play ();
			}
		}
		if (isTop || isBottom)
		{
			// Otherwise if the tank is moving and if the idling clip is currently playing...
			if (m_MovementAudio.clip == m_EngineIdling)
			{
				// ... change the clip to driving and play.
				m_MovementAudio.clip = m_EngineDriving;
				//	m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play();
			}
		}
	}
}
