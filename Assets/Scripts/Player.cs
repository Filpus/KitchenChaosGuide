using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance{ get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private float playerRadius = .7f;
    private float playerHeight = 2f; 
    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Jest wiele instacji gracza!");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInputOnOnInteractAction;


    }
    private void GameInputOnOnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }
    private void Update()
    {
        HandleMovement();
        HandleInteractions();

    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;


        if (!CanMove(moveDir, moveDistance))
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            
            if (CanMove(moveDirX,moveDistance))
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                if (CanMove(moveDirZ, moveDistance))
                {
                    moveDir = moveDirZ;
                }
            }
            
        }
        
        if (CanMove(moveDir, moveDistance))
        {
            transform.position += moveDir * moveDistance;
        }
        

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }


    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
        
        Debug.Log(selectedCounter);

    }
    public bool IsWalking() {
        return isWalking;
    }
    
    private bool CanMove(Vector3 moveDir, float moveDistance)
    {
        return !Physics.CapsuleCast(transform.position, transform.position+ Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
    }

    private void SetSelectedCounter([CanBeNull] BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
                    
        OnSelectedCounterChanged?.Invoke(this,new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = this.selectedCounter
        });
    }


    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
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