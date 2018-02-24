using System;

namespace YYProject.Algorithms
{
    //https://csclub.uwaterloo.ca/~pbarfuss/p132-sunday.pdf
    //http://www-igm.univ-mlv.fr/~lecroq/articles/spe95.pdf


    /// <summary>
    /// Provides the Sunday Quick-Search algorithm for searching <see cref="Byte"/>[] in a longger <see cref="Byte"/>[]. 
    /// </summary>
    public static class ByteArrayQuickSearch
    {

        /// <summary>
        ///  Searches for the specified <see cref="Byte"/>[] and returns the index of its first occurrence in a longger <see cref="Byte"/>[] source.
        /// </summary>
        /// <param name="source">The <see cref="Byte"/>[] to search.</param>
        /// <param name="pattern">The <see cref="Byte"/>[] to locate in source.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count"> The number of <see cref="Byte"/> positions to examine. If this value is negative, ignore it.</param>
        /// <returns>The index of the first occurrence of pattern in the source, if found; otherwise, –1.</returns>
        /// <exception cref="ArgumentNullException">The pattern can not be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The startIndex is negative or greater than the length of source.
        ///  Or the count is greater than the length of source minus startIndex.</exception>
        public static Int32 QSIndexOf(this Byte[] source, Byte[] pattern, Int32 startIndex = 0, Int32 count = -1)
        {
            return IndexOf(source, pattern, true, startIndex, count);
        }

        /// <summary>
        ///  Searches for the specified <see cref="Byte"/>[] and returns the index of its last occurrence in a longger <see cref="Byte"/>[] source.
        /// </summary>
        /// <param name="source">The <see cref="Byte"/>[] to search.</param>
        /// <param name="pattern">The <see cref="Byte"/>[] to locate in source.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count"> The number of <see cref="Byte"/> positions to examine. If this value is negative, ignore it.</param>
        /// <returns>The index of the last occurrence of pattern in the source, if found; otherwise, –1.</returns>
        /// <exception cref="ArgumentNullException">The pattern can not be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The startIndex is negative or greater than the length of source.
        ///  Or the count is greater than the length of source minus startIndex.</exception>
        public static Int32 QSLastIndexOf(this Byte[] source, Byte[] pattern, Int32 startIndex = 0, Int32 count = -1)
        {
            return IndexOf(source, pattern, false, startIndex, count);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Byte"/>[] exists in the longger <see cref="Byte"/>[] source. 
        /// </summary>
        /// <param name="source">The <see cref="Byte"/>[] to search.</param>
        /// <param name="pattern">The <see cref="Byte"/>[] to locate in source.</param>
        /// <returns>true if the specified <see cref="Byte"/>[] exists; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">The pattern can not be null.</exception>
        public static Boolean QSContains(this Byte[] source, Byte[] pattern)
        {
            return IndexOf(source, pattern) == -1 ? false : true;
        }

        private static Int32 IndexOf(Byte[] source, Byte[] pattern, Boolean breakWhenFindFirst = true, Int32 startIndex = 0, Int32 count = -1)
        {
            if (source == null)
            {
                return -1;
            }

            if (pattern == null) throw new ArgumentNullException("The pattern can not be null.");

            var sLength = source.Length;
            var pLehgth = pattern.Length;
            var pMaxIndex = pLehgth - 1;
            var endPos = sLength;

            if (sLength < pLehgth || sLength == 0 || pLehgth == 0) return -1;
            if (startIndex < 0 || startIndex > sLength) throw new ArgumentOutOfRangeException("The startIndex is negative or greater than the length of source.");
            if (count > 0)
            {
                endPos = startIndex + count;
                if (endPos > sLength)
                {
                    throw new ArgumentOutOfRangeException("The count is greater than the length of source minus startIndex.");
                }
            }

            endPos -= pLehgth;
            Int32 pIndex, step, result = -1;
            var charsFlag = QSInt32ArrayPool.Rent(pattern);         //Get the pos of the chars in pattern, see IntsPool.Rent!!!
            while (startIndex <= endPos)
            {
                for (pIndex = 0; pIndex <= pMaxIndex && source[startIndex + pIndex] == pattern[pIndex]; pIndex++)
                {
                    if (pIndex == pMaxIndex)
                    {
                        result = startIndex;
                    }
                }
                if (result > -1 && breakWhenFindFirst) break;
                step = startIndex + pLehgth;
                if (step >= sLength) break;
                startIndex += charsFlag[source[step]];
            }

            #region Un-used Boyer-Moore native
            //while (startIndex <= endPos)
            //{
            //    for (pIndex = pMaxIndex; pIndex > -1 && source[startIndex + pIndex] == pattern[pIndex]; pIndex--)
            //    {
            //        if (pIndex == 0)
            //        {
            //            result = startIndex;
            //        }
            //    }
            //    if (result > -1 && breakWhenFindFirst) break;
            //    startIndex += charsFlag[source[startIndex + pMaxIndex]];
            //}
            #endregion

            QSInt32ArrayPool.Return(charsFlag);
            return result;
        }
    }
}
