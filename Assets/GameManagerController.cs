using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerController : MonoBehaviour
{
    public GameObject DeathScreen;
    public GameObject HitScreen;
    public GameObject Player;
    public void RestartGame()
    {
        if (DeathScreen.GetComponent<CanvasGroup>().alpha == 1f) SceneManager.LoadScene("MainMap");

    }
    public void PlayerDeath()
    {
        DeathScreen.GetComponent<CanvasGroup>().alpha = 1f;
    }
    public void DisplayHitScreen()
    {
        StartCoroutine(HitScreenDelay());
    }
    IEnumerator HitScreenDelay()
    {
        HitScreen.GetComponent<CanvasGroup>().alpha = 0.5f;
        yield return new WaitForSeconds(0.1f);
        HitScreen.GetComponent<CanvasGroup>().alpha = 1f;
        yield return new WaitForSeconds(0.1f);
        HitScreen.GetComponent<CanvasGroup>().alpha = 0.5f;
        yield return new WaitForSeconds(0.1f);
        HitScreen.GetComponent<CanvasGroup>().alpha = 0f;
    }
}
