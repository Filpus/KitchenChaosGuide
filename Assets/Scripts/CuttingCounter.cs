using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;


    private int cuttingProgress;

    

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().getKitchenObjectSO()) )
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0;
                
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject()&&  HasRecipeWithInput(GetKitchenObject().getKitchenObjectSO()))
        {
            cuttingProgress++;

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().getKitchenObjectSO());
    
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {

                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(kitchenObject.getKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.spawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        if (cuttingRecipeSo != null) return cuttingRecipeSo.output;
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSo == null) return false;
        return true;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}
