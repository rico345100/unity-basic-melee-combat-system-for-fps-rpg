using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FPSWeapon : MonoBehaviour {
    private Animator animator;
    private bool isAttacking = false;
    private GameObject attackDetectionGObject;
    private GameObject guardDetectionGObject;
    private enum ATTACK_INDEX { FIRST, SECOND };

    void Start() {
        animator = GetComponent<Animator>();
        attackDetectionGObject = this.transform.Find("metarig/upper_arm.L.001/forearm.L.001/hand.L.001/weapon.L.001/AttackDetection").gameObject;
        guardDetectionGObject = this.transform.Find("metarig/upper_arm.L.001/forearm.L.001/hand.L.001/weapon.L.001/GuardDetection").gameObject;
    }

    void Update() {
        float movement = Input.GetAxis("Vertical");
        bool isGuarding = false;

        if(!isAttacking && movement != 0) {
            animator.SetBool("isWalking", true);
        }
        else {
            animator.SetBool("isWalking", false);
        }

        if(Input.GetKey(KeyCode.LeftShift)) {
            animator.SetFloat("speedMult", 2.0f);
        }
        else {
            animator.SetFloat("speedMult", 1.0f);
        }

        if(!isAttacking && Input.GetMouseButton(1)) {
            isGuarding = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isGuarding", true);
        }

        if(!isGuarding) {
            animator.SetBool("isGuarding", false);
        }

        if(!isAttacking && !isGuarding && Input.GetMouseButton(0)) {
            if(Random.Range(0, 2) == 0) {
                StartCoroutine(Attack(ATTACK_INDEX.FIRST));
            }
            else {
                StartCoroutine(Attack(ATTACK_INDEX.SECOND));
            }
        }
        
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("metarig|Guarding")) {
            guardDetectionGObject.GetComponent<Collider>().enabled = true;
        }
        else {
            guardDetectionGObject.GetComponent<Collider>().enabled = false;
        }
    }

    public void PlayGuardAnimation() {
        animator.SetTrigger("hasGuardSuccess");
    }

    IEnumerator Attack(ATTACK_INDEX attackIdx) {
        string which = attackIdx == ATTACK_INDEX.FIRST ? "isAttacking1" : "isAttacking2";

        isAttacking = true;

        animator.SetBool(which, true);
        yield return new WaitForSeconds(0.2f);

        attackDetectionGObject.GetComponent<Collider>().enabled = true;

        yield return new WaitForSeconds(0.8f);
        attackDetectionGObject.GetComponent<Collider>().enabled = false;
        animator.SetBool(which, false);

        isAttacking = false;
    }
}
