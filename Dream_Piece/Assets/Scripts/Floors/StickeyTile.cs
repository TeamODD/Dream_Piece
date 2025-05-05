using UnityEngine;

public class StickeyTile : MonoBehaviour
{
    //������ Ÿ��
    //���� Ÿ��
    //�����̴� Ÿ��
    //��
    //stickyTile���� �ʿ��� ��
    //�÷��̾ ������ ����ؾ� �ϴϱ� Trigger�� ��߰���)
    //�׸��� �ʿ��Ѱ� �ӷ°� ���� ������ ���̴� ���

    [SerializeField] public float ReduceSpeed = 1f;
    [SerializeField] public float ReduceJump = 4f;
    Player player;

    private void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Collision.gameObject.CompareTag("Player"))
        {
            Player player = Collision.gameObject.GetComponent<Player>();
          
            player.Speed -= ReduceSpeed;
           
        }
    }

    private void OnCollisionExit2D(Collision2D Collision)
    {

        if (Collision.gameObject.CompareTag("Player"))
        {
            Player player = Collision.gameObject.GetComponent<Player>();
            
            player.Speed += ReduceSpeed;
            
        }
        
    }
}
