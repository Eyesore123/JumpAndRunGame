using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Methode f�r den Button
    public void Quit()
    {
        Debug.Log("Spiel wird beendet...");

        // Editor-Modus: Beenden der Play-Session
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Build-Modus: Spiel beenden
        Application.Quit();
#endif
    }
}
