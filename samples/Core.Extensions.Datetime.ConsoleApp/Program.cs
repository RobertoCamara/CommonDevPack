

using CommonDevPack.Core.Extensions.DateTime;

Console.WriteLine("DateTime.Now {0}", DateTime.Now);

Console.WriteLine("DateTime.UtcNow {0}", DateTime.UtcNow);

Console.WriteLine("DateTime.UtcNow Applying Extension ToZonedDateTime {0}", DateTime.UtcNow.ToZonedDateTime());
