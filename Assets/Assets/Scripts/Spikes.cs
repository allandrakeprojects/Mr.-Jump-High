using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	public GameObject Heart_01;
    public GameObject Heart_02;

    private void OnTriggerEnter2D(Collider2D other)
	{
        if (Heart_01.activeSelf)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Health = 1;
                player.Damage();
                Heart_01.SetActive(false);
                Heart_02.SetActive(false);
            }
        }
	}
}
