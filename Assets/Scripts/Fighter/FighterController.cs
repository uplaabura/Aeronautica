using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public float fighterSpeed = 5f;
    public float min_X, max_X, min_Y, max_Y;

    // Start is called before the first frame update
    void Start()
    {
        //fighterBullets = Instantiate(Resources.Load("Bullets Built", typeof(GameObject))) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        FighterMovement();
    }

    void FighterMovement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += Time.deltaTime * fighterSpeed;

            if (newPosition.x > max_X)
            {
                newPosition.x = max_X;
            }

            transform.position = newPosition;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            Vector3 newPosition = transform.position;
            newPosition.x -= Time.deltaTime * fighterSpeed;

            if (newPosition.x < min_X)
            {
                newPosition.x = min_X;
            }

            transform.position = newPosition;
        }

        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            Vector3 newPosition = transform.position;
            newPosition.y += Time.deltaTime * fighterSpeed;

            if (newPosition.y > max_Y)
            {
                newPosition.y = max_Y;
            }

            transform.position = newPosition;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Vector3 newPosition = transform.position;
            newPosition.y -= Time.deltaTime * fighterSpeed;

            if (newPosition.y < min_Y)
            {
                newPosition.y = min_Y;
            }

            transform.position = newPosition;
        }
    }
}
