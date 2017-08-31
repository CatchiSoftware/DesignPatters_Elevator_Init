using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using net.catchi.elevator.implementation;
   

namespace net.catchi.elevator
{

    /**
     * Created by schwartz on 5/1/17.
     */
    public class GameClock
    {
        public GameClock(int numSteps)
        {
            m_numSteps = numSteps;
            m_currentStepNumber = 0;
        }

        public void run()
        {
            while (m_currentStepNumber < m_numSteps)
            { 
                System.Diagnostics.Debug.WriteLine("Executing step: " + m_currentStepNumber.ToString());

                List<StepAction> clonedActions = new List<StepAction>();
                clonedActions.AddRange(m_actions.Values);

                foreach (var action in clonedActions)
                {
                    try
                    {
                        action.Execute(m_currentStepNumber);
                    }
                    catch (Exception exc)
                    {
                        System.Diagnostics.Debug.WriteLine(
                                "Error executing operation: " +
                                        action.name +
                                        " with exception: " +
                                        exc.Message);
                    }
                }

                m_currentStepNumber += 1;
            }
        }

        public int currentStep()
        {
            return m_currentStepNumber;
        }

        public String registerStepAction(StepAction action)
        {
            String toReturn = Guid.NewGuid().ToString();
            m_actions.Add(toReturn, action);
            return toReturn;
        }

        public void unregisterStepAction(String uuid)
        {
            m_actions.Remove(uuid);
        }

        private int m_numSteps;
        private int m_currentStepNumber;
        private SortedDictionary<String, StepAction> m_actions = new SortedDictionary<string, StepAction>();

    };
}

