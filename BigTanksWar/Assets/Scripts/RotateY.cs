using UnityEngine;

public class RotateY : MonoBehaviour {
	public float turnVelocity = 8;
	void Update () {
		this.transform.Rotate (0, this.transform.position.y * turnVelocity, 0);
	}
}
