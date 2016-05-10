using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	float timeUpdate = 0;
	protected Rigidbody m_Rigidbody;
	private float timeDelay;
	public float distanceLookAt = 20f;
	[HideInInspector]public float currentDistance;

	public PlayerMovement player;

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

	void Start() {
		player = FindObjectOfType < PlayerMovement > ();
		timeDelay = Random.Range(6, 9);
		Transform _player = player.transform;
	//	currentDistance = distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_player.position.x, _player.position.z));
		currentDistance = Vector3.Distance (transform.position, _player.position);
	}

	public bool isInRange() {
		Transform _player = player.transform;
	//	currentDistance = distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_player.position.x, _player.position.z));
		currentDistance = Vector3.Distance (transform.position, _player.position);
		if (currentDistance < distanceLookAt)
			return true;
		else
			return false;
	}

	void Update() {
		if (!GameController.isPause) {
			Transform _player = player.transform;
			timeUpdate += Time.deltaTime;
			if (timeUpdate > timeDelay) {
				timeDelay = Random.Range(6, 9);
				timeUpdate = 0;
			}
			if (isInRange())
				this.transform.LookAt(_player);
			else {
				enemyTurn();
			}
			enemyMove();
		}
	}

	private void enemyTurn() {

		// Determine the number of degrees to be turned based on the input, speed and time between frames.
		float turn = 0;
		if (timeUpdate > 0.55f && timeUpdate < 0.95f) {
			turn = (Random.Range(2, 6));
		} else if (timeUpdate > 6 && timeUpdate < 7.5f) {
			turn = (Random.Range(1, 5));
		} else
			turn = 0;

		// Make this into a rotation in the y axis.
		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
		// Apply this rotation to the rigidbody's rotation.
		m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);

	}

	private void enemyMove() {

		// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
		if (timeUpdate > 1.9f && timeUpdate < timeDelay / 2) {
			Vector3 _movement = transform.forward * (Random.value + 1f) / 15;
			m_Rigidbody.MovePosition(m_Rigidbody.position + _movement);
		}

	}
}
