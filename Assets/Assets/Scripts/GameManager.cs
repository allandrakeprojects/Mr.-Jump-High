using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Lvl1;
    public GameObject Lvl1_BG;
    public GameObject Lvl2;
    public GameObject Lvl2_BG;
    public GameObject Lvl3;
    public GameObject Lvl3_BG;
    public GameObject Lvl4;
    public GameObject Lvl4_BG;


    void Start()
    {
        Manage();
    }

    void Manage()
    {
        int game_mode = PlayerPrefs.GetInt("GAME_MODE");
        if (game_mode == 0)
        {
            PlayerPrefs.SetInt("CURRENT_LEVEL", 1);
            PlayerPrefs.Save();

            Lvl1.SetActive(true);
            Lvl1_BG.SetActive(true);
        }
        else
        {
            int current_level = PlayerPrefs.GetInt("CURRENT_LEVEL", 1);
            if (current_level == 1)
            {
                Lvl1.SetActive(true);
                Lvl1_BG.SetActive(true);
            }
            else if (current_level == 2)
            {
                Lvl2.SetActive(true);
                Lvl2_BG.SetActive(true);
            }
            else if (current_level == 3)
            {
                Lvl3.SetActive(true);
                Lvl3_BG.SetActive(true);
            }
            else if (current_level == 4)
            {
                Lvl4.SetActive(true);
                Lvl4_BG.SetActive(true);
            }
        }

        PlayerPrefs.SetInt("GAME_MODE", 1);
        PlayerPrefs.Save();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Game");
    }
}
