using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.catchi.states
{

    /**
     * Created by schwartz on 5/1/17.
     */
    public class ElevatorMovement
    {
        ElevatorMovement(
                 bool _isStopped,
                 bool _isAscending,
                 bool _isDescending)
        {
            isStopped = _isStopped;
            isAscending = _isAscending;
            isDescending = _isDescending;
            stoppingDistance = 0;
        }


        public  bool isStopped;
        public  bool isAscending;
        public  bool isDescending;
        public int stoppingDistance;

        public static ElevatorMovement Stopped()
        {
            return new ElevatorMovement(true, false, false);
        }

        public static ElevatorMovement Moving( bool upOrDown)
        {
            return new ElevatorMovement(false,
                    upOrDown,
                    !upOrDown);
        }
    }
}
