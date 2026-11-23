# Tree Visitors

Practice the open/closed principle and the visitor pattern. The tree model in
`Tree.cs` is provided: do not modify it. Add behavior by implementing
`Solution.Solve` and the three visitors in `Solution.cs`. Each visitor is a new,
stateful instance intended for one traversal.

## Tree input

`Solve(TextReader input)` reads whitespace-separated integers and returns a tree
rooted at node 1. It must not write to standard output.

1. The number of nodes `n`.
2. `n` node values, in node-number order.
3. `n` colors, in node-number order: `0` is red and `1` is green.
4. Exactly `n - 1` undirected edges, each a pair of node numbers. Edges can
   appear in any order and either direction.

The input describes a valid tree. For this exercise, `1 <= n <= 100,000` and
`0 <= value <= 1,000`. Node numbers are between 1 and `n`. The root has depth
0; each child has its parent's depth plus one. A node without children is a
leaf, including a root with no neighbors. Other nodes are `TreeNode` instances.
Use an iterative construction to handle a deep tree.

## Visitors

Implement `GetResult`, `VisitNode` and `VisitLeaf` for each visitor:

- `SumInLeavesVisitor`: sum the values of all leaves.
- `ProductOfRedNodesVisitor`: multiply the values of all red nodes, including
  leaves, modulo `1_000_000_007`. The product of no red nodes is 1.
- `FancyVisitor`: return the absolute difference between the sum of non-leaf
  values at even depth and the sum of green leaf values.

Do not print results: callers visit the returned tree and use `GetResult()`.
Use an intermediate type large enough to multiply before reducing modulo.

## Example

Input:

```text
5
4 7 2 5 12
0 1 0 0 1
1 2
1 3
3 4
3 5
```

The visitors return `24`, `40`, and `15`, respectively.

## Run the tests

Run `dotnet test TreeVisitors.sln` from this directory. This branch contains a
sample implementation; the exercises branch contains the starter code. The
builder uses breadth-first traversal and takes O(n) time and space. Each visitor
takes O(n) time for a traversal and stores O(1) visitor state.
