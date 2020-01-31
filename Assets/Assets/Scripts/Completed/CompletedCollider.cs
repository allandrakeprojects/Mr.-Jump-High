using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedCollider : MonoBehaviour
{
    private LevelManager levelManager;
    public GameObject Level_Passed;
    public GameObject Next_Level;
    private bool isStop = true;

    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            Level_Passed.SetActive(true);

            int current_level = PlayerPrefs.GetInt("CURRENT_LEVEL");
            if (current_level == 4)
            {
                Next_Level.SetActive(false);
            }
            else
            {
                if (isStop)
                {
                    isStop = false;
                    current_level += 1;
                    PlayerPrefs.SetInt("CURRENT_LEVEL", current_level);
                    PlayerPrefs.Save();
                }
            }
        }
    }
}
