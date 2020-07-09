using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("生成敵人")]
    public GameObject enemy;
    [Header("生成點")]
    public Transform[] points;
    [Header("間隔時間")]
    public float time = 2.5f;
    [Header("每次降低多少間隔時間")]
    public float timeToSub = 0.05f;

    /// <summary>
    /// 生成
    /// </summary>
    public void Spawn()
    {
        int r = Random.Range(0, points.Length);         // 取得隨機生成點
        Instantiate(enemy, points[r]);                  // 生成敵人在生成點

        Invoke("Spawn", time);                          // 延遲呼叫生成

        time -= timeToSub;                              // 間隔時間減少
        time = Mathf.Clamp(time, 0.5f, 10f);            // 避免間隔時間低於 0.5
    }
    
}
