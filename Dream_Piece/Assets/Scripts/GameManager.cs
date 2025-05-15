using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //public Player player;
    public int DreamPiece = 0;
    [SerializeField]
    private int DreamPiece1 = 10;
    [SerializeField]
    private int DreamPiece2 = 10;
    [SerializeField]
    private int DreamPiece3 = 10;

    public int clearStage;
    private bool hasCleared1 = false;
    private bool hasCleared2 = false;
    private bool hasCleared3 = false;

    //
    private PlayerAnima animaController;

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

    public IEnumerator StageClear()
    {
        // Find Animation Controll
        if (animaController == null)
        {
            Player player = Object.FindAnyObjectByType<Player>();
            if (player != null)
            {
                animaController = player.GetComponent<PlayerAnima>();
            }
        }
        //

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Stage1Scene" && DreamPiece >= DreamPiece1)
        {
            animaController.Play_St12_Clear();
            yield return new WaitForSeconds(2f);
            SaveClearStage(1);
        }
        else if (currentScene == "Stage2Scene" && DreamPiece >= DreamPiece2)
        {
            animaController.Play_St12_Clear();
            yield return new WaitForSeconds(2f);
            SaveClearStage(2);
        }
        else if (currentScene == "Stage3Scene" && DreamPiece >= DreamPiece3)
        {
            animaController.Play_St3_Clear();
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("EndingScene");
        }
    }

    public void StartStageClear()
    {
        StartCoroutine(StageClear());
    }

    void SaveClearStage(int stage)
    {
        int saved = PlayerPrefs.GetInt("ClearStage", 0);
        if (saved < stage)
        {
            PlayerPrefs.SetInt("ClearStage", stage);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene("StageSelect");
    }
}
