using System.Linq;
using LanguageExt;

namespace RfxCom.Messages
{
    public static class Enum
    {
        public static Option<T> ParseEnum<T>(byte value)
        {
            return System.Enum.GetValues(typeof(T))
                    .Cast<int>()
                    .Where(x => x == value)
                    .Cast<T>().HeadOrNone();
        }
    }
}