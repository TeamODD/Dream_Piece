using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isAutoPlaying)
        {
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
}
