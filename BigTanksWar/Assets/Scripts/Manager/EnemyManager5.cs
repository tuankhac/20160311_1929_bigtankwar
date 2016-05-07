using UnityEngine;
using System.Collections;

public class EnemyManager5 : MonoBehaviour {
	private bool isMove = false;

	void Update () {
		if (Vector3.Distance (transform.position, GameObject.FindGameObjectWithTag ("Player").transform.position) < 44) {
			if (!isMove) {
				GameObject[] _enemy = GameObject.FindGameObjectsWithTag ("Enemy");
				GameObject[] _point = GameObject.FindGameObjectsWithTag ("EnemyZone5");
				for (int i = 0; i < _point.Length; i++) {
					Vector3 _temp = _point [i].transform.position;
					_enemy [i].transform.position = _temp;
				}
				isMove = true;
			}
		} else {
			if (isMove) {
				GameObject[] _enemy = GameObject.FindGameObjectsWithTag ("Enemy");
				GameObject[] _point = GameObject.FindGameObjectsWithTag ("EnemyGlobal");
				for (int i = 0; i < _point.Length; i++) {
					Vector3 _temp = _point [i].transform.position;
					_enemy [i].transform.position = _temp;
				}
				isMove = false;
			}
		}
	}
}
