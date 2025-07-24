using UnityEngine;

public class RandomSeed : NewMonoBehaviour
{
    [SerializeField] private int seed = 12345; // có thể để public hoặc set từ UI

    protected override void Awake()
    {
        seed = Random.Range(0, 10000000); // Tạo seed ngẫu nhiên từ 0 đến 9999999
        Random.InitState(seed); // Khởi tạo seed tại đầu chương trình
    }
}
