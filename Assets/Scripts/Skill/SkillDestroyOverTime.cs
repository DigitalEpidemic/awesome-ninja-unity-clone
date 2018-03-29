using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDestroyOverTime : MonoBehaviour {

	public float timer = 3f;

	void Start () {
		Destroy (gameObject, timer);
	}

} // SkillDestroyOverTime