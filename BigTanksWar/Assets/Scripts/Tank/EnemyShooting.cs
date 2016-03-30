using UnityEngine;
using System.Collections;
namespace Complete {
	public class EnemyShooting : TankShooting {
		public float minDistance = 15f;
		public float maxDistance = 20f;

		private Transform tPlayer;
		private PlayerMovement player;
		private float dis = 0;

		private float timeOut = 0;

		private void Start() {
			player = FindObjectOfType(typeof(PlayerMovement))as PlayerMovement;
			tPlayer = player.transform;
		}
		// Update is called once per frame
		void Update() {
			if (!GameController.isPause) {
				timeOut += Time.deltaTime / 2;
				dis = player.distance(new Vector2(transform.position.x, transform.position.z),
					new Vector2(tPlayer.position.x, tPlayer.position.z));
				this.enemiesFire();
			}
		}

		private void enemiesFire() {
			if (dis < minDistance) {
				if (timeOut > 0.6) {
					Fire();
					timeOut = 0;
				}
			} else if (dis < maxDistance) {
				if (timeOut > 1) {
					Fire();
					timeOut = 0;
				}
			}
		}
	}
}
