using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{

    public VideoPlayer prologue;
    public VideoPlayer intro;


    private void Start()
    {
        prologue.loopPointReached += PrologueLoop;
        intro.loopPointReached += IntroLoop;
    }
    public void PlayPrologue()
    {
        prologue.Play();
    }
    private void PrologueLoop(VideoPlayer source)
    {
        // intro.Play();
        // �޸������Ĺؼ����裺
        prologue.Stop();                  // �� ͣ��ǰһ��
        intro.gameObject.SetActive(true); // �� ���� intro
        StartCoroutine(PlayIntroDelayed()); // �� ��΢�ӳ��ٲ���
    }

    IEnumerator PlayIntroDelayed()
    {
        yield return new WaitForEndOfFrame(); // �ȴ�һ֡�ٲ���
        intro.Play();
    }

    private void IntroLoop(VideoPlayer source)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

  

}
