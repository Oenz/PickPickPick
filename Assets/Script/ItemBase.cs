using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour, IInteract
{
    public void Interact(GameObject owner)
    {
        Use(owner);
    }

    protected abstract void Use(GameObject target);
}
