using System;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;



    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
    }

    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArg e)
    {
        //UpdateVisual();
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);

                
        }
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.getKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.GetComponentInChildren<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
            
        }
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);

                
        }
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.getKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
            
            //iconTransform.gameObject.SetActive(true);
        }
    }
}
