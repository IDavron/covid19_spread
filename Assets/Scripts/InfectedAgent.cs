using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedAgent : MonoBehaviour
{
    public GameObject city;
    Rigidbody rb;
    Vector3 c1 = new Vector3(43, 1, 9);
    Vector3 c2 = new Vector3(43, 1, 39);
    Vector3 c3 = new Vector3(14, 1, 39);
    Vector3 c4 = new Vector3(14, 1, 9);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Move());
    }

    // Update is called once per frame
    IEnumerator Move()
    {
        while (true)
        {
            while (Vector3.Distance(transform.position, city.transform.position + c1) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, city.transform.position + c1, 2 * Time.deltaTime);
                yield return null;
            }
            while (Vector3.Distance(transform.position, city.transform.position + c2) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, city.transform.position + c2, 2 * Time.deltaTime);
                yield return null;
            }
            while (Vector3.Distance(transform.position, city.transform.position + c3) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, city.transform.position + c3, 2 * Time.deltaTime);
                yield return null;
            }
            while (Vector3.Distance(transform.position, city.transform.position + c4) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, city.transform.position + c4, 2 * Time.deltaTime);
                yield return null;
            }
            yield return null;
        }  
    }
}
