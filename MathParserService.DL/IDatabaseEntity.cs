using System;

namespace MathParserService.DL
{
    public interface IDatabaseEntity
    {
        Guid Id { get; set; }

        DateTimeOffset CreateDate { get; set; }
    }
}