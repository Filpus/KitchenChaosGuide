using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualSelectedObjects;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += InstanceOnOnSelectedCounterChanged;
    }

    private void InstanceOnOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in visualSelectedObjects)
        {
            visualGameObject.SetActive(true);
        }

    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualSelectedObjects)
        {
            visualGameObject.SetActive(false);
        }
    }
}
