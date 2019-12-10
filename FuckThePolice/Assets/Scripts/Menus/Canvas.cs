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
    public uint day_valor;

    public Text points;
    public Text next_star;
    int star_points;
    int points_valor;

    public Image star1;
    public Image star2;
    public Image star3;
    public Image star4;
    public Image star5;
    uint stars;

    public Text civilians;
    uint time_to_add_civilian;
    uint time_to_add_criminal;
    uint civilians_helped;
    uint civilian_win_condition = 30;

    // Start is called before the first frame update
    void Start()
    {
        time_valor = 45;
        hour = 05;
        day_valor = 1;
        InvokeRepeating("IncrementTime", 0.0f, 0.2f);
        star_points = 1000;
        points_valor = 0;
        stars = 0;
        time_to_add_civilian = 6;
        civilians_helped = 0;
        time_to_add_criminal = 6;
    }

    // Update is called once per frame
    void Update()
    {
        Points();
        if(hour < 22 && hour >= 6)
        {
            if (time_to_add_civilian + 1 == hour)
            {
                this.gameObject.GetComponent<Game_Manager>().AddCivilian();
                time_to_add_civilian = hour;
            }
        }

        if(hour == time_to_add_criminal)
        {
            switch (time_to_add_criminal)
            {
                case 6:
                    this.gameObject.GetComponent<Game_Manager>().Day();
                    time_to_add_criminal = 12;
                    break;
                case 12:
                    this.gameObject.GetComponent<Game_Manager>().AddCriminal(1);
                    time_to_add_criminal = 18;
                    break;
                case 18:
                    this.gameObject.GetComponent<Game_Manager>().AddCriminal(2);
                    time_to_add_criminal = 20;
                    break;
                case 20:
                    this.gameObject.GetComponent<Game_Manager>().Night();
                    time_to_add_civilian = 6;
                    time_to_add_criminal = 23;
                    break;
                case 23:
                    this.gameObject.GetComponent<Game_Manager>().AddCriminal(3);
                    time_to_add_criminal = 6;
                    break;
                default:
                    break;
            }
        }

        if((day_valor == 6 && hour == 00) || (stars >= 5 && civilians_helped >= civilian_win_condition))
        {
            EndGame();
        }
        
    }

    public uint GetStars()
    {
        return stars;
    }

    public void SetStars(uint _stars)
    {
        stars = _stars;
        Stars();
    }

    public void CivilianHelped()
    {
        civilians_helped++;
        civilians.text = civilians_helped.ToString() ;
        SetPoints(150);
    }

    public int GetPoints()
    {
        return points_valor;
    }

    public void SetPoints(int _points)
    {
        points_valor += _points;
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
        else
            time.text = "";
        time.text += hour.ToString() + ":";

        if (time_valor < 10)
            time.text += "0";
        time.text += time_valor.ToString();
    }

    void Points()
    {

        if (star_points - points_valor <= 0)
        {
            SetStars(stars + 1);
            star_points += star_points + (star_points / 2);
        }
        else if (points_valor < (star_points / 5) * 2)
        {
            
            if (points_valor > 1000)
            {
                SetStars(stars - 1);
                star_points -= (star_points / 5) * 3;
            }
            else
            {
                SetStars(0);
                star_points = 1000;
            }
                
        }
            
            
        next_star.text = (star_points - points_valor).ToString() + "€";
        points.text = points_valor.ToString() + "€";
    }

    void Stars()
    {

        switch (stars)
        {
            case 0:
                star1.sprite = Resources.Load<Sprite>("Sprites/star_black");
                break;
            case 1:
                star1.sprite = Resources.Load<Sprite>("Sprites/star");
                star2.sprite = Resources.Load<Sprite>("Sprites/star_black");
                break;
            case 2:
                star2.sprite = Resources.Load<Sprite>("Sprites/star");
                star3.sprite = Resources.Load<Sprite>("Sprites/star_black");
                break;
            case 3:
                star3.sprite = Resources.Load<Sprite>("Sprites/star");
                star4.sprite = Resources.Load<Sprite>("Sprites/star_black");
                break;
            case 4:
                star4.sprite = Resources.Load<Sprite>("Sprites/star");
                star5.sprite = Resources.Load<Sprite>("Sprites/star_black");
                break;
            case 5:
                star5.sprite = Resources.Load<Sprite>("Sprites/star");
                break;
            default:
                break;
        }
    }

    void EndGame()
    {
        if(stars < 5 || civilians_helped < civilian_win_condition)
        {
            //Lose
        }
        else
        {
            //Win
        }
    }
}
