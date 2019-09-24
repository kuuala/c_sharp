using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class RightBorderTask
    {
        /// <returns>
        /// Возвращает индекс правой границы. 
        /// То есть индекс минимального элемента, который не начинается с prefix и большего prefix.
        /// Если такого нет, то возвращает items.Length
        /// </returns>
        /// <remarks>
        /// Функция должна быть НЕ рекурсивной
        /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
        /// </remarks>
        public static int GetRightBorderIndex(
            IReadOnlyList<string> phrases,
            string prefix,
            int left,
            int right)
        {
            var delta = right - left;
            while (delta > 1)
            {
                var mid = left + delta / 2;
                if (string.Compare(prefix, phrases[mid], StringComparison.OrdinalIgnoreCase) >= 0
                    || phrases[mid].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    left = mid;
                else
                    right = mid;
                delta = right - left;
            }
            return right;
        }
    }
}
