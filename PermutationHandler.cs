namespace NumerologyCalculator;

public class PermutationHandler
{
    public List<List<EvalString.Operator>> GenerateOperatorPossibilities(string word, bool allowDivision=true, bool canStartNegative=false)
    {

        var digits = word.Length;
        
        var _base = allowDivision ? 4 : 3; // calculated base for generated numbers (number of potential operators)
        var baseN = (long) Math.Pow(10, digits); // subtract one 
        var decimalNumber = BaseNToDecimal(baseN.ToString(), _base);

        List<List<EvalString.Operator>> operatorOptions = new();
        
        for (var i = 0; i < decimalNumber; i++)
        {
            var operators = $"{DecimalToBaseN(i, _base).PadLeft(digits, '0')}";

            if (!canStartNegative && operators.StartsWith("1"))
            {
                continue;
            }
            
            operatorOptions.Add(
                ConvertNumbersToOperators(operators)
                );
        }

        return operatorOptions;
    }

    private List<EvalString.Operator> ConvertNumbersToOperators(string baseFourStrings)
    {
        List<EvalString.Operator> operators = new();
        foreach(var c in baseFourStrings.ToCharArray())
        {
            switch (c)
            {
                case '0':
                    operators.Add(EvalString.Operator.Add);
                    break;
                case '1' :
                    operators.Add(EvalString.Operator.Sub);
                    break;
                case '2' :
                    operators.Add(EvalString.Operator.Mult);
                    break;
                case '3' :
                    operators.Add(EvalString.Operator.Div);
                    break;
            }
        }

        return operators;
    }
    
    static int BaseNToDecimal(string number, int baseN)
    {
        int decimalNumber = 0;
        int power = 0;

        for (int i = number.Length - 1; i >= 0; i--)
        {
            int digit = DigitToValue(number[i]);
            decimalNumber += digit * (int)Math.Pow(baseN, power);
            power++;
        }

        return decimalNumber;
    }

    static string DecimalToBaseN(int decimalNumber, int baseN)
    {
        if (decimalNumber == 0)
            return "0";

        string baseNNumber = "";
        while (decimalNumber > 0)
        {
            int remainder = decimalNumber % baseN;
            baseNNumber = ValueToDigit(remainder) + baseNNumber;
            decimalNumber /= baseN;
        }

        return baseNNumber;
    }

    static int DigitToValue(char digit)
    {
        if (char.IsDigit(digit))
            return int.Parse(digit.ToString());
        else
            return char.ToUpper(digit) - 'A' + 10;
    }

    static char ValueToDigit(int value)
    {
        if (value < 10)
            return value.ToString()[0];
        else
            return (char)('A' + value - 10);
    }

}