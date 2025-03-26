using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sr;
    public float speed = 2;
    public bool canRun = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");

        sr.flipX = (direction < 0);
        animator.SetFloat("Movement", Mathf.Abs(direction));

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            canRun = false;
        }
        if (canRun)
        {
            transform.position += transform.right * direction * speed * Time.deltaTime;
        }
        
    }
    public void AttackDoneDidDone()
    {
        canRun = true;
    }
}
