using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private Animator animator;
	private Collider enemyCollider;
	private HitDetectionTable hitDetectionTable;
	public float health = 100.0f;
	public bool isDead = false;
	
	void Start() {
		animator = GetComponent<Animator>();
		enemyCollider = GetComponent<Collider>();
		hitDetectionTable = new HitDetectionTable();
	}

	public void TakeDamage(float damage, string damageIdentifier, float timeForNextAttack) {
		if(isDead) return;
		if(hitDetectionTable.Get(damageIdentifier)) {
			// print("ENEMY TOOK THE DAMAGE, BUT HAS IMMUNE FOR " + damageIdentifier);
			return;
		}
		
		// print("ENEMY GOT HIT, NOW HAS " + health + " AND HAS IMMUNE ABOUT" + damageIdentifier + " FOR " + timeForNextAttack + " seconds.");
		StartCoroutine(SetDamageImmuneForSeconds(damageIdentifier, timeForNextAttack - 0.1f));
	
		health -= damage;

		if(health <= 0) {
			isDead = true;
			animator.SetTrigger("isDead");
			enemyCollider.enabled = false;
		}
	}

	IEnumerator SetDamageImmuneForSeconds(string identifier, float duration) {
		hitDetectionTable.Set(identifier);

		yield return new WaitForSeconds(duration);

		hitDetectionTable.Remove(identifier);

		yield break;
	}
}
