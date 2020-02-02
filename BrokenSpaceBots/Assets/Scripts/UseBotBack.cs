using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBotBack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetAxis("Fire1") > 0 Input.Get)
            CheckUseBot();
    }

    void CheckUseBot()
    {
        /*
        Collider[] nearbyObjects = Physics.Raycast(transform.position, transform.forward, 2f);
        foreach (Collider collider in nearbyObjects)
        {
            BotBackPanel botBackPanel = collider.GetComponent<BotBackPanel>();
            if (botBackPanel != null)
            {
                float dotProduct = Vector3.Dot(botBackPanel.transform.forward, transform.forward);

                Debug.Log(botBackPanel.transform.forward + " " + transform.forward + " " + dotProduct);
                if (dotProduct > 0.25) // If facing roughly same direction.
                {
                    Debug.Log("Hack da panel!");
                    break;
                }
            }
        }
        */
    }
}
