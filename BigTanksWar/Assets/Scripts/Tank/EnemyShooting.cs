using UnityEngine;

public class EnemyShooting : TankShooting {
	private EnemyMovement movement;
	private float timeOut = 0;

	private void Start() {
		movement = this.GetComponent<EnemyMovement>();
	}

	void Update() {
		if (!GameController.isPause) {
			timeOut += Time.deltaTime;

			if (movement.isInRange () && timeOut > 1.7f) {
				Fire ();
				timeOut = 0;
			}
		}
	}
}
