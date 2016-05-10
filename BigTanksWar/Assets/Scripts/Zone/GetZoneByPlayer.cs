using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetZoneByPlayer : MonoBehaviour {
	const int maxEnemy = 5;
	public GameObject player;
	public ParticleSystem particle;

	private GameController gameController;
	private EnemyManager enemyManager;
	void Start() {
		gameController = FindObjectOfType < GameController > ();
		enemyManager = FindObjectOfType < EnemyManager > ();
		particle.Play();
	}

	void Update() {
		if (gameController.getScoreInZone() >= maxEnemy)
			if (Vector3.Distance(transform.position, player.transform.position) < 5) {
				particle.startColor = Color.blue;
				enemyManager.init(enemyManager.getEmemyGlobal());
				gameController.setScoreInZone(0);
				gameController.countStar++;
				PlayerPrefs.SetInt("StarZone", gameController.countStar);
			Debug.Log ("countStr " + gameController.countStar);
			}

		if (GameController.isPause) {
			gameController.countStar = 0;
		}
	}
}
