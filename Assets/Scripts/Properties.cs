﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ForceType
{
    Graviton,
    Fluxion,
    Electron
}

public class Properties : MonoBehaviour
{

    public ForceType type;
    public float size;
    public string movementType;
    bool click = false;

    // Use this for initialization
    void Start()
    {
        setSize(size);
        setSprite();

        switch (type)
        {
            case ForceType.Graviton:
                GameObject.Find("Beam").GetComponent<Beam>().setGravitonCount(GameObject.Find("Beam").GetComponent<Beam>().gravitonCount + 1);
                break;
            case ForceType.Electron:
                GameObject.Find("Beam").GetComponent<Beam>().setElectronCount(GameObject.Find("Beam").GetComponent<Beam>().electronCount + 1);
                break;
            case ForceType.Fluxion:
                GameObject.Find("Beam").GetComponent<Beam>().setFluxionCount(GameObject.Find("Beam").GetComponent<Beam>().fluxionCount + 1);
                break;
            default:
                Debug.Log("Invalid type! Type: " + type);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void setSize(float newSize)
    {
        GetComponent<Transform>().localScale = new Vector3(newSize, newSize, newSize);
    }

    public void setType(ForceType type)
    {
        this.type = type;

        switch(type) {
        case ForceType.Graviton:
            this.tag = "graviton";
            setSprite(); //TODO put actual sprite here
            break;
        case ForceType.Fluxion:
            this.tag = "fluxion";
            setSprite(); //TODO put actual sprite here
            break;
        case ForceType.Electron:
            this.tag = "electron";
            setSprite(); //TODO put actual sprite here
            break;
        }
    }

    public void setSprite()
    {

    }

}
