using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrap : ItemBase
{
    [SerializeField] float _powerAmount = 50.0f;
    protected override void Use(GameObject target)
    {
        target.GetComponent<Rigidbody>()?.AddForce(Vector3.up * _powerAmount, ForceMode.Impulse);
        
    }
}
