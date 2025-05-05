using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float Speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        // PingPong�� �̿��� ������ �������� �ݺ� �̵�
        float time = Mathf.PingPong(Time.time * Speed, 1);
        transform.position = Vector2.Lerp(pointA.position, pointB.position, time);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);  // �÷��̾ ������ �ڽ����� ����
        }
    }

    // �÷��̾ ���ǿ��� ������ �� �θ� �ʱ�ȭ
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);  // �÷��̾� �θ� ����
        }
    }
}

