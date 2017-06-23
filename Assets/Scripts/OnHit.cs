using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour {
	public float damage = 25.0f;
	public GameObject objectForGetNextAttackTime;
	public string ownerTag;
	private Animator animatorForGetNextAttackTime;

	void Start() {
		animatorForGetNextAttackTime = objectForGetNextAttackTime.GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other) {
		if(ownerTag != other.tag && other.tag == "Enemy") {
			float totalTime= GetCurrentAnimationTotalTime();
			float time = GetCurrentAnimationTime();
			float immuneTime = totalTime - time;

			other.gameObject.GetComponent<Enemy>().TakeDamage(damage, gameObject.GetInstanceID().ToString(), immuneTime);
		}
		else if(ownerTag != other.tag && other.tag == "Player") {
			float totalTime= GetCurrentAnimationTotalTime();
			float time = GetCurrentAnimationTime();
			float immuneTime = totalTime - time;
			
			// print("Name: " + info.name + "\nTotal Time: " + totalTime + "\nTime: " + time);

			other.gameObject.GetComponent<Player>().TakeDamage(damage, gameObject.GetInstanceID().ToString(), immuneTime);
		}
    }

	float GetCurrentAnimationTime() {
		AnimatorStateInfo animatorState = animatorForGetNextAttackTime.GetCurrentAnimatorStateInfo(0);
		AnimatorClipInfo[] animatorClip = animatorForGetNextAttackTime.GetCurrentAnimatorClipInfo(0);

		return animatorClip[0].clip.length * animatorState.normalizedTime;
	}

	float GetCurrentAnimationTotalTime() {
		float totalTime;
		AnimationClip info = animatorForGetNextAttackTime.GetCurrentAnimatorClipInfo(0)[0].clip;
		
		// I think this approach is good, but sometimes, animation was set to Idle, and it's almost 4 seconds and it ruins everything.
		// So temporarily I set to exception. Someday, I have to fix this.
		if(info.name == "metarig|Idle") {
			totalTime = 1.3f;
		}
		else {
			totalTime = info.length;
		}

		return totalTime;
	}
}
