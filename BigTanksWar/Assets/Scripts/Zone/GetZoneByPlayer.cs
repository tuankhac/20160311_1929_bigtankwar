using UnityEngine;
using System.Collections;

public class GetZoneByPlayer : MonoBehaviour {
	public GameObject player;
	public ParticleSystem particle;
	GameController gameController;

	void Start () {
		gameController = FindObjectOfType<GameController> ();
		particle.Play ();
	}

	void Update () {
		if(gameController.getScoreInZone() >=3)
			if (Vector3.Distance (transform.position, player.transform.position) < 5) {
				particle.startColor = Color.blue;
			gameController.setScoreInZone (0);
			}
	}
}
