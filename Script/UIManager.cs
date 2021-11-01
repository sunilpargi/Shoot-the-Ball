using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public static bool isRestart;
    public GameObject gameScene;
    public GameObject HomeUI;
    public GameObject GameUI;
    public GameObject blackFG;
    public GameObject pausePanel;
    public GameObject GameOverUI;
    public GameObject RewardPanel;

    public GameObject[] allBallsImg;
    public Sprite enabledBallImg;
    public Sprite disabledBallImg;

    public int score;
    public Text scoreText;

    public int scoreMultiplier = 1;
    public GameObject scoreMultiImage;
    public Text scoreMultiText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        HomeUI.SetActive(true);
        gameScene.SetActive(false);

        if (isRestart)
        {
            isRestart = false;
            HomeUI.SetActive(false);
            gameScene.SetActive(true);
            GameUI.SetActive(true);
            GameManager.instance.StartGame();
        }
        else
        {
            AdManager.instance.RequestBanner();
        }
    }
    // Update is called once per frame
    void Update()
    {
        {
            int ballCount = GameManager.instance.totalBalls;
            for (int i = 0; i < 5; i++)
            {
                if (i < ballCount)
                {
                    allBallsImg[i].GetComponent<Image>().sprite = enabledBallImg;
                }
                else
                {
                    allBallsImg[i].GetComponent<Image>().sprite = disabledBallImg;
                }
            }
        }
    }

    public void B_Start()
    {
        StartCoroutine(StartRoutine());
       

    }

    IEnumerator StartRoutine()
    {
        ShowBlackFade();
        yield return new WaitForSeconds(.5f);
        gameScene.SetActive(true);
        GameUI.SetActive(true);
        HomeUI.SetActive(false);

        GameManager.instance.StartGame();
        AdManager.instance.HideBanner();
    }

    public void ShowBlackFade()
    {

        StartCoroutine("FadeRoutine");
    }

    IEnumerator FadeRoutine()
    {
        blackFG.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        blackFG.SetActive(false);
    }

    public void UpdateBallIcons()
    {
        int ballCount = GameManager.instance.totalBalls;
        for (int i = 0; i < 5; i++)
        {
            if (i < ballCount)
            {
                allBallsImg[i].GetComponent<Image>().sprite = enabledBallImg;
            }
            else
            {
                allBallsImg[i].GetComponent<Image>().sprite = disabledBallImg;
            }
        }
    }

    public void UpdateScore()
    {
        score += scoreMultiplier * 1;
        scoreText.text = score.ToString();
    }

    public void UpdateScoreMultiplier()
    {
        if (GameManager.instance.shootedBall == 1)
        {
            scoreMultiplier++;
            scoreMultiImage.SetActive(true);
            scoreMultiText.text = scoreMultiplier.ToString();
        }
        else
        {
            scoreMultiplier = 1;
            scoreMultiImage.SetActive(false);
        }
    }

    public void B_Restart()
    {

        StartCoroutine("RestartGameRoutine");


    }

    IEnumerator RestartGameRoutine()
    {
        ShowBlackFade();
        isRestart = true;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(0);
    }

    public void B_Bck()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void B_Bck_Yes()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

    public void B_Bck_No()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }


    public void B_Exit()
    {
        StartCoroutine(BExit());
    }
    
    public IEnumerator BExit()
    {
        GameOverUI.SetActive(false);
        ShowBlackFade();
        isRestart = false;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(0);
    }


   
}
