using UnityEngine;

public class UIBackgroundScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeedX = 0.1f; // Kecepatan horizontal
    [SerializeField] private float scrollSpeedY = 0.0f; // Kecepatan vertikal
    private RectTransform rectTransform;
    private Vector2 startPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // Hitung posisi baru berdasarkan waktu
        float newX = Mathf.Repeat(Time.time * scrollSpeedX * 100, rectTransform.rect.width);
        float newY = Mathf.Repeat(Time.time * scrollSpeedY * 100, rectTransform.rect.height);

        // Geser posisi
        rectTransform.anchoredPosition = startPosition + new Vector2(newX, newY);
    }
}
