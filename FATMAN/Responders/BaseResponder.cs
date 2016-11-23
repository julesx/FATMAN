using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FATMAN.Interfaces;
using Discord;

namespace FATMAN.Responders
{
    public abstract class BaseResponder<TEventInfo> : IResponder where TEventInfo : class, ISnowflakeEntity
    {
        public Task RespondAsync(ISnowflakeEntity eventInfo)
        {
            return RespondAsync(eventInfo as TEventInfo);
        }

        public Task RespondAsync(ISnowflakeEntity beforeInfo, ISnowflakeEntity afterInfo)
        {
            return RespondAsync(beforeInfo as TEventInfo, afterInfo as TEventInfo);
        }

        public virtual Task RespondAsync(TEventInfo eventInfo)
        {
            throw new NotImplementedException();
        }

        public virtual Task RespondAsync(TEventInfo beforeInfo, TEventInfo afterInfo)
        {
            throw new NotImplementedException();
        }
    }
}
