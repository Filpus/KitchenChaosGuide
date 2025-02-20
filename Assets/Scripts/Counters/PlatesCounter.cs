using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateTaken;
    
    private float spawnPlateTimer;
    [SerializeField] private float spawnPlateTime = 4f;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    [SerializeField] private int platesSpawnedAmountMax = 4;
    private int platesSpawnedAmount;
    
    
    
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTime)
        {
            spawnPlateTimer = 0f;
            if (platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesSpawnedAmount > 0)
            {
                KitchenObject.spawnKitchenObject(plateKitchenObjectSO, player);
                platesSpawnedAmount--;
                OnPlateTaken?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
