using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    //public Dictionary<string, float> config2 { get; set; } = new();
    public Dictionary<string, Dictionary<string, float> > config
    {
        get;
        private set;
    } = new();

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
    }

    public void SetNewDictionary(string dictionaryName, string[] names)
    {
        config.Add(dictionaryName, new Dictionary<string, float>());
        for(int i = 0; i < names.Length; i++)
        {
            config[dictionaryName].Add(names[i], 0);
            //ƒобавил в определенный словарь значени€ (damge, speed, health)
        }
    }
}
