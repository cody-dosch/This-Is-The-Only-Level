using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    public float moveSpeed;
    public float jumpPower;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private GameObject spawnPipe;

    public event Action OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = GameObject.FindGameObjectWithTag("PlayerGroundCheck").transform;
        spawnPipe = GameObject.FindGameObjectWithTag("SpawnPipe");
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal input
        horizontal = Input.GetAxisRaw("Horizontal");

        // Handle jump and short jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Update player direction
        Flip();
    }

    private void FixedUpdate()
    {
        // Handle horizontal movement
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Handle Spike Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Respawn();
            OnDeath.Invoke();
        }
    }

    private void Respawn()
    {
        var respawnX = spawnPipe.transform.position.x;
        var respawnY = spawnPipe.transform.position.y;
        var respawnZ = transform.position.z;

        transform.position = new Vector3 (respawnX, respawnY, respawnZ);
    }
}
