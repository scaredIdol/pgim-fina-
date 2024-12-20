using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Jika menggunakan UI Text biasa
using UnityEngine.SceneManagement; // Untuk manajemen scene

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5; // Maksimal nyawa pemain
    [SerializeField] private float invincibilityDuration = 1f; // Durasi tidak bisa diserang setelah terkena damage
    [SerializeField] private Color damageColor = Color.red; // Warna efek damage
    [SerializeField] private SpriteRenderer playerSprite; // Sprite renderer pemain
    [SerializeField] private Text healthText; // UI Text untuk menampilkan nyawa
    [SerializeField] private AudioClip damageSound; // Sound effect untuk damage
    [SerializeField] private AudioClip deathSound; // Sound effect untuk mati
   
    [SerializeField] private AudioSource audioSource; // Audio source untuk player

    private int currentHealth; // Nyawa saat ini
    private bool isInvincible = false; // Apakah pemain dalam mode tak bisa diserang
    private Color originalColor; // Warna asli sprite pemain
 

    void Start()
    {
        // Atur nilai awal
        currentHealth = 3; // Atur nyawa awal
        originalColor = playerSprite.color; // Simpan warna asli
        UpdateHealthUI();

      
    }

    void UpdateHealthUI()
    {
        healthText.text = currentHealth.ToString(); // Menampilkan jumlah nyawa
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return; // Jangan terima damage jika sedang invincible

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Pastikan nyawa tidak kurang dari 0
        UpdateHealthUI(); // Perbarui UI nyawa

        // Mainkan sound effect untuk damage
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        // Mulai efek damage
        StartCoroutine(DamageEffect());

        if (currentHealth <= 0)
        {
            Die(); // Panggil fungsi mati jika nyawa habis
        }
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Pastikan tidak melebihi maxHealth
        UpdateHealthUI(); // Perbarui tampilan UI nyawa
    }

    public bool IsHealthFull()
    {
        return currentHealth >= maxHealth;
    }

    IEnumerator DamageEffect()
    {
        isInvincible = true; // Aktifkan mode invincible
        playerSprite.color = damageColor; // Ubah warna ke damageColor
        yield return new WaitForSeconds(0.5f); // Tunggu sebentar
        playerSprite.color = originalColor; // Kembalikan warna asli
        yield return new WaitForSeconds(0.1f); // Tunggu sebentar
        playerSprite.color = damageColor; // Ulangi perubahan warna
        yield return new WaitForSeconds(0.3f);
        playerSprite.color = originalColor; // Kembali ke normal
        yield return new WaitForSeconds(invincibilityDuration - 0.2f); // Sisa durasi invincibility
        isInvincible = false; // Matikan mode invincible
    }
void Die()
{
    Debug.Log("Pemain mati!");

    // Mainkan suara kematian
    if (deathSound != null && audioSource != null)
    {
        audioSource.PlayOneShot(deathSound);
    }

    // Nonaktifkan Collider untuk menembus tanah
    Collider2D collider = GetComponent<Collider2D>();
    if (collider != null)
    {
        collider.enabled = false;
    }

    // Nonaktifkan Rigidbody2D fisika
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        rb.simulated = false;
    }

    // Nonaktifkan kontrol player
    PlayerMovement playerMovement = GetComponent<PlayerMovement>();
    if (playerMovement != null)
    {
        playerMovement.enabled = false;
    }

    // Mulai animasi turun
    StartCoroutine(SinkIntoGround());
}

IEnumerator SinkIntoGround()
{
    float sinkSpeed = 10f; // Kecepatan turun
    float duration = 2f;  // Durasi turun
    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
        transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Muat ulang scene setelah animasi selesai
    ReloadScene();
}


void ReloadScene()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TakeDamage(1); // Kurangi 1 nyawa jika menabrak musuh
        }
    }

    
}
