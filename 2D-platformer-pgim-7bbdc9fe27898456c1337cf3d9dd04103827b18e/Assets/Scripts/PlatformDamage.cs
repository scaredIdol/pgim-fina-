using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Untuk me-reload scene

public class PlatformDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1; // Jumlah damage yang diterima player
    [SerializeField] private float delayBeforeReload = 0.5f; // Waktu tunda sebelum reload scene

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Periksa apakah player yang menabrak platform
        if (collision.collider.CompareTag("Player"))
        {
            // Panggil fungsi TakeDamage dari script PlayerHealth (jika ada)
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

            // Restart scene setelah delay
            StartCoroutine(RestartSceneWithDelay());
        }
    }

    private IEnumerator RestartSceneWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeReload); // Tunggu sebelum reload
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene saat ini
    }
}
