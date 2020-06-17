﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{

    [SerializeField] private GameObject paddle;
    [SerializeField] private GameObject standardBlock;
    [SerializeField] private GameObject bonusBlock;

    [SerializeField] private GameObject freezeBlock;
    [SerializeField] private GameObject speedUpBlock;

    private float screenHeight;
    private float offsetX=.5f;
    
    void Start()
    {
        Instantiate(paddle);
        screenHeight = (ScreenUtils.ScreenTop + ScreenUtils.ScreenBottom) / 5;
        float blockAmountInWidth = 6;
        
        
        for (float i = 0; i < blockAmountInWidth; i++)
        {
            for (float j = 0; j < 4; j++)
            {
                GameObject tempInstance = RandomizeProbabilities();
                // Instantiate(j<3? standardBlock: bonusBlock, new Vector2(Mathf.Lerp(ScreenUtils.ScreenLeft + offsetX,
                Instantiate(tempInstance, new Vector2(Mathf.Lerp(ScreenUtils.ScreenLeft + offsetX,
                    ScreenUtils.ScreenRight - offsetX,
                    (i) / (blockAmountInWidth-1)),  j* (screenHeight+offsetX*2)), Quaternion.identity);
            }
        }
    }

    void Update()
    {
        
    }

    GameObject RandomizeProbabilities()
    {
        float rand = Random.value;

        if (rand <= ConfigurationUtils.StandardProbability)
            return standardBlock;
        else if (rand > ConfigurationUtils.StandardProbability && rand <= ConfigurationUtils.BonusProbability)
            return bonusBlock;
        else if (rand > ConfigurationUtils.BonusProbability && rand <= ConfigurationUtils.FreezeProbabilities)
            return freezeBlock;
        else
            return speedUpBlock;            
    }
}
