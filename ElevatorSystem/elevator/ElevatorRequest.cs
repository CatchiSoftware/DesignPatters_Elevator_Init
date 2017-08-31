using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using net.catchi.states;

namespace net.catchi.elevator
{

    /**
     * Created by schwartz on 5/1/17.
     */
    public class ElevatorRequest
    {
        ElevatorRequest()
        {
        }

        public ElevatorRequest(int _floorNumber,
                        bool _direction)
        {
            floorNumber = _floorNumber;
            direction = _direction;
            requestedInsideElevator = false;

        }

        public ElevatorRequest(int _floorNumber,
                        bool _direction,
                        bool _requestedInsideElevator)
        {
            floorNumber = _floorNumber;
            direction = _direction;
            requestedInsideElevator = _requestedInsideElevator;

        }


        public int floorNumber;
        public bool direction;
        public bool requestedInsideElevator;

        bool compatibleDirection(ElevatorMovement elevatorMovement)
        {
            return elevatorMovement.isStopped ||
                    requestedInsideElevator ||
                    (elevatorMovement.isAscending == direction);
        }
    }
}