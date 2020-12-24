using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    public NavMeshAgent agent;

    private float infection_radius = 10.0f;
    Vector3 dirToGo;
    float moveSpeed;
    Rigidbody m_AgentRb;

    //Status
    public bool infected = false;
    public double hunger = 100.0;
    public int agentsAround = 0;
    public int infectedAround = 0;

    public Material greenMaterial;
    public Material redMaterial;

    //Infection variables
    private double changeToGetInfected = 0.001;
    private double infectionTime = 0.0;
    private double cureTime = 30.0;
    private double chanceToDie = 0.00001;
    double chance = 0.0;
    // Start is called before the first frame update
    void Start()
    {
        dirToGo = new Vector3();
        moveSpeed = 1.0f;
        m_AgentRb = GetComponent<Rigidbody>();
        StartCoroutine(Live());
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {

    /*    dirToGo = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f, UnityEngine.Random.Range(-1.0f, 1.0f));
        m_AgentRb.AddForce(dirToGo * moveSpeed, ForceMode.VelocityChange);
        if (m_AgentRb.velocity.sqrMagnitude > 10) m_AgentRb.velocity *= 0.95f;*/
    }

    void Infect()
    {
        infected = true;
        gameObject.tag = "InfectedAgent";
        infectionTime = Time.time;
        GetComponent<Renderer>().material = redMaterial;
    }

    void Cure()
    {
        infected = false;
        gameObject.tag = "Agent";
        infectionTime = 0.0f;
        GetComponent<Renderer>().material = greenMaterial;
    }

    void Die()
    {
        Destroy(gameObject);
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
                    chance = Math.Round(UnityEngine.Random.Range(0.0f, 1.0f), 5);
                    if (chance <= chanceToDie + 0.0 && chance >= chanceToDie + 0.0)
                    {
                        Die();
                    }
                }
            }

            agentsAround = 0;
            infectedAround = 0;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, infection_radius);
            foreach (var collider in hitColliders)
            {
                if ((collider.tag == "Agent" || collider.tag == "InfectedAgent") && collider.gameObject.name != gameObject.name) agentsAround++;
                if (collider.tag == "InfectedAgent" && collider.gameObject.name != gameObject.name) infectedAround++;
            }
            hunger -= 1;
            if (infectedAround > 0)
            {
                chance = Math.Round(UnityEngine.Random.Range(0.0f, 1.0f), 3);
                print(chance);
                if (chance <= changeToGetInfected + 0.0 && chance >= changeToGetInfected + 0.0 && !infected)
                {
                    Infect();
                }
            }
            yield return new WaitForSeconds(1.0f);
        }   
    }

    IEnumerator Move()
    {
        Vector3 dest = new Vector3(UnityEngine.Random.Range(-150, 150), 1.5f, UnityEngine.Random.Range(-150, 150));
        agent.SetDestination(dest);
        while (true)
        { 
            if (Vector3.Distance(transform.position, dest) < 1.0f)
            {
                dest = new Vector3(UnityEngine.Random.Range(-50, 50), 1.5f, UnityEngine.Random.Range(-50, 50));
                agent.SetDestination(dest);
            }
            yield return null;
        }
        

    }

}
