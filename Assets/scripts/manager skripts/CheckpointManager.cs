using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    private Vector3 checkpointPosition;

    private void Awake()
    {
        // Singleton: Nur eine Instanz pro Szene
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Doppelte Instanzen entfernen
        }

        // Setze eine Standardposition f�r den Checkpoint
        checkpointPosition = transform.position;
    }

    // Setzt den Checkpoint auf eine neue Position
    public void SetCheckpoint(Vector3 newPosition)
    {
        checkpointPosition = newPosition;
        Debug.Log("Checkpoint gesetzt: " + checkpointPosition);
    }

    // Gibt die aktuelle Checkpoint-Position zur�ck
    public Vector3 GetCheckpointPosition()
    {
        return checkpointPosition;
    }
}
