using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedCollider : MonoBehaviour
{
    private LevelManager levelManager;
    public GameObject Level_Passed;

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
        }
    }
}
