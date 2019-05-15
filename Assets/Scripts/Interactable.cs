using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public int numer = -1;

    public abstract void Interact();

    public virtual bool CanGather()
    {
        return false;
    }

    public virtual bool CanTalkWith()
    {
        return false;
    }

    public virtual bool CanInteract()
    {
        return true;
    }
}
