using UnityEngine;
using System.Collections;

public class GetZoneByPlayer : MonoBehaviour {
	public GameObject player;
	public ParticleSystem particle;

	void Start () {
		particle.Play ();
	}

	void Update () {
		if (Vector3.Distance (transform.position, player.transform.position) < 5) {
			particle.startColor = Color.blue;
		}
	}
}
