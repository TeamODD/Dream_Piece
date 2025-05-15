using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public Button stage1Btn;
    public Button stage2Btn;
    public Button stage3Btn;

    public GameObject chain1;
    public GameObject chain2;
    public GameObject chain3;

    void Start()
    {
        // 기본 잠금 상태
        stage2Btn.interactable = false;
        stage3Btn.interactable = false;

        // 클리어 여부 확인
        int clearStage = PlayerPrefs.GetInt("ClearStage", 0);

        // 클리어 상태에 따라 잠금 해제
        if (clearStage >= 1)
        {
            stage2Btn.interactable = true;
            chain2.SetActive(false);
        }
        if (clearStage >= 2)
        {
            stage3Btn.interactable = true;
            chain3.SetActive(false);
        }

        // 스테이지 1은 항상 열려있음
        chain1.SetActive(false);

        // 버튼 클릭 시 씬 이동
        stage1Btn.onClick.AddListener(() => SceneManager.LoadScene("Stage1Scene"));
        stage2Btn.onClick.AddListener(() => SceneManager.LoadScene("Stage2Scene"));
        stage3Btn.onClick.AddListener(() => SceneManager.LoadScene("Stage3Scene"));
    }
}