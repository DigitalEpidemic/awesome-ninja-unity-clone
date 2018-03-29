using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMove : MonoBehaviour {

	public float x = 0f, y = 0f, z = 0f;

	public bool local = false;

	void Update () {
		if (local) {
			transform.Translate (new Vector3 (x, y, z) * Time.deltaTime);
		}

		if (!local) {
			transform.Translate (new Vector3 (x, y, z) * Time.deltaTime, Space.World);
		}
	}

} // SkillMove