using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f; // Kecepatan musuh
    [SerializeField] GameObject deathEffect; // Efek saat musuh mati (opsional)
    [SerializeField] float flipCooldown = 0.5f; // Waktu jeda sebelum musuh bisa berbalik arah lagi
    
    Rigidbody2D myRigidbody;
    Animator myAnimator; // Tambahkan variabel animator
    SpriteRenderer mySpriteRenderer; // Tambahkan variabel untuk sprite renderer (efek warna)
    float lastFlipTime = 0f; // Waktu terakhir musuh berbalik arah

    [SerializeField] private Color damageColor = Color.red; // Warna efek damage
    [SerializeField] private float damageFlashDuration = 0.2f; // Durasi warna damage

    void Start()
    {
        // Mengambil komponen Rigidbody2D, Animator, dan SpriteRenderer
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>(); // Tambahkan ini untuk akses ke sprite musuh

        if (myRigidbody == null)
        {
            Debug.LogError("Rigidbody2D tidak ditemukan pada musuh!");
        }

        if (myAnimator == null)
        {
            Debug.LogError("Animator tidak ditemukan pada musuh!");
        }

        if (mySpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer tidak ditemukan pada musuh!");
        }
    }

    void Update()
    {
        Move();

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D hit in hits)
        {
            // Abaikan collider milik musuh sendiri
            if (hit.gameObject == gameObject)
            {
                continue;
            }

            // Jika menabrak musuh lain atau boundary, balik arah
            if ((hit.CompareTag("Enemy") || hit.CompareTag("Boundary")) && Time.time > lastFlipTime + flipCooldown)
            {
                FlipDirection();
                lastFlipTime = Time.time; // Update waktu flip terakhir
                break;
            }
        }
    }

    void Move()
    {
        // Menggerakkan musuh dengan kecepatan tertentu
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
        
        // Atur animasi berjalan
        myAnimator.SetBool("isRunning", Mathf.Abs(myRigidbody.velocity.x) > 0.01f);
    }

    void FlipDirection()
    {
        // Membalik arah gerakan
        moveSpeed = -moveSpeed;

        // Membalik arah sprite
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

        Debug.Log("Musuh membalik arah: " + moveSpeed);
    }

    public void TakeDamage()
    {
        // Matikan pergerakan musuh
        myRigidbody.velocity = Vector2.zero;


        // Efek warna damage dan delay sebelum musuh dihancurkan
        StartCoroutine(DamageEffect());

        // Hancurkan objek musuh setelah animasi selesai
        Destroy(gameObject, 0.1f); // Tunggu 0.5 detik agar animasi bisa diputar
    }

    IEnumerator DamageEffect()
    {
        // Ubah warna sprite menjadi warna damage
        mySpriteRenderer.color = damageColor;

        // Tunggu beberapa saat
        yield return new WaitForSeconds(damageFlashDuration);

        // Kembalikan warna sprite ke warna aslinya
        mySpriteRenderer.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Jika musuh menyentuh platform berbahaya
        if (collision.gameObject.CompareTag("Danger"))
        {
            Debug.Log("Musuh menyentuh platform berbahaya dan mati!");
            TakeDamage();
        }
    }
}
