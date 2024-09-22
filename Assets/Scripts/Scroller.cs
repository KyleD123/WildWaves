using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float speed = 0.5f;
    private Vector3 startingPos;

    private bool keepScrolling = true;

    // Start is called before the first frame update
    void Start()
    {
        this.startingPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(keepScrolling)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if(transform.position.x < -10f)
            {
                transform.position = startingPos;
            }
        }
    }
}
