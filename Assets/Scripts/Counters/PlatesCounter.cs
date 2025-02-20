using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    private float spawnPlateTimer;
    [SerializeField] private float spawnPlateTime = 4f;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTime)
        {
            KitchenObject.spawnKitchenObject(plateKitchenObjectSO, this);
        }
    }
}
