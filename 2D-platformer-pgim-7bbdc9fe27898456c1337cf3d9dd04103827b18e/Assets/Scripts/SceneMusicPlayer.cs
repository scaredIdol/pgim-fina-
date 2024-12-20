using UnityEngine;

public class SceneMusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip sceneMusic; // Musik untuk scene ini

    void Awake()
    {
        // Tambahkan AudioSource jika belum ada
        if (!TryGetComponent(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        // Pastikan musik diputar setiap kali scene dimuat
        if (sceneMusic != null)
        {
            audioSource.clip = sceneMusic;
            audioSource.Play();
            Debug.Log("Memainkan musik: " + sceneMusic.name);
        }
        else
        {
            Debug.LogWarning("Tidak ada musik yang diatur untuk scene ini!");
        }
    }
}
