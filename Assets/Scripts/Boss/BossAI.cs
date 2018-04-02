using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

	private Animator anim;
	private Transform playerTransform;
	private PlayerHealth playerHealth;
	private BossHealth bossHealth;

	private string ANIMATION_SKILL_ONE = "Skill1";
	private string ANIMATION_SKILL_TWO = "Skill2";
	private string ANIMATION_SKILL_THREE = "Skill3";
	private string ANIMATION_WALK = "Walk";

	void Awake () {
		anim = GetComponent<Animator> ();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = playerTransform.gameObject.GetComponent<PlayerHealth> ();
		bossHealth = GetComponent<BossHealth> ();
	}

	void Update () {
		float distance = Vector3.Distance (transform.position, playerTransform.position);

		if (bossHealth.realHealth > 0) {
			transform.LookAt (playerTransform);
		}

		if (playerHealth.realHealth <= 0) {
			anim.SetBool (ANIMATION_SKILL_ONE, false);
			anim.SetBool (ANIMATION_SKILL_TWO, false);
			anim.SetBool (ANIMATION_SKILL_THREE, false);
			anim.SetBool (ANIMATION_WALK, false);
		}

		if (playerHealth.realHealth > 0) {
			if (distance > 5) {
				anim.SetBool (ANIMATION_WALK, true);
				anim.SetBool (ANIMATION_SKILL_ONE, false);
				anim.SetBool (ANIMATION_SKILL_TWO, false);
				anim.SetBool (ANIMATION_SKILL_THREE, false);
			} else {
				anim.SetBool (ANIMATION_WALK, false);

				if (distance > 2.5f) {
					anim.SetBool (ANIMATION_SKILL_ONE, true);
					anim.SetBool (ANIMATION_SKILL_TWO, false);
					anim.SetBool (ANIMATION_SKILL_THREE, false);
				}

				if (distance <= 2.5f && distance > 0.5f) {
					anim.SetBool (ANIMATION_SKILL_ONE, false);
					anim.SetBool (ANIMATION_SKILL_TWO, true);
					anim.SetBool (ANIMATION_SKILL_THREE, false);
				}

				if (distance <= 0.5f) {
					anim.SetBool (ANIMATION_SKILL_ONE, false);
					anim.SetBool (ANIMATION_SKILL_TWO, false);
					anim.SetBool (ANIMATION_SKILL_THREE, true);
				}
			}
		}

	}

} // BossAI