using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUTTING = "Cut";
    
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounterOnOnCut ;
        
    }

    private void CuttingCounterOnOnCut(object sender, EventArgs e)
    {
        animator.SetTrigger(CUTTING);
    }
}
