using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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
        GameObject gameCamera = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject menuCamera = GameObject.FindGameObjectWithTag("MenuCamera");
        menuCamera.transform.SetPositionAndRotation(gameCamera.transform.position, gameCamera.transform.rotation);
    }
    private void OnClickReplay(ClickEvent click)
    {
        string currentSceneName = "";
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "GameOverScene")
             currentSceneName = scene.name; 
        }
        StartCoroutine(LoadScene(currentSceneName));
    }
    public IEnumerator LoadScene(string name)
    {
        var previousGameUnload = SceneManager.UnloadSceneAsync(name);
        while (!previousGameUnload.isDone)
        {
            if (previousGameUnload.progress >= 0.9f)
                break;
            yield return null;
        }

        var loadScene = SceneManager.LoadSceneAsync(name,LoadSceneMode.Additive);
        loadScene.allowSceneActivation = false;
        while (!loadScene.isDone)
        {
            if (loadScene.progress >= 0.9f)
                break;
            yield return null;
        }
        loadScene.allowSceneActivation = true;
        while (!loadScene.isDone)
            yield return null;
        
        StartLevel();

        var gameOverUnload = SceneManager.UnloadSceneAsync("GameOverScene");
        while (!gameOverUnload.isDone)
        {
            if (gameOverUnload.progress >= 0.9f)
                break;
            yield return null;
        }

    }
    public void StartLevel()
    {
        PlayerLivesObject lives = null;
        while (lives == null) { lives = (PlayerLivesObject)FindObjectOfType(typeof(PlayerLivesObject)); Debug.Log(lives); }
        PlayerPointsObject points = (PlayerPointsObject)FindObjectOfType(typeof(PlayerPointsObject));
        lives.ShowView();
        points.ShowView();
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        Debug.Log(ballRigidbody.useGravity);
        ballRigidbody.useGravity = true;
        Debug.Log(ballRigidbody.useGravity);
    }
    private void OnClickReturn(ClickEvent click)
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
