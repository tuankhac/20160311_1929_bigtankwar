using UnityEngine;
using Complete;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameController : MonoBehaviour {

	public static bool isPause = false;
	public GameObject[] arrStar;
	PlayerMovement playerMovement;
	PlayerShooting playerShooting;
	EnemyShooting enemeyShooting;
	EnemyMovement enemyMovement;

	public GameObject GameOverCanvas;
	public Text ScoreText;
	public Text GameScoreText;

	public GameObject powerFull;
	private GameObject clonePowerFull;
	bool isPowerShow = false;
	bool isStillShow = false;
	int score = 0;
	int scoreInZone = 0;
	public int countStar;
	float timeToShow = 0;
	Vector3 position = new Vector3(40, 1, 72.9f);

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

		for (int i = 0; i < arrStar.Length; i++) {
			arrStar [i].SetActive (false);
		}
	}
	// Use this for initialization
	void Start() {
		//this sets timescale to 1 at start.
		Time.timeScale = 1;
		//this disables GameOverCanves and GameCanvas, enables startCanvas.
		GameOverCanvas.SetActive(false);

		playerMovement = FindObjectOfType < PlayerMovement > ();
		playerShooting = FindObjectOfType < PlayerShooting > ();
		enemeyShooting = FindObjectOfType < EnemyShooting > ();
		enemyMovement = FindObjectOfType < EnemyMovement > ();
	}

	// Update is called once per frame
	void Update() {
		if (!isPause) {
			enemeyShooting.enabled = true;
			playerMovement.enabled = true;
			playerShooting.enabled = true;
			enemyMovement.enabled = true;
			timeToShow += Time.deltaTime;

			if (isPowerShow) {
				if (GameObject.Find("Player") != null) {
					position.x = Random.Range(0, 10) + GameObject.Find("Player").gameObject.transform.position.x;
					position.z = Random.Range(0, 10) + GameObject.Find("Player").gameObject.transform.position.z;
					clonePowerFull = (GameObject)Instantiate(powerFull, position, Quaternion.identity);

					isPowerShow = false;
				}
			} else {
				if (timeToShow > 30) {
					Destroy(clonePowerFull);
					isStillShow = false;
					timeToShow = 0;
				}
			}
		} else {
			enemeyShooting.enabled = false;
			playerMovement.enabled = false;
			playerShooting.enabled = false;
			enemyMovement.enabled = false;
		}
	}

	public void AddScore() {
		//Add score by 1 and showing that score to GameScoreText.
		score += 1;
		if (playerMovement.ePlayerZone != "") {
			if (Vector3.Distance(GameObject.FindGameObjectWithTag(playerMovement.ePlayerZone).transform.position,
					playerMovement.transform.position) < 44) {
				scoreInZone++;
			} else
				scoreInZone = 0;
		}
		GameScoreText.text = score.ToString();

		int _time = (int)Time.time;
		if (Advertisement.IsReady() && score % 7 ==0 && _time > 900)
			Advertisement.Show();
	}

	public void RequestPower(float statePower) {
		if (statePower < 50 && !isStillShow) {
			isPowerShow = true;
			isStillShow = true;
		}
	}

	public int getScoreInZone() {
		return scoreInZone;
	}
	public void setScoreInZone(int value) {
		scoreInZone = value;
	}
	public int getCountStar() {
		return countStar;
	}

	int min = 20, max = 30;
	public void addEnemy(Transform ene) {
		float x = ene.transform.position.x + Random.Range(min, max) ;
		float z = ene.transform.position.z + Random.Range(min, max) ;
		if (x > 140)
			x  =  ene.transform.position.x - Random.Range(min, max) ;
		if (z > 140)
			z = ene.transform.position.z - Random.Range (min, max);
		Vector3 position = new Vector3(x, 0, z);
		ene.transform.position = position;
		ene.gameObject.SetActive(true);
	}

	public void GameOver() {
		if (Advertisement.IsReady() && Random.Range(1, 3) == 2)
			Advertisement.Show();
		//Plays GameOverSound
		AudioSource audio = GetComponent < AudioSource > ();
		audio.Play();
		//Pause the time
		Time.timeScale = 0;
		//activate the gameover canvas
		GameOverCanvas.SetActive(true);
		//show the score value on ScoreText
		ScoreText.text = "Your Score: " + score.ToString();

		//show highscore value on HighScoreText
		for (int i = 0; i < countStar; i++) {
			arrStar [i].SetActive (true);
		}
	}

	public void OnDeath(Transform m_Transform) {
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
