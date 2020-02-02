using UnityEngine;

public class UseBotBack : MonoBehaviour
{
    bool m_isAxisInUse = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            if (m_isAxisInUse == false)
            {
                // Call your event function here.
                m_isAxisInUse = true;
                CheckUseBot();
            }
        }
        if (Input.GetAxisRaw("Fire1") == 0)
        {
            m_isAxisInUse = false;
        }
    }

    void CheckUseBot()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 2f))
        {
            BotBackPanel botBackPanel = hitInfo.collider.GetComponent<BotBackPanel>();
            if (botBackPanel != null)
            {
                float dotProduct = Vector3.Dot(botBackPanel.transform.forward, transform.forward);
                if (dotProduct > 0.25) // If facing roughly same direction.
                {
                    Debug.Log("Hack da panel!");
                }
            }
        }
    }
}
