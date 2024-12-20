using UnityEngine;

public class HealthBooster : MonoBehaviour
{
    [SerializeField] private int healthAmount = 1; // Jumlah nyawa yang ditambahkan
    [SerializeField] private AudioClip healthSound; // AudioClip untuk efek suara
    private AudioSource audioSource; // AudioSource dari GameObject HealthBooster
    private bool hasTriggered = false; // Cegah multiple trigger

    private void Start()
    {
        // Ambil komponen AudioSource dari Health Booster itu sendiri
        audioSource = GetComponent<AudioSource>();

        // Periksa jika tidak ada AudioSource
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is missing on the HealthBooster GameObject.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return; // Jangan lanjut jika sudah trigger sebelumnya

        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Periksa apakah nyawa pemain sudah penuh
                if (playerHealth.IsHealthFull())
                {
                    Debug.Log("Player's health is full. Health Booster not taken.");
                    return; // Jangan lakukan apa-apa jika nyawa penuh
                }

                // Tambahkan nyawa jika belum penuh
                playerHealth.AddHealth(healthAmount);
                Debug.Log($"Player health increased by {healthAmount}");

                // Mainkan sound effect jika tersedia
                if (healthSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(healthSound);
                }

                hasTriggered = true; // Tandai sebagai sudah trigger
                Destroy(gameObject, 0.3f); // Tambahkan delay sebelum menghancurkan untuk memutar suara
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on Player object!");
            }
        }
    }
}
