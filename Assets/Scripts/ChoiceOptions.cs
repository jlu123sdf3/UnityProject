using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ChoiceOptions : MonoBehaviour
{
    private UIDocument document;
    private Button playbutton;
    private Button returnbutton;
    private Button nextButton;
    private Button prevButton;
    private List<string> levels = new List<string> { "LevelOne", "LevelTwo", "LevelThree" };
    private int index = 0;
    private void Awake()
    {
        document = GetComponent<UIDocument>();

        playbutton = document.rootVisualElement.Query(className: "btn").AtIndex(0) as Button;
        playbutton.RegisterCallback<ClickEvent>(OnClickPlay);

        returnbutton = document.rootVisualElement.Query(className: "btn").AtIndex(1) as Button;
        returnbutton.RegisterCallback<ClickEvent>(OnClickReturn);

        nextButton = document.rootVisualElement.Query(className: "arrowBtn").AtIndex(0) as Button;
        nextButton.RegisterCallback<ClickEvent>(OnClickPrev);

        prevButton = document.rootVisualElement.Query(className: "arrowBtn").AtIndex(1) as Button;
        prevButton.RegisterCallback<ClickEvent>(OnClickNext);

        showLevelPreview();
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }
    private void showLevelPreview()
    {
        SceneManager.LoadScene(levels[index], LoadSceneMode.Additive);
        
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }
    private void UnloadCurrentScene()
    {
        SceneManager.UnloadSceneAsync(levels[index]);
    }
    private void OnClickPlay(ClickEvent click)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != levels[index])
                SceneManager.UnloadSceneAsync(scene);
        }
    }
    private void OnClickReturn(ClickEvent click)
    {
       SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);        
    }
    private void OnClickNext(ClickEvent click)
    {
        UnloadCurrentScene();
        index = (index+1)%levels.Count;
        showLevelPreview();
    }
    private void OnClickPrev(ClickEvent click)
    {
        UnloadCurrentScene();
        index = (index - 1) % levels.Count;
        showLevelPreview();
    }
}
