using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using net.catchi.configuration;
using net.catchi.elevator.implementation;
using net.catchi.states;

namespace net.catchi.elevator
{

    /**
     * Created by schwartz on 5/1/17.
     */
    public class ElevatorBank
    {
        public ElevatorBank(BuildingConfiguration _buildingConfiguration,
                     List<Elevator> _elevators,
                     GameClock _gameClock)
        {

            buildingConfiguration = _buildingConfiguration;
            elevators = _elevators;
            gameClock = _gameClock;

            foreach (Elevator elevator in elevators)
            {
                elevator.registerGameClock(gameClock);
            }
        }

        public BuildingConfiguration buildingConfiguration;
        public GameClock gameClock;
        public List<Elevator> elevators;

        public void registerPickupRequest(int floorNum, bool direction)
        {
            ElevatorRequest toRegister = new ElevatorRequest(floorNum, direction);
            int numElevators = elevators.Count;
            if (numElevators == 0)
                return;

            int offset = 0;
            if (numElevators > 1)
            {
                offset = m_random.Next(numElevators);
            }
            Elevator elevator = elevators[offset];
            elevator.registerRequest(toRegister);
            m_openRequests.Add(toRegister);

        }

        void tryShiftingOpenRequests(int unusedInt)
        { }

        private List<ElevatorRequest> m_openRequests = new List<ElevatorRequest>();

        private Random m_random = new Random();
    };

}