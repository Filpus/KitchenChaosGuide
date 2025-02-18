using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    [SerializeField] private Transform counterTopPoint;
    public void Interact()
    {
        Debug.Log("Interact!");
        Transform kitchenObjectSOTransform = Instantiate(kitchenObjectSo.prefab, counterTopPoint);
        kitchenObjectSOTransform.localPosition = Vector3.zero;
        
        
    }
}
