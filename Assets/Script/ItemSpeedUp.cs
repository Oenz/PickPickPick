using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeedUp : ItemBase
{
    [SerializeField] float _speedAmount = 0.5f;
    protected override void Use(GameObject target)
    {
        IPowerUp ipu = target.GetComponent<IPowerUp>();
        if (ipu != null)
        {
            ipu.SpeedUp(_speedAmount);
        }
    }
}
