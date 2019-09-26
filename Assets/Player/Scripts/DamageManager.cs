using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour {

    [SerializeField]
    GameObject indicatorPrefab;
    [SerializeField]
    Transform parent;
    private Stack<GameObject> indicator = new Stack<GameObject>();
    public Stack<GameObject> Indicator {
        get { return indicator; }
        set { indicator = value; }
    }
    private static DamageManager instance;
    public static DamageManager Instance {
        get {
            if(instance == null) {
                instance = GameObject.FindObjectOfType<DamageManager>();
            }
            return DamageManager.instance;
        }
    }

    private void Start() {
        CreateIndicators(10);
        //for (int i = 0; i < 5; i++) { SpawnIndicator(); }
    }

    void CreateIndicators(int amount) {
        for (int i = 0; i < amount; i++) {
            indicator.Push(Instantiate(indicatorPrefab));
            indicator.Peek().transform.SetParent(parent);
            indicator.Peek().name = "HitIndicator";
            indicator.Peek().GetComponent<RectTransform>().localPosition = Vector3.zero;
            indicator.Peek().GetComponent<RectTransform>().localScale = new Vector3(2, 2, 0);
            indicator.Peek().SetActive(false);
        }
    }

    public void SpawnIndicator(Transform enemy) {
        if (indicator.Count == 0)
            CreateIndicators(10);
        GameObject tmp = indicator.Pop();
        tmp.SetActive(true);
        tmp.GetComponent<HitIndicator>().target = enemy;
        StartCoroutine(tmp.GetComponent<HitIndicator>().TransitionOut());
    }
}
