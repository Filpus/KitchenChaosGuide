using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameOverUI : MonoBehaviour

{    
    [SerializeField] private TextMeshProUGUI deliveredRecipesText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManagerOnOnStateChanged;
        Hide();
    }

    private void GameManagerOnOnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            deliveredRecipesText.text = DeliveryManager.Instance.GetDeliveredRecipes().ToString();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {

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
