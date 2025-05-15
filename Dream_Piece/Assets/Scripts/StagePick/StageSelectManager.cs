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
        // �⺻ ��� ����
        stage2Btn.interactable = false;
        stage3Btn.interactable = false;

        // Ŭ���� ���� Ȯ��
        int clearStage = PlayerPrefs.GetInt("ClearStage", 0);

        // Ŭ���� ���¿� ���� ��� ����
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

        // �������� 1�� �׻� ��������
        chain1.SetActive(false);

        // ��ư Ŭ�� �� �� �̵�
        stage1Btn.onClick.AddListener(() => SceneManager.LoadScene("Stage1Scene"));
        stage2Btn.onClick.AddListener(() => SceneManager.LoadScene("Stage2Scene"));
        stage3Btn.onClick.AddListener(() => SceneManager.LoadScene("Stage3Scene"));
    }
}