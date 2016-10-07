using UnityEngine;
using System.Collections;

public class MeleeScript : Player
{


    public MeleeScript(string newPlayerName, string newPlayerClass, Rigidbody2D newPlayerRB2D, bool newIsMelee, int newPlayerLevel, int newHealth, float newSpeed, int newExperience, Animator newAnim) : base(newPlayerName, newPlayerClass, newPlayerRB2D, newIsMelee, newPlayerLevel, newHealth, newSpeed, newExperience, newAnim)
    {

    }

    /// <summary>
    /// Start will override Player Start(), which only contains call to default constructor. LoadData() will eventually load from file
    /// but for now just sets values of this Player
    /// </summary>
    void Start()
    {
        DefaultSettings();
        loadData();
        Debug.Log("Name: " + playerName + " " + " Class: " + playerClass + " Level: " + playerLevel + " Health: " + health
        + " Melee: " + isMelee + " Speed: " + speed + "  Exp: " + experience + "\n" + "has been created");
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
        this.alive = true;
    }






}
