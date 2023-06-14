using UnityEngine;
public class CylindrSpawner : MonoBehaviour
{
    public void Initialize(GameObject enemyPrefab, Vector3 position, Color color)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = position;
        enemy.GetComponent<MeshRenderer>().material.color = color;
    }
}
