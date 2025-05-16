using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CartoonController : MonoBehaviour
{
    [System.Serializable]
    public class AutoGroup
    {
        public int startIndex;
        public int count;
    }

    public List<GameObject> stepObjects;       // ��ü ���� ������Ʈ (���� �߿�)
    public List<AutoGroup> autoGroups;         // �ڵ� ��� ������
    public float autoDelay = 0.5f;

    private int currentIndex = 0;
    private bool isAutoPlaying = false;

    void Start()
    {
        foreach (var obj in stepObjects)
            obj.SetActive(false);

        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == "EndingScene")
        {
            SoundManager.Instance.PlayBGM("EndingBGM");
        }
    }

    void Update()
    {
        if (((Input.GetKeyDown(KeyCode.Return) || (Input.GetKeyDown(KeyCode.Space)))) && !isAutoPlaying)
        {
            SoundManager.Instance.PlaySFX("Book");
            if (currentIndex >= stepObjects.Count)
                return;

            // ���� �ε����� �ڵ� �׷� �������̸� �ڵ� ��� �ڷ�ƾ ����
            AutoGroup group = autoGroups.Find(g => g.startIndex == currentIndex);
            if (group != null)
            {
                StartCoroutine(AutoPlayRange(group.startIndex, group.count));
                return;
            }

            // �Ϲ� ���� ����
            stepObjects[currentIndex].SetActive(true);
            currentIndex++;
        }

        if ((!isAutoPlaying && currentIndex >= stepObjects.Count) || Input.GetKey(KeyCode.X))
        {
            SceneManager.LoadScene("StageSelect");
        }


    }

    IEnumerator AutoPlayRange(int start, int count)
    {
        isAutoPlaying = true;

        for (int i = 0; i < count && (start + i) < stepObjects.Count; i++)
        {
            stepObjects[start + i].SetActive(true);
            currentIndex++;
            yield return new WaitForSeconds(autoDelay);
        }

        isAutoPlaying = false;
    }

    public void StartScene()
    {
        SceneManager.LoadScene("MainScene");
    }

}
