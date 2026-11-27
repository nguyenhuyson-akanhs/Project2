using System.Collections.Generic;
using UnityEngine;

public class EnemisSpawner : MonoBehaviour
{
    [Header("AssetDrop")]
    public List<EnemisGeneralStatus> _ListEnemis = new();

    [Header("SpawnPosition")]
    public GameObject PointUpSide;
    public GameObject PointDownSide;

    public async Awaitable Spawn()
    {
        while (true)
        {
            foreach (var Enemis in _ListEnemis)
            {
                int roll = Random.Range(1, 1001);

                if (roll <= Enemis._SpawnRate)
                {
                    // Instantiate
                    GameObject obj = Instantiate(Enemis._Prefab,
                                                 Vector3.zero,
                                                 Quaternion.identity);

                    // Set parent
                    if(Random.Range(0, 2) == 0)
                    {
                        obj.transform.SetParent(PointUpSide.transform,false);
                    }
                    else
                    {
                        obj.transform.SetParent(PointDownSide.transform,false);

                        if (obj.TryGetComponent<Rigidbody2D>(out var rb)) rb.gravityScale *= -1;
                        obj.transform.localScale = new Vector3(1, -1, 1);
                    }

                    obj.transform.localPosition = Vector3.zero;
                }
                await Awaitable.WaitForSecondsAsync(0.5f);
            }
        }
    }

    private void Start()
    {
        _ = Spawn();
    }
}
/* tạo thêm enemis asset / bullet asset: spawn rate v.v
 * tạo list trong spawner, cho vào queue, dùng await spawn lần lượt, không dày quá
 * 
 * enemis/bullet cần:- prefab đển instance
 *                   - spawn rate(int 1 - 1000)
 *                   - damage
 */