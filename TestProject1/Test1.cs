namespace TestProject1
{
    using Lab;
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestItemCount()
        {
            List<int> sizes = new List<int> { 10, 20, 30, 40, 50 };
            foreach(var n in sizes) {
                Problem p = new Problem(n, 1);
                Assert.AreEqual(n, p.item_list.Count);
            }
        }

        [TestMethod]
        public void TestSolver()
        {
            Problem p = new Problem(10, 42069);
            Result r = p.solve(15);
            Assert.AreNotEqual(0, r.items.Count);
        }


        [TestMethod]
        public void TestSmallBackpack()
        {
            Problem p = new Problem(10, 42069);
            Result r = p.solve(0);
            Assert.AreEqual(0, r.items.Count);
        }


        [TestMethod]
        public void TestCorrectSolution()
        {
            Problem p = new Problem(10, 42069);
            Result r = p.solve(10);
            CollectionAssert.AreEqual(new int[] {4,5,6}, r.items);
        }

        [TestMethod]
        public void TestNoItems()
        {
            Problem p = new Problem(0, 42069);
            Result r = p.solve(10);
            CollectionAssert.AreEqual(new int[] {}, r.items);
        }
    }
}
