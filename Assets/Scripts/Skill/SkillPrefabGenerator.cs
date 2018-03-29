using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPrefabGenerator : MonoBehaviour {
	
	public GameObject[] skillPrefabs;

	private int randomNumber;

	public int thisManyTimes = 3;
	public float overThisTime = 3f;

	public float x_Width, y_Width, z_Width;
	public float x_RotationMax, y_RotationMax = 180f, z_RotationMax;

	public bool allUseSameRotation = false;
	private bool allRotationDecided = false;

	private float x_Current, y_Current, z_Current;
	private float x_RotationCurrent, y_RotationCurrent, z_RotationCurrent;

	private float timeCounter;
	private float effectCounter;

	private float trigger;

	void Start () {
		if (thisManyTimes < 1) {
			thisManyTimes = 1;
		}

		trigger = overThisTime / thisManyTimes;
	}

	void Update () {
		timeCounter += Time.deltaTime;

		if (timeCounter > trigger && effectCounter <= thisManyTimes) {
			randomNumber = Random.Range (0, skillPrefabs.Length);

			x_Current = transform.position.x + (Random.value * x_Width) - (x_Width * 0.5f);
			y_Current = transform.position.y + (Random.value * y_Width) - (y_Width * 0.5f);
			z_Current = transform.position.z + (Random.value * z_Width) - (z_Width * 0.5f);

			if (!allUseSameRotation || !allRotationDecided) {
				x_RotationCurrent = transform.rotation.x + (Random.value * x_RotationMax * 2) - (x_RotationMax);
				y_RotationCurrent = transform.rotation.y + (Random.value * y_RotationMax * 2) - (y_RotationMax);
				z_RotationCurrent = transform.rotation.z + (Random.value * z_RotationMax * 2) - (z_RotationMax);
				allRotationDecided = true;
			}

			GameObject skill = Instantiate (skillPrefabs [randomNumber], new Vector3 (x_Current, y_Current, z_Current), transform.rotation);
			skill.transform.Rotate (x_RotationCurrent, y_RotationCurrent, z_RotationCurrent);

			timeCounter -= trigger;
			effectCounter += 1;
		}
	}

} // SkillPrefabGenerator