using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialsManager : MonoBehaviour {

    [SerializeField] GameObject[] specialPrfabs;
    [SerializeField] Transform specialSpawn;

    private Stack<GameObject> swordSpecial = new Stack<GameObject>();
    public Stack<GameObject> SwordSpecial {
        get { return swordSpecial; }
        set { swordSpecial = value; }
    }

    private Stack<GameObject> fistSpecial = new Stack<GameObject>();
    public Stack<GameObject> FistSpecial {
        get { return fistSpecial; }
        set { fistSpecial = value; }
    }
    
    private static SpecialsManager instance;
    public static SpecialsManager Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<SpecialsManager>();
            }
            return SpecialsManager.instance;
        }
    }

    void Start() {
        CreateSpecials(15);
    }
    
    void CreateSpecials(int amount) {
        for(int i = 0; i < amount; i++) {
            fistSpecial.Push(Instantiate(specialPrfabs[0]));
            swordSpecial.Push(Instantiate(specialPrfabs[1]));

            swordSpecial.Peek().name = "SwordSpecial";
            swordSpecial.Peek().SetActive(false);
            fistSpecial.Peek().name = "PunchSpecial";
            fistSpecial.Peek().SetActive(false);
        }
    }

    public void SpawnSpecial(int specialIndex, float addRot) {
        if(specialIndex == 0) {
            GameObject tmp = fistSpecial.Pop();
            tmp.SetActive(true);
            tmp.transform.position = specialSpawn.position;
            tmp.GetComponent<Special>().active = true;
            tmp.transform.localRotation = specialSpawn.rotation;
        }
        if (specialIndex == 1) {
            Vector3 localRot = specialSpawn.localRotation.eulerAngles;
            localRot.z = addRot;
            specialSpawn.localRotation = Quaternion.Euler(localRot);
            GameObject tmp = swordSpecial.Pop();
            tmp.SetActive(true);
            tmp.transform.position = specialSpawn.position;
            tmp.transform.localRotation = specialSpawn.rotation;
            tmp.GetComponent<Special>().active = true;
        }
    }
}