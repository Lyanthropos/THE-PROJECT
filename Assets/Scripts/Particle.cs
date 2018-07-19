using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Particle : PooledObject
{
    private List<bool> mProperties;
    List<GameObject> mActiveForces;
    GameObject[] dragableF, staticF, dynamicF;
    float currentX, currentY;
    //Vector2 resultant;
    public new Rigidbody2D rigidbody;
    private List<ForceType> mForces;
    private Vector2 velocity;
    int negater = 1;
    GameObject g;

	private void Awake()
	{
        g = this.gameObject;
	}

	private void OnEnable()
	{
        GetComponentInParent<Beam>().ActiveParticles(g, true);
	}

	private void OnDisable()
    {
        GetComponentInParent<Beam>().ActiveParticles(g, false);
	}
	/*private Vector2 Gravity(List<float> xDistance, List<float> yDistance, List<float> mass, bool active) {
        float totalXForce = 0;
        float totalYForce = 0;
        float force;

        if (active)
        {
            for (int i = 0; i < xDistance.Count; i++)
            {
                Vector2 distance = new Vector2(currentX - xDistance[i], currentY - yDistance[i]);

                force = (mass[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));

                if (currentX - xDistance[i] > 0)
                {
                    totalXForce -= force;
                }
                else
                {
                    totalXForce += force;
                }

                if (currentY - yDistance[i] > 0)
                {
                    totalYForce -= force;
                }
                else
                {
                    totalYForce += force;
                }
            }
        }

        gravForce.x = totalXForce;
        gravForce.y = totalYForce;

        return gravForce;
    }
    
    private Vector2 Electrostatic(List<float> xDistance, List<float> yDistance, List<float> charge, bool active)
    {
        float totalXForce = 0;
        float totalYForce = 0;
        float force;

        if (active)
        {
            for (int i = 0; i < xDistance.Count; i++)
            {

                Vector2 distance = new Vector2(currentX - xDistance[i], currentY - yDistance[i]);
                force = (charge[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));

                if (mProperties[3])
                {
                    if (currentX - xDistance[i] > 0)
                    {
                        totalXForce += force;
                    }
                    else
                    {
                        totalXForce -= force;
                    }

                    if (currentY - yDistance[i] > 0)
                    {
                        totalYForce += force;
                    }
                    else
                    {
                        totalYForce -= force;
                    }
                }
                else
                {
                    if (currentX - xDistance[i] > 0)
                    {
                        totalXForce -= force;
                    }
                    else
                    {
                        totalXForce += force;
                    }

                    if (currentY - yDistance[i] > 0)
                    {
                        totalYForce -= force;
                    }
                    else
                    {
                        totalYForce += force;
                    }
                }
            }
        }

        elecForce.x = totalXForce;
        elecForce.y = totalYForce;

        return elecForce;
    }

    private Vector2 Flux(List<float> xDistance, List<float> yDistance, List<float> fluxcapacity, bool active)
    {
        float totalXForce = 0;
        float totalYForce = 0;
        float force;

        if (active)
        {
            for (int i = 0; i < xDistance.Count; i++)
            {

                Vector2 distance = new Vector2(currentX - xDistance[i], currentY - yDistance[i]);
                force = (fluxcapacity[i] * fluxConstant) / (Mathf.Pow(distance.magnitude, 2));

                if (currentX - xDistance[i] > 0)
                {
                    totalXForce += force;
                }
                else
                {
                    totalXForce -= force;
                }

                if (currentY - yDistance[i] > 0)
                {
                    totalYForce += force;
                }
                else
                {
                    totalYForce -= force;
                }
            }
        }

        fluxForce.x = totalXForce;
        fluxForce.y = totalYForce;

        return fluxForce;
    }*/

	void OnTriggerEnter2D(Collider2D col)
    {
        Vector2 newVelocity, currentVelocity;

        if (!col.CompareTag("Goal") && !col.CompareTag("Mirror"))
        {
            ReturnToPool();
        }
        else if (col.CompareTag("Mirror"))
        {
            currentVelocity = this.gameObject.GetComponent<Rigidbody2D>().velocity;
            int angle = angleFinder(col.transform.eulerAngles.z);

            switch (angle)
            {
                case 1:
                    newVelocity = new Vector2(-currentVelocity.x, currentVelocity.y);
                    break;
                case 2:
                    newVelocity = new Vector2(currentVelocity.x, -currentVelocity.y);
                    break;
                case 3:
                    newVelocity = new Vector2(currentVelocity.y * negater, -currentVelocity.x * negater);
                    break;
                default:
                    newVelocity = currentVelocity;
                    break;
            }

            gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity;
        }
    }

    private int angleFinder(float mirrorAngle)
    {
        if (mirrorAngle > 180)
        {
            negater = -1;
        }
        else
        {
            negater = 1;
        }

        if (Mathf.Abs(mirrorAngle) % 180 == 0)
        {
            return 1;
        }

        if (Mathf.Abs(mirrorAngle) % 90 == 0 || mirrorAngle == 0)
        {
            return 2;
        }

        if (Mathf.Approximately(Mathf.Abs(mirrorAngle), 45) || Mathf.Approximately(Mathf.Abs(mirrorAngle), 135) || Mathf.Approximately(Mathf.Abs(mirrorAngle), 225) || Mathf.Approximately(Mathf.Abs(mirrorAngle), 315))
        {
            return 3;
        }

        Debug.Log("Invalid mirror angle!");
        return 4;
    }
}
