using UnityEngine;

public class FollowMouseCursor : MonoBehaviour
{
    public void Update()
    {
        transform.position = Input.mousePosition;
    }
}
