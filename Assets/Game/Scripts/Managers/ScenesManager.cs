using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonobehaviourSingleton<ScenesManager>
{
    public override void Awake()
    {
        base.Awake();
    }

    //Scenes Functions
    public void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Gameplay");
    }

    //Exit
    public void ExitGame() {
        Application.Quit();
    }

    public string GetActualScene()
    {
        return SceneManager.GetActiveScene().name;
    }
}
