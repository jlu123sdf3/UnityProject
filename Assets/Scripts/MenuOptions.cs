using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuOptions : MonoBehaviour
{
   private UIDocument document;
    private Button playbutton;
    private Button scoreButton;
    private Button settingsButton;
    private Button quitButton;
    private List<Button> allButtons;
    private void Awake()
    {
        document = GetComponent<UIDocument>();
        playbutton = document.rootVisualElement.Query(className: "btn").AtIndex(0) as Button;
        playbutton.RegisterCallback<ClickEvent>(OnClickPlay);

        scoreButton = document.rootVisualElement.Query(className: "btn").AtIndex(1) as Button;
        scoreButton.RegisterCallback<ClickEvent>(OnClickOpenScores);

        settingsButton = document.rootVisualElement.Query(className: "btn").AtIndex(2) as Button;
        settingsButton.RegisterCallback<ClickEvent>(OnClickOpenSettings);

        quitButton = document.rootVisualElement.Query(className: "btn").AtIndex(3) as Button;
        quitButton.RegisterCallback<ClickEvent>(OnClickQuit);
    }
    private void OnClickPlay(ClickEvent click)
    {
        Debug.Log("Play");
    }
    private void OnClickOpenSettings(ClickEvent click)
    {
        Debug.Log("Settings");
    }
    private void OnClickOpenScores(ClickEvent click)
    {
        Debug.Log("Scores");
    }
    private void OnClickQuit(ClickEvent click)
    {
        Debug.Log("Quit");
    }
}
