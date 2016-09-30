using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{

    public GameObject player;

    private Text ammoText;
    private Image HealthBar;
    private Image SpecialBar;
    private Image ExpBar;

    private float healthBarScaleMax;
    private float expBarScaleMax;
    private float specialBarScaleMax;


    // Use this for initialization
    void Start()
    {
<<<<<<< HEAD

=======
        ammoText = GetComponentInChildren<Text>();
        Image[] imagesHUD = GetComponentsInChildren<Image>();

        foreach(Image subImage in imagesHUD)
        {
            switch(subImage.name)
            {
                case "HealthBar":
                    this.HealthBar = subImage;
                    break;
                case "ExpBar":
                    this.ExpBar = subImage;
                    break;
                case "SpecialBar":
                    this.SpecialBar = subImage;
                    break;
                
            }
        }

        if (HealthBar != null)
            healthBarScaleMax = HealthBar.rectTransform.localScale.y;

        if (ExpBar != null)
            expBarScaleMax = ExpBar.rectTransform.localScale.y;

        if (SpecialBar != null)
            specialBarScaleMax = SpecialBar.rectTransform.localScale.y;
>>>>>>> refs/remotes/origin/master
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        playerHUD();
    }

    void playerHUD()
    {
        if (player == null && !player.tag.Contains("Player"))
            return;

        TextHUD();

        HealthHUD();

        ExpHUD();

        SpecialHUD();
    }

    void TextHUD()
    {
        ammoText.text = player.GetComponent<PlayerController>().getHUDString();
    }

    void HealthHUD()
    {
        HealthBar.rectTransform.localScale = new Vector3(
            HealthBar.rectTransform.localScale.x,
            player.GetComponent<PlayerController>().healthPercent() * this.healthBarScaleMax,
            HealthBar.rectTransform.localScale.z
            );
    }

    void ExpHUD()
    {
        ExpBar.rectTransform.localScale = new Vector3(
            ExpBar.rectTransform.localScale.x,
            player.GetComponent<PlayerController>().expPercent() * this.expBarScaleMax,
            ExpBar.rectTransform.localScale.z
            );

    }

    void SpecialHUD()
    {
        //SpecialBar.rectTransform.localScale = new Vector3(
        //    SpecialBar.rectTransform.localScale.x,
        //    player.GetComponent<PlayerController>().specialPercent() * this.specialBarScaleMax,
        //    SpecialBar.rectTransform.localScale.z
        //    );
    }

}