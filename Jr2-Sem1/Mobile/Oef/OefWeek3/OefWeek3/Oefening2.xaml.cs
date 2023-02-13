namespace OefWeek3;

public partial class Oefening2 : ContentPage
{
	public Oefening2()
	{
		InitializeComponent();
	}

	private void CalculatorClick(object sender, EventArgs e)
	{
		string input = ((Button)sender).Text;
		switch (input)
		{
			case "=":
				Calculate(LblEquation.Text);
				break;
            case "C":
				LblEquation.Text = String.Empty;
                break;
            case "0":
				LblEquation.Text = LblEquation.Text + "0";
                break;
            case "1":
                LblEquation.Text = LblEquation.Text + "1";
                break;
            case "2":
                LblEquation.Text = LblEquation.Text + "2";
                break;
            case "3":
                LblEquation.Text = LblEquation.Text + "3";
                break;
            case "4":
                LblEquation.Text = LblEquation.Text + "4";
                break;
            case "5":
                LblEquation.Text = LblEquation.Text + "5";
                break;
            case "6":
                LblEquation.Text = LblEquation.Text + "6";
                break;
            case "7":
                LblEquation.Text = LblEquation.Text + "7";
                break;
            case "8":
                LblEquation.Text = LblEquation.Text + "8";
                break;
            case "9":
                LblEquation.Text = LblEquation.Text + "9";
                break;
            case "/":
                LblEquation.Text = LblEquation.Text + "/";
                break;
            case "X":
                LblEquation.Text = LblEquation.Text + "X";
                break;
            case "-":
                LblEquation.Text = LblEquation.Text + "-";
                break;
            case "+":
                LblEquation.Text = LblEquation.Text + "+";
                break;
        }
	}
	private void Calculate(string equation)
	{
        int? output=null;
        string value=string.Empty;
        string operation;
        if ("0123456789".Contains(equation[equation.Length-1]))
        {
            for (int i = 0; i < equation.Length; i++)
            {
                if ("0123456789".Contains(equation[i]))
                {
                    value += equation[i];
                }
                else
                {
                    if (output==null)
                    {
                        output = int.Parse(value);
                    }
                    else switch (equation[i])
                    {
                        case '/':if (value == "0")
                            {
                                throw new Exception("Cannot divide by zero");
                            }
                            else
                            {
                                output = output / int.Parse(value);
                                value = String.Empty;
                            }
                            output = output / int.Parse(value);
                            break;
                        case 'X':
                                output = output * int.Parse(value);
                            break;
                        case '-':
                                output = output - int.Parse(value);
                                break;
                        case '+':
                                output = output + int.Parse(value);
                                break;
                    }
                }
            }
        }
        else
        {
            throw new Exception("End your equation with a number");
        }
	}
}