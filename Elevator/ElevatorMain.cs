using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using net.catchi.configuration;
using net.catchi.states;
using net.catchi.users;
using net.catchi.elevator;
using net.catchi.elevator.implementation;

namespace net.catchi.elevatorMain
{


    /**
     * Created by schwartz on 5/1/17.
     */
    public class ElevatorMain
    {
        public static void Main(String[] args)
        {
            System.Diagnostics.Debug.WriteLine("Starting Up");
            GameClock clock = new GameClock(100);

            BuildingConfiguration buildingConfiguration = new BuildingConfiguration("Devonshire Place",
                -1,
                8,
                13);

            ElevatorConfiguration elevatorConfiguration = new ElevatorConfiguration("Elevator 1",
                4000);

            List<ElevatorConfiguration> elevatorConfigurations = new List<ElevatorConfiguration>();
            elevatorConfigurations.Add(elevatorConfiguration);


            ElevatorBankConfiguration bankConfiguration = new ElevatorBankConfiguration("Main Elevators",
                buildingConfiguration,
                elevatorConfigurations);

            Elevator firstElevator = new Elevator(elevatorConfiguration,
                buildingConfiguration);

            List<Elevator> elevators = new List<Elevator>();
            elevators.Add(firstElevator);

            ElevatorBank bank = new ElevatorBank(buildingConfiguration,
                elevators,
                clock);

            ElevatorUserGeneration userGenerator = new ElevatorUserGeneration(5,
                buildingConfiguration.minFloor,
                buildingConfiguration.maxFloor,
                bank);

            StepAction nextAction = new StepAction("SimpleAction",
    (int stepNumber) =>
            userGenerator.registerUser(stepNumber));


            clock.registerStepAction(nextAction);
            clock.run();

            System.Diagnostics.Debug.WriteLine("Successfully executed run.  Shutting down");
        }
    }
}
  
        