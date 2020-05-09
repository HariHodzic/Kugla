using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Transform kugla;
    [SerializeField]
    private Text ScoreValue;
    public bool GameOn = true;
    void Update()
    {
        if(GameOn)
        ScoreValue.text = kugla.position.x.ToString("0");
    }
}
