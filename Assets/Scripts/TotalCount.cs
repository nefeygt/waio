using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TotalCount : MonoBehaviour
{
    [SerializeField] private Text totalText;
    
    void Start()
    {
        totalText.text = "Score: " + ItemCollector.TotalCollectedFruits + "/" + ItemCollector.TotalFruits;
    }
}
