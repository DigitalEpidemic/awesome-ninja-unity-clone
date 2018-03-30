using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float realHealth;

	private AudioSource audioSource;
	public AudioClip enemyDeathSound;

	private bool enemyDead;
	private bool enemyIsHit;

	private Animator anim;

	public GameObject deathEffect;
	public Transform deathEffectPoint;

	public GameObject attackPointOne;
	public GameObject attackPointTwo;

	private string ANIMATION_ATTACK = "Attack";
	private string ANIMATION_BE_ATTACKED = "BeAttacked";
	private string ANIMATION_DEAD = "Dead";

	private string BASE_LAYER_DYING = "Base Layer.Dying";
	private string BASE_LAYER_HIT = "Base Layer.Hit";

	void Awake () {
		audioSource = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (enemyDead) {
			StopDeathAnimation();
		}

		if (enemyIsHit) {
			EnemyAttacked ();
		}

		if (!enemyIsHit) {
			StopEnemyHit ();
		}
	}

	void EnemyDying () {
		anim.SetBool (ANIMATION_DEAD, true);
		anim.SetBool (ANIMATION_BE_ATTACKED, false);
		enemyDead = true;
				StartCoroutine (DeathEffect ());
		attackPointOne.SetActive (false);
		attackPointTwo.SetActive (false);
		audioSource.PlayOneShot (enemyDeathSound);
	}

	void StopDeathAnimation () {
		if (anim.GetCurrentAnimatorStateInfo (0).IsName (BASE_LAYER_DYING)) {
			anim.SetBool (ANIMATION_DEAD, false);
		}
	}

	public void EnemyTakeDamage (float amount) {
		realHealth -= amount;

//		Debug.Log ("The current health is " + realHealth);

		if (realHealth <= 0) {
			realHealth = 0;
			EnemyDying ();
		}

		if (amount > 0) {
			enemyIsHit = true;
		}

	}

	void EnemyAttacked () {
		enemyIsHit = false;
		anim.SetBool (ANIMATION_BE_ATTACKED, true);
		anim.SetBool (ANIMATION_ATTACK, false);
		transform.Translate (Vector3.back * 0.4f);
	}

	void StopEnemyHit () {
		if (anim.GetCurrentAnimatorStateInfo (0).IsName (BASE_LAYER_HIT)) {
			anim.SetBool (ANIMATION_BE_ATTACKED, false);
		}
	}

	IEnumerator DeathEffect () {
		yield return new WaitForSeconds (2f);
		Instantiate (deathEffect, deathEffectPoint.position, deathEffectPoint.rotation);
		Destroy (gameObject);
	}

} // EnemyHealth