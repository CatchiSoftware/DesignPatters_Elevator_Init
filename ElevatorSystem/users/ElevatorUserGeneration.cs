using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using net.catchi.elevator;

namespace net.catchi.users
{


    public class ElevatorUserGeneration
    {
        public ElevatorUserGeneration(int _divisor,
                               int _minFloor,
                               int _maxFloor,
                               ElevatorBank _bank)
        {
            divisor = _divisor;
            minFloor = _minFloor;
            maxFloor = _maxFloor;
            bank = _bank;

            users = new List<ElevatorUser>();
        }

        static int userNumber;
        int divisor;
        int minFloor;
        int maxFloor;
        ElevatorBank bank;
        List<ElevatorUser> users;
        private Random m_random = new Random();

        public void registerUser(int stepNumber)
        {
            if (stepNumber % divisor != 0)
                return;

            int delta = maxFloor - minFloor;
            int randomStart = m_random.Next(delta);
            int randomEnd = m_random.Next(delta);

            if (randomStart == randomEnd)
                return;

            System.Diagnostics.Debug.WriteLine("Creating a user moving from floor: "
                     + (minFloor + randomStart).ToString()
                     + " to floor: "
                     + (minFloor + randomEnd).ToString());


            ElevatorUser user = new ElevatorUser(userNumber++,
                    minFloor + randomStart,
                    minFloor + randomEnd);

            users.Add(user);
            user.registerWithBank(bank);
        }
    };

}