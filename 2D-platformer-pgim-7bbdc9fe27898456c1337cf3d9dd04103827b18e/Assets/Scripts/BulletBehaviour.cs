using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Peluru menabrak: " + collision.gameObject.name); // Cek apa yang ditabrak peluru

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Musuh terkena peluru!"); // Log jika musuh terdeteksi

            EnemyBehavior enemy = collision.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.TakeDamage(); // Panggil metode TakeDamage di musuh
                Debug.Log("TakeDamage dipanggil!"); // Log jika fungsi dipanggil
            }
            else
            {
                Debug.LogWarning("EnemyBehavior tidak ditemukan pada objek musuh!");
            }

            Destroy(gameObject); // Hancurkan peluru
        }
    }
}
