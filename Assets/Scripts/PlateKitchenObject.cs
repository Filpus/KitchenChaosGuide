using System;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArg> OnIngredientAdded;

    public class OnIngredientAddedEventArg : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSos;

    private List<KitchenObjectSO> kitchenObjectSOList;

    public void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSOList.Contains(kitchenObjectSO) || !validKitchenObjectSos.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArg
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }

    public List<KitchenObjectSO> getKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
