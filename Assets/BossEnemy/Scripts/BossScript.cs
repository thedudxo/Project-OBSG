﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class BossScript : MonoBehaviour
{

    public BossState bossState;
    Transform playerTarget;
    Vector3 direction;
    bool attacking = true;
    bool beam = false;
    bool stopCounter = true;
    public bool dead = false;
    float rangeRadius = 20;
    float distance;
    int attackFrame;
    int attackFrame_counter = 0;
    [SerializeField] List<string> longRangeAttacks = new List<string>();
    [SerializeField] List<string> midRangeAttacks = new List<string>();
    [SerializeField] int bossHealth;
    [Header("Energy Wave:")]
    [SerializeField] GameObject energyWavePrefab;
    [SerializeField] Transform energyWaveSpawn;
    [Header("Eye Beam:")]
    [SerializeField] EyeBeam beamScript;
    [SerializeField] Transform beamTrigger;
    [SerializeField] GameObject beamBurnPrefab;
    [SerializeField] VisualEffect beamChargeL;
    [SerializeField] VisualEffect beamChargeR;
    [Header("Entry:")]
    [SerializeField] ParticleSystem ground;
    [SerializeField] VisualEffect sand;

    private Stack<GameObject> energyWaveStack = new Stack<GameObject>();
    public Stack<GameObject> EnergyWaveStack {
        get { return energyWaveStack; }
        set { energyWaveStack = value; }
    }

    private Stack<GameObject> beamBurnStack = new Stack<GameObject>();
    public Stack<GameObject> BeamBurnStack {
        get { return beamBurnStack; }
        set { beamBurnStack = value; }
    }

    delegate void EveryFrame();
    EveryFrame everyFrame;
    delegate void AttackFrame();
    AttackFrame aFrame;

    private void Start() {
        bossState = BossState.longRange;
        ChangeState(BossState.longRange);
        SetPrefab();
        CreateEnergyWave(5);
        CreateBeamBurn(100);
    }

    public void SetPrefab() {
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
    }

    public void CreateEnergyWave(int amount) {
        for (int i = 0; i < amount; i++) {
            energyWaveStack.Push(Instantiate(energyWavePrefab));
            energyWaveStack.Peek().name = "Energy Wave";
            energyWaveStack.Peek().SetActive(false);
        }
    }

    void CreateBeamBurn(int amount) {
        for (int i = 0; i < amount; i++) {
            beamBurnStack.Push(Instantiate(beamBurnPrefab));
            beamBurnStack.Peek().name = "BeamBurn";
            beamBurnStack.Peek().SetActive(false);
        }
    }

    private void Update() {
        DistanceCheck(playerTarget);
        FindDirection(playerTarget);
        MonitorStates();
        RotateTowardsTarget();
        if (!stopCounter) {
            attackFrame_counter++;
            if (attackFrame_counter > attackFrame) {
                if (aFrame != null)
                    aFrame();
                attackFrame = Random.Range(200, 400);
                attackFrame_counter = 0;
            }
        }
    }

    void MonitorStates() {
        switch (bossState) {
            case BossState.midRange:
                if (distance > rangeRadius)
                    ChangeState(BossState.longRange);
                break;
            case BossState.longRange:
                if (distance < rangeRadius)
                    ChangeState(BossState.midRange);
                break;
            default:
                break;
        }
    }

    void ChangeState(BossState targetState) {
        bossState = targetState;
        everyFrame = null;
        aFrame = null;
        switch (targetState) {
            case BossState.midRange:
                aFrame = midRange;
                break;
            case BossState.longRange:
                aFrame = longRange;
                break;
            default:
                break;
        }
    }

    void midRange() {
        AttackTarget(midRangeAttacks);
    }

    void longRange() {
        AttackTarget(longRangeAttacks);
    }

    void DistanceCheck(Transform target) {
        distance = Vector3.Distance(transform.position, target.position);
    }

    void RotateTowardsTarget() {
        if (dead) { return; }
        direction.y = direction.y + 1;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        if (beam && attacking) {
            int layerMask = 1 << 12;
            //layerMask = ~layerMask;
            RaycastHit hit;
            if (Physics.Raycast(beamScript.transform.position, beamScript.transform.forward, out hit, Mathf.Infinity, layerMask)) {
                beamScript.UpdateLength(hit);
                var pos = hit.point;
                pos.y = pos.y + 0.1f;
                beamTrigger.position = pos;
                BeamBurn(hit.point);
            }
            beamScript.RotateTowardsPlayer();
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1.5f);
        } else if (!beam && !attacking) {
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
        } else if (!beam && attacking) { return; }
    }

    void FindDirection(Transform target) {
        direction = target.position - transform.position;
    }

    void AttackTarget(List<string> list) {
        stopCounter = true;
        int i = Random.Range(0, list.Count);
        string attack = list[i];
        GetComponent<Animator>().SetTrigger(attack);
    }

    public void DealDamage(int damage) {
        if (!dead) {
            bossHealth -= damage;
            CheckHealth();
        }
    }

    void BeamBurn(Vector3 hit) {
        GameObject tmp = beamBurnStack.Pop();
        tmp.GetComponent<BurnScript>().boss = gameObject;
        tmp.transform.position = hit;
        tmp.SetActive(true);
    }

    public void EnergyWave() {
        GameObject tmp = energyWaveStack.Pop();
        tmp.transform.position = energyWaveSpawn.position;
        tmp.SetActive(true);
        tmp.GetComponent<EnergyBall>().boss = GetComponent<BossScript>();
        tmp.GetComponent<EnergyBall>().ResetPrefab();
    }

    void CheckHealth() {
        if (bossHealth <= 0) {
            Dead();
            dead = true;
        }
    }

    void Dead() {
        GetComponent<Animator>().SetTrigger("Die");
        beamScript.gameObject.SetActive(false);
        stopCounter = true;
    }

    public void AttackBool(string attack) {
        attacking = true;
        if (attack != null) {
            beam = true;
            beamScript.gameObject.SetActive(true);
            beamTrigger.GetComponent<Collider>().enabled = true;
            beamTrigger.GetComponent<ParticleSystem>().Play();
        }
    }

    public void Beam() {
        beam = false;
        beamScript.transform.localRotation = beamScript.rot;
        beamScript.gameObject.SetActive(false);
        beamTrigger.GetComponent<Collider>().enabled = false;
        beamTrigger.GetComponent<ParticleSystem>().Stop();
    }

    public void resetBool() {
        attacking = false;
        stopCounter = false;
    }

    public void startVFX() {
        beamChargeL.SendEvent("Charge");
        beamChargeR.SendEvent("Charge");
    }

    public void StopVFX() {
        beamChargeL.SendEvent("StopCharge");
        beamChargeR.SendEvent("StopCharge");
    }

    public void Entry() {
        sand.SendEvent("sand");
        ground.Play();
    }

    public enum BossState {
        midRange, longRange
    }
}
