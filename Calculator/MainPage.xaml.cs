namespace Calculator;

public partial class MainPage : ContentPage
{
    private double firstNumber = 0;
    private double secondNumber = 0;
    private string currentOperator = "";
    private bool isFirstNumberAfterOperator = true;

    public MainPage()
    {
        InitializeComponent();
        Display.Text = "0";
        ExpressionLabel.Text = "";
    }

    private void OnNumberPressed(object? sender, EventArgs e)
    {
        Button pressedButton = sender as Button;

        if (pressedButton != null)
        {
            if (isFirstNumberAfterOperator)
            {
                Display.Text = pressedButton.Text;
                isFirstNumberAfterOperator = false;
            }
            else
            {
                Display.Text = Display.Text + pressedButton.Text;
            }
        }
    }

    private void OnOperatorPressed(object? sender, EventArgs e)
    {
        Button pressedButton = sender as Button;

        if (pressedButton == null)
        {
            return;
        }

        if (isFirstNumberAfterOperator)
        {
            currentOperator = pressedButton.Text;
            if (currentOperator != "" && currentOperator != "=")
            {
                ExpressionLabel.Text = firstNumber.ToString() + " " + currentOperator;
            }
            return;
        }

        isFirstNumberAfterOperator = true;

        if (currentOperator == "")
        {
            currentOperator = pressedButton.Text;
            firstNumber = double.Parse(Display.Text);

            if (currentOperator != "=")
            {
                ExpressionLabel.Text = firstNumber.ToString() + " " + currentOperator;
            }
        }
        else
        {
            secondNumber = double.Parse(Display.Text);
            double result = 0;
            double oldFirstNumber = firstNumber;
            double oldSecondNumber = secondNumber;
            string oldOperator = currentOperator;

            switch (currentOperator)
            {
                case "+": result = firstNumber + secondNumber; break;
                case "-": result = firstNumber - secondNumber; break;
                case "*": result = firstNumber * secondNumber; break;
                case "/": result = firstNumber / secondNumber; break;
            }

            if (pressedButton.Text == "=")
            {
                ExpressionLabel.Text = oldFirstNumber.ToString() + " " + oldOperator + " " + oldSecondNumber.ToString() + " =";
            }
            else
            {
                ExpressionLabel.Text = result.ToString() + " " + pressedButton.Text;
            }

            Display.Text = result.ToString();
            currentOperator = pressedButton.Text;
            if (pressedButton.Text == "=") currentOperator = "";
            firstNumber = result;
        }
    }

    private void OnClearEntryPressed(object? sender, EventArgs e)
    {
        Display.Text = "0";
        isFirstNumberAfterOperator = true;
    }

    private void OnClearAllPressed(object? sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentOperator = "";
        isFirstNumberAfterOperator = true;
        Display.Text = "0";
        ExpressionLabel.Text = "";
    }

    private void OnDecimalPointPressed(object? sender, EventArgs e)
    {
        if (isFirstNumberAfterOperator)
        {
            Display.Text = "0.";
            isFirstNumberAfterOperator = false;
        }
        else if (!Display.Text.Contains("."))
        {
            Display.Text = Display.Text + ".";
        }
    }

    private void OnSqrtPressed(object? sender, EventArgs e)
    {
        double value = double.Parse(Display.Text);
        double result = Math.Sqrt(value);

        ExpressionLabel.Text = "√(" + value.ToString() + ")";
        Display.Text = result.ToString();

        firstNumber = result;
        secondNumber = 0;
        currentOperator = "";
        isFirstNumberAfterOperator = true;
    }

    private void OnSquarePressed(object? sender, EventArgs e)
    {
        double value = double.Parse(Display.Text);
        double result = value * value;

        ExpressionLabel.Text = value.ToString() + "²";
        Display.Text = result.ToString();

        firstNumber = result;
        secondNumber = 0;
        currentOperator = "";
        isFirstNumberAfterOperator = true;
    }
}
