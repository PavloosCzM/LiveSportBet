using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveSportBet.Models
{

    // This class store data for match
    public class MatchModel
    {
        public enum Rate { Team1 = 1, Draw = 2, Team2 = 3 };
        private float team1Rate;
        private float drawRate;
        private float team2Rate;
        private string name;
        private int id;

        // This method cahnge rates bets 
        public void changeRate(Rate rate, float number)
        {
            switch (rate)
            {
                case Rate.Team1:
                    team1Rate = number;
                    break;
                case Rate.Draw:
                    drawRate = number;
                    break;
                case Rate.Team2:
                    team2Rate = number;
                    break;
            }
        }

        // Constructor for filling obbject with data
        public MatchModel(float team1Rate, float drawRate, float team2Rate, string name, int id)
        {
            this.team1Rate = team1Rate;
            this.drawRate = drawRate;
            this.team2Rate = team2Rate;
            this.name = name;
            this.id = id;
        }

        // This method returns an array of string that is needed to first render to the page
        public string[] GetData()
        {
            return new string[] {
                name,
                team1Rate.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture),
                drawRate.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture),
                team2Rate.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture),
                id.ToString()
            };
        }

        // Returns ID
        public int getId()
        {
            return id;
        }

    }
}
