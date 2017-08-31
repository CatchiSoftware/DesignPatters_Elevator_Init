using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.catchi.states
{


    /**
     * Created by schwartz on 5/1/17.
     */
    public class TripStatus
    {
        public TripStatus(int _userID,
                                          int _sourceFloor,
                                          int _targetFloor)
        {
            userID = _userID;
            sourceFloor = _sourceFloor;
            targetFloor = _targetFloor;
            wasRegistered = false;
            registrationTime = 0;
            wasPickedUp = false;
            timeToPickup = 0;
            wasDroppedOff = false;
            timeToDropoff = 0;
        }

        public int userID;
        public int sourceFloor;
        public int targetFloor;
        public bool wasRegistered;
        public int registrationTime;
        public bool wasPickedUp;
        public int timeToPickup;
        public bool wasDroppedOff;
        public int timeToDropoff;

    }
}