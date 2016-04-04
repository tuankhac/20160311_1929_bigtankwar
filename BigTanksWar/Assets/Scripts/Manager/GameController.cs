using UnityEngine;
using System.Collections;
using Complete;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	public static bool isPause = false;

	PlayerMovement playerMovement;
	PlayerShooting playerShooting;
	EnemyShooting enemeyShooting;

	public GameObject GameOverCanvas;
	//public GameObject GameCanvas;
	//public GameObject StartCanvas;
	public Text ScoreText;
	public Text HighScoreText;
	public Text GameScoreText;

	public GameObject powerFull;
	private GameObject clonePowerFull;
	bool isPowerShow = false;
	bool isStillShow = false;
	int score = 0;
	int highscore;
	float timeToShow = 0;
	Vector3 position = new Vector3(40, 0, 72.9f);
	Vector3 infinite = new Vector3(0f, -50f, 0);

	private AudioSource m_ExplosionAudio; // The audio source to play when the tank explodes.
	private ParticleSystem m_ExplosionParticles; // The particle system the will play when the tank is destroyed.
	public GameObject m_ExplosionPrefab;
	private void Awake() {
		// Instantiate the explosion prefab and get a reference to the particle system on it.
		m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent < ParticleSystem > ();

		// Get a reference to the audio source on the instantiated prefab.
		m_ExplosionAudio = m_ExplosionParticles.GetComponent < AudioSource > ();

		// Disable the prefab so it can be activated when it's required.
		m_ExplosionParticles.gameObject.SetActive(false);

		clonePowerFull = Instantiate(powerFull);
		clonePowerFull.gameObject.SetActive(false);
	}
	// Use this for initialization
	void Start() {
		//this sets timescale to 1 at start.
		Time.timeScale = 1;
		//this derives the value of highscore at start.
		highscore = PlayerPrefs.GetInt("HighScore", 0);
		//this disables GameOverCanves and GameCanvas, enables startCanvas.
		GameOverCanvas.SetActive(false);
		//GameCanvas.SetActive (false);
		//StartCanvas.SetActive (true);

		playerMovement = FindObjectOfType(typeof(PlayerMovement))as PlayerMovement;
		playerShooting = FindObjectOfType(typeof(PlayerShooting))as PlayerShooting;
		enemeyShooting = FindObjectOfType(typeof(EnemyShooting))as EnemyShooting;

	}

	// Update is called once per frame
	void Update() {
		if (!isPause) {
			enemeyShooting.enabled = true;
			playerMovement.enabled = true;
			playerShooting.enabled = true;
		} else {
			enemeyShooting.enabled = false;
			playerMovement.enabled = false;
			playerShooting.enabled = false;
		}
		timeToShow += Time.deltaTime;
		if (isPowerShow) {
			if (GameObject.Find("Player") != null) {
				position.x = Random.Range(0, 10) + GameObject.Find("Player").gameObject.transform.position.x;
				position.z = Random.Range(0, 10) + GameObject.Find("Player").gameObject.transform.position.z;
				clonePowerFull.gameObject.transform.position = position;
				clonePowerFull.gameObject.SetActive(true);

				isPowerShow = false;
			}
		} else {
			//Debug.Log (clonePowerFull.transform.position);
			//if(timeToShow > 30){
			//clonePowerFull.gameObject.SetActive (false);
			isStillShow = false;
			timeToShow = 0;
			//}
		}

	}
		
	public void AddScore() {
		//Add score by 1 and showing that score to GameScoreText.
		score += 1;
		GameScoreText.text = score.ToString();
	}

	public void RequestPower(float statePower) {
		if (statePower < 50 && !isStillShow) {
			isPowerShow = true;
			isStillShow = true;
		}
	}

	public void addEnemy(Transform ene) {
		float x = Random.Range(20, 30) + ene.transform.position.x;
		float z = Random.Range(20, 40) + ene.transform.position.z;
		if (x > 140)
			x = 140;
		if (z > 140)
			z = 140;
		Vector3 position = new Vector3(x, 0, z);
		//Instantiate (enemy, position, Quaternion.identity);
		ene.transform.position = position;
		ene.gameObject.SetActive(true);
	}

	public void GameOver() {
		//Plays GameOverSound
		AudioSource audio = GetComponent < AudioSource > ();
		audio.Play();
		//Pause the time
		Time.timeScale = 0;
		//activate the gameover canvas
		GameOverCanvas.SetActive(true);
		//show the score value on ScoreText
		ScoreText.text = "Your Score: " + score.ToString();
		//if score is greater than highscore change the stored value of highscore
		if (score > highscore) {
			PlayerPrefs.SetInt("HighScore", score);
		}
		//show highscore value on HighScoreText
		highscore = PlayerPrefs.GetInt("HighScore", 0);
		HighScoreText.text = "High Score: " + highscore.ToString();
	}

	public void OnDeath(Transform m_Transform) {
		//InitParticle();

		// Set the flag so that this function is only called once.
		TankHealth.m_Dead = true;

		// Move the instantiated explosion prefab to the tank's position and turn it on.
		m_ExplosionParticles.transform.position = m_Transform.position;
		m_ExplosionParticles.gameObject.SetActive(true);

		// Play the particle system of the tank exploding.
		m_ExplosionParticles.Play();

		// Play the tank explosion sound effect.
		m_ExplosionAudio.Play();
		m_ExplosionAudio.Stop();
		// Turn the tank off.
		m_Transform.gameObject.SetActive(false);
	}
}
