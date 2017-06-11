using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public int lastStandingCount = -1;
    public Text standingPinsDisplay;
    public float distanceToRaise = 35f;
    public GameObject pinSet;

    private ActionMaster actionMaster = new ActionMaster();
    private float lastChangeTime;
    private bool ballEnteredBox = false;
    private Ball ball;
    private Pin[] standingPins;
    private Animator animator;

	// Use this for initialization
	void Start () {
        ball = FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        standingPinsDisplay.text = CountStanding().ToString();
        if (ballEnteredBox)
        {
            CheckStanding();
        } 
	}

    void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.GetComponentInParent<Pin>() != null)
        {
            Destroy(collider.gameObject.transform.parent.gameObject);
        }
    }

    Pin[] GetStandingPins()
    {
        List<Pin> standingPins = new List<Pin>();
        Pin[] Pins = GameObject.FindObjectsOfType<Pin>();
        foreach (Pin pin in Pins)
        {
            if (pin.IsStanding())
            {
                standingPins.Add(pin);
            }
        }
        return standingPins.ToArray();
    }

    void CheckStanding ()
    {
        if (CountStanding() != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = CountStanding();
            return;
        }

        float settleTimeLimit = 3f;
        if (Time.time - lastChangeTime > settleTimeLimit) 
        {
            PinsHaveSettled();
                    }
    }

    void PinsHaveSettled ()
    {
        ballEnteredBox = false;
        ball.Reset();
        lastStandingCount = -1;
        standingPinsDisplay.color = Color.green;
        PlayAnimation();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Ball>())
        {
            ballEnteredBox = true;
            standingPinsDisplay.color = Color.red;
        }
    }

    int CountStanding()
    {

        return GetStandingPins().Length;
    }

    public void RaisePins ()
    {
        Debug.Log("raising pins");
        Pin[] pins = GetStandingPins();
        Debug.Log("pins to raise: " + pins);
        foreach (Pin pin in pins)
        {
            Debug.Log("raising pin: " + pin);

            FreezeObject(pin.gameObject);
            pin.transform.Translate(new Vector3(0,distanceToRaise,0));
        }
    }


    public void LowerPins ()
    {
        Pin[] pins = GetStandingPins();
        Debug.Log("pins to lower: " + pins);
        foreach (Pin pin in pins)
        {
            Debug.Log("lowering pin: " + pin);
            pin.transform.Translate(new Vector3(0, -distanceToRaise, 0));
            UnfreezeObject(pin.gameObject);
        }
    }

    public void RenewPins()
    {
        GameObject pins = GameObject.Find("Pins");
        if (pins != null) Destroy(pins);
        Instantiate(pinSet, new Vector3(0, 0, 1829), Quaternion.identity);
    }

    private void FreezeObject (GameObject myObject)
    {
        Rigidbody objectRigidbody = myObject.GetComponent<Rigidbody>();
        objectRigidbody.useGravity = false;
        objectRigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        objectRigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        objectRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
        objectRigidbody.constraints = RigidbodyConstraints.FreezePositionX;
        objectRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        objectRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    private void UnfreezeObject(GameObject myObject)
    {
        Rigidbody objectRigidbody = myObject.GetComponent<Rigidbody>();
        objectRigidbody.useGravity = true;
        objectRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationX;
        objectRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        objectRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
        objectRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
        objectRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
        objectRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
    }

    private void PlayAnimation()
    {
        ActionMaster.Action action = new ActionMaster.Action();
        action = actionMaster.Bowl(10 - CountStanding());
        if (action == ActionMaster.Action.Tidy)        { animator.SetTrigger("tidy_trigger");  }
        if (action == ActionMaster.Action.EndTurn)     { animator.SetTrigger("reset_trigger"); }
        if (action == ActionMaster.Action.Reset)       { animator.SetTrigger("reset_trigger"); }
    }
}
