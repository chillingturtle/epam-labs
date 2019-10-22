using NUnit.Framework;
using TriangleProj;

namespace Tests
{
    public class TriangleSidesPossbilityTest
    {
        private static readonly int[][] ZeroSides = {
            new int[] { 0, 1, 2 },
            new int[] { 1, 0, 2 },
            new int[] { 2, 1, 0 }
        };

        private static readonly int[][] NegativeSides = {
            new int[] { -1, 1, 2 },
            new int[] { 1, -2, 2 },
            new int[] { 2, 1, -3 }
        };

        private static readonly int[][] InconsistentWithInequalitySides = {
            new int[] { 1, 1, 10 },
            new int[] { 9, 3, 2 },
            new int[] { 2, 15, 5 }
        };

        [TestCaseSource("ZeroSides"), TestCaseSource("NegativeSides"), TestCaseSource("InconsistentWithInequalitySides")]
        public void SidesNotValid(int a, int b, int c)
        {
            Assert.IsFalse(Triangle.CheckIsPossibleToCreate(a, b, c));
        }

        [TestCase(2, 4, 3)]
        public void SidesValid(int a, int b, int c)
        {
            Assert.IsTrue(Triangle.CheckIsPossibleToCreate(a, b, c));
        }
    }
}