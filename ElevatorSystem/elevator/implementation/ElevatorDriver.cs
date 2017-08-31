using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using net.catchi.configuration;
// using net.catchi.elevator;
using net.catchi.states;


namespace net.catchi.elevator.implementation
{

    public class ElevatorDriver
    {
        public ElevatorDriver(ElevatorConfiguration _elevator,
                       BuildingConfiguration _building)
        {
            m_elevator = _elevator;
            building = _building;

            m_state = new ElevatorState(
                    0,
                    DoorState.Open(),
                    ElevatorMovement.Stopped(),
                    ElevatorMovement.Stopped());

            m_action = new ElevatorAction(ElevatorAction.Opening, 0);

            m_stepAction = new StepAction("ElevatorDriver Action",
                (stepNumber) =>
            proceedToNextState(stepNumber));
        }

        private ElevatorConfiguration m_elevator;
        private BuildingConfiguration building;


        public void registerGameClock(GameClock toRegister)
        {
            toRegister.registerStepAction(m_stepAction);
        }

        public ElevatorState getState()
        {
            return m_state;
        }

        public List<ElevatorRequest> getElevatorRequests()
        {
            return m_openRequests;
        }

        public void RegisterElevatorRequest(ElevatorRequest request)
        {
            m_openRequests.Add(request);
        }

        bool hasGoal()
        {
            return m_goals.Count > 0;
        }

        List<ElevatorRequest> getGoals()
        {
            return m_goals;
        }

        public void addGoal(ElevatorRequest request)
        {
            m_goals.Add(request);
        }

        void setGoals(List<ElevatorRequest> goals)
        {
            m_goals = goals;
        }

        // The 
        void proceedToNextState(int stepNumber)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Elevator: "
                        + m_elevator.name

                        + " At height: "
                        + m_state.height.ToString()
                        + " action: "
                        + m_action.actionName);

                if (m_action.actionName == ElevatorAction.Opening)
                {
                    m_state.door = DoorState.Open();
                    int floorNumber = m_state.height / building.heightPerFloor;

                    m_goals.RemoveAll((request) =>
                         request.floorNumber == floorNumber);

                    m_action.duration -= 1;
                    if (m_action.duration <= 0)
                    {
                        m_action = new ElevatorAction(ElevatorAction.Stopped, 2);
                    }
                    return;
                }
                if (m_action.actionName == ElevatorAction.Stopping)
                {
                    ElevatorState nextState = m_state;
                    nextState.movement = ElevatorMovement.Stopped();
                    nextState.height = m_action.terminalHeight;
                    m_state = nextState;
                    m_action = new ElevatorAction(ElevatorAction.Opening,
                            m_state.height);
                    return;
                }

                if (m_action.actionName == ElevatorAction.Move_Up)
                {
                    HandleMovementUp(stepNumber);
                    return;
                }

                if (m_action.actionName == ElevatorAction.Move_Down)
                {
                    HandleMovementUp(stepNumber);
                    return;
                }

