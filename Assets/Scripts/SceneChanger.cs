using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator fadeboxAnimator;
    public static int lastLevelIndex = 0;

    public void restarLevel()
    {
        GoToScene(lastLevelIndex);
    }

    public void GoToScene(int index)
    {
        Time.timeScale = 1;
        fadeboxAnimator.SetInteger("State", 1);
        StartCoroutine(WaitForFade(index));
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitForFade(int index)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(index);
    }
}
