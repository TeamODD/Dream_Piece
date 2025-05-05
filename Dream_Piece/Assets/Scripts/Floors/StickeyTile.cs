using UnityEngine;

public class StickeyTile : MonoBehaviour
{
    //끈적이 타일
    //얼음 타일
    //움직이는 타일
    //벽
    //stickyTile에서 필요한 건
    //플레이어가 닿으면 기능해야 하니까 Trigger로 써야겠지)
    //그리고 필요한건 속력과 점프 강도를 줄이는 방식

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
