using System.Text;
using TreeVisitors;

namespace TreeVisitors.Tests;

[TestClass]
public class SolutionTests
{
    private static void AssertResults(string input, int leaves, int product, int fancy)
    {
        Tree root = Solution.Solve(new StringReader(input));
        var sumVisitor = new SumInLeavesVisitor();
        var productVisitor = new ProductOfRedNodesVisitor();
        var fancyVisitor = new FancyVisitor();

        root.Accept(sumVisitor);
        root.Accept(productVisitor);
        root.Accept(fancyVisitor);

        Assert.AreEqual(leaves, sumVisitor.GetResult());
        Assert.AreEqual(product, productVisitor.GetResult());
        Assert.AreEqual(fancy, fancyVisitor.GetResult());
    }

    [TestMethod]
    public void SampleTree()
    {
        AssertResults("5\n4 7 2 5 12\n0 1 0 0 1\n1 2\n1 3\n3 4\n3 5\n", 24, 40, 15);
    }

    [TestMethod]
    public void SingleRedRootIsALeaf()
    {
        AssertResults("1\n7\n0\n", 7, 7, 0);
    }

    [TestMethod]
    public void SingleGreenRootHasEmptyRedProduct()
    {
        AssertResults("1\n9\n1\n", 9, 1, 9);
    }

    [TestMethod]
    public void UnorderedReversedEdgesAndDepths()
    {
        AssertResults("6\n10 20 30 40 50 60\n0 1 0 1 0 1\n5 4\n3 2\n6 3\n2 1\n4 3\n", 110, 15000, 20);
    }

    [TestMethod]
    public void RedZeroMakesProductZero()
    {
        AssertResults("3\n5 0 3\n0 0 1\n1 2\n1 3\n", 3, 0, 2);
    }

    [TestMethod]
    public void RedProductIsReducedWithoutIntegerOverflow()
    {
        AssertResults("4\n1000 1000 1000 1000\n0 0 0 0\n1 2\n2 3\n3 4\n", 1000, 999993007, 2000);
    }

    [TestMethod]
    public void WhitespaceIsNotLimitedToSingleSpacesOrLines()
    {
        AssertResults("  3\t\n 5  7\n9\n1\t1 1\n2 1  \n3\t1\n", 16, 1, 11);
    }

    [TestMethod]
    public void DeepTreeCanBeBuiltAndVisitedWithoutRecursion()
    {
        const int count = 100_000;
        var input = new StringBuilder();
        input.AppendLine(count.ToString());
        for (int i = 0; i < count; i++) input.Append("1 ");
        input.AppendLine();
        for (int i = 0; i < count; i++) input.Append("0 ");
        input.AppendLine();
        for (int i = 1; i < count; i++) input.Append(i).Append(' ').Append(i + 1).AppendLine();
        AssertResults(input.ToString(), 1, 1, 50_000);
    }

    [TestMethod]
    public void FixedTreeModelSupportsCustomVisitors()
    {
        var root = new TreeNode(1, Color.Red, 0);
        var child = new TreeNode(2, Color.Green, 1);
        child.AddChild(new TreeLeaf(3, Color.Red, 2));
        root.AddChild(child);
        root.AddChild(new TreeLeaf(4, Color.Green, 1));
        var visitor = new RecordingVisitor();

        root.Accept(visitor);

        CollectionAssert.AreEqual(new[] { "N1:0", "N2:1", "L3:2", "L4:1" }, visitor.Visited);
    }

    [TestMethod]
    public void VisitorsCanBeImplementedBeforeTheBuilder()
    {
        var root = new TreeNode(4, Color.Red, 0);
        var child = new TreeNode(2, Color.Red, 1);
        child.AddChild(new TreeLeaf(5, Color.Red, 2));
        child.AddChild(new TreeLeaf(12, Color.Green, 2));
        root.AddChild(new TreeLeaf(7, Color.Green, 1));
        root.AddChild(child);

        var sum = new SumInLeavesVisitor();
        var product = new ProductOfRedNodesVisitor();
        var fancy = new FancyVisitor();
        root.Accept(sum);
        root.Accept(product);
        root.Accept(fancy);

        Assert.AreEqual(24, sum.GetResult());
        Assert.AreEqual(40, product.GetResult());
        Assert.AreEqual(15, fancy.GetResult());
    }

    [TestMethod]
    public void RedProductStartsAtOne()
    {
        Assert.AreEqual(1, new ProductOfRedNodesVisitor().GetResult());
    }

    private sealed class RecordingVisitor : TreeVis
    {
        public List<string> Visited { get; } = [];
        public override int GetResult() => Visited.Count;
        public override void VisitNode(TreeNode node) => Visited.Add($"N{node.Value}:{node.Depth}");
        public override void VisitLeaf(TreeLeaf leaf) => Visited.Add($"L{leaf.Value}:{leaf.Depth}");
    }
}
