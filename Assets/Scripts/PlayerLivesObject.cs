using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;


public class PlayerLivesObject : MonoBehaviour
{
    int lives;
    UIDocument document;
    Label livesLabel;
    Button returnBtn;
    void Awake()
    {
        document = GetComponent<UIDocument>();
        livesLabel = document.rootVisualElement.Query("LivesValue").AtIndex(0) as Label;
        Lives = 3;
        returnBtn = document.rootVisualElement.Query("ReturnButton").AtIndex(0) as Button;
        returnBtn.RegisterCallback<ClickEvent>(OnClickReturn);
    }
    
    public void HideView()
    {
        livesLabel.style.visibility = Visibility.Hidden;
    }
    public void ShowView()
    {
        livesLabel.style.visibility = Visibility.Visible;
        returnBtn.style.visibility = Visibility.Visible;
    }
    public int Lives { 
        get { return lives; } 
        set {
            lives = value; 
            livesLabel.text = lives.ToString(); 
            //PlayerPrefs.SetInt("score", value); 
        } 
    }
    private void OnClickReturn(ClickEvent click)
    {
        SceneManager.LoadScene("LevelChoice", LoadSceneMode.Single);
    }
}
