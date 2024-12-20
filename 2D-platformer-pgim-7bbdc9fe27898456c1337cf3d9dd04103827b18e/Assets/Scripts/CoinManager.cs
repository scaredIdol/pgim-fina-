using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance; // Singleton untuk akses global
    public Text coinText; // Referensi ke UI Text untuk koin
    private int coinCount = 0; // Jumlah koin

    private void Awake()
    {
        // Singleton Pattern
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        coinText.text = coinCount.ToString(); // Update teks koin
    }
}
