using System.Data;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace NumerologyCalculator;

public class EvalString : InputString
{

    public enum Operator
    {
        Add,
        Sub,
        Mult,
        Div
    }

    /** All operators occur BEFORE the char they share an index with. 
     * Example: input = "PISS"
     *    
     *    _chars     = [  P   I   S   S  ]
     *    _vals      = [  16  9   19  19 ]
     *    _operators = [ +   -   +   +   ]
     *
     *    char equation = +P-I+S+S
     *    int equation  = +16-9+19+19 = 45
     *
     *    Note that the case of the first operator being MULT or DIV must be handled separately.
     */
    private List<Operator> _ops;

    private readonly DataTable _dt = new();

    public EvalString(string input) : base(input)
    {
        _ops = new List<Operator>();

        for (var i = 0; i < _chars.Count; i++)
        {
            _ops.Add(Operator.Add);
        }
    }

    private char OperatorToChar(Operator op)
    {
        return op switch
        {
            Operator.Add => '+',
            Operator.Sub => '-',
            Operator.Div => '/',
            Operator.Mult => '*',
            _ => '_'
        };
    }

    private string ToEquation()
    {
        var str = "";
        
        if (_ops.Count != _chars.Count)
        {
            throw new ApplicationException("Incorrect number of operators present for calculation");
        }
        
        for (var i = 0; i < _chars.Count; i++)
        {
            str += $"{OperatorToChar(_ops[i])}{_vals[i]}.0";
        }

        return str;
    }

    public decimal Evaluate(List<Operator> operators, bool shouldPrint=false)
    {
        _ops = operators;
        var eq = ToEquation();
        if (eq.StartsWith("*") || eq.StartsWith("/"))
        {
            return 0;
        }

        var result = (decimal) _dt.Compute(eq, "");

        if (shouldPrint)
        {
            Console.WriteLine($"{eq} = {result}");
        }

        return result;
    }

    public bool IsMatch(List<Operator> operators, decimal value)
    {
        var result = Evaluate(operators);
        if (result != value) return false;

        var eq = ToEquation();

        var numMult = operators.FindAll(@operator => @operator == Operator.Mult).Count;
        
        Console.WriteLine($"{eq} = {result}   |   MULT = {numMult}");
        Console.WriteLine(ToString());
        return true;
    }

    public override string ToString()
    {
        var str = "";
        for (var i = 0; i < _chars.Count; i++)
        {
            var op = "";
            if (_ops.Count > i)
            {
                op = OperatorToChar(_ops[i]).ToString();
            }
            str += $"{op}{_chars[i]}";
        }

        return $"{str}\n{ToValueString()}\n";
    }
    
    public string ToValueString()
    {
        var str = "";
        for (var i = 0; i < _chars.Count; i++)
        {
            var op = "";
            if (_ops.Count > i)
            {
                op = OperatorToChar(_ops[i]).ToString();
            }
            str += $"{op}{_vals[i]}";
        }

        return str;
    }
}