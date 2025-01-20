using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class HowToPlayScript : MonoBehaviour
{
    private UIDocument document;
    private Button backButton;
    void Start()
    {
        document = GetComponent<UIDocument>();
        backButton = document.rootVisualElement.Q<Button>() as Button;
        backButton.RegisterCallback<ClickEvent>(OnClickReturn);
    }

    private void OnClickReturn(ClickEvent click)
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
