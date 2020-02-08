using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody2d;
    public bool controls = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hz = Input.GetAxisRaw("Horizontal");
        float vz = Input.GetAxisRaw("Vertical");

        MovePlayer(hz, vz);

        AnimatePlayer(hz, vz);
    }

    private void AnimatePlayer(float hz, float vz)
    {
        //animator.speed = 1;
        if (hz > 0)
        {
            EnableAnimations(true, false, false);
            spriteRenderer.flipX = true;
            animator.speed = 1;
        }
        else if(hz < 0)
        {
            EnableAnimations(true, false, false);
            spriteRenderer.flipX = false;
            animator.speed = 1;
        }
        else if(vz < 0)
        {
            EnableAnimations(false, false, true);
            animator.speed = 1;
        }
        else if(vz > 0)
        {
            EnableAnimations(false, true, false);
            animator.speed = 1;
        }
        else
        {
            animator.speed = 0;
        }
    }

    private void EnableAnimations(bool x, bool up, bool down)
    {
        animator.SetBool("MoveX", x);
        animator.SetBool("MoveUp", up);
        animator.SetBool("MoveDown", down);
    }

    private void MovePlayer(float hz, float vz)
    {
        transform.Translate(new Vector3(hz * moveSpeed * Time.deltaTime, vz * moveSpeed * Time.deltaTime));
    }
}
