using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.catchi.states
{

    public class ElevatorState
    {
        public ElevatorState(int _height,
                      DoorState _door,
                      ElevatorMovement _movement,
                      ElevatorMovement _overallDirection)
        {
            height = _height;
            door = _door;
            movement = _movement;
            overallDirection = _overallDirection;
            weight = 0;
        }

        public int height;
        public DoorState door;
        public ElevatorMovement movement;
        public ElevatorMovement overallDirection;
        public int weight;
    }
}
