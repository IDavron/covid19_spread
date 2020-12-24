using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScript : MonoBehaviour
{
    public static CityScript instance;
    public UIManagerScript uiManager;
    int day;
    int agents;
    int infected;
    int cured = 0;
    int dead = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(City());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator City() {
        while (true)
        {
            day = (int)Time.time / 10;
            agents = GameObject.FindGameObjectsWithTag("Agent").Length + GameObject.FindGameObjectsWithTag("InfectedAgent").Length;
            infected = GameObject.FindGameObjectsWithTag("InfectedAgent").Length;

            uiManager.setGlobalData(day, agents, infected, cured, dead);
            yield return new WaitForSeconds(1);

        }
    }

    public void addCured()
    {
        cured++;
    }

    public void addDied()
    {
        dead++;
    }
}
