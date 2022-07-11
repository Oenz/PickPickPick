using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour, IInteract
{
    public void Interact(GameObject owner)
    {
        Use(owner);
        StartCoroutine(CancelTimer(owner));
    }

    protected abstract void Use(GameObject target);

    IEnumerator CancelTimer(GameObject target)
    {
        yield return new WaitForSeconds(3.0f);
        CancelUse(target);
    }

    protected virtual void CancelUse(GameObject target)
    {
        
    }
}
