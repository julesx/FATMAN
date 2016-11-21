using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FATMAN.Interfaces;

namespace FATMAN.Responders
{
    public abstract class BaseResponder : IResponder
    {
        public abstract Task Respond<TEventInfo>(TEventInfo eventInfo);
    }
}
