namespace NumerologyCalculator;

public class InputString
{
    protected string _input;
    protected List<char> _chars;
    protected  List<decimal> _vals;

    private Dictionary<char, decimal> AlphabetVals = new();

    public InputString(string input)
    {
        CalculateCharMap();
        _input = input;
        _chars = input.ToCharArray().ToList();
        _vals = ConvertStringToNumberValues(_input);
    }

    private void CalculateCharMap()
    {
        for (var i = 1; i <= 26; i++)
        {
            AlphabetVals.Add((char) (i + 64), i);
        }
        for (var i = 1; i <= 26; i++)
        {
            AlphabetVals.Add((char) (i + 96), i);
        }
        AlphabetVals.Add(' ', 0);
    }

    protected List<decimal> ConvertStringToNumberValues(string input)
    {
        var chars = new List<decimal>();
        foreach(var c in _chars)
        {
            if (!AlphabetVals.TryGetValue(c, out decimal charVal))
            {
                throw new ArgumentException($"Illegal input: {input}! Character ${c} not allowed!");
            }
            chars.Add(charVal);
        }
        return chars;
    }

    
    private void PrintCharMap()
    {
        foreach (var c in AlphabetVals)
        {
            Console.WriteLine($"{c.Key} - {c.Value}");
        }
    }
    
    public override string ToString()
    {
        var str = "";
        for (var i = 0; i < _chars.Count; i++)
        {
            str += $"{_chars[i]}({_vals[i]:D2})  ";
        }

        return str;
    }

    
}