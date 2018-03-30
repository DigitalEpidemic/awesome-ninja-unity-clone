using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour {

	public LayerMask enemyLayer;
	public float radius;
	public GameObject attackEffect;

	public Transform hitPoint;
	public float damageCount;

	private EnemyHealth enemyHealth;
	private bool collided;

	void Update () {
		Collider[] hits = Physics.OverlapSphere (hitPoint.position, radius, enemyLayer);

		foreach (Collider c in hits) {
			if (c.isTrigger) {
				// Skip next lines of code if true
				continue;
			}
			enemyHealth = c.gameObject.GetComponent<EnemyHealth> ();
			collided = true;

			if (collided) {
				Instantiate (attackEffect, hitPoint.position, hitPoint.rotation);
				enemyHealth.EnemyTakeDamage (damageCount);
			}
		}
	}

} // AttackCollision