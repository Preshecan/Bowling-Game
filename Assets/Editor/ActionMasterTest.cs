using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

[TestFixture]
public class ActionMasterTest {

    private List<int> pinFalls;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    //private ActionMaster actionMaster;

    [SetUp]
    public void Setup() {
        pinFalls = new List<int>();
        //actionMaster = new ActionMaster();
    }

    [Test]
    public void T00PassingTest() {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn() {
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
        //Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }
  
    [Test]
    public void T02Bowl8ReturnsTidy() {
    pinFalls.Add(8);
    Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T03Bowl28SpareReturnsEndTurn() {
        int[] rolls = {8, 2};
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    
    [Test]
     public void T04CheckResetAtStrikeInLastFrame() {
     int[] rolls = {1,1 ,1,1 ,1,1 ,1,1 ,1,1, 1,1, 1,1, 1,1, 1,1,10}; //19 bowls
      /* 
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
    */
        Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
     }
    /*

            [Test]
            public void T05CheckResetAtStrikeInFrame21() {
                int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1 }; //20 bowls

                foreach (int roll in rolls) {
                    actionMaster.Bowl(roll);
                }
                Assert.AreEqual(endGame, actionMaster.Bowl(10));
            }

            [Test]
            public void T06CheckEndGameAtBowl20() {
                int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1 }; //19 bowls

                foreach (int roll in rolls) {
                    actionMaster.Bowl(roll);
                }
                Assert.AreEqual(reset, actionMaster.Bowl(5));
            }

            */
}
