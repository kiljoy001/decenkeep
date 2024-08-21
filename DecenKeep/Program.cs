namespace DecenKeep;

class Program
{
    private static bool _debugMode = false;
    private static bool _useTestnet = false;
    private static bool _useAdminMode = false;
    
    static void Main(string[] args)
    {
        foreach (var arg in args)
        {
            if (arg.Equals("--debug", StringComparison.OrdinalIgnoreCase))
            {
                _debugMode = true;
            }
            else if (arg.Equals("--testnet", StringComparison.OrdinalIgnoreCase))
            {
                _useTestnet = true;
            }
        }

        if (_debugMode)
        {
            Console.WriteLine("Debug mode enabled.");
        }
        
        if (_useTestnet)
        {
            Console.WriteLine("Using Emercoin testnet.");
        }
        //TO-DO implement application features
        // var program = new Menu();
        // await program.DisplayTestMenuAsync();
    }

}


