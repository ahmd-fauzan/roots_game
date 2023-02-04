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

    private GameObject _prevPanel;
    public GameObject targetPanel;
    public Toggle windowedToggle;

    void Start()
    {
        _isOpenPanel = false;
        ClosePanel();

        if (windowedToggle != null)
        {
            windowedToggle.onValueChanged.AddListener(delegate {
                OnToggleWindowed(windowedToggle);
            });
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToTitleScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenCreditPanel()
    {
        _isOpenPanel = true;
        _activePanel = TitlePanelType.CREDIT;
    }

    public void ClosePanel()
    {
        _activePanel = TitlePanelType.NONE;
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
        if (!_isOpenPanel) return;
        _isOpenPanel = false;

        targetPanel.SetActive(true);
        var i = targetPanel.transform.childCount - 1;
        while (i >= 0)
        {
            var child = targetPanel.transform.GetChild(i);
            if (_activePanel == child.gameObject.name)
            {
                child.gameObject.SetActive(true);
                i = -1;
            }
            i -= 1;
        }
    }
}