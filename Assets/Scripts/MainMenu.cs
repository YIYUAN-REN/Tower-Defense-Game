using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 3f;
    public string levelToLoad = "MainScene";

    public void Play()
    {
        StartCoroutine(LoadGame());
    }

    public void Quit()
    {
        Debug.Log("Exciting...");
        Application.Quit();
    }

    public void Config()
    {
        Debug.Log("OptionMenu");
    }

    IEnumerator LoadGame()
    {
        transition.SetTrigger("Play");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelToLoad);
    }
}
