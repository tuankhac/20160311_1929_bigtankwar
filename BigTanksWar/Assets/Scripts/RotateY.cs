﻿using UnityEngine;

public class RotateY : MonoBehaviour {
	void Update () {
		this.transform.Rotate (0, this.transform.position.y * 8, 0);
	}
}
