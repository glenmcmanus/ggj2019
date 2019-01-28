using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public UnityEvent SceneUnloaded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadSceneHome()
    {
        SceneManager.LoadSceneAsync("Home", LoadSceneMode.Additive);
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        SceneUnloaded.Invoke();
    }

    public void UnloadSceneHome()
    {
        SceneManager.UnloadSceneAsync("Home");
    }
}
