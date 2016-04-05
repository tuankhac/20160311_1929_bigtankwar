using UnityEngine;
using System.Collections;

public class PlayerShooting : TankShooting {
	private bool isDown = false;

	void Start () {
		Fire ();
	}

	// Update is called once per frame
	void Update() {
		
		if (isDown || Input.GetMouseButtonDown(0)) {
			Fire();
		}

	}

	public void touchDown() {
		isDown = true;
	}

	public void touchUp() {
		isDown = false;
	}
}
