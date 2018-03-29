using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrail : MonoBehaviour {

	private MeleeWeaponTrail weaponTrail;
	private Transform sword;

	public GameObject hitPoint;
	public GameObject slashThreeEffectPrefab;
	public Transform slashThreePoint;

	private AudioSource audioSource;

	public AudioClip swordHit1;
	public AudioClip swordHit2;
	public AudioClip earthHitSound;
	public AudioClip jiaoHanSheng;

	void Awake () {
		sword = GameObject.Find ("Sword").transform;
		weaponTrail = sword.gameObject.GetComponent<MeleeWeaponTrail> ();
		audioSource = GetComponent<AudioSource> ();
	}

	void SlashOneWeaponTrailStart (bool show) {
		if (show) {
			weaponTrail.Emit = true;
			hitPoint.SetActive (true);
			audioSource.PlayOneShot (swordHit1);
		}
	}

	void SlashOneWeaponTrailEnd (bool end) {
		if (end) {
			weaponTrail.Emit = false;
			hitPoint.SetActive (false);
		}
	}

	void SlashTwoWeaponTrailStart (bool show) {
		if (show) {
			weaponTrail.Emit = true;
			hitPoint.SetActive (true);
			audioSource.PlayOneShot (swordHit2);
		}
	}

	void SlashTwoWeaponTrailEnd (bool end) {
		if (end) {
			weaponTrail.Emit = false;
			hitPoint.SetActive (false);
		}
	}

	void SlashThreeWeaponTrailStart (bool show) {
		if (show) {
			weaponTrail.Emit = true;
			hitPoint.SetActive (true);
			audioSource.PlayOneShot (jiaoHanSheng);
		}
	}

	void SlashThreeWeaponTrailEnd (bool end) {
		if (end) {
			weaponTrail.Emit = false;
			hitPoint.SetActive (false);
		}
	}

	void SlashThreeEffect (bool show) {
		if (show) {
			Instantiate (slashThreeEffectPrefab, slashThreePoint.position, slashThreePoint.rotation);
			audioSource.PlayOneShot (earthHitSound);
		}
	}

} // SwordTrail