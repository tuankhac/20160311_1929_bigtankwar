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
	bool isPowerShow = false;
	int score = 0;
	int highscore;
	Vector3 position = new Vector3(40, 0, 72.9f);
	Vector3 infinite = new Vector3(0f, -50f, 0);
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

		Debug.Log (powerFull.gameObject.transform.position);
		powerFull.gameObject.SetActive (false);

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

		if (isPowerShow) {
			//if (Time.deltaTime % 2 == 0) {
			position.x = 0;
			position.z = 0;
			powerFull.gameObject.transform.position = position;
			powerFull.gameObject.SetActive (true);
			Debug.Log (powerFull.gameObject.transform.position + " " + isPowerShow);
			isPowerShow = false;
			//}
		} else {
			
			//powerFull.transform.position = infinite;
		}
		Debug.Log (powerFull.gameObject.transform.position + " " + isPowerShow);
	}

	public void AddScore() {
		//Add score by 1 and showing that score to GameScoreText.
		score += 1;
		GameScoreText.text = score.ToString();
	}

	public void RequestPower(float statePower) {
		if (statePower < 50)
			isPowerShow = true;
	}

	public void addEnemy(Transform ene) {

		//enemy = enemies[Random.Range(0,enemies.Length)];
		Vector3 position = new Vector3(Random.Range(0, 30) + ene.transform.position.x, 0,
			Random.Range(0, 30) + ene.transform.position.z);
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
}
