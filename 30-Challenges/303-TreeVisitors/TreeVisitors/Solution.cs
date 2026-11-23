namespace TreeVisitors;

public static class Solution
{
    public static Tree Solve(TextReader input)
    {
        string[] tokens = input.ReadToEnd().Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
        int position = 0;
        int count = int.Parse(tokens[position++]);
        var values = new int[count];
        var colors = new Color[count];
        var neighbors = new List<int>[count];

        for (int i = 0; i < count; i++)
        {
            values[i] = int.Parse(tokens[position++]);
            neighbors[i] = [];
        }

        for (int i = 0; i < count; i++)
        {
            colors[i] = int.Parse(tokens[position++]) == 0 ? Color.Red : Color.Green;
        }

        for (int i = 0; i < count - 1; i++)
        {
            int first = int.Parse(tokens[position++]) - 1;
            int second = int.Parse(tokens[position++]) - 1;
            neighbors[first].Add(second);
            neighbors[second].Add(first);
        }

        if (count == 1)
        {
            return new TreeLeaf(values[0], colors[0], 0);
        }

        var root = new TreeNode(values[0], colors[0], 0);
        var pending = new Queue<(int Index, int Parent, TreeNode Node)>();
        pending.Enqueue((0, -1, root));

        while (pending.Count > 0)
        {
            var (index, parent, node) = pending.Dequeue();
            foreach (int child in neighbors[index])
            {
                if (child == parent)
                {
                    continue;
                }

                int depth = node.Depth + 1;
                if (neighbors[child].Count == 1)
                {
                    node.AddChild(new TreeLeaf(values[child], colors[child], depth));
                }
                else
                {
                    var childNode = new TreeNode(values[child], colors[child], depth);
                    node.AddChild(childNode);
                    pending.Enqueue((child, index, childNode));
                }
            }
        }

        return root;
    }
}

public class SumInLeavesVisitor : TreeVis
{
    private int sum;

    public override int GetResult() => sum;

    public override void VisitNode(TreeNode node) { }

    public override void VisitLeaf(TreeLeaf leaf) => sum += leaf.Value;
}

public class ProductOfRedNodesVisitor : TreeVis
{
    private const int Modulus = 1_000_000_007;
    private long product = 1;

    public override int GetResult() => (int)product;

    public override void VisitNode(TreeNode node) => MultiplyIfRed(node);

    public override void VisitLeaf(TreeLeaf leaf) => MultiplyIfRed(leaf);

    private void MultiplyIfRed(Tree tree)
    {
        if (tree.Color == Color.Red)
        {
            product = product * tree.Value % Modulus;
        }
    }
}

public class FancyVisitor : TreeVis
{
    private int evenDepthNodes;
    private int greenLeaves;

    public override int GetResult() => Math.Abs(evenDepthNodes - greenLeaves);

    public override void VisitNode(TreeNode node)
    {
        if (node.Depth % 2 == 0)
        {
            evenDepthNodes += node.Value;
        }
    }

    public override void VisitLeaf(TreeLeaf leaf)
    {
        if (leaf.Color == Color.Green)
        {
            greenLeaves += leaf.Value;
        }
    }
}
