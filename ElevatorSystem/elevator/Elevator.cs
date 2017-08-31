﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using net.catchi.configuration;
using net.catchi.states;
using net.catchi.elevator.implementation;

namespace net.catchi.elevator
{


    /**
     * Created by schwartz on 5/1/17.
     */
    public class Elevator
    {

        public Elevator(ElevatorConfiguration _elevatorConfiguration,
                        BuildingConfiguration _buildingConfiguration) :
            this(_elevatorConfiguration, _buildingConfiguration, new ElevatorStrategy())
        { 
        }


        protected Elevator(ElevatorConfiguration _elevatorConfiguration,
                 BuildingConfiguration _buildingConfiguration,
                 ElevatorStrategy _elevatorStrategy)
        {

            elevatorConfiguration = _elevatorConfiguration;
            buildingConfiguration = _buildingConfiguration;
            elevatorStrategy = _elevatorStrategy;
            m_driver = new ElevatorDriver(_elevatorConfiguration, _buildingConfiguration);
        }


        public ElevatorConfiguration elevatorConfiguration;
        public BuildingConfiguration buildingConfiguration;
        public ElevatorStrategy elevatorStrategy;

        public ElevatorState State()
        {
            return m_driver.getState();
        }

        // Convenience functions bootrapped off the configuration:
        public int maxFloor()
        {
            return buildingConfiguration.maxFloor;
        }

        public int minFloor()
        {
            return buildingConfiguration.minFloor;
        }

        public int heightPerFloor()
        {
            return buildingConfiguration.heightPerFloor;
        }

        // The primary entry point to influence the elevator:
        //   external buttons (on floors and on the elevator panel)
        //   call RegisterRequest to call the elevator, go to a floor, etc.
        public void registerRequest(ElevatorRequest request)
        {
            m_requests.Add(request);
            m_driver.addGoal(request);

        }

        public int FloorHeight(int floorNumber)
        {
            return heightPerFloor() * floorNumber;
        }

        public void OpenDoor(int desiredHeight)
        {
            m_driver.OpenDoor(desiredHeight / buildingConfiguration.heightPerFloor);
        }

        public void ProcessRequest(ElevatorRequest openRequest)
        {
            m_driver.addGoal(openRequest);
        }

        public void RequestShutdown()
        {
            m_shutdownState = true;
        }

        public void registerGameClock(GameClock gameClock)
        {
            m_driver.registerGameClock(gameClock);
        }

        // Storage for the requests that have been registered
        List<ElevatorRequest> m_requests = new List<ElevatorRequest>();

        ElevatorDriver m_driver;

        public void SetState(ElevatorState nextState)
        {
            m_driver.setState(nextState);
        }

        static bool m_shutdownState;
    }
}

