using UnityEngine;
using System.Collections;

public class MeleeScript : Player
{
    public MeleeScript(string newPlayerName, string newPlayerClass, Rigidbody2D newPlayerRB2D, bool newIsMelee, int newPlayerLevel, int newHealth, float newSpeed, int newExperience, Animator newAnim) : base(newPlayerName, newPlayerClass, newPlayerRB2D, newIsMelee, newPlayerLevel, newHealth, newSpeed, newExperience, newAnim)
    {

    }

    public void loadData() {
        this.playerName = "RZA";
        this.playerClass = "RoboSamurai";
        this.PlayerRB2D = GetComponent<Rigidbody2D>();
        this.isMelee = true;
        this.playerLevel = 1;
        this.health = 40;
        this.speed = 100;
        this.experience = 0;
        this.anim = GetComponent<Animator>();
    }


}
