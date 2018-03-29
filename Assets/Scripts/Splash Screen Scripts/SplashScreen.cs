using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	void Start () {
		StartCoroutine (LoadSceneAfterSplashScreen());
	}
	
	IEnumerator LoadSceneAfterSplashScreen() {
		yield return new WaitForSecondsRealtime (2.5f);
		SceneManager.LoadScene ("MainMenu");
	}

} // script


















































