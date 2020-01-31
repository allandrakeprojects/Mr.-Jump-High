using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioClip[] menuSound;
    private LevelManager levelManager;
    public Text coinCountText;
    public Text heartCountText;
    public Text messageHeartText;
    public bool detectStart = false;
    [SerializeField] private GameObject IAPMenu;
    [SerializeField] private GameObject LevelSelection;
    public Button[] LevelBars;
    public Player player;
    private Animator _anim;
    private SpriteRenderer _playerSprite;

    // GameOver
    public GameObject levelGameOver;

    // Start is called before the first frame update
    void Start()
    {

        //assign handle to playerAnimation
        player = FindObjectOfType<Player>();
        _anim = FindObjectOfType<Player>().GetComponentInChildren<Animator>();
        _playerSprite = FindObjectOfType<Player>().GetComponentInChildren<SpriteRenderer>();
        levelManager = FindObjectOfType<LevelManager>();
        UpdateScore();
        UpdateLevel();
    }

    public void MainMenuButton()
    {
        PlayerPrefs.SetString("waitDate", System.DateTime.Now.AddMinutes(30).ToString());
        PlayerPrefs.Save();

        Time.timeScale = 1;

        levelManager.LoadMainMenuAfterDelay();
    }

    public void PLAYGAME()
    {
        PlayerPrefs.SetInt("heart", 1000);
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt("heart") == 0)
        {
            messageHeartText.text = "Not Enough Heart!";
        }
        else
        {
            if (!detectStart)
            {
                detectStart = true;
                //messageHeartText.text = "";
                //int getHeart = PlayerPrefs.GetInt("heart") - 1;
                //Debug.Log(getHeart.ToString());
                //PlayerPrefs.SetInt("heart", getHeart);
                //PlayerPrefs.Save();
                //Debug.Log(getHeart.ToString());
                //heartCountText.text = getHeart.ToString();
                levelManager.LoadGameAfterDelay();
            }
        }
    }

    private bool detectRetry = false;

    public void Retry()
    {
        levelGameOver.GetComponent<Canvas>().enabled = false;
        detectRetry = true;
        Vector3 newPos = _playerSprite.transform.localScale;
        newPos.x = -1.0f;
        _playerSprite.transform.localScale = newPos;
        player.transform.position = new Vector3(21.56f, 8.11f, 0f);
    }

    void Update()
    {
        if (detectRetry)
        {
            int wait_to_play = PlayerPrefs.GetInt("WAIT_TO_PLAY");
            if (wait_to_play == 0)
            {
                _anim.SetTrigger("Attack");
                _anim.SetBool("IsJumping", true);
                StartCoroutine(Restart());
                detectRetry = false;
            }
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }

    public void QuitButton()
    {
        levelManager.LoadQuitAfterDelay();
    }

    public void Resume()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenIAP()
    {
        IAPMenu.SetActive(true);
    }

    public void CloseIAP()
    {
        IAPMenu.SetActive(false);
    }

    public void OpenLevelSelection()
    {
        LevelSelection.SetActive(true);
    }

    public void CloseLevelSelection()
    {
        LevelSelection.SetActive(false);
    }

    public void OpenLevel_1()
    {
        PlayerPrefs.SetInt("level_selected", 1);
        PlayerPrefs.Save();
        PLAYGAME();
    }

    public void OpenLevel_2()
    {
        PlayerPrefs.SetInt("level_selected", 2);
        PlayerPrefs.Save();
        PLAYGAME();
    }

    public void OpenLevel_3()
    {
        PlayerPrefs.SetInt("level_selected", 3);
        PlayerPrefs.Save();
        PLAYGAME();
    }

    public void OpenLevel_4()
    {
        PlayerPrefs.SetInt("level_selected", 4);
        PlayerPrefs.Save();
        PLAYGAME();
    }

    public void OpenLevel_5()
    {
        PlayerPrefs.SetInt("level_selected", 5);
        PlayerPrefs.Save();
        PLAYGAME();
    }

    public void UpdateScore()
    {
        try
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            if (sceneName.Equals("Main_Menu"))
            {
                if (PlayerPrefs.GetInt("highscore", 0) == 0)
                {
                    coinCountText.text = "HIGHSCORE: 0";
                }
                else
                {
                    coinCountText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore");
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("highscore", 0) == 0)
                {
                    coinCountText.text = "SCORE: 0";
                }
                else
                {
                    if (PlayerPrefs.GetInt("score", 0) == 10000)
                    {
                        coinCountText.text = "NEW HIGHSCORE: " + PlayerPrefs.GetInt("highscore");
                    }
                    else
                    {
                        coinCountText.text = "SCORE: " + PlayerPrefs.GetInt("score");
                    }

                    PlayerPrefs.SetInt("score", 0);
                    PlayerPrefs.Save();
                }
            }
        }
        catch (Exception er)
        {
            // leave blank
        }

    }

    public void UpdateLevel()
    {
        try
        {
            if (PlayerPrefs.GetString("level", "1,0,0,0,0") == "1,0,0,0,0")
            {
                // Level 1
                PlayerPrefs.SetString("level", "1,0,0,0,0");
                PlayerPrefs.Save();
                LevelBars[0].interactable = true;
            }
            else
            {
                string level = PlayerPrefs.GetString("level");
                string[] levels = level.Split(',');
                if (levels[0] == "1")
                {
                    LevelBars[0].interactable = true;
                }

                if (levels[1] == "1")
                {
                    LevelBars[1].interactable = true;
                }

                if (levels[2] == "1")
                {
                    LevelBars[2].interactable = true;
                }

                if (levels[3] == "1")
                {
                    LevelBars[3].interactable = true;
                }

                if (levels[4] == "1")
                {
                    LevelBars[4].interactable = true;
                }
            }
        }
        catch (Exception err)
        {
            // leave blank
        }
    }
}
