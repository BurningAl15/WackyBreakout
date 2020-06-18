using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    [SerializeField] private Text text;

    private void Start()
    {
        text.text = "Your Score is: " + PlayerPrefs.GetInt("Score");
    }
}
