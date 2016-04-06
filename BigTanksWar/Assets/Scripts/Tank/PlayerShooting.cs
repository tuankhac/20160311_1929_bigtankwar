using UnityEngine;
using System.Collections;

public class PlayerShooting : TankShooting {

	void Start () {
		Fire ();
	}

	public void touchDown() {
		Fire();
	}

}
