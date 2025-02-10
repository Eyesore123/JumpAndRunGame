using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Einstellungen")]
    public int pointValue = 100;  // Punktewert der M�nze
    public int coinValue = 1;     // Anzahl der M�nzen, die der Spieler erh�lt
    public AudioClip collectSound;

    private AudioSource audioSource;

    private void Start()
    {
        // Audio-Komponente initialisieren
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = collectSound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Punkte und Coins vergeben
            AddPointsAndCoinsToPlayer();

            Debug.Log($"M�nze eingesammelt. Punkte: {pointValue}, Coins: {coinValue}");

            // Sound abspielen
            PlayCollectSound();

            // M�nze zerst�ren
            Destroy(gameObject, 0.1f);
        }
    }

    private void AddPointsAndCoinsToPlayer()
    {
        // Punkte erh�hen
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddPoints(pointValue);
            CoinManager.Instance.AddCoins(coinValue);
        }
    }

    private void PlayCollectSound()
    {
        if (audioSource != null && collectSound != null)
        {
            audioSource.Play();
        }
    }
}
