using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	private Transform zombieTransform;

	public float chaseSpeed;

	private CapsuleCollider col;
	private Transform player;
	private Animator anim;

	private EnemyHealth enemyHealth;
	private PlayerHealth playerHealth;

	private bool victory;

	// Animation states
	private string ANIMATION_ATTACK = "Attack";
	private string ANIMATION_RUN = "Run";
	private string ANIMATION_SPEED = "Speed";
	private string ANIMATION_VICTORY = "Victory";

	private string BASE_LAYER_STAND = "Base Layer.Stand";

	void Awake () {
		col = GetComponent<CapsuleCollider> ();
		anim = GetComponent<Animator> ();

		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = GetComponent<PlayerHealth> ();

		zombieTransform = this.transform;
		enemyHealth = GetComponent<EnemyHealth> ();
	}

	void Update () {
		float distance = Vector3.Distance (zombieTransform.position, player.position);

		if (enemyHealth.realHealth > 0) {
			if (distance > 2.5f) {
				Chase ();
			} else {
				Attack ();
			}
			transform.LookAt (player);
		}
	}

	void Chase () {
		anim.SetBool (ANIMATION_RUN, true);
		anim.SetFloat (ANIMATION_SPEED, chaseSpeed);
		anim.SetBool (ANIMATION_ATTACK, false);
	}

	void Attack () {
		anim.SetBool (ANIMATION_ATTACK, true);
	}

} // EnemyAI