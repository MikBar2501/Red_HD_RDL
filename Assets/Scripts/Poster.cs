﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poster : Interactable
{
    public Texture2D image;
    public string desc;

    public override void Interact()
    {
        if (image != null)
            UI._UI.ShowImage(image, desc);
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
