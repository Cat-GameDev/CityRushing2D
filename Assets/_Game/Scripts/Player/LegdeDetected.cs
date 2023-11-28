using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegdeDetected : MonoBehaviour
{
    bool isCanDetected = true;

    public bool IsCanDetected { get => isCanDetected;}
    [SerializeField] BoxCollider2D boxCd;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isCanDetected = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCd.bounds.center, boxCd.size, 0);

        foreach (var hit in colliders)
        {
            if (hit.gameObject.GetComponent<Platform>() != null)
                return;
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isCanDetected = true;
        }
    }
}
