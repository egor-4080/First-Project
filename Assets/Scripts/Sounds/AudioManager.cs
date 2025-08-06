using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    Dictionary<AudioSource, float> audioValues = new Dictionary<AudioSource, float>();

    private float currentValue = 1;

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
        Slider slider = FindFirstObjectByType<AudioSliderStart>(FindObjectsInactive.Include)
            .GetComponent<Slider>();
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
        AudioSource[] audios = FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (AudioSource audio in audios)
        {
            if (!audioValues.ContainsKey(audio))
            {
                audioValues.Add(audio, audio.volume);
            }
        }
    }

    public void AddNewAudio(AudioSource newAudio, float value)
    {
        audioValues.Add(newAudio, value);
    }

    public void AudioChanger(float value)
    {
        currentValue = value;
        foreach (var audio in audioValues)
        {
            if (audio.Key != null)
            {
                audio.Key.volume = audio.Value * currentValue;
            }
        }
    }
}