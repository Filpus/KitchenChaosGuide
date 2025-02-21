using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSo;
        public GameObject gameObject;
    }

    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectsSOGameObjectList;
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectsSOGameObjectList) {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArg e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in kitchenObjectsSOGameObjectList)
        {
            if (kitchenObjectSoGameObject.kitchenObjectSo == e.kitchenObjectSO)
            {
                kitchenObjectSoGameObject.gameObject.SetActive(true);
            }
        }
        //e.kitchenObjectSO
    }
}
