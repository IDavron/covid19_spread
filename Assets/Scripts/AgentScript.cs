using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentScript : MonoBehaviour
{
    private float infection_radius = 10.0f;

    public Collider[] hitColliders;

    //Status
    public bool infected = false;
    public double hunger = 100.0;
    public int agentsAround = 0;
    public int infectedAround = 0;

    public Material greenMaterial;
    public Material redMaterial;

    //Infection variables
    private double changeToGetInfected = 0.01;
    private double infectionTime = 0.0;
    private double cureTime = 30.0;
    private double chanceToDie = 0.001;
    double chance = 0.0;


    //Sounds
/*    public AudioSource infectSound;
    public AudioSource deathSound;
    public AudioSource cureSound;*/

    public GameObject highlighter;
    MLScript foodCollectorAgent;

    // Start is called before the first frame update
    void Awake()
    {
        foodCollectorAgent = transform.parent.GetComponent<MLScript>();
    }
    void Start()
    {
        StartCoroutine(Live());
    }
    public void Infect()
    {
        if (!infected)
        {
            infected = true;
            transform.parent.tag = "InfectedAgent";
            infectionTime = Time.time;
            GetComponent<Renderer>().material = redMaterial;
            //infectSound.Play();
        }  
    }

    public void Cure()
    {
        infected = false;
        transform.parent.tag = "Agent";
        infectionTime = 0.0f;
        GetComponent<Renderer>().material = greenMaterial;
        CityScript.instance.addCured();
        //cureSound.Play();
    }

    void Die()
    {
        foodCollectorAgent.Punish();
        CityScript.instance.addDied();
        //deathSound.Play();
        //Destroy(gameObject);
    }

    IEnumerator Live()
    {
        while (true)
        {
            if (infected)
            {
                if (Time.time > infectionTime + cureTime)
                {
                    Cure();
                }
                else
                {
                    chance = Math.Round(UnityEngine.Random.Range(0.0f, 1.0f), 3);
                    if (chance <= chanceToDie + 0.008 && chance >= chanceToDie - 0.001)
                    {
                        Die();
                    }
                }
            }

            agentsAround = 0;
            infectedAround = 0;
            hitColliders = Physics.OverlapSphere(transform.position, infection_radius);
            foreach (var collider in hitColliders)
            {
                if ((collider.tag == "Agent" || collider.tag == "InfectedAgent")) agentsAround++;
                if (collider.tag == "InfectedAgent") infectedAround++;
            }
            if (infectedAround > 0)
            {
                chance = Math.Round(UnityEngine.Random.Range(0.0f, 1.0f), 3);
                //if (chance <= changeToGetInfected + 0.030 && chance >= changeToGetInfected - 0.030 && !infected)
                if(true)
                {
                    Die();
                }
            }
            yield return new WaitForSeconds(1.0f);
        }   
    }

    public void highlight()
    {
        highlighter.SetActive(true);
    }

    public void dishighlight()
    {
        highlighter.SetActive(false);
    }


}
