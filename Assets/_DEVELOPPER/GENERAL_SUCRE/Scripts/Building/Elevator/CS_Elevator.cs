using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CS_Elevator : MonoBehaviour
{
    [SerializeField] Animator animator;
    bool isIn;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            animator.SetBool("PlayerIsIn", true);
            StopAllCoroutines();
            isIn = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Wait(1));
        }
    }

    public IEnumerator Wait(float timer)
    {
        yield return new WaitForSeconds(timer);
        animator.SetBool("PlayerIsIn", false);
        isIn = false;

    }
}
