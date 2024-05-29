using UnityEngine;

public class RoundHandler : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoint;
    [SerializeField] GameObject playerUnitPrefab;
    [SerializeField] GameObject enemyUnitPrefab;


    private void Awake()
    {
        for (int i = 0; i < InventorySystem.singleton.units.Count; i++)
        {
            Instantiate(playerUnitPrefab, spawnPoint[0].position + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)), Quaternion.identity);
            if (InventorySystem.singleton.units.Count-1 == i)
            {

                if (Random.Range(0, 1) == 0)
                {
                    Instantiate(enemyUnitPrefab, spawnPoint[1].position + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)), Quaternion.identity);
                }    
            }
            else
            {
                Instantiate(enemyUnitPrefab, spawnPoint[1].position + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)), Quaternion.identity);
            }
        }
    }
}
