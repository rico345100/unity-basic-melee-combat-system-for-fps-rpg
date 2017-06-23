using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {
	Animator animator;
	GameObject attackDetectionGObject;
	Enemy enemy;
	float rotatingSpeed = 10f;
	bool isAttacking = false;
	public Transform target;

	void Start() {
		animator = GetComponent<Animator>();
		attackDetectionGObject = this.transform.Find("metarig/hips/spine/chest/shoulder.R/upper_arm.R/forearm.R/hand.R/weapon.01.R/AttackDetection").gameObject;
		enemy = GetComponent<Enemy>();
	}

	void Update() {
		if(enemy.isDead) return;

		if(Vector3.Distance(target.position, this.transform.position) < 10) {
			Vector3 direction = target.position - this.transform.position;
			direction.y = 0;

			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotatingSpeed * Time.deltaTime);

			animator.SetBool("isIdle", false);

			if(direction.magnitude > 1.3f) {
				this.transform.Translate(0, 0, 0.05f);
				animator.SetBool("isWalking", true);
				animator.SetBool("isAttacking", false);
			}
			else if(!isAttacking) {
				animator.SetBool("isWalking", false);
				animator.SetBool("isAttacking", true);

				StartCoroutine(EnableAttackDetection());
			}
		}
		else {
			attackDetectionGObject.GetComponent<Collider>().enabled = false;

			animator.SetBool("isIdle", true);
			animator.SetBool("isWalking", false);
			animator.SetBool("isAttacking", false);
		}
	}

	IEnumerator EnableAttackDetection() {
		isAttacking = true;

		attackDetectionGObject.GetComponent<Collider>().enabled = false;
		yield return new WaitForSeconds(0.5f);

		attackDetectionGObject.GetComponent<Collider>().enabled = true;
		yield return new WaitForSeconds(0.5f);

		isAttacking = false;
		attackDetectionGObject.GetComponent<Collider>().enabled = false;
		yield break;
	}
}
