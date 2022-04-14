using FluentValidation;
using FluentValidation.Validators;

namespace CommonDevPack.Domain.Core.FluentValidation.Validation;
public class CnpjNumberValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public override string Name => "CnpjNumberValidator";

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        return ValidateCnpjNumber(value?.ToString()!);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
      => "'{PropertyName}' is not a valid cnpj number.";


    static int[] multiplier1 => new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
    static int[] multiplier2 => new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
    private bool ValidateCnpjNumber(string cnpjNumber)
    {
        if (string.IsNullOrWhiteSpace(cnpjNumber)) return false;

        Span<int> tempCnpj = stackalloc int[14];
        int position = 0;
        var allEquals = true;
        for (int i = 0; i < cnpjNumber.Length; i++)
        {
            if (char.IsDigit(cnpjNumber[i]))
            {
                if (position > 13) return false;
                tempCnpj[position] = cnpjNumber[i] - '0';
                if (allEquals && position > 0)
                    allEquals = tempCnpj[position] == tempCnpj[position - 1];
                
                position++;
            }
        }
        if (position != 14) return false;
        if (allEquals) return false;

        int sum1 = 0;
        int sum2 = 0;
        for (int i = 0; i < 12; i++)
        {
            sum1 += tempCnpj[i] * multiplier1[i];
            sum2 += tempCnpj[i] * multiplier2[i];
        }

        int remainder = sum1 % 11;
        if (remainder < 2) remainder = 0;
        else remainder = 11 - remainder;

        int digit = remainder;
        if (tempCnpj[12] != digit) return false;

        sum2 += digit * multiplier2[12];

        remainder = sum2 % 11;

        if (remainder < 2) remainder = 0;
        else remainder = 11 - remainder;

        return tempCnpj[13] == remainder;
    }
}