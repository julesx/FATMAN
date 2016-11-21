using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FATMAN.Interfaces;
using Discord;

namespace FATMAN.Responders
{
    public abstract class BaseResponder : IResponder
    {
        public Task Respond(ISnowflakeEntity eventInfo)
        {
            throw new NotImplementedException();
        }
    }
}
