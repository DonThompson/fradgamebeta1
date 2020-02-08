using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleMove : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool onGround = true;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onGround && Input.GetKeyDown("space"))
        {
            rigidbody2d.AddForce(new Vector2(0, jumpForce));
            onGround = false;
        }
    }

    private void FixedUpdate()
    {
        float hz = Input.GetAxisRaw("Horizontal");
        float vz = Input.GetAxisRaw("Vertical");

        onGround = Physics2D.OverlapPoint(groundCheck.position, whatIsGround);
        if(hz != 0)
        {
            transform.Translate(new Vector3(hz * moveSpeed * Time.deltaTime, 0f, 0f));
        }
    }
}
