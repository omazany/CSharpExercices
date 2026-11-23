namespace TreeVisitors;

public enum Color
{
    Red,
    Green
}

public abstract class Tree
{
    protected Tree(int value, Color color, int depth)
    {
        Value = value;
        Color = color;
        Depth = depth;
    }

    public int Value { get; }
    public Color Color { get; }
    public int Depth { get; }

    public abstract void Accept(TreeVis visitor);
}

public sealed class TreeNode(int value, Color color, int depth) : Tree(value, color, depth)
{
    private readonly List<Tree> children = [];

    public void AddChild(Tree child) => children.Add(child);

    public override void Accept(TreeVis visitor)
    {
        var pending = new Stack<Tree>();
        pending.Push(this);
        while (pending.Count > 0)
        {
            Tree current = pending.Pop();
            if (current is TreeNode node)
            {
                visitor.VisitNode(node);
                for (int i = node.children.Count - 1; i >= 0; i--)
                {
                    pending.Push(node.children[i]);
                }
            }
            else
            {
                current.Accept(visitor);
            }
        }
    }
}

public sealed class TreeLeaf(int value, Color color, int depth) : Tree(value, color, depth)
{
    public override void Accept(TreeVis visitor) => visitor.VisitLeaf(this);
}

public abstract class TreeVis
{
    public abstract int GetResult();
    public abstract void VisitNode(TreeNode node);
    public abstract void VisitLeaf(TreeLeaf leaf);
}
