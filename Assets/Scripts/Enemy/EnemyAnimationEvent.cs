using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour {

	public GameObject attackPointOne;
	public GameObject attackPointTwo;
	public GameObject enemyAttackEffect;

	void EnemyAttackOne (bool attacking) {
		if (attacking) {
			Instantiate (enemyAttackEffect, attackPointOne.transform.position, attackPointOne.transform.rotation);
		}
	}

	void EnemyAttackTwo (bool attacking) {
		if (attacking) {
			Instantiate (enemyAttackEffect, attackPointTwo.transform.position, attackPointTwo.transform.rotation);
		}
	}

	void EnemyAttackOneStart (bool attackStarted) {
		if (attackStarted) {
			attackPointOne.SetActive (true);
		}
	}

	void EnemyAttackOneEnd (bool attackEnded) {
		if (attackEnded) {
			attackPointOne.SetActive (false);
		}
	}

	void EnemyAttackTwoStart (bool attackStarted) {
		if (attackStarted) {
			attackPointTwo.SetActive (true);
		}
	}

	void EnemyAttackTwoEnd (bool attackEnded) {
		if (attackEnded) {
			attackPointTwo.SetActive (false);
		}
	}

} // EnemyAnimationEvent