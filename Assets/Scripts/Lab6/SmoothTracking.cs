using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SmoothTracking : MonoBehaviour

{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    public Transform Player;
    Vector3 Target;

    public float TrackingSpeed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            Vector3 currentPosition = Vector3.Lerp(transform.position, Target, TrackingSpeed * Time.deltaTime);
            transform.position = currentPosition;

            Target = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        }
    }
}
