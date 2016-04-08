using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	private bool is_load_gameplay = false;
	private bool load_scene = false;
	public Text text_loading;

	void Update () {
		if (is_load_gameplay && !load_scene) {
			load_scene = true;

			text_loading.text = "Loading...";
			StartCoroutine (LoadNewScene ());
		}
	}

	public void setLoadGameplay () {
		is_load_gameplay = true;
	}

	IEnumerator LoadNewScene () {
		yield return new WaitForSeconds (3);
		//AsyncOperation async = Application.LoadLevelAsync (1);
		SceneManager.LoadSceneAsync (1);
		//Application.LoadLevel ("Sences/GamePlayer");
		//while (!async.isDone) {
		//	yield return null;
		//}
	}
}
