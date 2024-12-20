using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound; // Sound effect koin
    private AudioSource audioSource;

    private void Start()
    {
        // Ambil AudioSource dari koin itu sendiri
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Deteksi jika player menyentuh koin
        {
            // Mainkan sound effect
            if (coinSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(coinSound);
            }

            // Tambahkan skor koin (pastikan CoinManager berfungsi)
            CoinManager.instance.AddCoin(1);

            // Hancurkan objek koin setelah diambil
            Destroy(gameObject, 0.2f); // Delay sedikit untuk memastikan suara dimainkan
        }
    }
}
