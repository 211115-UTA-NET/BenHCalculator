using System.IO;
using System.Threading.Tasks;

var operations = new Dictionary<string, Func<double, double, double>>
{
    {"x", (x, y) => x * y },
    {"/", (x, y) => x / y },
    {"+", (x, y) => x + y },
    {"-", (x, y) => x - y },
    {"^", (x, y) => Math.Pow(x, y) },
    {"%", (x, y) => x % y }
};


int a = 0;
int b = 0;
string op = " ";//operation to be performed
string userCommand = " ";//used for dialogue options
string result = " "; //the full equation 
string fileName = " ";
double answer = 0; //the number after the equals sign

Console.WriteLine("***********CALCULATOR APP***********");
Console.WriteLine("Read input from file? (Y/N)");
userCommand = Console.ReadLine();

//If reading from File
if(userCommand == "Y"){
        
    Console.WriteLine("Which File?");
    fileName = Console.ReadLine();
    
    foreach (string line in System.IO.File.ReadLines($"{fileName}")){
        a = line[0] - '0';//subtracting '0' from a char converts it to an int
        op = Char.ToString(line[1]);
        b = line[2] - '0';
        answer = operations[op](a , b);
        result += $"\n{a}{op}{b} = {answer}";
        Console.WriteLine($"{a}{op}{b} = {answer}"); 
    } 

//If not reading from File
}else{
    //Repeats until user enters 'e' when prompted
    do{
        Console.WriteLine("Enter your first number, followed by the operation and then a second number");
        
        //Reads user input from console, then plugs into Func based on the operation given
        a = Convert.ToInt32(Console.ReadLine());
        op = Console.ReadLine();
        b = Convert.ToInt32(Console.ReadLine());
        answer = operations[op](a , b);
        result += $"\n{a}{op}{b} = {answer}";
        Console.WriteLine($"{a}{op}{b} = {answer}");

        
        Console.WriteLine("Enter 'e' to exit, anything else to continue");
        userCommand = Console.ReadLine();
    //breaks loop and proceeds to print to file prompt if user enters 'e'
    }while (userCommand != "e");
}

//Prompts user to enter a fileName for result to be appended on if desired
Console.WriteLine("Print to a file? (Y/N)");
userCommand = Console.ReadLine();

if(userCommand == "Y"){
    Console.WriteLine("Which File?");
    fileName = Console.ReadLine();
    using StreamWriter file = new($"{fileName}", append: true);
    await file.WriteLineAsync(result);
}