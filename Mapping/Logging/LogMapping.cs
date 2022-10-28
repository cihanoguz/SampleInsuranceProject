using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Mapping;
using Entity.Logs;

namespace Mapping.Logging
{
    public class LogMapping : BaseMap<Log>
    {
        public LogMapping(EntityTypeBuilder<Log> builder)
        {

        }
    }

}