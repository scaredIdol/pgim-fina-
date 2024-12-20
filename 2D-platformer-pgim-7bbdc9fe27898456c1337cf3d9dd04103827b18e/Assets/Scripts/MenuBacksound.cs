using UnityEngine;

public class SceneMusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip sceneMusic; // Musik untuk scene ini

    void Awake()
    {
        // Tambahkan AudioSource jika belum ada
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        // Mainkan musik jika ada
        if (sceneMusic != null)
        {
            audioSource.clip = sceneMusic;
            audioSource.Play();
            Debug.Log("Memainkan musik: " + sceneMusic.name);
        }
        else
        {
            Debug.LogWarning("sceneMusic belum diatur di Inspector!");
        }
    }
}
