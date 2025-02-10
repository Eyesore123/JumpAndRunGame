using UnityEngine;
using TMPro;  // F�r TextMeshPro UI

public class LivesPickup : MonoBehaviour
{
    public int livesToAdd = 1;  // Anzahl der Leben, die hinzugef�gt werden
    public AudioClip pickupSound;  // Sound beim Einsammeln (optional)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.AddLives(livesToAdd);  // Leben hinzuf�gen

                // Soundeffekt abspielen (optional)
                if (pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                }

                // Pickup zerst�ren
                Destroy(gameObject);
            }
        }
    }
}
