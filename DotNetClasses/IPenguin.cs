using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetClasses {
    public interface IPenguin {
        IHerd Herd { get; }
        string Fly { get; }
    }
}
