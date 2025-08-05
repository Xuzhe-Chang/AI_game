using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{

    public Animator logoTitle;
    public Animator mainManuScreen;
    public Animator audioMenuScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(DelayDisplayOpening());
    }

    IEnumerator DelayDisplayOpening()
    {
        
        logoTitle.Play("FadeOut");
        mainManuScreen.Play("FadeOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Option()
    {
        StartCoroutine(DelayDisplayAudioMenu());
    }
    IEnumerator DelayDisplayAudioMenu()
    {
        logoTitle.Play("TitleFadeOut");
        mainManuScreen.Play("FadeOut");
        yield return new WaitForSeconds(0.5f);
        audioMenuScreen.Play("FadeIn");

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitAudioMenu()
    {
        StartCoroutine(DelayShutAduioMenu());

    }

    IEnumerator DelayShutAduioMenu()
    {
        audioMenuScreen.Play("FadeOut");
        yield return new WaitForSeconds(0.5f);
        logoTitle.Play("TitleFadeIn");
        mainManuScreen.Play("FadeIn");
    }

}
