using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    IEnumerator followCoroutine = null;
    IEnumerator statusCoroutine = null;

    public float speed = 20.0f;
    public float maxHeight = 50.0f;
    public float minHeight = 5.0f;

    Vector3 offset = new Vector3(0, 7, -4);
    private bool isFollowing = false;
    private float cameraHeight = 10.0f;

    //Double click detection
    private float clickTime = 0.0f;
    GameObject hitObject;
    Ray ray;
    RaycastHit hit;

    AgentScript agent = null;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && !isFollowing)
            {
                if((hit.transform.tag == "Agent" || hit.transform.tag == "InfectedAgent") && checkForDoubleClick(Time.realtimeSinceStartup, hit.transform.gameObject))
                {
                    followCoroutine = follow(hit.transform.gameObject);
                    StartCoroutine(followCoroutine);
                }

                if(hit.transform.tag == "Agent" || hit.transform.tag == "InfectedAgent")
                {
                    if (agent != null)
                    {
                        agent.dishighlight();
                    }
                    agent = hit.transform.GetComponentInChildren<AgentScript>();
                    agent.highlight();
                    if (statusCoroutine != null)StopCoroutine(statusCoroutine);
                    statusCoroutine = setStatus(agent);
                    StartCoroutine(statusCoroutine);
                }
                else
                {
                    //StopCoroutine(statusCoroutine);
                    //UIManagerScript.instance.hideStatus();
                }
            }
           
        }
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(unfollow());
        }
        moveCamera();     
    }

    bool checkForDoubleClick(float time, GameObject obj)
    {
        if(time - clickTime < 0.3f && obj == hitObject)
        {
            return true;
        }
        else
        {
            hitObject = obj;
            clickTime = time;
            return false;
        }
    }

    void moveCamera()
    {
        Vector3 pos = transform.position;
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        if (Input.mousePosition.x < 1)
        {
            pos.x -= speed * Time.deltaTime;
        }
        if (Input.mousePosition.x > Screen.width * 0.95)
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.mousePosition.y < 1)
        {
            pos.z -= speed * Time.deltaTime;
        }
        if (Input.mousePosition.y > Screen.height * 0.95)
        {
            pos.z += speed * Time.deltaTime;
        }

        if(zoom > 0 && transform.position.y > minHeight)
        {
            pos += transform.forward * speed * 10 * Time.deltaTime;
        }else  if(zoom < 0 && transform.position.y < maxHeight)
        {
            pos -= transform.forward * speed * 10 * Time.deltaTime;
        }
        if (!isFollowing)
        {
            transform.position = pos;
        }
    }

    IEnumerator follow(GameObject agent)
    {
        cameraHeight = transform.position.y;
        isFollowing = true;

        while (true)
        {
            Vector3 target = agent.transform.position + offset;
            float speedMultiplier = (transform.position - agent.transform.position).magnitude / 5;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            yield return null;
        }
    }

    IEnumerator setStatus(AgentScript agent)
    {
        while (true)
        {
            UIManagerScript.instance.setStatus(agent);
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator unfollow()
    {
        StopCoroutine(followCoroutine);
        transform.parent = null;
        Vector3 target = new Vector3(transform.position.x, cameraHeight, transform.position.z);
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            yield return null;
        }
        isFollowing = false;
    }
}
