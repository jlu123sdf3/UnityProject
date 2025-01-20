using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class PlayerPointsObject : MonoBehaviour
{
    int score;
    int multiplier = 1;
    const int secondsToIncrease = 20;
    double timeElapsed;
    UIDocument document;
    Label scoreLabel;
    Label multiplierLabel;
    void Awake()
    {
        timeElapsed = secondsToIncrease;
        document = GetComponent<UIDocument>();
        scoreLabel = document.rootVisualElement.Query("PointsValue").AtIndex(0) as Label;
        multiplierLabel = document.rootVisualElement.Query("MultiplierValue").AtIndex(0) as Label;
    }
    public void HideView()
    {
        document.rootVisualElement.Query("PointsRoot").AtIndex(0).style.visibility = Visibility.Hidden;
        document.rootVisualElement.Query("MultiplierRoot").AtIndex(0).style.visibility = Visibility.Hidden;
    }
    public void ShowView()
    {
        document.rootVisualElement.Query("PointsRoot").AtIndex(0).style.visibility = Visibility.Visible;
        document.rootVisualElement.Query("MultiplierRoot").AtIndex(0).style.visibility = Visibility.Visible;

    }
    private void Update()
    {
            timeElapsed -= Time.deltaTime;
        if (timeElapsed < 0)
        {
            timeElapsed += secondsToIncrease;
            Multiplier++;
        }

    }
    public int Score { 
        get { return score; } 
        set { 
            score += (value - score )* multiplier; 
            scoreLabel.text = score.ToString(); 
        } 
    }
    public void resetMultiplier()
    {
        Multiplier = 1;
    }
    public int Multiplier
    {
        get { return multiplier; }
        set
        {
            multiplier = value;
            multiplierLabel.text = "x " + multiplier.ToString();
        }
    }
}
