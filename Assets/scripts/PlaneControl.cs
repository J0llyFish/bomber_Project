using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    public GameObject positioner;public GameObject y_axis_positioner;
    public Rigidbody plane_rigid;
    public PlanePropellerControll propellerControll;
    public float aero_resistance = 0.01f;
    [Tooltip("engine max power in horse power!")]
    public float max_power = 1600;
    public float power_ratio = 1;
    //public float angular_aero_reisitance_coeff = 0.25f;
    [Tooltip("air density in kg/m^3")]
    public float air_density = 1.225f;
    [Tooltip("wing area might be a fcn of angle ,we assume it to be constant here.(m^2)")]
    public float wing_area = 17 ; 
    [Tooltip("empirical number ,just for jun :)")]
    public float attack_angle_coeff = 20f;
    [Tooltip("Atmospheric pressure in pa")]
    public float pressure = 101325;
    [Tooltip("power of pressure in coanda effect calc")]
    public float pressure_power = 1f;
    public float foil_shape_efficiency_factor = 1f;
    [Tooltip("reference height(sea plane) of 1 atm")]
    public float reference_height = 0;
    public float distance_sum_debug = 0;
    void Awake(){
        plane_rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //start test
        //plane_data_update();
        //plane_rigid.velocity = plane_vector * 80f;
    }

    void Update()
    {
        plane_data_update();
        engine_control();
        engine_efficiency_control();
        aero_resistance_control();
        attack_angle_control();
        //Debug.Log("v:"+plane_rigid.velocity.magnitude+"x:"+plane_rigid.velocity.x.ToString("0.###")+"y:"+plane_rigid.velocity.y.ToString("0.###")+"z:"+plane_rigid.velocity.z.ToString("0.###"));
        if(plane_rigid.velocity.magnitude > 10000){
            Time.timeScale = 0;
        }
        //debug();
    }
    float ttimer = -1;
    void debug(){
        distance_sum_debug += plane_rigid.velocity.magnitude * Time.deltaTime;
        if(ttimer <= 0){
            ttimer = 0.1f;
            Debug.Log("v:"+plane_rigid.velocity.magnitude*3.6+"x:"+plane_rigid.velocity.x.ToString("0.###")+"y:"+plane_rigid.velocity.y.ToString("0.###")+"z:"+plane_rigid.velocity.z.ToString("0.###"));
            Debug.Log("dot:"+dot_product+"v.vector"+plane_rigid.velocity);
            Debug.Log("coanda_effect:"+Mathf.Sqrt(plane_rigid.velocity.magnitude) *foil_shape_efficiency_factor* Mathf.Clamp01(dot_product) * pressure * wing_area);
            Debug.Log("attack_angle:"+attack_angle_coeff* (1-dot_product)  *plane_rigid.velocity.magnitude* wing_area);
        }else{
            ttimer -= Time.deltaTime;
        }
    }

    public Vector3 plane_vector = new Vector3(0,0,0);public Vector3 wing_vector = new Vector3(0,0,0);
    float xDistance,yDistance,zDistance/*,distance,phi,thetaY,thetaX*/;float dot_product = 0;
    void plane_data_update(){
        xDistance = positioner.gameObject.transform.position.x - transform.position.x;
        yDistance = positioner.gameObject.transform.position.y - transform.position.y;
        zDistance = positioner.gameObject.transform.position.z - transform.position.z;
        plane_vector = new Vector3(xDistance,yDistance,zDistance).normalized;
        wing_vector = (y_axis_positioner.transform.position - transform.position).normalized;
        //Debug.Log(plane_vector.magnitude);
        dot_product = plane_rigid.velocity.normalized.x * plane_vector.x + plane_rigid.velocity.normalized.y * plane_vector.y + plane_rigid.velocity.normalized.z * plane_vector.z;
        //at 25C
        pressure = 101325f * Mathf.Clamp01(Mathf.Exp((float)(-0.000118575*transform.position.y+reference_height)));
    }
    void engine_control(){
        power_ratio = Mathf.Clamp01(power_ratio);
        plane_rigid.AddRelativeForce(Vector3.forward * max_power * 745.48f * power_ratio * pressure/101325f * Time.deltaTime);
        propellerControll.enginePowerCoeff += power_ratio * Time.deltaTime;
        
    }
    float calc_buffer;

    void aero_resistance_control(){
        //linear
        plane_rigid.velocity -= plane_rigid.velocity * aero_resistance * Time.deltaTime;
        //non linear (coanda effect)
        if(plane_rigid.velocity.magnitude >0.001f){
            calc_buffer = Mathf.Sqrt(plane_rigid.velocity.magnitude) *foil_shape_efficiency_factor* Mathf.Clamp01(dot_product) * Mathf.Pow(pressure,pressure_power) * wing_area * Time.deltaTime;
            if(calc_buffer > 0.0001){
                plane_rigid.AddRelativeForce(Vector3.up * calc_buffer);
            }
            //Debug.Log(plane_rigid.velocity.magnitude *foil_shape_efficiency_factor* Mathf.Clamp01(1-dot_product) * pressure * wing_area * Time.deltaTime);
        }
    }

    void engine_efficiency_control(){
        if(Input.GetKey(GameController.gameController.keyMap.plane_increase_power)){
            power_ratio += Time.deltaTime;
            power_ratio = Mathf.Clamp01(power_ratio);
        }
        if(Input.GetKey(GameController.gameController.keyMap.plane_decrease_power)){
            power_ratio -= Time.deltaTime;
            power_ratio = Mathf.Clamp01(power_ratio);
        }
    }

    void attack_angle_control(){
        if(plane_rigid.velocity.magnitude >0.0001f){
            //calc_buffer = attack_angle_coeff* (1-dot_product) * plane_rigid.velocity.magnitude* wing_area *Time.deltaTime;
            calc_buffer = (-plane_rigid.velocity.normalized.x)*wing_vector.x+(-plane_rigid.velocity.normalized.y)*wing_vector.y+(-plane_rigid.velocity.normalized.z)*wing_vector.z;
            //Debug.Log("y_dot_prodcut"+calc_buffer);
            if(calc_buffer > 0.0001){
                plane_rigid.AddRelativeForce(Vector3.up * calc_buffer * attack_angle_coeff *plane_rigid.velocity.magnitude* wing_area* (pressure/101325f)/*Mathf.Pow((pressure/101325f),1.33f)*/*Time.deltaTime);
            }
        }
    }

    
}
