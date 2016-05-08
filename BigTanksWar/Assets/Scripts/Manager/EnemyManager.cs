using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	private bool isMove = false;
	public string enemyZone;
	private string enemyGlobal = "EnemyGlobal";
	PlayerMovement playerMovement; 

	void Start(){
		playerMovement = FindObjectOfType<PlayerMovement> ();
	}
	void Update () {
		if (isGlobal() && !isMove) {
			init (enemyGlobal);
			isMove = true;
			playerMovement.ePlayerZone = "";
		}
		if (Vector3.Distance (transform.position,playerMovement.transform.position) < 44 && isMove) {
			init (enemyZone);
			isMove = false;
			playerMovement.ePlayerZone = enemyZone;
		}
	}
	void init(string tag){
		GameObject[] _enemy = GameObject.FindGameObjectsWithTag ("Enemy");
		GameObject[] _point = GameObject.FindGameObjectsWithTag (tag);
		for (int i = 0; i < _point.Length; i++) {
			Vector3 _temp = _point [i].transform.position;
			_enemy [i].transform.position = _temp;
		}
	}
	bool isGlobal(){
		GameObject[] objs = GameObject.FindGameObjectsWithTag (enemyZone);
		for(int i = 0 ;i < objs.Length ;i++)
			if (Vector3.Distance (objs[i].transform.position, playerMovement.transform.position) < 44) {
				return false;
				break;
			}
		return true;
	}
}