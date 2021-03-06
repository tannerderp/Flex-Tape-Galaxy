﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour
{
    [SerializeField] Sprite[] healthSprites;

    public void ChangeSprite(int index)
    {
        GetComponent<Image>().sprite = healthSprites[index];
    }
}
