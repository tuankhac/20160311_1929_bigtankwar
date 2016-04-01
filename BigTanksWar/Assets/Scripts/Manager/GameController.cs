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
	public GameObject[] enemies;

	GameObject enemy;
	int score = 0;
	int highscore;

	// Use this for initialization
	void Start() {
		//this sets timescale to 1 at start.
		Time.timeScale = 1;
		//this derives the value of highscore at start.
		highscore = PlayerPrefs.GetInt("HighScore", 0);
		//this disables GameOverCanves and GameCanvas, enables startCanvas.
		GameOverCanvas.SetActive (false);
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
	}


	public void AddScore(){
		//Add score by 1 and showing that score to GameScoreText.
		score+=1;
		GameScoreText.text= score.ToString ();
	}
	public void addEnemy(){
		
		enemy = enemies[Random.Range(0,enemies.Length)];
		Vector3 position = new Vector3 (Random.Range(-30,30)* enemy.transform.position.x,0,
			Random.Range(-30,30)* enemy.transform.position.z);
		Instantiate (enemy, position, Quaternion.identity);
		enemy.SetActive (true);
	}

	public void GameOver(){
		//Plays GameOverSound
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		//Pause the time
		Time.timeScale = 0;
		//activate the gameover canvas
		GameOverCanvas.SetActive (true);
		//show the score value on ScoreText
		ScoreText.text = "Your Score: " + score.ToString();
		//if score is greater than highscore change the stored value of highscore
		if (score > highscore) {
			PlayerPrefs.SetInt ("HighScore", score);
		}
		//show highscore value on HighScoreText
		highscore = PlayerPrefs.GetInt ("HighScore",0);
		HighScoreText.text = "High Score: " + highscore.ToString ();
	}
}
