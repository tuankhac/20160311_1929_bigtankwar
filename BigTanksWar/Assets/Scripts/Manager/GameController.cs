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
	bool IsGameOver;
	int score=0;
	int highscore;

	// Use this for initialization
	void Start() {
		//this sets timescale to 1 at start.
		Time.timeScale = 1;
		//this derives the value of highscore at start.
		highscore = PlayerPrefs.GetInt("HighScore",0);
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

	public void Pause() {
		isPause = !isPause;
	}
	public void AddScore(){
		//Add score by 1 and showing that score to GameScoreText.
		score+=1;
		GameScoreText.text= score.ToString ();
	}
	public void addEnemy(){
		Vector3 position = new Vector3 (Random.Range(-50,50),0,Random.Range(-50,50));
		enemy = enemies[Random.Range(0,enemies.Length)];
		Instantiate (enemy, position, Quaternion.identity);
		enemy.SetActive (true);
	}
	public void Restart(){
		//Reload the game on restart button
		Application.LoadLevel (0);
	}
	public void GameOver(){
		//sets is isGameOver to true
		IsGameOver = true;
		//Plays GameOverSound
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		//Pause the time
		Time.timeScale = 0;
		//activate the gameover canvas
		GameOverCanvas.SetActive (true);
		//show the score value on ScoreText
		ScoreText.text = "Score:" + score.ToString();
		//if score is greater than highscore change the stored value of highscore
		if (score > highscore) {
			PlayerPrefs.SetInt ("HighScore", score);
		}
		//show highscore value on HighScoreText
		highscore = PlayerPrefs.GetInt ("HighScore",0);
		HighScoreText.text = "Highscore:" + highscore.ToString ();
	}
}
