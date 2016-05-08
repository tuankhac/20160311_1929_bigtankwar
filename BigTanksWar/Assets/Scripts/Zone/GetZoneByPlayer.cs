using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetZoneByPlayer : MonoBehaviour {
	public GameObject player;
	public ParticleSystem particle;

	private GameController gameController;
	private EnemyManager enemyManager;
	private int countStar = 0;
	void Start() {
		gameController = FindObjectOfType < GameController > ();
		enemyManager = FindObjectOfType < EnemyManager > ();
		particle.Play();
	}

	void Update() {
		if (gameController.getScoreInZone() >= 5)
			if (Vector3.Distance(transform.position, player.transform.position) < 5) {
				particle.startColor = Color.blue;
				enemyManager.init(enemyManager.getEmemyGlobal());
				gameController.setScoreInZone(0);
				countStar++;
				PlayerPrefs.SetInt("StarZone", countStar);
			}

		if (GameController.isPause) {
			countStar = 0;
		}
	}
	public int getCountStar() {
		return countStar;
	}
}
