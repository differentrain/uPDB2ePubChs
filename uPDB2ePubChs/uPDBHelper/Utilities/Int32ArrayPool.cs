using System;
using System.Collections.Concurrent;

namespace YYProject.Algorithms
{

    internal static class QSInt32ArrayPool
    {
        private static ConcurrentBag<Int32[]> _objects;

        static QSInt32ArrayPool()
        {
            _objects = new ConcurrentBag<Int32[]>();
        }

        public static Int32[] Rent(Byte[] pattern)
        {
            _objects.TryTake(out var item);
            var buffer = item ?? new Int32[256];
            Int32 i;
            var length = pattern.Length;
            var endIndex = length - 1;
            var badMov = length + 1;

            #region Un-used Boyer-Moore native
            //for (i = 0; i < 256; i++)
            //{
            //    buffer[i] = length;
            //}

            //for (i = 0; i < endIndex; i++)
            //{
            //    buffer[pattern[i]] = endIndex - i;
            //}
            #endregion

            for (i = 0; i < 256; i++)
            {
                buffer[i] = badMov;

            }

            for (i = 0; i <= endIndex; i++)
            {
                buffer[pattern[i]] = length - i;
            }
            return buffer;
        }

        public static void Return(Int32[] ints)
        {
            _objects.Add(ints);
        }
    }
}
