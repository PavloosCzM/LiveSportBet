using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveSportBet.Events
{
    // There we creating custom arguments for notifyEvent.
    public class JsUpdateEventArgs : EventArgs
    {
        public int matchId;
        public int rateId;
        public float data;


        public JsUpdateEventArgs(int matchId, int rateId, float data)
        {
            this.matchId = matchId;
            this.rateId = rateId;
            this.data = data;
        }
    }
}
