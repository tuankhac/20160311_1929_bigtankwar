using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Manager : MonoBehaviour {
	public Image image;
	public void Pause() {
		GameController.isPause = !GameController.isPause;
		if (GameController.isPause)
			image.color = Color.red;
		else
			image.color = Color.white;
	}
	public void Restart() {
		GameController.isPause = false;
		Application.LoadLevel(1);
	}
}
