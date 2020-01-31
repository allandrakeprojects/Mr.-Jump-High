using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePowerUps : MonoBehaviour
{
    float speed = 2f;
    float height = 0.5f;

    float firstPosX = 0;
    float firstPosY = 0;

    void Update()
    {
        Vector3 pos = transform.position;
        if (firstPosX == 0)
        {
            firstPosX = (pos.x * 2);
            firstPosY = (pos.y * 2);
        }

        float newY = firstPosY + Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(firstPosX, newY, pos.z) * height;
    }
}
