using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private int maxJumpCount = 2;
    [SerializeField] private GameObject bulletPrefab;  // Prefab peluru
    [SerializeField] private Transform firePoint;      // Titik tembak
    [SerializeField] private float bulletSpeed = 20f;  // Kecepatan peluru

    private Vector2 moveInput;
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private CapsuleCollider2D myCapsuleCollider;
    private float gravityScaleAtStart;
    private int jumpCount = 0;

    private bool isPaused = false; // Status pause (true = game terhenti)
    private bool isAlive = true;   // Status player hidup/mati

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) return; // Jika player mati, hentikan semua fungsi di Update()
        Run();
        FlipSprite();
        ClimbLadder();
        CheckIfGrounded();
    }

    void OnMove(InputValue value)
    {
        if (isPaused || !isAlive) return; // Cegah input jika mati
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (isPaused || !isAlive) return; // Cegah input jika mati
        if (value.isPressed && jumpCount < maxJumpCount)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
            jumpCount++;
        }
    }

    void OnFire(InputValue value)
    {
        if (isPaused || !isAlive) return; // Cegah input jika mati
        if (value.isPressed)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("BulletPrefab or FirePoint is not assigned!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = new Vector2(transform.localScale.x * bulletSpeed, 0f);
        }
        else
        {
            Debug.LogWarning("Bullet prefab does not have a Rigidbody2D!");
            Destroy(bullet);
        }
    }

    void CheckIfGrounded()
    {
        bool isTouchingGround = myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        bool isTouchingLadder = myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));

        if (isTouchingGround || isTouchingLadder)
        {
            jumpCount = 0;
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    // Fungsi baru: Memberhentikan Player saat mati
    public void Die()
    {
        isAlive = false; // Tandai player sebagai mati

        // Hentikan semua gerakan
        myRigidbody.velocity = Vector2.zero;
        myRigidbody.gravityScale = 0f;

        // Hentikan animasi berjalan
        myAnimator.SetBool("isRunning", false);
        myAnimator.SetBool("isClimbing", false);

    }
}
