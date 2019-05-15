using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Interactable
{
    public float RespawnTime = 180;
    public int shield = 0;
    bool canGather = true;

    public override void Interact()
    {
        canGather = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Invoke("Respawn", RespawnTime);
    }

    void Respawn()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        canGather = true;
    }

    public override bool CanInteract()
    {
        return canGather;
    }

    public override bool CanGather()
    {
        return canGather;
    }
}
