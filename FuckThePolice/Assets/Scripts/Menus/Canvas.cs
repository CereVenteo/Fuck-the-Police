using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public Text time;
    public Text day;
    int time_valor;
    int hour;
    public int day_valor;

    public Text points;
    public Text next_star;
    int star_points;
    int points_valor;

    public Image star1;
    public Image star2;
    public Image star3;
    public Image star4;
    public Image star5;
    int stars;

    public Text civilians;
    int time_to_add_civilian;
    int time_to_add_civilian_min;
    int time_to_add_criminal;
    int civilians_helped;
    int civilian_win_condition;

    public GameObject win_condition;
    public Text win_text;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        time_valor = 45;
        hour = 05;
        day_valor = 1;
        InvokeRepeating("IncrementTime", 0.0f, 0.15f);
        star_points = 1000;
        points_valor = 0;
        stars = 0;
        time_to_add_civilian = 6;
        time_to_add_civilian_min = 10;
        civilians_helped = 0;
        time_to_add_criminal = 6;
        civilian_win_condition = 30;
    }

    // Update is called once per frame
    void Update()
    {
        Points();
        if(hour < 20 && hour >= 6)
        {
            if (time_to_add_civilian == hour && time_to_add_civilian_min == time_valor)
            {
                switch(day_valor)
                {
                    case 2:
                        if (time_to_add_civilian_min == 30)
                            this.gameObject.GetComponent<Game_Manager>().AddCivilian();
                        break;
                    case 3:
                        if (time_to_add_civilian_min == 20 || time_to_add_civilian_min == 40)
                            this.gameObject.GetComponent<Game_Manager>().AddCivilian();
                        break;
                    case 4:
                        if (time_to_add_civilian_min == 15 || time_to_add_civilian_min == 30 || time_to_add_civilian_min == 45)
                            this.gameObject.GetComponent<Game_Manager>().AddCivilian();
                        break;
                    case 5:
                        if (time_to_add_civilian_min == 10 || time_to_add_civilian_min == 20 || time_to_add_civilian_min == 30 || time_to_add_civilian_min == 40)
                            this.gameObject.GetComponent<Game_Manager>().AddCivilian();
                        break;
                    default:
                        break;
                }

                if (time_to_add_civilian_min == time_valor)
                    time_to_add_civilian_min = time_valor + 1;

                if (time_to_add_civilian_min == 59)
                {
                    this.gameObject.GetComponent<Game_Manager>().AddCivilian();
                    time_to_add_civilian = hour + 1;
                    time_to_add_civilian_min = 0;
                }
                
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

        if((day_valor == 6 && hour == 00) || stars >= 5 || Input.GetKey(KeyCode.L))
        {
            EndGame();
        }
        
    }

    public int GetStars()
    {
        return stars;
    }

    public void SetStars(int _stars)
    {
        stars = _stars;
        Stars();
    }

    public void CivilianHelped()
    {
        civilians_helped++;
        civilians.text = civilians_helped.ToString();
        SetPoints(80);
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
            star_points += 1000;
        }
        else if (points_valor < star_points - 1000)
        {
            SetStars(stars - 1);
            star_points -= 1000;
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
        win_condition.SetActive(true);
        if(stars < 5)
        {
            win_text.text = "YOU LOSE";
            int score_value = (stars * 1000) + (civilians_helped * 200) + ((5 - day_valor) * 5000) + ((24 - hour) * 120) + ((60 - time_valor) * 2) + (points_valor * 10);
            score.text = "Stars: " + stars.ToString() + " * 1000 \n" +
                "Civilians Helped: " + civilians_helped.ToString() + " * 200 \n" +
                "Restart Days: " + (5 - day_valor).ToString() + " * 5000 \n" +
                "Restart Time: " + (24 - hour).ToString() + ":" + (60 -time_valor).ToString() + " * 2 * min \n" + 
                "Money: " + points_valor.ToString() + "* 10 \n" + 
                "Total Score: " + score_value.ToString();
        }
        else
        {
            win_text.text = "YOU WIN";
            int score_value = (stars * 1000) + (civilians_helped * 200) + ((5 - day_valor) * 5000) + ((24 - hour) * 120) + ((60 - time_valor) * 2) + (points_valor * 10);
            score.text = "Stars: " + stars.ToString() + " * 1000 \n" +
                "Civilians Helped: " + civilians_helped.ToString() + " * 200 \n" +
                "Restart Days: " + (5 - day_valor).ToString() + " * 5000 \n" +
                "Restart Time: " + (24 - hour).ToString() + ":" + (60 - time_valor).ToString() + " * 2 * min \n" +
                "Money: " + points_valor.ToString() + "* 10 \n" +
                "Total Score: " + score_value.ToString();
        }
    }
}
