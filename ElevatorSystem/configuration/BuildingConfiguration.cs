using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.catchi.configuration
{

    /**
     * Created by schwartz on 5/1/17.
     */
    public class BuildingConfiguration
    {
        public BuildingConfiguration(String _name,
                              int _minFloor,
                              int _maxFloor,
                              int _heightPerFloor)
        {
            name = _name;
            minFloor = _minFloor;
            maxFloor = _maxFloor;
            heightPerFloor = _heightPerFloor;
        }

        public String name;
        public int minFloor;
        public int maxFloor;
        public int heightPerFloor;
    };
}