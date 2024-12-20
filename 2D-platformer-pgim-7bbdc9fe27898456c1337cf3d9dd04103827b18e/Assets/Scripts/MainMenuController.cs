using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Fungsi untuk memulai permainan
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // Ganti "GameScene" dengan nama scene gameplay Anda
    }
}
