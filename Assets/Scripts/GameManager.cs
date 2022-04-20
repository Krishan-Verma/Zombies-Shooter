using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int enimiesAlive = 0;
    public int round = 0;

    public Text roundNo;
    public Text roundSurvivedNo;

    public GameObject EndScene;
    public GameObject pauseMenu;
    public GameObject[] SpawnPoints;
    public GameObject enemyPrefeb;
    public CanvasGroup hurtPanel;
    public GameObject controlPanel;


    void Update()
    {
        if(enimiesAlive==0)
        {
            round++;
            roundNo.text = "Round:"+round.ToString();
            NextWave(round);
        }

        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Pause();
        //}
    }

    public void NextWave(int round)
    {
        for(int i=0;i<round;i++)
        {
            GameObject spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            GameObject enemySpawned=Instantiate(enemyPrefeb,spawnPoint.transform.position,Quaternion.identity);
            enemySpawned.GetComponent<EnemyManager>().gameManager = GetComponent<GameManager>();
            enimiesAlive++;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        
    }

    public void MainMenu()
    {   
        Time.timeScale = 1;
        AudioListener.volume = 1;
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        controlPanel.SetActive(false);
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        hurtPanel.gameObject.SetActive(false);
        AudioListener.volume = 0;
    }

    public void Continue()
    {
        controlPanel.SetActive(true);
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        hurtPanel.gameObject.SetActive(true);
        AudioListener.volume = 1;
    }

    public void EndGame()
    {
        controlPanel.SetActive(false);
        Time.timeScale = 0;
        EndScene.SetActive(true);
        roundSurvivedNo.text = round.ToString();
        AudioListener.volume = 0;
        
    }
}
