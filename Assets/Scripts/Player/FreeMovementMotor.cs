using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovementMotor : MonoBehaviour {

	[HideInInspector]
	public Vector3 movementDirection;

	private Rigidbody myBody;

	public float walkingSpeed = 5f;
	public float walkingSnappiness = 50f;
	public float turningSmoothing = 0.3f;

	void Awake () {
		myBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		Vector3 targetVelocity = movementDirection * walkingSpeed;
		Vector3 deltaVelocity = targetVelocity - myBody.velocity;

		if (myBody.useGravity) {
			deltaVelocity.y = 0f;
		}

		myBody.AddForce (deltaVelocity * walkingSnappiness, ForceMode.Acceleration);

		Vector3 faceDirection = movementDirection;

		if (faceDirection == Vector3.zero) {
			myBody.angularVelocity = Vector3.zero;
		} else {
			float rotationAngle = AngleAroundAxis (transform.forward, faceDirection, Vector3.up);
			myBody.angularVelocity = (Vector3.up * rotationAngle * turningSmoothing);
		}
	}

	float AngleAroundAxis (Vector3 directionA, Vector3 directionB, Vector3 axis) {
		directionA = directionA - Vector3.Project (directionA, axis);
		directionB = directionB - Vector3.Project (directionB, axis);

		float angle = Vector3.Angle (directionA, directionB);

		return angle * (Vector3.Dot (axis, Vector3.Cross (directionA, directionB)) < 0 ? -1 : 1);
	}

} // FreeMovementMotor