using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{

    /// <summary>
    /// 目標
    /// </summary>
    public static Transform target;

    [Header("塔旋轉部位")]
    public Transform towerRotation;
    [Header("閃爍")]
    public ParticleSystem psShine;
    [Header("血量"), Range(100, 500)]
    public float hp;
    [Header("血條")]
    public Image hpBar;
    [Header("結束畫面")]
    public GameObject final;
    [Header("數量")]
    public Text textCount;
    [Header("殭屍死亡音效")]
    public AudioClip soundZombie;
    [Header("塔受傷音效")]
    public AudioClip soundHit;

    /// <summary>
    /// 殭屍數量
    /// </summary>
    private int count;
    /// <summary>
    /// 血量最大值
    /// </summary>
    private float hpMax;
    /// <summary>
    /// 音效來源：喇叭
    /// </summary>
    private AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();      // 取得喇叭
        hpMax = hp;                             // 血量最大值 = 血量
    }

    private void Update()
    {
        Attack();
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        if (target && hp > 0)                       // 如果 有目標 並且 血量 > 0
        {
            Vector3 pos = target.position;          // 取得目標座標
            pos.y = towerRotation.position.y;       // 目標座標 Y 軸 等於本身 避免 歪掉
            psShine.Play();                         // 播放閃爍特效

            towerRotation.LookAt(pos);              // 看著目標物件

            count++;                                // 殭屍數量遞增

            aud.PlayOneShot(soundZombie);           // 播放殭屍死亡音效
        }
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收的傷害</param>
    public void Damage(float damage)
    {
        hp -= damage;                                       // 血量遞減
        hpBar.fillAmount = hp / hpMax;                      // 更新血條
        aud.PlayOneShot(soundHit);                          // 播放塔撞擊音效

        if (hp <= 0)                                        // 如果 血量 <= 0
        {
            final.SetActive(true);                          // 顯示結束畫面
            textCount.text = "擊殺殭屍數量：" + count;       // 更新殭屍數量
        }
    }

    /// <summary>
    /// 重新遊戲
    /// </summary>
    public void Replay()
    {
        SceneManager.LoadScene("Virforia_scene");
    }

    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void Quit()
    {
        UnityEngine.Application.Quit();
    }
}
