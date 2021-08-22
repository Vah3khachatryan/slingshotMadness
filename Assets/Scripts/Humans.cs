using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Humans : MonoBehaviour
{
    Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
 
        float pickAnumber = Random.Range(0,4);
 
        animator.SetFloat ("randIdle", pickAnumber);
    }


}
