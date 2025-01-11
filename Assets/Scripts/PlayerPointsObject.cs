using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class PlayerPointsObject : MonoBehaviour
{
    int score;
    UIDocument document;
    Label scoreLabel;
    void Awake()
    {
        document = GetComponent<UIDocument>();
        scoreLabel = document.rootVisualElement.Query("PointsValue").AtIndex(0) as Label;
    }
    public void HideView()
    {
        document.rootVisualElement.Query("PointsRoot").AtIndex(0).style.visibility = Visibility.Hidden;
    }
    public void ShowView()
    {
        document.rootVisualElement.Query("PointsRoot").AtIndex(0).style.visibility = Visibility.Visible;
    }
    public int Score { 
        get { return score; } 
        set { 
            score = value; 
            scoreLabel.text = score.ToString(); 
            //PlayerPrefs.SetInt("score", value); 
        } 
    }
}
