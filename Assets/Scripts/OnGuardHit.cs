using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGuardHit : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Attack") {
			GameObject fpsWeapon = this.transform.parent.parent.parent.parent.parent.parent.gameObject;
			GameObject player = fpsWeapon.transform.parent.parent.gameObject;
			
			player.GetComponent<Player>().isGuarded = true;
			fpsWeapon.GetComponent<FPSWeapon>().PlayGuardAnimation();
		}
    }
}
