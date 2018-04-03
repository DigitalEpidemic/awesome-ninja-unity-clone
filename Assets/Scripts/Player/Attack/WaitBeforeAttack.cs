using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitBeforeAttack : MonoBehaviour {

	public int waitTime;
	private int fadeTime;

	private Text waitText;
	private Image fadeImage;

	private bool canFade;
	private GameObject waitPanel;

	void Awake () {
		waitPanel = transform.GetChild (0).gameObject;

		waitText = waitPanel.GetComponentInChildren<Text> ();
		waitText.text = waitTime.ToString ();

		fadeImage = waitPanel.GetComponent<Image> ();
		fadeTime = waitTime;

		waitPanel.SetActive (false);
	}

	void Update () {
		FadeOut ();
	}

	public void ActivateFadeOut () {
		waitPanel.SetActive (true);
		waitText.text = waitTime.ToString ();
		Color temp = fadeImage.color;
		temp.a = 1f;
		fadeImage.color = temp;
		StartCoroutine (CountDown ());
	}

	void FadeOut () {
		if (canFade) {
			Color temp = fadeImage.color;
			temp.a -= (Time.deltaTime / fadeTime) / 2f;
			fadeImage.color = temp;
		}
	}

	IEnumerator CountDown () {
		canFade = true;
		yield return new WaitForSeconds (1f);
		waitTime -= 1;

		if (waitTime != -1) {
			waitText.text = waitTime.ToString ();
			StartCoroutine (CountDown ());
		} else {
			waitTime = fadeTime;
			waitPanel.SetActive (false);
		}
	}

} // WaitBeforeAttack