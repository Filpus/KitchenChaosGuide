using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance;
    
    
    
    
    
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interacAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Transform pressToRebindKeyTransform;
    private void Awake()
    {
        soundEffectsButton.onClick.AddListener((() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        }));
        
        musicButton.onClick.AddListener((() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        }));
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
        
        moveUpButton.onClick.AddListener(()=> {RebindBinding(GameInput.Binding.Move_Up);});
        moveDownButton.onClick.AddListener(()=> {RebindBinding(GameInput.Binding.Move_Down);});
        moveLeftButton.onClick.AddListener(()=> {RebindBinding(GameInput.Binding.Move_Left);});
        moveRightButton.onClick.AddListener(()=> {RebindBinding(GameInput.Binding.Move_Right);});
        interactButton.onClick.AddListener(()=> {RebindBinding(GameInput.Binding.Interact);});
        interacAltButton.onClick.AddListener(()=> {RebindBinding(GameInput.Binding.InteractAlternate);});
        pauseButton.onClick.AddListener(()=> {RebindBinding(GameInput.Binding.Pause);});
        
        
        
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += (sender, args) => Hide();
        
        UpdateVisual();
        HidePressToRebindKey();
        Hide();
    }


    private void UpdateVisual()
    {
        soundEffectsText.text =  "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }


    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding,() =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }

}
