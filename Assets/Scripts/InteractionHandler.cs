﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class InteractionHandler : MonoBehaviour
{
    public Animator animator;
    public static InteractionHandler _object;
    public float Range = 5;
    public List<Interactable> interactables;
    Interactable closest;

    //inventory
    List<string> items;

    void Awake()
    {
        closest = null;
        items = new List<string>();
        _object = this;
        interactables = new List<Interactable>();
        GetComponent<SphereCollider>().radius = Range;
    }

    void FixedUpdate()
    {
        if (interactables.Count == 0 && closest == null)
            return;


        float min = Mathf.Infinity;
        int choosen = -1;

        float dist;
        for (int i = 0; i < interactables.Count; i++)
        {
            dist = Vector3.Distance(transform.position, interactables[i].transform.position);
            if (dist < min)
            {
                min = dist;
                choosen = i;
            }
        }
        if (choosen != -1)
        {
            if (interactables[choosen] != closest)
            {
                closest = interactables[choosen];
                UI._UI.SetXToJason(true);
            }
        }
        else if(closest != null)
        {
            closest = null;
            UI._UI.SetXToJason(false);
        }
        
        if(Input.GetButtonDown("Interaction") && closest != null)
        {
            interactables[choosen].Interact();
            if (interactables[choosen].CanGather())
            {
                animator.SetTrigger("Gather");
                GatherItem(interactables[choosen] as Consumable);
            }
            if (interactables[choosen].CanTalkWith())
                animator.SetTrigger("Contact");
        }
    }

    void GatherItem(Consumable consumable)
    {
        GetComponentInParent<PlayerRadiationSettings>().AddResistance(consumable.shield);
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable I = other.GetComponent<Interactable>();
        if (I != null && !interactables.Contains(I))
        {
            interactables.Add(I);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable I = other.GetComponent<Interactable>();
        if (I != null)
            interactables.Remove(I);
    }


}
