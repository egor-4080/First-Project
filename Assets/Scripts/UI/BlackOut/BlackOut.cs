using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackOut : MonoBehaviour
{
    [SerializeField] private Rooms roooms;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu") return;
        StartCoroutine(nameof(SetDayBreak));
    }

    public void StartSettingBlackOut()
    {
        StartCoroutine(nameof(SetBlackOut));
    }

    private IEnumerator SetBlackOut()
    {
        float a = 0;

        while (a < 1)
        {
            a += Time.deltaTime;
            image.color = new Color(0, 0, 0, a);
            yield return null;
        }

        roooms.QuickGame();
    }

    private IEnumerator SetDayBreak()
    {
        float a = 1;

        while (a > 0)
        {
            a -= Time.deltaTime;
            image.color = new Color(0, 0, 0, a);
            yield return null;
        }

        Destroy(gameObject);
    }
}