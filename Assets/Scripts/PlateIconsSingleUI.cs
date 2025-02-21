using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSo)
    {
        image.sprite = kitchenObjectSo.sprite;
    }
}
