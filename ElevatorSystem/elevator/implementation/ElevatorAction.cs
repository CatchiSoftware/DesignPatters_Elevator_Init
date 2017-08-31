using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.catchi.elevator.implementation
{

    /**
     * Created by schwartz on 5/1/17.
     */
    public class ElevatorAction
    {
        public ElevatorAction(String _actionName,
                    int _terminalHeight)
        {
            actionName = _actionName;
            terminalHeight = _terminalHeight;
            duration = 1;
        }

        ElevatorAction(String _actionName,
                       int _terminalHeight,
                       int _duration)
        {
            actionName = _actionName;
            terminalHeight = _terminalHeight;
            duration = _duration;

        }

       public String actionName;
       public int terminalHeight;
       public int duration;

        public  const String  Move_Up = "Move_Up";
    public const String Move_Down = "Move_Down";
    public  const String Stopping = "Stopping";
    public  const String Stopped = "Stopped";
    public  const String Opening = "Opening";
};
}
