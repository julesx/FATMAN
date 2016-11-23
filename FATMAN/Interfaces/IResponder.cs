using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace FATMAN.Interfaces
{
    public interface IResponder
    {
        Task RespondAsync(ISnowflakeEntity eventInfo);
        Task RespondAsync(ISnowflakeEntity beforeInfo, ISnowflakeEntity afterInfo);
    }
}
