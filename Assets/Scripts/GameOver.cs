using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;

    public AudioSource audioSource;
    public Image panel;
    public Color clear = Color.clear;
    public Color white = Color.white;

    public float fadeStep = 0.1f;
    public float fadeRate = 0.1f;
    public WaitForSeconds fadeWait;

    public float transitionDelay = 1f;
    WaitForSeconds delayWait;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            fadeWait = new WaitForSeconds(fadeRate);
            delayWait = new WaitForSeconds(transitionDelay);
        }
    }

    [ContextMenu("GameOver")]
    public void OnGameOver()
    {
        panel.gameObject.SetActive(true);
        StartCoroutine(FadeToWhite());
    }

    IEnumerator FadeToWhite()
    {
        float t = 0;
        while(t < 1)
        {
            panel.color = Vector4.Lerp(clear, white, t);

            t += fadeStep;
                
            yield return fadeWait;
        }

        yield return delayWait;

        SceneManager.LoadScene(0);
    }
}
