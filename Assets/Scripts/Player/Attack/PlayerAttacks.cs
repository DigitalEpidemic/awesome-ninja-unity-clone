using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

	// Skill one
	public GameObject skillOne_EffectPrefab;
	public GameObject skillOne_DamagePrefab;

	public Transform skillOne_Point;

	public Transform skillOne_Point1;
	public Transform skillOne_Point2;
	public Transform skillOne_Point3;
	public Transform skillOne_Point4;
	public Transform skillOne_Point5;
	public Transform skillOne_Point6;
	public Transform skillOne_Point7;
	public Transform skillOne_Point8;

	// Skill two
	public GameObject skillTwo_EffectPrefab;
	public GameObject skillTwo_DamagePrefab;

	public Transform skillTwo_Point;

	public Transform skillTwo_Point1;
	public Transform skillTwo_Point2;
	public Transform skillTwo_Point3;
	public Transform skillTwo_Point4;
	public Transform skillTwo_Point5;
	public Transform skillTwo_Point6;

	// Skill three
	public GameObject skillThree_EffectPrefab;

	public Transform skillThree_Point1;
	public Transform skillThree_Point2;
	public Transform skillThree_Point3;
	public Transform skillThree_Point4;
	public Transform skillThree_Point5;

	public AudioClip skillOne_Sound1;
	public AudioClip skillOne_Sound2;
	public AudioClip playerSkillOneSound;
	public AudioClip skillTwo_Sound;
	public AudioClip skillThree_Sound;

	private Animator anim;
	private AudioSource audioSource;

	private bool skillOne_NotUsed;
	private bool skillTwo_NotUsed;
	private bool skillThree_NotUsed;

	// Animation states
	private string ANIMATION_ATTACK = "Attack";
	private string ANIMATION_SKILL_ONE = "Skill1";
	private string ANIMATION_SKILL_TWO = "Skill2";
	private string ANIMATION_SKILL_THREE = "Skill3";

	void Awake () {
		anim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();

		skillOne_NotUsed = true;
		skillTwo_NotUsed = true;
		skillThree_NotUsed = true;
	}

	void Update () {
		HandleButtonPresses ();
	}

	public void AttackButtonPressed () {
		anim.SetBool (ANIMATION_ATTACK, true);
	}

	public void AttackButtonReleased () {
		anim.SetBool (ANIMATION_ATTACK, false);
	}

	public void SkillOneButtonPressed () {
		if (skillOne_NotUsed) {
			skillOne_NotUsed = false;
			anim.SetBool (ANIMATION_SKILL_ONE, true);
			StartCoroutine (ResetSkills (1));
		}
	}

	public void SkillTwoButtonPressed () {
		if (skillTwo_NotUsed) {
			skillTwo_NotUsed = false;
			anim.SetBool(ANIMATION_SKILL_TWO, true);
			StartCoroutine (ResetSkills (2));
		}
	}

	public void SkillThreeButtonPressed () {
		if (skillThree_NotUsed) {
			skillThree_NotUsed = false;
			anim.SetBool(ANIMATION_SKILL_THREE, true);
			StartCoroutine (ResetSkills (3));
		}
	}

	void HandleButtonPresses () {
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			anim.SetBool (ANIMATION_ATTACK, true);
		}
		if (Input.GetKeyUp (KeyCode.LeftControl)) {
			anim.SetBool (ANIMATION_ATTACK, false);
		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if (skillOne_NotUsed) {
				skillOne_NotUsed = false;
				anim.SetBool (ANIMATION_SKILL_ONE, true);
				StartCoroutine (ResetSkills (1));
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			if (skillTwo_NotUsed) {
				skillTwo_NotUsed = false;
				anim.SetBool(ANIMATION_SKILL_TWO, true);
				StartCoroutine (ResetSkills (2));
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			if (skillThree_NotUsed) {
				skillThree_NotUsed = false;
				anim.SetBool(ANIMATION_SKILL_THREE, true);
				StartCoroutine (ResetSkills (3));
			}
		}

	}

	// Skill Effects

	// Skill One
	void SkillOne (bool skillOne) {
		if (skillOne) {
			Instantiate (skillOne_EffectPrefab, skillOne_Point.position, skillOne_Point.rotation);
			audioSource.PlayOneShot (skillOne_Sound1);
			StartCoroutine (SkillOneCoroutine ());
		}
	}

	void SkillOneSound (bool play) {
		if (play) {
			audioSource.PlayOneShot (playerSkillOneSound);
		}
	}

	void SkillOneEnd (bool skillOneEnd) {
		if (skillOneEnd) {
			anim.SetBool (ANIMATION_SKILL_ONE, false);
		}
	}

	IEnumerator SkillOneCoroutine () {
		yield return new WaitForSeconds (1.5f);
		audioSource.PlayOneShot (skillOne_Sound2);
		Instantiate (skillOne_DamagePrefab, skillOne_Point1.position, skillOne_Point1.rotation);
		Instantiate (skillOne_DamagePrefab, skillOne_Point2.position, skillOne_Point2.rotation);
		Instantiate (skillOne_DamagePrefab, skillOne_Point3.position, skillOne_Point3.rotation);
		Instantiate (skillOne_DamagePrefab, skillOne_Point4.position, skillOne_Point4.rotation);
		Instantiate (skillOne_DamagePrefab, skillOne_Point5.position, skillOne_Point5.rotation);
		Instantiate (skillOne_DamagePrefab, skillOne_Point6.position, skillOne_Point6.rotation);
		Instantiate (skillOne_DamagePrefab, skillOne_Point7.position, skillOne_Point7.rotation);
		Instantiate (skillOne_DamagePrefab, skillOne_Point8.position, skillOne_Point8.rotation);
	}

	// Skill Two
	void SkillTwo (bool skillTwo) {
		if (skillTwo) {
			Instantiate (skillTwo_EffectPrefab, skillTwo_Point.position, skillTwo_Point.rotation);
			audioSource.PlayOneShot (skillTwo_Sound);
			StartCoroutine (SkillTwoCoroutine ());
		}
	}

	void SkillTwoEnd (bool skillTwoEnd) {
		if (skillTwoEnd) {
			anim.SetBool (ANIMATION_SKILL_TWO, false);
		}
	}

	IEnumerator SkillTwoCoroutine () {
		yield return new WaitForSeconds (1.5f);
		Instantiate (skillTwo_DamagePrefab, skillTwo_Point1.position, skillTwo_Point1.rotation);
		Instantiate (skillTwo_DamagePrefab, skillTwo_Point2.position, skillTwo_Point2.rotation);
		Instantiate (skillTwo_DamagePrefab, skillTwo_Point3.position, skillTwo_Point3.rotation);
		Instantiate (skillTwo_DamagePrefab, skillTwo_Point4.position, skillTwo_Point4.rotation);
		Instantiate (skillTwo_DamagePrefab, skillTwo_Point5.position, skillTwo_Point5.rotation);
		Instantiate (skillTwo_DamagePrefab, skillTwo_Point6.position, skillTwo_Point6.rotation);
	}

	// Skill Three
	void SkillThree (bool skillThree) {
		if (skillThree) {
			Instantiate (skillThree_EffectPrefab, skillThree_Point1.position, skillThree_Point1.rotation);
			Instantiate (skillThree_EffectPrefab, skillThree_Point2.position, skillThree_Point2.rotation);
			Instantiate (skillThree_EffectPrefab, skillThree_Point3.position, skillThree_Point3.rotation);
			Instantiate (skillThree_EffectPrefab, skillThree_Point4.position, skillThree_Point4.rotation);
			Instantiate (skillThree_EffectPrefab, skillThree_Point5.position, skillThree_Point5.rotation);
		}
	}

	void SkillThreeEnd (bool skillThreeEnd) {
		if (skillThreeEnd) {
			anim.SetBool (ANIMATION_SKILL_THREE, false);
		}
	}

	IEnumerator ResetSkills (int skill) {
		yield return new WaitForSeconds (3f);

		switch (skill) {
		case 1:
			skillOne_NotUsed = true;
			break;
		case 2:
			skillTwo_NotUsed = true;
			break;
		case 3:
			skillThree_NotUsed = true;
			break;
		}
	}

} // PlayerAttacks