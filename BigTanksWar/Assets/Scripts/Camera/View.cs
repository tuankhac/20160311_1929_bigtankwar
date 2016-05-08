using UnityEngine;

public class View : MonoBehaviour {
	public GameObject player;
	public GameObject pointFollow;
	public GameObject pointLookAt;
	public Vector3 speed = new Vector3(4.0f, 2.0f, 1.0f);

	// Update is called once per frame
	void Update() {
		FollowPosition();
		this.transform.LookAt(pointLookAt.transform);
	}

	void FollowPosition() {
		Vector3 _nextPosition = pointFollow.transform.position;

		_nextPosition.x = Mathf.Lerp(this.transform.position.x, _nextPosition.x, speed.x * Time.deltaTime);
		_nextPosition.z = Mathf.Lerp(this.transform.position.z, _nextPosition.z, speed.z * Time.deltaTime);

		this.transform.position = _nextPosition;
	}
}
