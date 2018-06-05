using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;
    public GameObject pinSet;

    private bool ballOutOfPlay = false;
    private int lastStandingCount = -1;
    private float lastChangeTime; 
    private float distanceToRaise;
    private int lastSettledCount = 10;

    private Pin pin;
    private Ball ball;
    private Animator animator;

    ActionMaster actionMaster = new ActionMaster(); // has to be here to make sure it only has a single instance

    void Start() {
        ball = GameObject.FindObjectOfType<Ball>();
        pin = GameObject.FindObjectOfType<Pin>();
        distanceToRaise = pin.distanceToRaise;
        //actionMaster = GameObject.FindObjectOfType<ActionMaster>();
        animator = GetComponent<Animator>();
    }

	void Update () {
        standingDisplay.text = CountStanding().ToString();

        if (ballOutOfPlay)
        {
            UpdateStandingCountAndSettle();
            standingDisplay.color = Color.red;
        }
	}

    public void SetBallOutOfPlay() {
        ballOutOfPlay = true;
    }

    public void RaisePins() {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            pin.RaiseIfStanding();
        }
    }

    public void LowerPins() {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            pin.Lower();
        }
    }

    public void RenewPins() {
        Instantiate(pinSet, new Vector3(0, distanceToRaise, 1829), Quaternion.identity);
    }

    void UpdateStandingCountAndSettle() {
        int currentStanding = CountStanding();

        if (currentStanding != lastStandingCount) {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
        }

        float timeDelay = 3f;

        if (timeDelay < (Time.time - lastChangeTime)) {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled() {
        CountFallenPins();
        StateActions();

        standingDisplay.color = Color.green;
        lastStandingCount = -1;
        ballOutOfPlay = false;
        ball.Reset();
    }

    public void StateActions() {
        ActionMaster.Action action = actionMaster.Bowl(CountFallenPins());
        Debug.Log(action);
        

        if (action == ActionMaster.Action.Tidy) {
            animator.SetTrigger("tidyTrigger");
        } else if (action == ActionMaster.Action.EndTurn) {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        } else if (action == ActionMaster.Action.Reset) {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        } else if (action == ActionMaster.Action.EndGame) {
            throw new UnityException("Don't know how to handle end-game yet");
        }
    }

    public int CountFallenPins() {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;
        return pinFall;
    }

    public int CountStanding() {
        int standing = 0;

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                standing += 1;
            }
        }

        return standing;
    }
}
