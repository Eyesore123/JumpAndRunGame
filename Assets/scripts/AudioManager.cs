﻿using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Range(0f, 1f)]
    public float volume = 0.5f;

    private const string VolumeKey = "Volume";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // BLOCKADE FIX: Entferne jegliche UI-Blockierung
            Canvas canvas = GetComponentInChildren<Canvas>();
            if (canvas != null)
            {
                Destroy(canvas.gameObject);  // Löscht jegliches Canvas unter AudioManager
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        volume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        ApplyVolume();
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
        ApplyVolume();
    }

    void ApplyVolume()
    {
        AudioListener.volume = volume;
        Debug.Log("Lautstärke gesetzt: " + Mathf.RoundToInt(volume * 100) + "%");
    }
}
