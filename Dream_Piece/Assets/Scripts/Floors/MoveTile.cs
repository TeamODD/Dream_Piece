using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float Speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        // PingPong을 이용해 발판이 양쪽으로 반복 이동
        float time = Mathf.PingPong(Time.time * Speed, 1);
        transform.position = Vector2.Lerp(pointA.position, pointB.position, time);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);  // 플레이어를 발판의 자식으로 설정
        }
    }

    // 플레이어가 발판에서 내렸을 때 부모 초기화
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);  // 플레이어 부모 해제
        }
    }
}

