using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Renderer cubeRenderer;
    public float colorChangeSpeed = 1.0f;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * colorChangeSpeed, 1.0f);
        Color newColor = Color.Lerp(Color.blue, Color.red, t);
        cubeRenderer.material.color = newColor;
    }
}
