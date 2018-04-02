using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBossCollision : MonoBehaviour {

	public LayerMask bossLayer;
	public float radius;
	public GameObject attackEffect;
	public Transform hitPoint;
	public float damageCount;

	private bool collided;
	private BossHealth bossHealth;

	void Update () {
		Collider[] hits = Physics.OverlapSphere (transform.position, radius, bossLayer);

		foreach (Collider c in hits) {
			if (c.isTrigger) {
				continue;
			}
			collided = true;
			bossHealth = c.gameObject.GetComponent<BossHealth> ();

			if (collided) {
				Instantiate (attackEffect, hitPoint.position, hitPoint.rotation);
				bossHealth.BossTakeDamage (damageCount);
			}
		}
	}

} // AttackBossCollision