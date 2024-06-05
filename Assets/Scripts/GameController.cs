using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject gameOver;
    public GameObject pauseScreen;

    public Animator transition;
    public AudioClip buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    public void Restart()
    {
        SoundManager.sons.PlaySound(buttonSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PauseGame(bool pause)
    {
        // pause = true - o jogo pausa || pause = false - o jogo roda
        pauseScreen.SetActive(pause);

        //diminua a velocidade que o tempo passa, 0 = tempo nao passa, 1 = tempo normal, 2 = tempo passa 2 vezes mais rapido
        if(pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void Resume()
    {
        SoundManager.sons.PlaySound(buttonSound);
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void mainMenu()
    {
        SoundManager.sons.PlaySound(buttonSound);
        SceneManager.LoadScene("MainMenu");
    }

    public void quit()
    {
        SoundManager.sons.PlaySound(buttonSound);
        Application.Quit();
    }
    public void start()
    {
        SoundManager.sons.PlaySound(buttonSound);
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        NextLevel();
    }

    public void NextLevel()
    {
        StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    //IEnumarator, vc consegue dar um sleep no código
    IEnumerator LoadNextLevel(int levelIndex)
    {
        transition.SetTrigger("start");

        // espera x segundos antes de executar a linha de baixo
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }
}
