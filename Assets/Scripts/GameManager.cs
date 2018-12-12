using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public delegate void GameDelegate();
    public static event GameDelegate OnGameOverConfirmed;




    public static GameManager Instance;

    public GameObject MainMenu;
    public GameObject GameOverMenu;
    public GameObject HowToPlay;
    public GameObject GamePlay;
    public Text Score;
    public Text Lives;
    public Text currentScore;

    enum PageState
    {
        None,
        Start,
        Play,
        HowToPlay,
        GameOver
    }

    int score = 0;
    public int lifes = 3;
    bool gameOver = true;

    public bool GameOver { get { return gameOver; } }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (score == 20)
        {
            GameObject.Find("Environment/one").GetComponent<Parallaxer>().shiftSpeed = -4.5f;
            GameObject.Find("Environment/zero").GetComponent<Parallaxer>().shiftSpeed = -4.0f;
            GameObject.Find("Environment/bug").GetComponent<Parallaxer>().shiftSpeed = -4.9f;
            GameObject.Find("Environment/life").GetComponent<Parallaxer>().shiftSpeed = -3.6f;
            GameObject.Find("Environment/coffee").GetComponent<Parallaxer>().shiftSpeed = -3.6f;
            GameObject.Find("Environment/sand-watch").GetComponent<Parallaxer>().shiftSpeed = -3.6f;
            GameObject.Find("Environment/wrench").GetComponent<Parallaxer>().shiftSpeed = -3.2f;



        }
        else if (score == 40)
        {
            GameObject.Find("Environment/one").GetComponent<Parallaxer>().shiftSpeed = -5.5f;
            GameObject.Find("Environment/zero").GetComponent<Parallaxer>().shiftSpeed = -5.7f;
            GameObject.Find("Environment/bug").GetComponent<Parallaxer>().shiftSpeed = -5.5f;
            GameObject.Find("Environment/life").GetComponent<Parallaxer>().shiftSpeed = -4.5f;
            GameObject.Find("Environment/coffee").GetComponent<Parallaxer>().shiftSpeed = -4.5f;
            GameObject.Find("Environment/sand-watch").GetComponent<Parallaxer>().shiftSpeed = -4.6f;
            GameObject.Find("Environment/wrench").GetComponent<Parallaxer>().shiftSpeed = -4.2f;



        }
        else if (score == 60)
        {
            GameObject.Find("Environment/one").GetComponent<Parallaxer>().shiftSpeed = -6.5f;
            GameObject.Find("Environment/zero").GetComponent<Parallaxer>().shiftSpeed = -6.7f;
            GameObject.Find("Environment/bug").GetComponent<Parallaxer>().shiftSpeed = -7.2f;
            GameObject.Find("Environment/life").GetComponent<Parallaxer>().shiftSpeed = -7.3f;
            GameObject.Find("Environment/coffee").GetComponent<Parallaxer>().shiftSpeed = -7.3f;
            GameObject.Find("Environment/sand-watch").GetComponent<Parallaxer>().shiftSpeed = -7.6f;
            GameObject.Find("Environment/wrench").GetComponent<Parallaxer>().shiftSpeed = -6.2f;




            GameObject.Find("Environment/one").GetComponent<Parallaxer>().spawnRate = 0.5f;
            GameObject.Find("Environment/zero").GetComponent<Parallaxer>().spawnRate = 0.75f;
            GameObject.Find("Environment/bug").GetComponent<Parallaxer>().spawnRate = 2.0f;
            GameObject.Find("Environment/coffee").GetComponent<Parallaxer>().spawnRate = 2.0f;
            GameObject.Find("Environment/wrench").GetComponent<Parallaxer>().spawnRate = 2.0f;

        }
        else if (score == 80)
        {
            GameObject.Find("Environment/one").GetComponent<Parallaxer>().shiftSpeed = -8.2f;
            GameObject.Find("Environment/zero").GetComponent<Parallaxer>().shiftSpeed = -7.5f;
            GameObject.Find("Environment/bug").GetComponent<Parallaxer>().shiftSpeed = -8.0f;
            GameObject.Find("Environment/life").GetComponent<Parallaxer>().shiftSpeed = -8.9f;
            GameObject.Find("Environment/coffee").GetComponent<Parallaxer>().shiftSpeed = -8.9f;
            GameObject.Find("Environment/sand-watch").GetComponent<Parallaxer>().shiftSpeed = -8.6f;
            GameObject.Find("Environment/wrench").GetComponent<Parallaxer>().shiftSpeed = -7.2f;



        }
        else if (score == 100)
        {
            GameObject.Find("Environment/one").GetComponent<Parallaxer>().shiftSpeed = -8.7f;
            GameObject.Find("Environment/zero").GetComponent<Parallaxer>().shiftSpeed = -8.2f;
            GameObject.Find("Environment/bug").GetComponent<Parallaxer>().shiftSpeed = -8.7f;
            GameObject.Find("Environment/life").GetComponent<Parallaxer>().shiftSpeed = -9.5f;
            GameObject.Find("Environment/coffee").GetComponent<Parallaxer>().shiftSpeed = -9.5f;
            GameObject.Find("Environment/sand-watch").GetComponent<Parallaxer>().shiftSpeed = -9.6f;
            GameObject.Find("Environment/wrench").GetComponent<Parallaxer>().shiftSpeed = -9.2f;



        }
    }

    void OnEnable()
    {
        BucketController.OnPlayerDied += OnPlayerDied;
        BucketController.OnPlayerScored += OnPlayerScored;
        BucketController.OnPlayerGainLife += OnPlayerGainLife;
        BucketController.OnPlayerLoseLife += OnPlayerLoseLife;
        Parallaxer.OnPlayerGainLife += OnPlayerGainLife;
        Parallaxer.OnPlayerLoseLife += OnPlayerLoseLife;
        BucketController.OnPlayerLoseScore += OnPlayerLoseScore;

    }

    void OnDisable()
    {
        BucketController.OnPlayerDied -= OnPlayerDied;
        BucketController.OnPlayerScored -= OnPlayerScored;
        BucketController.OnPlayerGainLife -= OnPlayerGainLife;
        BucketController.OnPlayerLoseLife -= OnPlayerLoseLife;
        Parallaxer.OnPlayerGainLife -= OnPlayerGainLife;
        Parallaxer.OnPlayerLoseLife -= OnPlayerLoseLife;
        BucketController.OnPlayerLoseScore -= OnPlayerLoseScore;

    }

    void OnPlayerScored()
    {
        score++;
        Score.text = score.ToString();
    }
    void OnPlayerLoseScore()
    {
        score--;
        Score.text = score.ToString();
    }

    void OnPlayerDied()
    {
        gameOver = true;
        currentScore.text = "Score: " + score.ToString();
        lifes = 3;
        GameObject.Find("Canvas/GamePlay/bucket").GetComponent<BucketController>().speed = 0.1f;
        int savedScore = PlayerPrefs.GetInt("HighScore");
        if (score > savedScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        SetPageState(PageState.GameOver);
    }
    void OnPlayerGainLife()
    {
        lifes++;
        Lives.text = "lives: " + lifes.ToString();
    }
    void OnPlayerLoseLife()
    {
        lifes--;
        Lives.text = "lives: " + lifes.ToString();
        if (lifes == 0)
        {
            OnPlayerDied();
        }
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                MainMenu.SetActive(false);
                GameOverMenu.SetActive(false);
                GamePlay.SetActive(false);
                HowToPlay.SetActive(false);
                break;
            case PageState.Start:
                MainMenu.SetActive(true);
                GameOverMenu.SetActive(false);
                GamePlay.SetActive(false);
                HowToPlay.SetActive(false);
                break;
            case PageState.Play:
                MainMenu.SetActive(false);
                GameOverMenu.SetActive(false);
                GamePlay.SetActive(true);
                HowToPlay.SetActive(false);
                break;
            case PageState.GameOver:
                MainMenu.SetActive(false);
                GameOverMenu.SetActive(true);
                GamePlay.SetActive(false);
                HowToPlay.SetActive(false);
                break;
            case PageState.HowToPlay:
                MainMenu.SetActive(false);
                GameOverMenu.SetActive(false);
                GamePlay.SetActive(false);
                HowToPlay.SetActive(true);
                break;
        }
    }

    public void ConfirmGameOver()
    {
        SetPageState(PageState.Start);
        Score.text = "0";
        lifes = 3;
        OnGameOverConfirmed();
    }
    public void instructions()
    {
        SetPageState(PageState.HowToPlay);
    }

    public void StartGame()
    {
        SetPageState(PageState.Play);
        score = 0;
        lifes = 3;
        gameOver = false;
        Lives.text = "lives: " + lifes.ToString();

    }

}