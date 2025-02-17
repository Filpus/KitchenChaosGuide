using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    
    private float playerRadius = .7f;
    private float playerHeight = 2f; 
    private bool isWalking;


    private void Update()
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

    public bool IsWalking() {
        return isWalking;
    }

    private bool CanMove(Vector3 moveDir, float moveDistance)
    {
        return !Physics.CapsuleCast(transform.position, transform.position+ Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
    }
}