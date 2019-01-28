using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;
    public StorySequence sequence;

    public void StartGame()
    {
        sequence.StartSequence();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
