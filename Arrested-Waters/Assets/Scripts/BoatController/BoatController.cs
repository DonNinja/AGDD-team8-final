using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speedKPH { get
        {
            return boat.velocity.magnitude * 18f / 5f;
        } }
    public float speedScalar;
    public float brakePower;
    public float anchorPower;
    public float steerSpeedMouse;
    public float steerSpeedKey;
    public float steerAdjustSpeed;
    public float maxSteeringAngle;
    public float boatWidth;
    public float weightTransfer;
    public float cgHeight;
    public float waterResist;
    public float airResist;
    public float eBrakeGripRatioFront;
    public float eBrakeGripRatioRear;
    public float speedSteerCorrection;
    public float speedTurningStability;
    public float lowSpeedWiggleCorr;

    public float cgCorr = 20;
    public float axleCorr = 2;

    private float steerAngle = 0;

    public float throttle;
    private float eBrake;
    private float headAngle;
    private float angularVel;
    private float trackWidth;
    private float steerFilter;
    private float frontGrip = 2.5f;
    private float rearGrip = 2.5f;
    private float cornerStiffFront = 5.0f;
    private float cornerStiffRear = 5.2f;

    public Vector2 vel;
    private Vector2 accel;
    private Vector2 localVel;
    private Vector2 localAccel;

    public Rigidbody2D boat;
    public Axle frontAxle;
    public Axle rearAxle;
    public Engine engine;
    public GameObject cg;
    public GameObject sail;
    public GameObject playerSeat;
    public GameObject boatFX;

    //controls
    public InputController InputController;

    private KeyCode key_throttle = KeyCode.W;
    private KeyCode key_brake = KeyCode.S;
    private KeyCode key_ebrake = KeyCode.Space;
    private KeyCode key_steerLeft = KeyCode.A;
    private KeyCode key_steerRight = KeyCode.D;

    private int mouse_throttle = 0;
    private int mouse_brake = 1;
    private int mouse_ebrake = 2;
    private bool useMouseSteering = false;

    public bool debug = false;

    public void updateControls()
    {
        key_throttle = InputController.key_throttle;
        key_brake = InputController.key_brake;
        key_ebrake = InputController.key_ebrake;
        key_steerLeft = InputController.key_steerLeft;
        key_steerRight = InputController.key_steerRight;

        mouse_throttle = InputController.mouse_throttle;
        mouse_brake = InputController.mouse_brake;
        mouse_ebrake = InputController.mouse_ebrake;
        useMouseSteering = InputController.useMouseSteering;
    }

    void Start()
    {
        boat = GetComponent<Rigidbody2D>();
        //frontAxle = GetComponent<Axle>();
        //rearAxle = GetComponent<Axle>();
        //engine = GetComponent<Engine>();
        //cg = GetComponent<GameObject>();

        boat.inertia = 855;    

        vel = Vector2.zero;
        frontAxle.cgDist = Vector2.Distance(cg.transform.position, frontAxle.axle.transform.position) * axleCorr;
        rearAxle.cgDist = Vector2.Distance(cg.transform.position, rearAxle.axle.transform.position) * axleCorr;
        boatWidth = frontAxle.cgDist + rearAxle.cgDist;

        frontAxle.Init(boat.mass, boatWidth, frontGrip);
        rearAxle.Init(boat.mass, boatWidth, rearGrip);
        trackWidth = Vector2.Distance(frontAxle.leftTire.transform.position, frontAxle.rightTire.transform.position);

        // Automatic transmission
        engine.UpdateAutomaticTransmission(boat);
    }

    //handle input update
    private void Update()
    {
        throttle = 0;
        eBrake = 0;

        if (useMouseSteering)
        {
            //handle mouse steering
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            // flatten z axis
            dir.z = 0;

            Vector3 neutralDir = transform.up;
            steerAngle = Mathf.Clamp(Vector3.SignedAngle(neutralDir, dir, Vector3.forward), -maxSteeringAngle, maxSteeringAngle);
            steerFilter = SmoothSteering(Mathf.Sign(steerAngle));
            steerAngle = Mathf.Deg2Rad * steerFilter * Mathf.Abs(steerAngle);
        }
        else
        {
            float steerInput = 0;
            if (Input.GetKey(key_steerLeft))
            {
                steerInput = 1;
            }
            else if (Input.GetKey(key_steerRight))
            {
                steerInput = -1;
            }

            steerFilter = SmoothSteering(steerInput);
            steerAngle = SpeedAdjustedSteering(steerFilter);
            steerAngle = Mathf.Clamp(steerAngle, -maxSteeringAngle * Mathf.Deg2Rad, maxSteeringAngle * Mathf.Deg2Rad);
        }

        if (Input.GetKey(key_throttle) || Input.GetMouseButton(mouse_throttle))
        {
            throttle = 1;
        }
        if (Input.GetKey(key_brake) || Input.GetMouseButton(mouse_brake))
        {
            throttle = -1;
        }
        if (Input.GetKey(key_ebrake) || Input.GetMouseButton(mouse_ebrake))
        {
            eBrake = 1;
        }
    }


    //handle physics update
    void FixedUpdate()
    {
        vel = boat.velocity;
        headAngle = (boat.rotation + 90) * Mathf.Deg2Rad;

        float sn = Mathf.Sin(headAngle);
        float cs = Mathf.Cos(headAngle);

        localVel.x = cs * vel.x + sn * vel.y;
        localVel.y = cs * vel.y - sn * vel.x;

        //weight transfer
        float transferX = weightTransfer * localAccel.x * cgHeight / boatWidth;
        float transferY = weightTransfer * localAccel.y * cgHeight / trackWidth * cgCorr;

        //weight per axle
        float weightFront = boat.mass * (frontAxle.weightRatio * -Physics2D.gravity.y - transferX);
        float weightRear = boat.mass * (frontAxle.weightRatio * -Physics2D.gravity.y + transferX);

        //weight per tire
        frontAxle.leftTire.activeWeight = weightFront - transferY;
        frontAxle.rightTire.activeWeight = weightFront + transferY;
        rearAxle.leftTire.activeWeight = weightRear - transferY;
        rearAxle.rightTire.activeWeight = weightRear + transferY;

        //calculate cg
        Vector3 pos = Vector2.zero;
        if (localAccel.magnitude > 1f)
        {
            float wfl = Mathf.Max(0, (frontAxle.leftTire.activeWeight - frontAxle.leftTire.restingWeight));
            float wfr = Mathf.Max(0, (frontAxle.rightTire.activeWeight - frontAxle.rightTire.restingWeight));
            float wrl = Mathf.Max(0, (rearAxle.leftTire.activeWeight - rearAxle.leftTire.restingWeight));
            float wrr = Mathf.Max(0, (rearAxle.rightTire.activeWeight - rearAxle.rightTire.restingWeight));
            

            pos = (frontAxle.leftTire.transform.localPosition) * wfl +
                (frontAxle.rightTire.transform.localPosition) * wfr +
                (rearAxle.leftTire.transform.localPosition) * wrl +
                (rearAxle.rightTire.transform.localPosition) * wrr;

            float weightTotal = wfl + wfr + wrl + wrr;

            if (weightTotal > 0)
            {
                pos /= weightTotal;
                pos.Normalize();
                pos.x = Mathf.Clamp(pos.x, -0.6f, 0.6f);
            }
            else
            {
                pos = Vector2.zero;
            }
        }
        
        cg.transform.localPosition = Vector2.Lerp(cg.transform.localPosition, pos, 0.1f);

        //velocity per tire
        frontAxle.leftTire.angularVel = frontAxle.cgDist * angularVel;
        frontAxle.rightTire.angularVel = frontAxle.cgDist * angularVel;
        rearAxle.leftTire.angularVel = -rearAxle.cgDist * angularVel;
        rearAxle.rightTire.angularVel = -rearAxle.cgDist * angularVel;

        //slip angle
        frontAxle.slipAngle = Mathf.Atan2(localVel.y + frontAxle.angularVel, Mathf.Abs(localVel.x)) - Mathf.Sign(localVel.x) * steerAngle;
        rearAxle.slipAngle = Mathf.Atan2(localVel.y + rearAxle.angularVel, Mathf.Abs(localVel.x));

        //throttle/brake
        float activeThrottle = (throttle * engine.GetTorque(boat)) * (engine.GearRatio * engine.EffectiveGearRatio);

        //torque per tire (rwd)
        rearAxle.leftTire.torque = activeThrottle / rearAxle.leftTire.radius;
        rearAxle.rightTire.torque = activeThrottle / rearAxle.rightTire.radius;

        //grip & friction per tire
        frontAxle.leftTire.grip = frontGrip * (1.0f - eBrake * (1.0f - eBrakeGripRatioFront));
        frontAxle.rightTire.grip = frontGrip * (1.0f - eBrake * (1.0f - eBrakeGripRatioFront));
        rearAxle.leftTire.grip = rearGrip * (1.0f - eBrake * (1.0f - eBrakeGripRatioRear));
        rearAxle.rightTire.grip = rearGrip * (1.0f - eBrake * (1.0f - eBrakeGripRatioRear));

        if(speedKPH > lowSpeedWiggleCorr)
        {
            frontAxle.leftTire.friction = Mathf.Clamp(-cornerStiffFront * frontAxle.slipAngle, -frontAxle.leftTire.grip, frontAxle.leftTire.grip) * frontAxle.leftTire.activeWeight;
            frontAxle.rightTire.friction = Mathf.Clamp(-cornerStiffFront * frontAxle.slipAngle, -frontAxle.rightTire.grip, frontAxle.rightTire.grip) * frontAxle.rightTire.activeWeight;
            rearAxle.leftTire.friction = Mathf.Clamp(-cornerStiffRear * rearAxle.slipAngle, -rearAxle.leftTire.grip, rearAxle.leftTire.grip) * rearAxle.leftTire.activeWeight;
            rearAxle.rightTire.friction = Mathf.Clamp(-cornerStiffRear * rearAxle.slipAngle, -rearAxle.rightTire.grip, rearAxle.rightTire.grip) * rearAxle.rightTire.activeWeight;
            boatFX.SetActive(true);
        }
        else
        {
            frontAxle.leftTire.friction = Mathf.Clamp(-cornerStiffFront * frontAxle.slipAngle, -frontAxle.leftTire.grip, frontAxle.leftTire.grip) * frontAxle.leftTire.activeWeight * 0.1f;
            frontAxle.rightTire.friction = Mathf.Clamp(-cornerStiffFront * frontAxle.slipAngle, -frontAxle.rightTire.grip, frontAxle.rightTire.grip) * frontAxle.rightTire.activeWeight * 0.1f;
            rearAxle.leftTire.friction = Mathf.Clamp(-cornerStiffRear * rearAxle.slipAngle, -rearAxle.leftTire.grip, rearAxle.leftTire.grip) * rearAxle.leftTire.activeWeight * 0.1f;
            rearAxle.rightTire.friction = Mathf.Clamp(-cornerStiffRear * rearAxle.slipAngle, -rearAxle.rightTire.grip, rearAxle.rightTire.grip) * rearAxle.rightTire.activeWeight * 0.1f;
            boatFX.SetActive(false);
        }

        //sum forces
        float tractionX = rearAxle.torque;
        float tractionY = 0;

        float dragForceX = -waterResist * localVel.x - airResist * localVel.x * Mathf.Abs(localVel.x);
        float dragForceY = -waterResist * localVel.y - airResist * localVel.y * Mathf.Abs(localVel.y);

        float totalForceX = dragForceX + tractionX;
        float totalForceY = dragForceY + tractionY + Mathf.Cos(steerAngle) * frontAxle.friction + rearAxle.friction;

        //engine braking
        if (throttle == 0)
        {
            vel = Vector2.Lerp(vel, Vector2.zero, 0.005f);
        }

        //adjust Y force so it levels out the car heading at high speeds
        if (boat.velocity.magnitude > 10)
        {
            totalForceY *= (boat.velocity.magnitude + 1) / (21f - speedTurningStability);
        }

        //acceleration
        localAccel.x = totalForceX / boat.mass;
        localAccel.y = totalForceY / boat.mass;

        accel.x = cs * localAccel.x - sn * localAccel.y;
        accel.y = sn * localAccel.x + cs * localAccel.y;

        //velocity
        vel.x += accel.x * Time.fixedDeltaTime;
        vel.y += accel.y * Time.fixedDeltaTime;

        //angular torque/accel
        float angularTorque = (frontAxle.friction * frontAxle.cgDist) - (rearAxle.friction * rearAxle.cgDist);
        var angularAccel = angularTorque / boat.inertia;
        angularVel += angularAccel * Time.fixedDeltaTime;

        // Simulation likes to calculate high angular velocity at very low speeds - adjust for this
        if (vel.magnitude < 1 && Mathf.Abs(steerAngle) < 0.05f)
        {
            angularVel = 0;
        }
        else if (speedKPH < 0.75f)
        {
            angularVel = 0;
        }

        // Car will drift away at low speeds
        if (boat.velocity.magnitude < 0.1f && activeThrottle == 0)
        {
            localAccel = Vector2.zero;
            vel = Vector2.zero;
            angularTorque = 0;
            angularVel = 0;
            accel = Vector2.zero;
            boat.angularVelocity = 0;
        }

        //update car
        headAngle += angularVel * Time.fixedDeltaTime;
        boat.velocity = vel * speedScalar;

        boat.MoveRotation(Mathf.Rad2Deg * headAngle - 90);
        frontAxle.leftTire.transform.localRotation = Quaternion.Euler(0, 0, steerAngle * Mathf.Rad2Deg);
        frontAxle.rightTire.transform.localRotation = Quaternion.Euler(0, 0, steerAngle * Mathf.Rad2Deg);
        sail.transform.localRotation = Quaternion.Euler(0, 0, steerAngle * Mathf.Rad2Deg + 30);
    }

    float SmoothSteering(float steerInput)
    {

        float steer = 0;

        if (Mathf.Abs(steerInput) > 0.001f)
        {
            if (useMouseSteering)
            {
                steer = Mathf.Clamp(steerFilter + steerInput * Time.deltaTime * steerSpeedMouse, -1.0f, 1.0f);
            }
            else
            {
                steer = Mathf.Clamp(steerFilter + steerInput * Time.deltaTime * steerSpeedKey, -1.0f, 1.0f);
            }
        }
        else
        {
            if (steerFilter > 0)
            {
                steer = Mathf.Max(steerFilter - Time.deltaTime * steerAdjustSpeed, 0);
            }
            else if (steerFilter < 0)
            {
                steer = Mathf.Min(steerFilter + Time.deltaTime * steerAdjustSpeed, 0);
            }
        }

        return steer;
    }

    float SpeedAdjustedSteering(float steerInput)
    {
        float activeVelocity = Mathf.Min(boat.velocity.magnitude, 250.0f);
        float steer = steerInput * (1.0f - (boat.velocity.magnitude / speedSteerCorrection));
        return steer;
    }


    void OnGUI()
    {
        if (debug)
        {
            GUI.Label(new Rect(5, 5, 300, 20), "Speed: " + speedKPH.ToString());
            GUI.Label(new Rect(5, 25, 300, 20), "RPM: " + engine.GetRPM(boat).ToString());
            GUI.Label(new Rect(5, 45, 300, 20), "Gear: " + (engine.CurrentGear + 1).ToString());
            GUI.Label(new Rect(5, 65, 300, 20), "LocalAcceleration: " + localAccel.ToString());
            GUI.Label(new Rect(5, 85, 300, 20), "Acceleration: " + accel.ToString());
            GUI.Label(new Rect(5, 105, 300, 20), "LocalVelocity: " + localVel.ToString());
            GUI.Label(new Rect(5, 125, 300, 20), "Velocity: " + vel.ToString());
            GUI.Label(new Rect(5, 145, 300, 20), "SteerAngle: " + steerAngle.ToString());
            GUI.Label(new Rect(5, 165, 300, 20), "Throttle: " + throttle.ToString());

            GUI.Label(new Rect(5, 205, 300, 20), "HeadingAngle: " + headAngle.ToString());
            GUI.Label(new Rect(5, 225, 300, 20), "AngularVelocity: " + angularVel.ToString());

            GUI.Label(new Rect(5, 245, 300, 20), "TireFL Weight: " + frontAxle.leftTire.activeWeight.ToString());
            GUI.Label(new Rect(5, 265, 300, 20), "TireFR Weight: " + frontAxle.rightTire.activeWeight.ToString());
            GUI.Label(new Rect(5, 285, 300, 20), "TireRL Weight: " + rearAxle.leftTire.activeWeight.ToString());
            GUI.Label(new Rect(5, 305, 300, 20), "TireRR Weight: " + rearAxle.rightTire.activeWeight.ToString());

            GUI.Label(new Rect(5, 325, 300, 20), "TireFL Friction: " + frontAxle.leftTire.friction.ToString());
            GUI.Label(new Rect(5, 345, 300, 20), "TireFR Friction: " + frontAxle.rightTire.friction.ToString());
            GUI.Label(new Rect(5, 365, 300, 20), "TireRL Friction: " + rearAxle.leftTire.friction.ToString());
            GUI.Label(new Rect(5, 385, 300, 20), "TireRR Friction: " + rearAxle.rightTire.friction.ToString());

            GUI.Label(new Rect(5, 405, 300, 20), "TireFL Grip: " + frontAxle.leftTire.grip.ToString());
            GUI.Label(new Rect(5, 425, 300, 20), "TireFR Grip: " + frontAxle.rightTire.grip.ToString());
            GUI.Label(new Rect(5, 445, 300, 20), "TireRL Grip: " + rearAxle.leftTire.grip.ToString());
            GUI.Label(new Rect(5, 465, 300, 20), "TireRR Grip: " + rearAxle.rightTire.grip.ToString());

            GUI.Label(new Rect(5, 485, 300, 20), "AxleF SlipAngle: " + frontAxle.slipAngle.ToString());
            GUI.Label(new Rect(5, 505, 300, 20), "AxleR SlipAngle: " + rearAxle.slipAngle.ToString());

            GUI.Label(new Rect(5, 525, 300, 20), "AxleF Torque: " + frontAxle.torque.ToString());
            GUI.Label(new Rect(5, 545, 300, 20), "AxleR Torque: " + rearAxle.torque.ToString());
        }
    }
}
