using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class TitlePanelType
{
    public static readonly string NONE = "";
    public static readonly string CREDIT = "CreditPanel";
    public static readonly string SETTINGS = "SettingPanel";
    public static readonly string EXIT = "ConfirmPanel";
}

public class SceneController : MonoBehaviour
{
    private string _activePanel;
    private bool _isOpenPanel;

    public GameObject targetPanel;
    public Toggle windowedToggle;
    public Slider musicVolSlider;

    void Start()
    {
        _isOpenPanel = false;
        ClosePanel();

        if (windowedToggle != null)
        {
            windowedToggle.onValueChanged.AddListener(delegate {
                OnToggleWindowed(windowedToggle);
            });
            windowedToggle.isOn = !Screen.fullScreen;
        }

        if (musicVolSlider != null)
        {
            musicVolSlider.onValueChanged.AddListener(delegate {
                OnVolumeUpdate(musicVolSlider);
            });
            AudioController.Instance.SetVolume(1);
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void LoadNextScene()
    {
        AudioController.Instance.ClickSFX();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToTitleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenCreditPanel()
    {
        _isOpenPanel = true;
        _activePanel = TitlePanelType.CREDIT;
    }

    public void ClosePanel()
    {
        _activePanel = TitlePanelType.NONE;
        if (targetPanel == null) return;
        targetPanel.SetActive(false);
        for (int i = 0; i < targetPanel.transform.childCount; i++)
        {
            var child = targetPanel.transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }

    public void OnToggleWindowed(Toggle change)
    {
        Screen.fullScreen = !windowedToggle.isOn;
    }

    public void OnVolumeUpdate(Slider change)
    {
        AudioController.Instance.SetVolume(change.value);
    }

    public void OpenConfirmExit()
    {
        _isOpenPanel = true;
        _activePanel = TitlePanelType.EXIT;
    }

    public void OpenSetting()
    {
        _isOpenPanel = true;
        _activePanel = TitlePanelType.SETTINGS;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "SampleScene" || SceneManager.GetActiveScene().buildIndex == 1)
            {
                GoToTitleScene();
            }
        }

        if (!_isOpenPanel) return;
        _isOpenPanel = false;

        if (targetPanel == null) return;
        targetPanel.SetActive(true);
        var i = targetPanel.transform.childCount - 1;
        while (i >= 0)
        {
            var child = targetPanel.transform.GetChild(i);
            if (_activePanel == child.gameObject.name)
            {
                child.gameObject.SetActive(true);
                i = 0; // Is found
            }
            i -= 1;
        }
    }
}