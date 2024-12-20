using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Untuk mengganti scene
using UnityEngine.UI;
using UnityEngine.InputSystem; // Tambahkan ini jika menggunakan Input System baru

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button homeButton;

    private bool isPaused = false; 
    private PlayerInput playerInput; // Tambahkan referensi ke PlayerInput

    void Start()
    {
        pausePanel.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);

        // Ambil PlayerInput dari Player
        playerInput = FindObjectOfType<PlayerInput>();

        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        homeButton.onClick.AddListener(GoToMainMenu);
    } 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
{
    isPaused = true;
    Time.timeScale = 0f;

    pausePanel.SetActive(true);
    pauseButton.gameObject.SetActive(false);
    resumeButton.gameObject.SetActive(true);
    restartButton.gameObject.SetActive(true);
    homeButton.gameObject.SetActive(true);

    // Pause semua audio
    AudioListener.pause = true;

    if (playerInput != null)
        playerInput.enabled = false; // Nonaktifkan input
}

public void ResumeGame()
{
    isPaused = false;
    Time.timeScale = 1f;

    pausePanel.SetActive(false);
    pauseButton.gameObject.SetActive(true);
    resumeButton.gameObject.SetActive(false);
    restartButton.gameObject.SetActive(false);
    homeButton.gameObject.SetActive(false);

    // Resume semua audio
    AudioListener.pause = false;

    if (playerInput != null)
        playerInput.enabled = true; // Aktifkan input
}


    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
{
    Time.timeScale = 1f; // Pastikan waktu berjalan normal

    // Kembali ke scene MainMenu
    SceneManager.LoadScene("MainMenu");
}

}
