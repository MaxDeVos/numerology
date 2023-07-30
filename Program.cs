using NumerologyCalculator;

public class Program
{
    public static void Main(string[] args)
    {
        Evaluator(args[0], false);
    }
    
    static void Evaluator(string input, bool division=true, bool canStartNegative=false, bool enforceAddOnSpace = false)
    {
        var inString = new EvalString(input);
        var permHandler = new PermutationHandler();
        var perms = permHandler.GenerateOperatorPossibilities(input, division, canStartNegative);

        foreach (var operators in perms)
        {
            inString.IsMatch(operators, 666m);
        }
        
    }
}

