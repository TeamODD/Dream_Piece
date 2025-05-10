using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //public Player player;
    private int DreamPiece = 0;
    [SerializeField]
    private int DreamPiece1 = 10;
    [SerializeField]
    private int DreamPiece2 = 10;
    [SerializeField]
    private int DreamPiece3 = 10;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        DreamPiece = 0;
    }

    public void AddDreamPiece()
    {
        DreamPiece++;
        Debug.Log(DreamPiece);
    }

    public void StageClear()
    {
        if(SceneManager.GetActiveScene().name == "Stage1Scene" && DreamPiece >= DreamPiece1)
        {
            Debug.Log("Á¶°Ç¿Ï");
            SceneManager.LoadScene("Stage2Scene");
        }
        else if (SceneManager.GetActiveScene().name == "Stage2Scene" && DreamPiece == DreamPiece2)
        {
            SceneManager.LoadScene("Stage3Scene");
        }
        else if (SceneManager.GetActiveScene().name == "Stage3Scene" && DreamPiece == DreamPiece3)
        {
            SceneManager.LoadScene("ClearScene");
        }
        else
        {
            
            return;
        }
    }
}
