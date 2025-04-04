using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;



public class Knight : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sr;
    public float speed = 2;
    public bool canRun = true;
    AudioSource audio;
    public AudioClip[] audioClips;
    public AudioClip[] Sword;
    int randomNumber;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
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
    public void FootFall()
    {
        
        randomNumber = Random.Range(0, audioClips.Length);
        Debug.Log(randomNumber);
        GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        audio.PlayOneShot(audioClips[randomNumber]);
    }
    public void SwordSlash()
    {
        audio.PlayOneShot(Sword[0]);
    }
}
