using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
		SceneManager.LoadSceneAsync (1);
	}
}
