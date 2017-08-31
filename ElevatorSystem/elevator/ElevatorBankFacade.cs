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
    public class ElevatorBankFacade
    {
        public ElevatorBankFacade(ElevatorBank _bank)
        {
            elevatorBank = _bank;
        }


        public int getNumElevators()
        {
            return elevatorBank.elevators.Count;
        }

        public ElevatorState[] getElevatorStates()
        {
            ElevatorState[] toReturn = new ElevatorState[getNumElevators()];
            int index = 0;
            foreach (Elevator elevator in elevatorBank.elevators)
            {
                toReturn[index] = elevator.State();
                index += 1;
            }

            return toReturn;
        }

        public void registerPickupRequest(int floor, bool isAscending)
        {
            elevatorBank.registerPickupRequest(floor, isAscending);
        }

        void registerDropoffRequest(String agentName,
                                    String elevatorName,
                                    int destinationFloor)
        {
            foreach (Elevator elevator in elevatorBank.elevators)
            {
                if (elevator.buildingConfiguration.name == elevatorName)
                {
                    int currentHeight = elevator.State().height;
                    int desiredHeight = elevator.FloorHeight(destinationFloor);
                    elevator.registerRequest(
                            new ElevatorRequest(
                                    destinationFloor,
                                    desiredHeight >= currentHeight));
                }
            }
        }

        private ElevatorBank elevatorBank;
    };
}
