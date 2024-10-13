using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    Dictionary<AudioSource, float> audioValues = new Dictionary<AudioSource, float>();

    private float currentValue = 1;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mod)
    {
        Slider slider = FindFirstObjectByType<Slider>();
        slider.onValueChanged.AddListener(AudioChanger);
    }

    public void OnNewAudiosAppeared()
    {
        audioValues.Clear();
        FindAllAudio();
        AudioChanger(currentValue);
    }

    private void FindAllAudio()
    {
        AudioSource[] audios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in audios)
        {
            if (!audioValues.ContainsKey(audio))
            {
                audioValues.Add(audio, audio.volume);
            }
        }
    }

    public void AudioChanger(float value)
    {
        currentValue = value;
        foreach (var audio in audioValues)
        {
            if(audio.Key != null)
            {
                audio.Key.volume = audio.Value * currentValue;
            }
        }
    }
}