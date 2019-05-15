
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class InteractionHandler : MonoBehaviour
{
    public static InteractionHandler _object;
    public float Range = 5;
    public List<Interactable> interactables;

    void Awake()
    {
        _object = this;
        interactables = new List<Interactable>();
        GetComponent<SphereCollider>().radius = Range;
    }
    
    void Update()
    {
        if(Input.GetButtonDown("Interaction"))
        {
            if (interactables.Count == 0)
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
            if(choosen != -1)
            {
                interactables[choosen].Interact();
                if (interactables[choosen].CanGather())
                        GetComponent<Animator>().SetTrigger("Gather");
                if (interactables[choosen].CanTalkWith())
                    GetComponent<Animator>().SetTrigger("Contact");
            }
        }
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
