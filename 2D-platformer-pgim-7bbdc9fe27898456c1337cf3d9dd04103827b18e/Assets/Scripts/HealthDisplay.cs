using UnityEngine;
using UnityEngine.UI; // Tambahkan namespace UI

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Text healthText; // Referensi ke UI Text
    [SerializeField] private int playerHealth = 3; // Nyawa awal pemain

    private void Start()
    {
        UpdateHealth(playerHealth); // Inisialisasi tampilan nyawa
    }

    public void UpdateHealth(int newHealth)
    {
        playerHealth = newHealth;
        healthText.text = playerHealth.ToString(); // Perbarui angka nyawa
    }

    // Contoh pengurangan nyawa
    public void DecreaseHealth(int amount)
    {
        playerHealth -= amount;
        playerHealth = Mathf.Max(playerHealth, 0); // Pastikan nyawa tidak negatif
        UpdateHealth(playerHealth);
    }
}
