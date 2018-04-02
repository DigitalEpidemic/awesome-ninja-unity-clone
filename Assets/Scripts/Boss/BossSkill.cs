using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour {

	public GameObject skill3;
	public GameObject skill3_Point;

	public AudioClip earthHit;
	private AudioSource audioSource;

	public GameObject skill1;
	public GameObject skill1_Point1;
	public GameObject skill1_Point2;
	public GameObject skill1_Point3;
	public GameObject skill1_Point4;
	public GameObject skill1_Point5;
	public GameObject skill1_Point6;
	public GameObject skill1_Point7;
	public GameObject skill1_Point8;
	public GameObject skill1_Point9;
	public GameObject skill1_Point10;

	public GameObject sword;
	public GameObject hitPoint;
	private MeleeWeaponTrail swordTrail;

	void Awake () {
		audioSource = GetComponent<AudioSource> ();
		swordTrail = sword.GetComponent<MeleeWeaponTrail> ();
	}

	void Skill1 (bool execute) {
		if (execute) {
			Instantiate (skill1, skill1_Point1.transform.position, skill1_Point1.transform.rotation);
			Instantiate (skill1, skill1_Point2.transform.position, skill1_Point2.transform.rotation);
			Instantiate (skill1, skill1_Point3.transform.position, skill1_Point3.transform.rotation);
			Instantiate (skill1, skill1_Point4.transform.position, skill1_Point4.transform.rotation);
			Instantiate (skill1, skill1_Point5.transform.position, skill1_Point5.transform.rotation);
			Instantiate (skill1, skill1_Point6.transform.position, skill1_Point6.transform.rotation);
			Instantiate (skill1, skill1_Point7.transform.position, skill1_Point7.transform.rotation);
			Instantiate (skill1, skill1_Point8.transform.position, skill1_Point8.transform.rotation);
			Instantiate (skill1, skill1_Point9.transform.position, skill1_Point9.transform.rotation);
			Instantiate (skill1, skill1_Point10.transform.position, skill1_Point10.transform.rotation);
			StartCoroutine (Skill1AfterWait ());
		}
	}

	void Skill3 (bool execute) {
		if (execute) {
			Instantiate (skill3, skill3_Point.transform.position, skill3_Point.transform.rotation);
			audioSource.PlayOneShot (earthHit);
		}
	}

	void SwordSlashAttack (bool isAttacking) {
		if (isAttacking) {
			swordTrail.Emit = true;
			hitPoint.SetActive (true);
		}
	}

	void SwordSlashAttackEnd (bool attackEnded) {
		if (attackEnded) {
			swordTrail.Emit = false;
			hitPoint.SetActive (false);
		}
	}

	IEnumerator Skill1AfterWait () {
		yield return new WaitForSeconds (1f);
		Instantiate (skill1, skill1_Point1.transform.position, skill1_Point1.transform.rotation);
		Instantiate (skill1, skill1_Point2.transform.position, skill1_Point2.transform.rotation);
		Instantiate (skill1, skill1_Point3.transform.position, skill1_Point3.transform.rotation);
		Instantiate (skill1, skill1_Point4.transform.position, skill1_Point4.transform.rotation);
		Instantiate (skill1, skill1_Point5.transform.position, skill1_Point5.transform.rotation);
		Instantiate (skill1, skill1_Point6.transform.position, skill1_Point6.transform.rotation);
		Instantiate (skill1, skill1_Point7.transform.position, skill1_Point7.transform.rotation);
		Instantiate (skill1, skill1_Point8.transform.position, skill1_Point8.transform.rotation);
		Instantiate (skill1, skill1_Point9.transform.position, skill1_Point9.transform.rotation);
		Instantiate (skill1, skill1_Point10.transform.position, skill1_Point10.transform.rotation);
	}

} // BossSkill