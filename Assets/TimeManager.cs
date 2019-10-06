using UnityEngine;

public class TimeManager : MonoBehaviour {
    
    [SerializeField] float bloodMeterDecrease = 2f;
    public bool inSlowMo = false;
    private static TimeManager instance;
    public static TimeManager Instance {
        get {
            if(instance == null) {
                instance = GameObject.FindObjectOfType<TimeManager>();
            }
            return TimeManager.instance;
        }
    }

    private void Update() {

        /*
         
        if (Input.GetKeyDown(KeyCode.Q)) {
            if(Time.timeScale == 1.0f && PlayerManager.bloodMeter != 0) {
                DoSlowmotion();
            } else if(Time.timeScale == 0.4f) {
                StopSlowMo();
            }
        }
        if (inSlowMo) {
            PlayerManager.bloodMeter -= Time.deltaTime * bloodMeterDecrease;
            if(PlayerManager.bloodMeter <= 0) {
                StopSlowMo();
                PlayerManager.bloodMeter = 0;
            }
        }

        */

    }

    void DoSlowmotion() {
        Debug.Log("SlowMo");
        Time.timeScale = 0.4f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        inSlowMo = true;
    }

    void StopSlowMo() {
        Debug.Log("NoSlowMo");
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        inSlowMo = false;
    }
}
