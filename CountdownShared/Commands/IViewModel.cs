using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownShared.Commands
{
    public interface IViewModel
    {
        bool CanUpdate { get; }

        void SaveChanged();
    }
}
