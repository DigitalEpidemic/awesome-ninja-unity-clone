using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour {

	public LayerMask zombieLayer;
	public float radius;
	public float damageCount;
	public GameObject damageEffect;

	private EnemyHealth attackTarget;
	private bool collided;

	void Update () {
		Collider[] hits = Physics.OverlapSphere (transform.position, radius, zombieLayer);

		foreach (Collider c in hits) {
			if (c.isTrigger) {
				continue;
			}
			attackTarget = c.gameObject.GetComponent<EnemyHealth> ();
			collided = true;

			if (collided) {
				Instantiate (damageEffect, transform.position, transform.rotation);
				attackTarget.EnemyTakeDamage (damageCount);
			}
		}
	}

} // SkillDamage