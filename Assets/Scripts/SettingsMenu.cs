using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private Slider volumeSlider;
    private Toggle fullscreenToggle;
    private Button backButton;
    private DropdownField resolutionDropdown; 
    private Resolution[] resolutions;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

       
        volumeSlider = root.Q<Slider>("volumeSlider");
        if (volumeSlider != null)
        {
            volumeSlider.RegisterValueChangedCallback(evt => SetVolume(evt.newValue));
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
        }

       
        fullscreenToggle = root.Q<Toggle>("fullscreenToggle");
        if (fullscreenToggle != null)
        {
            fullscreenToggle.value = PlayerPrefs.GetInt("Fullscreen", 0) == 1;
            fullscreenToggle.RegisterValueChangedCallback(evt => SetFullscreen(evt.newValue));
        }

      
        resolutionDropdown = root.Q<DropdownField>("resolutionDropdown");
        if (resolutionDropdown != null)
        {
            PopulateResolutions();
            resolutionDropdown.RegisterValueChangedCallback(evt => ApplyResolution(evt.newValue));
        }

        
        backButton = root.Q<Button>("backBtn");
        if (backButton != null)
        {
            backButton.clicked += BackToMainMenu;
        }
    }

    private void OnDisable()
    {
       
        if (volumeSlider != null)
        {
            PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        }

      
        if (fullscreenToggle != null)
        {
            PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.value ? 1 : 0);
        }
    }

    private void SetVolume(float volume)
    {

        if (volume <= 0.01f)
        {
            audioMixer.SetFloat("MasterVolume", -80f); 
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }
    }


    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void PopulateResolutions()
    {
       
        resolutions = Screen.resolutions;
        resolutionDropdown.choices.Clear();

        foreach (var resolution in resolutions)
        {
            string resolutionText = $"{resolution.width} x {resolution.height} @ {resolution.refreshRate}Hz";
            resolutionDropdown.choices.Add(resolutionText);
        }

       
        Resolution currentResolution = Screen.currentResolution;
        string currentResolutionText = $"{currentResolution.width} x {currentResolution.height} @ {currentResolution.refreshRate}Hz";
        resolutionDropdown.value = currentResolutionText;
    }

    private void ApplyResolution(string selectedResolution)
    {
        
        foreach (var resolution in resolutions)
        {
            string resolutionText = $"{resolution.width} x {resolution.height} @ {resolution.refreshRate}Hz";
            if (resolutionText == selectedResolution)
            {
                Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
                PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.index);
                PlayerPrefs.Save();                
                break;
            }
        }
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
