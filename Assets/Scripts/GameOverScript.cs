using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverScript : MonoBehaviour
{
    private UIDocument document;
    private Button replaybutton;
    private Button quitButton;
    void Start()
    {
        document = GetComponent<UIDocument>();
        replaybutton = document.rootVisualElement.Query(className: "btn").AtIndex(0) as Button;
        replaybutton.RegisterCallback<ClickEvent>(OnClickReplay);


        quitButton = document.rootVisualElement.Query(className: "btn").AtIndex(1) as Button;
        quitButton.RegisterCallback<ClickEvent>(OnClickReturn);
    }
    private void OnClickReplay(ClickEvent click)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("GameOverScene"));
    }
    private void OnClickReturn(ClickEvent click)
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

    }

}
