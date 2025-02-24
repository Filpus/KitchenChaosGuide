using System;
using UnityEditor;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] protected Transform counterTopPoint;
    public static event EventHandler OnAnyObjectPlacedHear;

    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHear = null;
    }

    protected KitchenObject kitchenObject;

    public virtual void Interact(Player player){}
    public virtual void InteractAlternate(Player player){}
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnAnyObjectPlacedHear?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
