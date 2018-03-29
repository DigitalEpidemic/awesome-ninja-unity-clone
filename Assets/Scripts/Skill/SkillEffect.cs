using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour {

	public LayerMask ignoreLayers;
	public GameObject skillEffectPrefab;
	public float radius;

	private bool collided = false;

	void Update () {
		Collider[] hits = Physics.OverlapSphere (transform.position, radius, ~ignoreLayers);

		foreach (Collider c in hits) {
			if (c.isTrigger) {
				continue;
			}
			collided = true;
		}

		if (collided) {
			Instantiate (skillEffectPrefab, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}

} // SkillEffect