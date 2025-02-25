using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button resumeButton;

    private void Awake()
    {
        
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScenes);
        });
        optionButton.onClick.AddListener((() =>
        {
            OptionUI.Instance.Show();
        }));
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += InstanceOnOnGamePaused;
        GameManager.Instance.OnGameUnpaused += InstanceOnOnGameUnpaused;
        Hide();
    }

    private void InstanceOnOnGameUnpaused(object sender, EventArgs e)
    {
        Hide();

    }


    private void InstanceOnOnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
