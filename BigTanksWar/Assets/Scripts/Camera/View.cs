using UnityEngine;

public class View : MonoBehaviour {
	public GameObject player;
	public GameObject pointFollow;
	public GameObject pointLookAt;
	public Vector3 speed = new Vector3(4.0f, 2.0f, 1.0f);

	public void Start() {
		//pointFollow.transform.position = this.transform.position;
	}

	// Update is called once per frame
	void Update() {
		FollowPosition();

		this.transform.LookAt(pointLookAt.transform);
	}

	void FollowPosition() {
		Vector3 _nextPosition = pointFollow.transform.position;

		_nextPosition.x = Mathf.Lerp(this.transform.position.x, _nextPosition.x, speed.x * Time.deltaTime);
		//_nextPosition.y = Mathf.Lerp (this.transform.position.y, _nextPosition.y, speed.y * Time.deltaTime);
		_nextPosition.z = Mathf.Lerp(this.transform.position.z, _nextPosition.z, speed.z * Time.deltaTime);

		this.transform.position = _nextPosition;

		_nextPosition = player.transform.position;
		_nextPosition.y += 10f;
	}
}
