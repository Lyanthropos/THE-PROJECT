using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newParticle : MonoBehaviour {

	//all the sprites
	public Sprite gravSprite, elecSprite, fluxSprite, hGravSprite, hElecSprite, hFluxSprite, transparentSprite;

	public Transform Place;
	public Text numberText;
	public Image forceSprite;
	public int numAvailable = 1;
	public ForceType type;

	// Use this for initialization
	void Start () {
        
		numberText.text = numAvailable.ToString();
		setSprite ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Place.GetComponent<PlaceForce> ().activeForce == type){
			
			if (Place.GetComponent<PlaceForce> ().decrment) {
				
				numAvailable--;
				Place.GetComponent<PlaceForce> ().decrment = false;
				Place.GetComponent<PlaceForce> ().activeForce = ForceType.Empty;
			
			}
		}

		setSprite ();
        if (numAvailable > 0) {
            numberText.text = numAvailable.ToString();
        }
        else {
            numberText.text = "";
        }

    }

	public void Activate() {

		if (Place.GetComponent<PlaceForce> ().activeForce == type) {

			Place.GetComponent<PlaceForce> ().activeForce = ForceType.Empty;

		} 
		else if (numAvailable > 0) {
		
			Place.GetComponent<PlaceForce> ().activeForce = type;
		
		} 
		else {

			Place.GetComponent<PlaceForce> ().activeForce = ForceType.Empty;

		}
	}

	void setSprite (){

        if (numAvailable > 0) {
            if (Place.GetComponent<PlaceForce>().activeForce == type) {

                switch (type) {
                case ForceType.Graviton:
                    this.GetComponent<Image>().sprite = hGravSprite;
                    break;
                case ForceType.Fluxion:
                    this.GetComponent<Image>().sprite = hFluxSprite;
                    break;
                case ForceType.Electron:
                    this.GetComponent<Image>().sprite = hElecSprite;
                    break;
                }

            }
            else {

                switch (type) {
                case ForceType.Graviton:
                    this.GetComponent<Image>().sprite = gravSprite;
                    break;
                case ForceType.Fluxion:
                    this.GetComponent<Image>().sprite = fluxSprite;
                    break;
                case ForceType.Electron:
                    this.GetComponent<Image>().sprite = elecSprite;
                    break;
                }
            }
            this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else {
            this.GetComponent<Image>().color = new Color (0f, 0f, 0f, 0f);
        }
	}
}
