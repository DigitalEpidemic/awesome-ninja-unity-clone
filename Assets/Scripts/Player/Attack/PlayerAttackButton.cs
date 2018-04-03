using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	private PlayerAttacks playerAttack;

	void Awake () {
		playerAttack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttacks> ();
	}

	public void OnPointerDown (PointerEventData data) {
		if (gameObject.name == "Attack Button") {
			playerAttack.AttackButtonPressed ();
		}
	}

	public void OnPointerUp (PointerEventData data) {
		if (gameObject.name == "Attack Button") {
			playerAttack.AttackButtonReleased ();
		}
	}

} // PlayerAttackButton