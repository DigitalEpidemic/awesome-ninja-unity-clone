using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamageBoss : MonoBehaviour {

	public LayerMask bossLayer;
	public float radius;
	public GameObject damageEffect;
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
				Instantiate (damageEffect, transform.position, transform.rotation);
				bossHealth.BossTakeDamage (damageCount);
				Destroy (gameObject);
			}
		}
	}

} // SkillDamageBoss