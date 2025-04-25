using System.Collections.Generic;
using NUnit.Framework;
using TsunagiModule.Goap;

public class GoapTest
{
    private const string ACTION_1 = "#1 increase int";
    private const string ACTION_2 = "#2 increase float";
    private const string ACTION_3 = "#3 increase double and switch boolean";

    [Test]
    public void GoapTestSimple()
    {
        Condition<int> goal = new Condition<int>("int", ConditionOperator.LargerOrEqual, 2);
        GoapState state = GenerateState();
        GoapSolver solver = GenerateSolverWithActionPool();

        //run
        GoapResult result = solver.Solve(state, goal, 10);

        // #1 -> #1
        Assert.AreEqual(true, result.success);
        Assert.AreEqual(2, result.length);
        Assert.AreEqual(ACTION_1, result.actions[0].name);
        Assert.AreEqual(ACTION_1, result.actions[1].name);
    }

    [Test]
    public void GoapTestPathFinding()
    {
        Condition<float> goal = new Condition<float>(
            "float",
            ConditionOperator.LargerOrEqual,
            0.5f
        );

        GoapState state = GenerateState();
        GoapSolver solver = GenerateSolverWithActionPool();

        //run
        GoapResult result = solver.Solve(state, goal, 10);

        // #1 -> #1 -> #1 -> #2
        Assert.AreEqual(true, result.success);
        Assert.AreEqual(4, result.length);
        Assert.AreEqual(ACTION_1, result.actions[0].name);
        Assert.AreEqual(ACTION_1, result.actions[1].name);
        Assert.AreEqual(ACTION_1, result.actions[2].name);
        Assert.AreEqual(ACTION_2, result.actions[3].name);
    }

    [Test]
    public void GoapTestMapping()
    {
        ConditionInterface goal = new ConditionAnd(
            new ConditionInterface[]
            {
                new Condition<double>("double", ConditionOperator.LargerOrEqual, 0.1),
                new Condition<bool>("boolean", ConditionOperator.Equal, false)
            }
        );
        GoapState state = GenerateState();
        GoapSolver solver = GenerateSolverWithActionPool();

        GoapResult result = solver.Solve(state, goal, 10);

        // #3 -> #3
        Assert.AreEqual(true, result.success);
        Assert.AreEqual(2, result.length);
        Assert.AreEqual(ACTION_3, result.actions[0].name);
        Assert.AreEqual(ACTION_3, result.actions[1].name);
    }

    [Test]
    public void GoapTestBetterCost()
    {
        const string ACTION_X = "#X int + 100 (cost 100)";

        Condition<int> goal = new Condition<int>("int", ConditionOperator.Equal, 3);
        GoapState state = GenerateState();
        GoapSolver solver = GenerateSolverWithActionPool();
        solver.AddAction(
            new GoapAction(
                ACTION_X,
                new NoCondition(),
                new StateDiffInterface[] { new StateDiffAddition<int>("int", 100) },
                100.0
            )
        );

        //run
        GoapResult result = solver.Solve(state, goal, 10);

        // #1 -> #1 -> #1
        Assert.AreEqual(true, result.success);
        Assert.AreEqual(3, result.length);
        Assert.AreEqual(ACTION_1, result.actions[0].name);
        Assert.AreEqual(ACTION_1, result.actions[1].name);
        Assert.AreEqual(ACTION_1, result.actions[2].name);
    }

    [Test]
    public void GoapTestTooDeep()
    {
        Condition<int> goal = new Condition<int>("int", ConditionOperator.Equal, 100);
        GoapState state = GenerateState();
        GoapSolver solver = GenerateSolverWithActionPool();

        //run
        GoapResult result = solver.Solve(state, goal, 10);

        // error
        Assert.AreEqual(false, result.success);
    }

    [Test]
    public void GoapTestImpossible()
    {
        Condition<int> goal = new Condition<int>("int", ConditionOperator.Equal, -1);
        GoapState state = GenerateState();
        GoapSolver solver = GenerateSolverWithActionPool();

        //run
        GoapResult result = solver.Solve(state, goal, 10);

        // error
        Assert.AreEqual(false, result.success);
    }

    private GoapState GenerateState()
    {
        GoapState state = new GoapState();
        state.SetRawValue("int", 0);
        state.SetRawValue("float", 0f);
        state.SetRawValue("double", 0.0);
        state.SetRawValue("boolean", false);
        return state;
    }

    private GoapSolver GenerateSolverWithActionPool()
    {
        GoapSolver solver = new GoapSolver();
        solver.AddAction(
            new GoapAction(
                ACTION_1,
                new NoCondition(),
                new StateDiffInterface[] { new StateDiffAddition<int>("int", 1) },
                1.0
            )
        );
        solver.AddAction(
            new GoapAction(
                ACTION_2,
                new Condition<int>("int", ConditionOperator.Larger, 2),
                new StateDiffInterface[] { new StateDiffAddition<float>("float", 1f) },
                2.0
            )
        );
        solver.AddAction(
            new GoapAction(
                ACTION_3,
                new NoCondition(),
                new StateDiffInterface[]
                {
                    new StateDiffAddition<double>("double", 1.0),
                    new StateDiffMapping<bool>(
                        "boolean",
                        new Dictionary<bool, bool>() { { false, true }, { true, false } }
                    )
                },
                3.0
            )
        );
        return solver;
    }
}
