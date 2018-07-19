using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public int angle, charge;
    public Particle particle;
    private Particle clone;
    public Sprite g, p, n, f, gp, gn, gf, pf, nf, gpf, gnf;

    private System.Diagnostics.Stopwatch timer;
    private string grav, elec, flux, spriteSearcher;
    private float initialMillis;
    private Vector2 velocity;
    private List<Sprite> sprites;
    private List<ForceType> mReactantForces;
    private List<GameObject> mReactsTo;
    private List<Rigidbody2D> mActiveParticles;
    private Sprite[] spriteArray;
    private GameObject[] dragableF, staticF, dynamicF;
    private static readonly string[] stringChecker = { "g", "p", "n", "f", "gp", "gn", "gf", "pf", "nf", "gpf", "gnf" };
    private bool[] mBeamProperties;
    Vector2 origen;

    public float gravityConstant = 1;
    public float electricConstant = 1;
    public float fluxConstant = 1;

    // Use this for initialization
    void Start()
    {
        
        sprites = new List<Sprite>();
        origen = new Vector2(transform.position.x, transform.position.y);
            
        sprites.Add(g);
        sprites.Add(p);
        sprites.Add(n);
        sprites.Add(f);
        sprites.Add(gp);
        sprites.Add(gn);
        sprites.Add(gf);
        sprites.Add(pf);
        sprites.Add(nf);
        sprites.Add(gpf);
        sprites.Add(gnf);

        timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        initialMillis = timer.ElapsedMilliseconds;
        SetSprite();
        InvokeRepeating("Spawn", 0.002f, 0.015f);
    }

    private void OnEnable()
    {
        mBeamProperties = new bool[4];
        mReactsTo = new List<GameObject>();
        mActiveParticles = new List<Rigidbody2D>();
        GameController.OnForceUpdate += HandleOnForceUpdate;
    }

    private void OnDisable()
    {
        GameController.OnForceUpdate -= HandleOnForceUpdate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float force;

        foreach (GameObject i in mReactsTo)
        {
            float forceX = i.transform.position.x;
            float forceY = i.transform.position.y;

            foreach (Rigidbody2D particle in mActiveParticles)
            {
                Vector2 resultant = new Vector2(0, 0);
                float currentX = particle.position.x;
                float currentY = particle.position.y;

                Vector2 distance = new Vector2(currentX - forceX, currentY - forceX);

                force = (i.GetComponent<Properties>().size * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));

                if (distance.x > 0 && distance.y > 0)
                {
                    resultant.x -= force;
                    resultant.y -= force;
                }
                else if (distance.x > 0 && distance.y < 0)
                {
                    resultant.x -= force;
                    resultant.y += force;
                }

                else if (distance.x < 0 && distance.y > 0)
                {
                    resultant.x += force;
                    resultant.y -= force;
                }
                else
                {
                    resultant.x += force;
                    resultant.y += force;
                }

                particle.AddForce(resultant, ForceMode2D.Impulse);
            }
        }
    }


    private void Spawn()
    {
        clone = particle.GetPooledInstance<Particle>(transform);
        clone.transform.position = origen;

        velocity = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward) * Vector2.up;
        velocity.Normalize();

        clone.GetComponent<Rigidbody2D>().AddForce(velocity * 400f, ForceMode2D.Impulse);
    }

    public void SetProperites(bool reactGrav, bool reactElec, bool reactFlux, bool beamPositive)
    {
        mBeamProperties[0] = reactGrav;
        mBeamProperties[1] = reactElec;
        mBeamProperties[2] = reactFlux;
        mBeamProperties[3] = beamPositive;
    }


    public bool[] GetProperties()
    {
        return mBeamProperties;
    }

    private void HandleOnForceUpdate(GameObject targetForce, bool active)
    {
        bool legal = Legal(targetForce.GetComponent<Properties>().type);

        if (active && legal)
        {
            mReactsTo.Add(targetForce);
        }
        else if (!active && legal)
        {
            mReactsTo.Remove(targetForce);
        }
    }

    private bool Legal(ForceType type)
    {
        return type.Equals(ForceType.Graviton) == mBeamProperties[0] || type.Equals(ForceType.Electron) == mBeamProperties[1] || type.Equals(ForceType.Fluxion) == mBeamProperties[2] ? true : false;
    }

    public void SetSprite()
    {
        grav = mBeamProperties[0] ? "g" : "";

        if (mBeamProperties[1] && mBeamProperties[3])
        {
            elec = "p";
        }
        else if (mBeamProperties[1])
        {
            elec = "n";
        }
        else
        {
            elec = "";
        }

        flux = mBeamProperties[2] ? "f" : "";

        spriteSearcher = (grav + elec + flux);

        for (int i = 0; i < sprites.Count; i++)
        {
            if (stringChecker[i] == spriteSearcher)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[i];
            }
        }
    }

    public void ActiveParticles(GameObject gameObject, bool active)
    {
        if (active)
        {
            mActiveParticles.Add(gameObject.GetComponent<Rigidbody2D>());
        }
        else 
        {
            mActiveParticles.Remove(gameObject.GetComponent<Rigidbody2D>());
        }
    }
}