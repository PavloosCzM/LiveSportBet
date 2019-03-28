using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveSportBet.Events;
using LiveSportBet.Models;
using static LiveSportBet.Models.MatchModel;

namespace LiveSportBet.Models
{
    // This class holdign list of mathces and manipulate with it
    public class MatchManager
    {
        private List<MatchModel> matches;
        Random random = new Random();
        private static MatchManager instance;
        public event EventHandler<JsUpdateEventArgs> onChange;

        public static MatchManager GetInstance()
        {
            if (instance == null)
            {
                instance = new MatchManager();
            }
            return instance;
        }


        // Constructor is uset to fill the object,
        private MatchManager()
        {
            matches = new List<MatchModel>();
            string[] names = new string[] {
                "Rumunsko - Faerské Ostrovy",
                "Česko - Brazílie",
                "Plzeň - Olomouc (5.z)",
                "Djokovič Novak - Bautista Roberto",
                "Irsko - Gruzie",
                "Kvitová Petra - Barty Ashleigh",
                "Itálie - Lichtenštejnsko",
                "Medvedev Daniil - Federer Roger",
                "Anderson Kevin - Thompson Jordan",
                "Coric Borna - Kyrgios Nick",
                "Kvitová Petra - Barty Ashleigh",
                "Medvedev Daniil - Federer Roger",
                "Montreal - Florida",
                "Goffin David - Tiafoe Frances",
                "Washington - Carolina"
            };
            int id = 0;
            foreach (var name in names)
            {
                matches.Add(new MatchModel(
                    (float)Math.Round(random.NextDouble() + random.Next(5), 2),
                    (float)Math.Round(random.NextDouble() + random.Next(5), 2),
                    (float)Math.Round(random.NextDouble() + random.Next(5), 2),
                    name,
                    id
                    
                ));
                id++;
            }
        }

        // This return array of matches for first render to the page
        public MatchModel[] getMatches()
        {
            return matches.ToArray();
        }

        // This method is changing rate in match, we must specify match by match id. 
        // In rateId input we use Team1, Draw, Team2 or IDs 1, 2 ,3.
        public void changeMatch(int matchId, int rateId, float data)
        {
            foreach (var match in matches) {
                if (match.getId() == matchId)
                {
                    match.changeRate((Rate)rateId, data);
                    try
                    {
                        onChange(this, new JsUpdateEventArgs(matchId, rateId, data));
                    }
                    catch (Exception) { }
                    return;
                }
            }
            
        }

    }
}
