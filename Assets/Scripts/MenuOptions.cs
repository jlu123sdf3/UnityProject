using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class MenuOptions : MonoBehaviour
{
   private UIDocument document;
    private Button playbutton;
    private Button scoreButton;
    private Button settingsButton;
    private Button quitButton;
    public TextField nametagInputField;
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

        nametagInputField = document.rootVisualElement.Q<TextField>();
        nametagInputField.RegisterCallback<ChangeEvent<string>>((changeEvent) =>
        {
            PlayerPrefs.SetString("nametag", changeEvent.newValue);
            PlayerPrefs.Save();

        });

    }

    private void OnClickPlay(ClickEvent click)
    {
        SceneManager.LoadScene("LevelChoice", LoadSceneMode.Single);
    }

    private void OnClickOpenSettings(ClickEvent click)
    {
        
       SceneManager.LoadScene("SettingsScene",LoadSceneMode.Single); 
        
    }
    private void OnClickOpenScores(ClickEvent click)
    {
        SceneManager.LoadScene("Scores", LoadSceneMode.Single);
    }
    private void OnClickQuit(ClickEvent click)
    {
        // Only works in build, not IDE
        Application.Quit();
    }
}
