using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blackout : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rooms room;
    [SerializeField] private float speed;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu") return;
        StartCoroutine(nameof(SetDayBreak));
    }

    public void BlackOutCoroutine()
    {
        StartCoroutine(nameof(SetBlackOut));
    }

    private IEnumerator SetBlackOut()
    {
        float a = 0;

        while (a < 1)
        {
            a += Time.deltaTime * speed;
            sprite.color = new Color(0, 0, 0, a);
            yield return null;
        }

        room.QuickGame();
    }

    private IEnumerator SetDayBreak()
    {
        float a = 1;

        while (a > 0)
        {
            a -= Time.deltaTime * speed;
            sprite.color = new Color(0, 0, 0, a);
            yield return null;
        }

        Destroy(gameObject);
    }
}