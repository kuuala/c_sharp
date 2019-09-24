using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        /// Эта функция уже реализована, она заработает, 
        /// как только вы выполните задачу в файле LeftBorderTask
        /// </remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count
                && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var pCount = phrases.Count;
            var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, pCount) + 1;
            if (left == pCount)
                return new string[0];
            var result = new List<string>();
            for (int i = 0; i < count; ++i)
            {
                var ind = left + i;
                if (ind >= pCount
                    || !phrases[ind].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    break;
                result.Add(phrases[ind]);
            }
            return result.ToArray();
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            var delta = right - left - 1;
            return delta <= 0 ? 0 : delta;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            var phrases = new List<string>();
            var result = AutocompleteTask.GetTopByPrefix(phrases, "C#", 0);
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases_CounterNonZero()
        {
            var phrases = new List<string>();
            var result = AutocompleteTask.GetTopByPrefix(phrases, "C#", 1);
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void TopByPrefix_IsEmpty_WhenCounterZero()
        {
            var phrases = new List<string> { "a", "ab" };
            var result = AutocompleteTask.GetTopByPrefix(phrases, "a", 0);
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void TopByPrefix_IsEmpty_NoPhrase()
        {
            var phrases = new List<string> { "a", "ab" };
            var result = AutocompleteTask.GetTopByPrefix(phrases, "z", 10);
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void TopByPrefix()
        {
            var phrases = new List<string> { "a", "ab", "ab" };
            var result = AutocompleteTask.GetTopByPrefix(phrases, "a", 10);
            Assert.AreEqual(3, result.Length);
            CollectionAssert.AreEqual(phrases, result);
        }

        [Test]
        public void CountByPrefix_IsTotalCount()
        {
            var phrases = new List<string>() { "m", "m", "c", "s" };
            var result = AutocompleteTask.GetCountByPrefix(phrases, "m");
            Assert.AreEqual(2, result);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            var phrases = new List<string>() { "m", "m", "c", "s" };
            var result = AutocompleteTask.GetCountByPrefix(phrases, "");
            Assert.AreEqual(4, result);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenAllContainPrefix()
        {
            var phrases = new List<string>() { "mmcs", "mmc", "mm", "m" };
            var result = AutocompleteTask.GetCountByPrefix(phrases, "m");
            Assert.AreEqual(4, result);
        }

        [Test]
        public void CountByPrefix_IsZero_WhenNoEntries()
        {
            var phrases = new List<string>() { "m", "m", "c", "s" };
            var result = AutocompleteTask.GetCountByPrefix(phrases, "z");
            Assert.AreEqual(0, result);
        }
    }
}
