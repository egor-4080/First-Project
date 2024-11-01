using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    [SerializeField] private List<string> noGunScenesNames;

    public Dictionary<string, Dictionary<string, float> > configStats{ get; private set; } = new();
    public List<string> noGunScenes { get; private set; } = new List<string>();


    public static Config instance {  get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        SetScenesName();
    }

    public void SetNewDictionary(string dictionaryName, string[] names)
    {
        configStats.Add(dictionaryName, new Dictionary<string, float>());
        for(int i = 0; i < names.Length; i++)
        {
            configStats[dictionaryName].Add(names[i], 0);
            //ƒобавил в определенный словарь значени€ (damge, speed, health)
        }
    }

    private void SetScenesName()
    {
        foreach(string name in noGunScenesNames)
        {
            noGunScenes.Add(name);
        }
    }
}
