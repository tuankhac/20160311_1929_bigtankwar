using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public float maxInZone = 44f;
	private bool isMove = false;
	public string enemyZone;
	private string enemyGlobal = "EnemyGlobal";
	PlayerMovement playerMovement;
	GameController gameController;
	void Start() {
		playerMovement = FindObjectOfType < PlayerMovement > ();
		gameController = FindObjectOfType<GameController> ();
	}
	void Update() {
		if (isGlobal() && !isMove) {
			init(enemyGlobal);
			isMove = true;
			gameController.setScoreInZone (0);
			playerMovement.ePlayerZone = "";
		}
		if (Vector3.Distance(transform.position, playerMovement.transform.position) < maxInZone && isMove) {
			init(enemyZone);
			isMove = false;
			playerMovement.ePlayerZone = enemyZone;
		}
	}
	public void init(string tag) {
		GameObject[]_enemy = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[]_point = GameObject.FindGameObjectsWithTag(tag);
		for (int i = 0; i < _point.Length; i++) {
			Vector3 _temp = _point[i].transform.position;
			_enemy[i].transform.position = _temp;
		}
	}
	private bool isGlobal() {
		GameObject[]objs = GameObject.FindGameObjectsWithTag(enemyZone);
		for (int i = 0; i < objs.Length; i++)
			if (Vector3.Distance(objs[i].transform.position, playerMovement.transform.position) < maxInZone) {
				return false;
			}
		return true;
	}
	public string getEmemyGlobal() {
		return enemyGlobal;
	}
}