                //Fallback: Act as if the elevator is stopped.
                ElevatorRequest toExecute = getNextGoal();
                if (toExecute != null)
                {
                    int targetHeight = toExecute.floorNumber * building.heightPerFloor;
                    if (targetHeight == m_state.height)
                    {
                        m_action = new ElevatorAction(ElevatorAction.Opening, targetHeight);
                    }
                    else if (targetHeight > m_state.height)
                    {
                        m_action = new ElevatorAction(ElevatorAction.Move_Up,
                                targetHeight);
                    }
                    else
                    {
                        m_action = new ElevatorAction(ElevatorAction.Move_Down,
                                targetHeight);
                    }
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.ToString());
            }

        }

        public void OpenDoor(int floorNumber)
        {
            m_action = new ElevatorAction(ElevatorAction.Opening,
                    floorNumber * building.heightPerFloor);
        }

        // Violates encapsulation:
        public void setState(ElevatorState state)
        {
            m_state = state;
        }


        private ElevatorRequest getNextGoal()
        {
            if (m_goals.Any())
                return m_goals.First();

            return null;
        }


        ElevatorState m_state;

        // Requests that should be serviced should this elevator
        // pass by the referenced floors.
        List<ElevatorRequest> m_openRequests = new List<ElevatorRequest>();

        // Ordered list of steps requests to be serviced 
        List<ElevatorRequest> m_goals = new List<ElevatorRequest>();

        void HandleMovementUp(int stepNumber)
        {
            if (!m_state.door.IsClosed)
            {
                m_state.door.IsClosed = true;
                return;
            }

            int nextHeight = m_state.height + m_elevator.distancePerStep;
            int maxHeight = building.maxFloor * building.heightPerFloor;
            if (m_action.terminalHeight > maxHeight)
                m_action.terminalHeight = maxHeight;

            if (nextHeight >= m_action.terminalHeight)
            {
                m_action = new ElevatorAction(ElevatorAction.Opening,
                        m_action.terminalHeight);

                m_state.height = m_action.terminalHeight;
                m_state.movement = ElevatorMovement.Stopped();
                return;
            }

            int nextFloor = 0;
            if (WillElevatorPassFloor(m_state.height,
                    m_state.movement,
                    m_elevator.distancePerStep,
                    building.heightPerFloor,
                    nextFloor))
            {
                foreach (ElevatorRequest request in m_openRequests)
                {
                    if ((request.floorNumber == nextFloor) &&
                            (request.requestedInsideElevator ||
                                    request.direction))
                    {
                        nextHeight = nextFloor * building.heightPerFloor;
                        m_action = new ElevatorAction(
                                ElevatorAction.Opening,
                                nextHeight);

                        m_state.height = nextHeight;
                        m_state.movement = ElevatorMovement.Stopped();

                        return;
                    }
                }
            }

            m_state.height = nextHeight;
        }

        void HandleMovementDown(int stepNumber)
        {
            if (!m_state.door.IsClosed)
            {
                m_state.door.IsClosed = true;
                return;
            }

            int nextHeight = m_state.height - m_elevator.distancePerStep;
            int minHeight = building.minFloor * building.heightPerFloor;
            if (m_action.terminalHeight < minHeight)
                m_action.terminalHeight = minHeight;

            if (nextHeight <= m_action.terminalHeight)
            {
                m_action = new ElevatorAction(ElevatorAction.Opening,
                        m_action.terminalHeight);

                m_state.height = m_action.terminalHeight;
                m_state.movement = ElevatorMovement.Stopped();
                return;
            }

            int nextFloor = 0;
            if (WillElevatorPassFloor(m_state.height,
                    m_state.movement,
                    m_elevator.distancePerStep,
                    building.heightPerFloor,
                    nextFloor))
            {
                foreach (ElevatorRequest request in m_openRequests)
                {
                    if ((request.floorNumber == nextFloor) &&
                            (request.requestedInsideElevator ||
                                    request.direction))
                    {
                        nextHeight = nextFloor * building.heightPerFloor;
                        m_action = new ElevatorAction(
                                ElevatorAction.Opening,
                                nextHeight);

                        m_state.height = nextHeight;
                        m_state.movement = ElevatorMovement.Stopped();

                        return;
                    }
                }
            }

            m_state.height = nextHeight;
        }

        public static bool
        WillElevatorPassFloor(int currentHeight,
                              ElevatorMovement direction,
                              int stepLength,
                              int heightPerFloor,
                              int nextFloorNum)
        {
            int nextHeight = (direction.isAscending) ?
                    currentHeight + stepLength :
                    currentHeight - stepLength;

            if (direction.isAscending)
            {
                if ((currentHeight < 0) && (nextHeight >= 0))
                {
                    nextFloorNum = 0;
                    return true;
                }

                if ((currentHeight / heightPerFloor) < (nextHeight / heightPerFloor))
                {
                    nextFloorNum = nextHeight / heightPerFloor;
                    return true;
                }

                return false;
            }

            if (direction.isDescending)
            {
                if ((nextHeight <= 0) && (currentHeight > 0))
                {
                    nextFloorNum = 0;
                    return true;
                }

                if ((nextHeight / heightPerFloor) < (currentHeight / heightPerFloor))
                {
                    nextFloorNum = nextHeight / heightPerFloor;
                    return true;
                }

                return false;
            }

            return false;
        }


        ElevatorAction m_action;
        StepAction m_stepAction;
        String m_registrationID;
    }
}
