using System;
using System.Collections.Generic;
using System.Linq;
using LaYumba.Functional;

namespace Exercises.Chapter01
{
    static class Exercises
    {
        // 1. Write a function that negates a given predicate: whenvever the given predicate
        // evaluates to `true`, the resulting function evaluates to `false`, and vice versa.

        public static Func<T, bool> Negate<T>(this Func<T, bool> predicate)
            => it => !predicate(it);

        // 2. Write a method that uses quicksort to sort a `List<int>` (return a new list,
        // rather than sorting it in place).

        public static List<int> QuickSort(this List<int> ints)
        {
            if (ints == null || !ints.Any()) return ints;

            var head = ints.First();
            var tail = ints.Skip(1).ToList();

            var smaller = tail.Where(x => x < head).ToList();
            var equal = tail.Where(x => x == head);
            var greater = tail.Where(x => x > head).ToList();

            return smaller.QuickSort()
                .Append(head).Concat(equal)
                .Concat(greater.QuickSort())
                .ToList();
        }

        // 3. Generalize your implementation to take a `List<T>`, and additionally a 
        // `Comparison<T>` delegate.

        public static List<T> QuickSort<T>(this List<T> list, Comparison<T> comparison)
        {
            if (list == null || !list.Any()) return list;

            var head = list.First();
            var tail = list.Skip(1).ToList();

            var smaller = tail.Where(x => comparison(x, head) < 0).ToList();
            var equal = tail.Where(x => comparison(x, head) == 0);
            var greater = tail.Where(x => comparison(x, head) > 0).ToList();

            return smaller.QuickSort(comparison)
                .Append(head).Concat(equal)
                .Concat(greater.QuickSort(comparison))
                .ToList();
        }

        // 4. In this chapter, you've seen a `Using` function that takes an `IDisposable`
        // and a function of type `Func<TDisp, R>`. Write an overload of `Using` that
        // takes a `Func<IDisposable>` as first
        // parameter, instead of the `IDisposable`. (This can be used to fix warnings
        // given by some code analysis tools about instantiating an `IDisposable` and
        // not disposing it.)
    }
}
