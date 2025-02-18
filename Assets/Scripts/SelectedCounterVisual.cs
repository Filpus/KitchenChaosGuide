using System;
using Unity.VisualScripting;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualSelectedCounter;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += InstanceOnOnSelectedCounterChanged;
    }

    private void InstanceOnOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
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
        visualSelectedCounter.SetActive(true);
    }

    private void Hide()
    {
        visualSelectedCounter.SetActive(false);
    }
}
