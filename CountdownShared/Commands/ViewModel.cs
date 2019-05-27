using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownShared.Commands
{
    interface ViewModel
    {
        bool CanUpdate { get; }

        void SaveChanged();
    }
}
