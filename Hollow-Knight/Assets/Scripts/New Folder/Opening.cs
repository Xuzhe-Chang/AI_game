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
        // 修复黑屏的关键步骤：
        prologue.Stop();                  // ① 停掉前一个
        intro.gameObject.SetActive(true); // ② 启用 intro
        StartCoroutine(PlayIntroDelayed()); // ③ 稍微延迟再播放
    }

    IEnumerator PlayIntroDelayed()
    {
        yield return new WaitForEndOfFrame(); // 等待一帧再播放
        intro.Play();
    }

    private void IntroLoop(VideoPlayer source)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

  

}
