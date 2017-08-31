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
    public class DoorState
    {
        DoorState(bool isOpen,
                   bool isClosing,
                   bool isOpening,
                   bool isClosed)
        {
            IsStopped = false;
            IsEmergency = false;

            IsOpen = isOpen;
            IsClosing = isClosing;
            IsOpening = isOpening;
            IsClosed = isClosed;
        }


        DoorState(bool isOpen,
                   bool isClosing,
                   bool isOpening,
                   bool isClosed,
                   bool isStopped)
        {
            IsStopped = isStopped;
            IsEmergency = false;

            IsOpen = isOpen;
            IsClosing = isClosing;
            IsOpening = isOpening;
            IsClosed = isClosed;
        }

        public bool IsOpen;
        public bool IsClosing;
        public bool IsOpening;
        public bool IsClosed;

        public bool IsStopped;
        public bool IsEmergency;

        public static DoorState Opening(bool isClosed)
        {
            return new DoorState(false, false, true, isClosed);
        }

        public static DoorState Open()
        {
            return new DoorState(true, false, false, false);
        }

        public static DoorState Closing(bool isOpen)
        {
            return new DoorState(isOpen, true, false, false);
        }

        public static DoorState Closed()
        {
            return new DoorState(false, false, false, true);
        }
    };
}

