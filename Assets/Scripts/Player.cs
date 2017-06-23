using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	private GameObject healthUI;
	private HitDetectionTable hitDetectionTable;
	public float health = 150.0f;
	public bool isDead = false;
	public bool isGuarded = false;

	void Start() {
		healthUI = GameObject.Find("PlayerHealth");
		hitDetectionTable = new HitDetectionTable();
	}

	void Update() {
		healthUI.GetComponent<Text>().text = "Player Health: " + health;
	}

	public void TakeDamage(float damage, string damageIdentifier, float timeForNextAttack) {
		if(isDead) return;

		if(hitDetectionTable.Get(damageIdentifier)) {
			// print("PLAYER GOT HIT, BUT HAS IMMUNE");
			return;
		}
		// print("PLAYER GOT HIT!");
		StartCoroutine(SetDamageImmuneForSeconds(damageIdentifier, timeForNextAttack - 0.1f));		

		if(isGuarded) {
			// print("PLAYER GUARDED ENEMY ATTACK! NO DAMAGE TAKEN!");
			isGuarded = false;
			return;
		}
		
		health -= damage;

		if(health <= 0) {
			isDead = true;
		}
	}

	IEnumerator SetDamageImmuneForSeconds(string identifier, float duration) {
		hitDetectionTable.Set(identifier);

		yield return new WaitForSeconds(duration);

		hitDetectionTable.Remove(identifier);

		yield break;
	}
}
