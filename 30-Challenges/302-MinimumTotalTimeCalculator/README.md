# Minimum Total Time Calculator

## Instructions

Calculate the minimal processing time required to serve all customers in a queue across multiple cashiers.

**Problem Description:**

You have a shop with **O cashiers** (cash desks/payment terminals) available. There is a queue of **M customers** waiting to be served. Each customer has a certain number of articles to process:
- Customer 1 has N₁ articles
- Customer 2 has N₂ articles
- Customer M has Nₘ articles

Each article takes a **constant time unit** to process (e.g., 1 time unit per article).

When a customer approaches an available cashier, they begin processing their articles. The cashier becomes occupied for the duration it takes to process all of the customer's articles.

**Your task:** Calculate the **minimum total time** required to process all customers in the queue, given the number of available cashiers.

## Example

Assume we have 2 cashiers and 5 customers with the following number of articles: `[3, 2, 1, 4, 5]`

```csharp
int cashiers = 2;
int[] customers = new int[] { 3, 2, 1, 4, 5 };
int result = CalculateMinimalProcessingTime(cashiers, customers);
// returns 10

// Explanation:
// Time 0: Cashier 1 starts Customer 1 (3 articles), Cashier 2 starts Customer 2 (2 articles)
// Time 2: Cashier 2 finishes, starts Customer 3 (1 article)
// Time 3: Cashier 1 finishes, starts Customer 4 (4 articles)
//         Cashier 2 finishes, starts Customer 5 (5 articles)
// Time 7: Cashier 1 finishes
// Time 8: Cashier 2 finishes
// Total time: 8
```

## Constraints

- 1 <= number of cashiers <= 100
- 1 <= number of customers <= 10^4
- 1 <= articles per customer <= 10^6
