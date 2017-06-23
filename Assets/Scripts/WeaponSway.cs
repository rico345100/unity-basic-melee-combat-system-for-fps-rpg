using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour {
	public float amount = 0.1f;
	public float maxAmount = 0.6f;
	public float smoothAmount = 0.6f;
	private Vector3 initPos;

	void Start() {
		initPos = this.transform.localPosition;
	}

	void Update() {
		float movementX = -Input.GetAxis("Mouse X") * amount;
		float movementY = -Input.GetAxis("Mouse Y") * amount;

		movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
		movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

		Vector3 finalPos = new Vector3(movementX, movementY, 0);
		transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos + initPos, smoothAmount * Time.deltaTime);
	}
}
