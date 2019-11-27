using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public Text time;
    public Text day;
    uint time_valor;
    uint hour;
    uint day_valor;

    public Text points;
    public Text next_star;
    uint star_points;
    uint points_valor;

    public Image star1;
    public Image star2;
    public Image star3;
    public Image star4;
    public Image star5;
    uint stars;

    // Start is called before the first frame update
    void Start()
    {
        time_valor = 00;
        hour = 00;
        day_valor = 1;
        InvokeRepeating("IncrementTime", 0.0f, 0.2f);
        star_points = 1000;
        points_valor = 0;
        stars = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Points())
            Stars();
    }

    void IncrementTime()
    {
        time_valor++;

        if (time_valor > 59)
        {
            time_valor = 00;
            hour += 1;
        }

        if (hour > 23)
        {
            day_valor += 1;
            hour = 00;
        }

        day.text = day_valor.ToString();

        if (hour < 10)
            time.text = "0";
        time.text += hour.ToString() + ":";

        if (time_valor < 10)
            time.text += "0";
        time.text += time_valor.ToString();
    }

    bool Points()
    {
        bool ret = false;

        if (star_points - points_valor == 0)
            return true;

        next_star.text = (star_points - points_valor).ToString();

        return ret;
    }

    void Stars()
    {
        stars++;
        star_points += star_points;

        switch (stars)
        {
            case 1:
                star1.sprite = Resources.Load<Sprite>("Sprites/star");
                break;
            case 2:
                star2.sprite = Resources.Load<Sprite>("Sprites/star");
                break;
            case 3:
                star3.sprite = Resources.Load<Sprite>("Sprites/star");
                break;
            case 4:
                star4.sprite = Resources.Load<Sprite>("Sprites/star");
                break;
            case 5:
                star5.sprite = Resources.Load<Sprite>("Sprites/star");
                break;
            default:
                break;
        }
            

    }
}
