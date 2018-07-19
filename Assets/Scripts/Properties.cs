using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum ForceType
{
    Graviton,
    Fluxion,
    Electron,
	Empty

}

public class Properties : MonoBehaviour
{

    public ForceType type;
    public float size;
	public Sprite gravSprite, elecSprite, fluxSprite;
    bool mStart;

    // Use this for initialization
    void Start()
    {
        setType(type);
        GameController.OnScreenForces(gameObject, true);
    }

	private void OnDestroy()
	{
        GameController.OnScreenForces(gameObject, false);
	}

	void setSize(float newSize)
    {
        GetComponent<Transform>().localScale = new Vector3(newSize, newSize, newSize);
    }

    public void setType(ForceType type)
    {
        this.type = type;

        switch (type)
        {
            case ForceType.Graviton:
                GetComponent<SpriteRenderer>().sprite = gravSprite;
                break;
            case ForceType.Fluxion:
                GetComponent<SpriteRenderer>().sprite = fluxSprite;
                break;
            case ForceType.Electron:
                GetComponent<SpriteRenderer>().sprite = elecSprite;
                break;
        }

    }

    public ForceType getType()
    {
        return this.type;
    }


}
