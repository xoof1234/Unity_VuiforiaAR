using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("速度"), Range(0, 10)]
    public float speed = 1.5f;
    [Header("停止距離"), Range(0, 5)]
    public float stopDistance = 2;
    [Header("追蹤目標名稱")]
    public string targetName = "塔";
    [Header("爆炸效果")]
    public GameObject explosion;
    [Header("傷害"), Range(10, 100)]
    public float damage;

    /// <summary>
    /// 目標：塔
    /// </summary>
    private Transform target;
    /// <summary>
    /// 導覽網格代理器
    /// </summary>
    private NavMeshAgent nma;

    private void Start()
    {
        nma = GetComponent<NavMeshAgent>();                 // 取得導覽網格代理器
        nma.speed = speed;                                  // 設定代理器 速度
        nma.stoppingDistance = stopDistance;                // 設定代理器 停止距離

        target = GameObject.Find(targetName).transform;     // 透過名稱取得 塔
        nma.SetDestination(target.position);                // 設定目的地為 塔
    }

    /// <summary>
    /// 爆炸
    /// </summary>
    private void CreateEffect()
    {
        GameObject expl = Instantiate(explosion, transform.position, transform.rotation);   // 生成爆炸特效
        Destroy(expl, 0.5f);        // 延遲刪除爆炸特效
        Destroy(gameObject);        // 刪除敵人
    }

    /// <summary>
    /// 追蹤
    /// </summary>
    private void Track()
    {
        nma.SetDestination(target.position);                    // 追蹤 塔

        if (nma.remainingDistance <= stopDistance)              // 如果 剩餘距離 <= 停止距離
        {
            CreateEffect();                                     // 爆炸
            target.GetComponent<Tower>().Damage(damage);        // 對塔造成傷害
        }
    }

    /// <summary>
    /// 點擊並死亡
    /// </summary>
    private void ClickAndDead()
    {
        CreateEffect();
        Tower.target = transform;           // 塔 目標 = 本身
    }

    private void OnMouseDown()
    {
        ClickAndDead();
    }

    private void Update()
    {
        Track();
    }
}
