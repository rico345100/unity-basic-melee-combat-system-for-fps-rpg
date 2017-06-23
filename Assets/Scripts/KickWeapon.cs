using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickWeapon : MonoBehaviour {
	Animator animator;
	Collider kickCollider;
	bool isKicking = false;

	void Start() {
		animator = GetComponent<Animator>();
		kickCollider = this.transform.Find("metarig/hips/thigh.L/shin.L/AttackDetection").GetComponent<Collider>();
	}

	void Update() {
		float movement = Input.GetAxis("Vertical");

		if(!isKicking && movement != 0) {
            animator.SetBool("isWalking", true);
        }
        else {
            animator.SetBool("isWalking", false);
        }

		if(Input.GetKey(KeyCode.LeftShift)) {
            animator.SetFloat("speedMult", 2.0f);
        }
        else {
            animator.SetFloat("speedMult", 1.5f);
        }

		if(!isKicking && Input.GetKeyDown(KeyCode.G)) {
			StartCoroutine(Kick());
		}
	}

	IEnumerator Kick() {
		animator.SetTrigger("isKicking");
		animator.SetBool("isWalking", false);
		kickCollider.GetComponent<Collider>().enabled = true;
		isKicking = true;

		yield return new WaitForSeconds(1.625f);
		kickCollider.GetComponent<Collider>().enabled = false;
		isKicking = false;
	}

	public void SetWalk() {
		if(isKicking) return;
		animator.SetBool("isWalking", true);
	}
	
	public void UnsetWalk() {
		animator.SetBool("isWalking", false);
	}
}
