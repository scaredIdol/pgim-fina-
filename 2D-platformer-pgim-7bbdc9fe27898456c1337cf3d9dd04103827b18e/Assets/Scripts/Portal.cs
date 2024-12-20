using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Nama scene yang ingin dimuat
    [SerializeField] private float delayBeforeLoading = 1f; // Delay sebelum load scene
    private bool hasTriggered = false; // Cegah multiple trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return; // Cegah multiple trigger
        if (collision.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoading);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log($"Loading scene: {sceneToLoad}");
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is empty or null!");
        }
    }
}
