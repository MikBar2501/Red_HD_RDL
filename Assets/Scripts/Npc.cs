using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Interactable
{

    public TextAsset dialog;
    public string NpcName;


    public override void Interact()
    {
        
        if (DialoguesDisplayer.isOpen() && DialoguesDisplayer.OpenedBy() == NpcName)
            DialoguesDisplayer.Close();
        else
            DialoguesDisplayer.Display(dialog, NpcName, transform);
    }

    public override bool CanTalkWith()
    {
        return true;
    }


    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
