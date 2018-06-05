using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionMaster {
    public enum Action { Tidy, Reset, EndTurn, EndGame };

    public int[] bowls = new int[21];
    public int bowl = 1;

    public static Action NextAction(List<int> pinFalls) {
        ActionMaster am = new ActionMaster();
        Action currentAction = new Action();

        foreach (int pinFall in pinFalls) {
            currentAction = am.Bowl(pinFall);
        }
        return currentAction;
    }

    public Action Bowl(int pins) {

        if (pins < 0 || pins > 10 ) {throw new UnityException("Invalid pins!");}

        bowls[bowl - 1] = pins;

        if (bowl == 21) {
            return Action.EndGame;
        }


        if (bowl >= 19 && Bowl21Awarded()) {
            bowl += 1;
            return Action.Reset;
        }else if(bowl == 20 && !Bowl21Awarded()) {
            return Action.EndGame;
        }else if(bowl == 20 && Bowl21Awarded()) {
            return Action.Tidy;
        }


    

        if (bowl % 2 != 0) {            //first bowl frames 
            if (pins == 10) {
                bowl += 2;
                return Action.EndTurn;
            } else {
                bowl += 1;
                return Action.Tidy;
            }
        }else if (bowl % 2 == 0) {      //second bowl frames 
            bowl += 1;
            return Action.EndTurn;
        }

        throw new UnityException("Not sure what to return!");
    }

    public bool Bowl21Awarded() {
        if (bowls[19-1] + bowls[20-1] >= 10) {
            return true;
        } else {
            return false;
        }
    }

    
}
