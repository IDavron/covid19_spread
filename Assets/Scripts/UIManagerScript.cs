using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript instance;
    private AgentScript agent;
    public GameObject canvas;
    [Header("Agent status panel")]
    [SerializeField] private Text name;
    [SerializeField] private Text infected;
    [SerializeField] private Text hunger;
    [SerializeField] private Text agentsAround;
    [SerializeField] private Text infectedAround;

    [Header("Global status panel")]
    [SerializeField] private Text g_day;
    [SerializeField] private Text g_agents;
    [SerializeField] private Text g_infected;
    [SerializeField] private Text g_cured;
    [SerializeField] private Text g_dead;

    public Button infectButton;
    public Button exitButton;

    public Slider timeSlider;
    public Text sliderText;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        infectButton.onClick.AddListener(infectButtonTask);
        exitButton.onClick.AddListener(exitButtonTask);
    }

    // Update is called once per frame
    void Update()
    {
        sliderText.text = Math.Round(timeSlider.value, 2) + "x";
        Time.timeScale = timeSlider.value;
    }

    public void setStatus(AgentScript agent)
    {
        this.agent = agent;
        if (agent)
        {
            name.text = "Name: " + agent.transform.parent.name;
            infected.text = "Infected: " + agent.infected;
            hunger.text = "Hunger: " + agent.hunger;
            agentsAround.text = "Agents arond: " + agent.agentsAround;
            infectedAround.text = "Infecteds around: " + agent.infectedAround;
        }
        showStatus();
    }

    public void hideStatus()
    {
        canvas.SetActive(false);
    }

    public void showStatus()
    {
        canvas.SetActive(true);
    }

    public void setGlobalData(int day, int agents, int infected, int cured, int dead)
    {
        g_day.text = "Day: " + day;
        g_agents.text = "Agents: " + agents;
        g_infected.text = "Infected: " + infected;
        g_cured.text = "Cured: " + cured;
        g_dead.text = "Died: " + dead;
    }

    void infectButtonTask()
    {
        print("pressed");
        if (agent != null)
        {
            agent.Infect();
        }
    }

    void exitButtonTask()
    {
        Application.Quit();
    }

}
