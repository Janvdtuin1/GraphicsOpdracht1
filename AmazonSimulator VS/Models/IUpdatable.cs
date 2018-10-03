using System;
using System.Collections.Generic;
using System.Linq;
using AmazonSimulator_VS;

namespace Models {
    interface IUpdatable
    {
        bool Update(int tick);
    }
}
