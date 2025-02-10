using UnityEngine;

public class SpecialCoin : MonoBehaviour
{
    public int coinIndex;  // Index der M�nze (z. B. 0, 1, 2)
    public AudioClip collectSound;  // Sound beim Einsammeln

    private AudioSource audioSource;
    private string coinKey;  // Key f�r diese spezielle M�nze im PlayerPrefs
    private SpriteRenderer spriteRenderer;  // Renderer der M�nze

    void Start()
    {
        string currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        coinKey = $"{currentLevel}.Coin{coinIndex}";  // Einzigartiger Key f�r diese M�nze

        // SpriteRenderer referenzieren
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Pr�fen, ob die M�nze schon eingesammelt wurde
        if (PlayerPrefs.HasKey(coinKey))
        {
            // M�nze transparent machen
            SetTransparency(0.5f);
        }

        // AudioSource hinzuf�gen
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collectSound;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Spezialm�nzen nur einsammeln, wenn sie noch nicht eingesammelt wurde
            if (!PlayerPrefs.HasKey(coinKey))
            {
                AddSpecialCoin();

                // Sound abspielen
                if (collectSound != null)
                {
                    audioSource.Play();
                }

                // M�nze im PlayerPrefs speichern
                PlayerPrefs.SetInt(coinKey, 1);
                PlayerPrefs.Save();

                // UI aktualisieren
                SpecialCoinUI uiManager = FindObjectOfType<SpecialCoinUI>();
                if (uiManager != null)
                {
                    uiManager.CollectCoin(coinIndex);  // Zeige die M�nze in der UI
                }

                // M�nze transparent machen
                SetTransparency(0.1f);
            }
        }
    }

    void AddSpecialCoin()
    {
        const string globalKey = "GlobalSpecialCoins";  // Key f�r globale Spezialm�nzen
        int globalSpecialCoins = PlayerPrefs.GetInt(globalKey, 0);
        globalSpecialCoins += 1;
        PlayerPrefs.SetInt(globalKey, globalSpecialCoins);

        Debug.Log($"Spezialm�nze eingesammelt! Globale Spezialm�nzen: {globalSpecialCoins}");
    }

    // Sichtbarkeit der M�nze �ndern
    private void SetTransparency(float alpha)
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }
}
