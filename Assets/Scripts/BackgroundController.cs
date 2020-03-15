using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float haw = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 yee = new Vector3(-1f, 0, 0);
            transform.localPosition += yee * haw * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 yee2 = new Vector3(1f, 0, 0);
            transform.localPosition += yee2 * haw * Time.deltaTime;
        }
    }
}
