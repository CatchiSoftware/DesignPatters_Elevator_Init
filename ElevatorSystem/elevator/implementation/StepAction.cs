using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.catchi.elevator.implementation
{

    /**
     * Created by schwartz on 5/1/17.
     */
    public  class StepAction
    {
        public StepAction(String _name, ExecutionAction action)
        {
            name = _name;
            m_action = action;
        }

        public delegate void ExecutionAction(int stepNumber);

        public String name;
        public ExecutionAction m_action;

        public void Execute(int stepNumber)
        {
            m_action(stepNumber);
        }

    }
}
