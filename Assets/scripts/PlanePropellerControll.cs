using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePropellerControll : MonoBehaviour
{
    public GameObject propeller;
    [Tooltip("engine power TIME accumulation for calculating propeller angular speed")]
    public float enginePowerCoeff;
    [Tooltip("engine power TIME accumulation for calculating propeller angular speed")]
    public float referenceTime=10f;
    private float angular_moment = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        propeller.transform.Rotate(Vector3.forward * angular_moment * 1500f * Time.deltaTime);
        propellerControl();
    }

    void propellerControl(){
        angular_moment = Mathf.Clamp01(enginePowerCoeff/referenceTime);
        enginePowerCoeff = Mathf.Clamp(enginePowerCoeff - 0.6f * enginePowerCoeff * Time.deltaTime,0,1f);
    }

    // void setAngularMoment(float setMoment){
    //     angular_moment = Mathf.Clamp01(setMoment);
    // }
}
