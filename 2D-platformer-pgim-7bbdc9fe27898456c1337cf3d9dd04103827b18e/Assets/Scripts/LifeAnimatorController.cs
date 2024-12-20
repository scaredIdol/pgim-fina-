using UnityEngine;

public class LifeAnimatorController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        // Ambil komponen Animator dari objek ini
        animator = GetComponent<Animator>();

        // Periksa apakah Animator ditemukan
        if (animator == null)
        {
            Debug.LogError("Animator tidak ditemukan pada objek " + gameObject.name);
        }

        // Pastikan animasi diputar dari awal
        animator.Play("Health"); // Ganti "Idle" dengan nama animasi Anda
    }
}
