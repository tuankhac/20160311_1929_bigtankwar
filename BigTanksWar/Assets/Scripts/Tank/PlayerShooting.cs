using UnityEngine;

public class PlayerShooting : TankShooting {

	void Start () {
		Fire ();
	}

	public void touchDown() {
		Fire();
	}

}
