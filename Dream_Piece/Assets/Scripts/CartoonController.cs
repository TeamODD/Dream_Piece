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

    public List<GameObject> stepObjects;       // 전체 등장 오브젝트 (순서 중요)
    public List<AutoGroup> autoGroups;         // 자동 재생 구간들
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

            // 현재 인덱스가 자동 그룹 시작점이면 자동 재생 코루틴 실행
            AutoGroup group = autoGroups.Find(g => g.startIndex == currentIndex);
            if (group != null)
            {
                StartCoroutine(AutoPlayRange(group.startIndex, group.count));
                return;
            }

            // 일반 단일 등장
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
