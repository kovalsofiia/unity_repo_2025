using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform Player;
    private NavMeshAgent Agent;

    // Вибирається в інспекторі: "Swamp", "Field", "All"
    public string enemyType = "Swamp";

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        // Встановлюємо доступну зону залежно від типу ворога
        switch (enemyType)
        {
            case "Swamp":
                Agent.areaMask = 1 << NavMesh.GetAreaFromName("Lake");
                break;
            case "Field":
                Agent.areaMask = 1 << NavMesh.GetAreaFromName("Field");
                break;
            case "All":
                Agent.areaMask = NavMesh.AllAreas;
                break;
        }
    }

    void Update()
    {
        Agent.destination = Player.position;
    }
}
