using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StorySequence : MonoBehaviour
{
    public Image image; 
    public Sprite[] sequence;
    public float delay;
    WaitForSeconds wait;

    public float fadeStep = 0.2f;
    public float fadeRate = 0.5f;
    WaitForSeconds fadeWait;

    private void Awake()
    {
        wait = new WaitForSeconds(delay);
        fadeWait = new WaitForSeconds(fadeRate);
    }

    public void StartSequence()
    {
        image.enabled = true;
        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        int counter = 1;
        float t = 0;
        while (counter < sequence.Length)
        {
            while (t <= 1)
            {
                image.color = Vector4.Lerp(Color.black, Color.white, t);
                t += fadeStep;

                yield return fadeWait;
            }
            t = 0;

            yield return wait;

            while (t <= 1)
            {
                image.color = Vector4.Lerp(Color.white, Color.black, t);
                t += fadeStep;

                yield return fadeWait;
            }
            t = 0;

            image.sprite = sequence[counter++];
        }

        while (t <= 1)
        {
            image.color = Vector4.Lerp(Color.black, Color.white, t);
            t += fadeStep;

            yield return fadeWait;
        }

        yield return wait;

        SceneManager.LoadScene(1);
    }
}
