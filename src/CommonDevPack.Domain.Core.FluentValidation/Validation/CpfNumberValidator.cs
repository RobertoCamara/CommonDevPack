using FluentValidation;
using FluentValidation.Validators;

namespace CommonDevPack.Domain.Core.FluentValidation.Validation;
public class CpfNumberValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public override string Name => "CpfNumberValidator";

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        return ValidateCpfNumber(value?.ToString()!);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
      => "'{PropertyName}' is not a valid cpf number.";

    static int[] multiplier1 => new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    static int[] multiplier2 => new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    private bool ValidateCpfNumber(string cpfNumber)
    {
        if (string.IsNullOrWhiteSpace(cpfNumber)) return false;

        Span<int> tempCpf = stackalloc int[11];
        int position = 0;
        var allEquals = true;
        for (int i = 0; i < cpfNumber.Length; i++)
        {
            if (char.IsDigit(cpfNumber[i]))
            {
                if (position > 10) return false;
                tempCpf[position] = cpfNumber[i] - '0';
                if (allEquals && position > 0)
                    allEquals = tempCpf[position] == tempCpf[position - 1];

                position++;
            }
        }
        if (position != 11) return false;
        if (allEquals) return false;

        int sum1 = 0;
        int sum2 = 0;
        for (int i = 0; i < 9; i++)
        {
            sum1 += tempCpf[i] * multiplier1[i];
            sum2 += tempCpf[i] * multiplier2[i];
        }

        int remainder = sum1 % 11;
        if (remainder < 2) remainder = 0;
        else remainder = 11 - remainder;

        int digit = remainder;
        if (tempCpf[9] != digit) return false;

        sum2 += digit * multiplier2[9];

        remainder = sum2 % 11;

        if (remainder < 2) remainder = 0;
        else remainder = 11 - remainder;

        return tempCpf[10] == remainder;
    }
}
