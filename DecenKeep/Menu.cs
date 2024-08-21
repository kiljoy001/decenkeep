using System.Data.SQLite;

namespace DecenKeep;

public class Menu
{
    private readonly IDatabaseService _databaseService;
    private readonly IFileService _fileService;
    private readonly IWalletService _walletService;
    private readonly IBlockchainService _blockchainService;

    public Menu(bool isDebug, IDatabaseService databaseService, IFileService fileService,
        IBlockchainService blockchainService)
    {
        IsDebug = isDebug;
        _databaseService = databaseService;
        _fileService = fileService;
        _blockchainService = blockchainService;
    }

    public bool IsDebug { get; set; }
    private void DisplayGreetings()
    {
        Console.WriteLine(
            "Welcome to DecenKeep, an aergolite based, backup service.");
    }

    private void DisplayOptions()
    {
        Console.WriteLine("Please choose an option:");
        Console.WriteLine("1) Create a new backup directory");
        Console.WriteLine("2) Add file to backup");
        Console.WriteLine("3) Remove a backup file");
        Console.WriteLine("4) List backed-up files");
        Console.WriteLine("5) Pin backed up files to IPFS");
        Console.WriteLine("6) List pinned files");
        Console.WriteLine("7) Encrypt secret");
        Console.WriteLine("8) Post secret & database CID to Emercoin network");
        Console.WriteLine("9) Help");
        Console.WriteLine("10) Exit program");
    }
    private Task CreateBackupDirectoryAsync()
    {
        Console.WriteLine("Creating a new backup directory...");
        // Implement your logic here
        return Task.CompletedTask;
    }

    private Task AddFileToBackupAsync()
    {
        Console.WriteLine("Adding file to backup...");
        // Implement your logic here
        return Task.CompletedTask;
    }

    private Task RemoveBackupFileAsync()
    {
        Console.WriteLine("Removing a backup file...");
        // Implement your logic here
        return Task.CompletedTask;
    }

    private Task ListBackedUpFilesAsync()
    {
        Console.WriteLine("Listing backed-up files...");
        // Implement your logic here
        return Task.CompletedTask;
    }

    private Task PinBackedUpFilesToIpfsAsync()
    {
        Console.WriteLine("Pinning backed-up files to IPFS...");
        // Implement your logic here
        return Task.CompletedTask;
    }

    private Task ListPinnedFilesAsync()
    {
        Console.WriteLine("Listing pinned files...");
        // Implement your logic here
        return Task.CompletedTask;
    }

    private Task EncryptSecretAsync()
    {
        Console.WriteLine("Encrypting secret...");
        // Implement your logic here
        return Task.CompletedTask;
    }

    private Task PostSecretAndCidToEmercoinAsync()
    {
        Console.WriteLine("Posting secret and database CID to Emercoin network...");
        // Implement your logic here
        return Task.CompletedTask;
    }

    private Task DisplayHelpAsync()
    {
        Console.WriteLine("Displaying help information...");
        // Implement your logic here
        return Task.CompletedTask;
    }

    private Task ExitProgramAsync()
    {
        Console.WriteLine("Exiting program...");
        Environment.Exit(0);
        return Task.CompletedTask;
    }
    public async Task DisplayTestMenuAsync()
    {
        DisplayGreetings();
        while (true)
        {
            DisplayOptions();
            string? optionChoice = Console.ReadLine();

            if (Enum.TryParse<MenuOption>(optionChoice, out var menuOption))
            {
                switch (menuOption)
                {
                    case MenuOption.CreateBackupDirectory:
                        await CreateBackupDirectoryAsync();
                        break;
                    case MenuOption.AddFileToBackup:
                        await AddFileToBackupAsync();
                        break;
                    case MenuOption.RemoveBackupFile:
                        await RemoveBackupFileAsync();
                        break;
                    case MenuOption.ListBackedUpFiles:
                        await ListBackedUpFilesAsync();
                        break;
                    case MenuOption.PinBackedUpFilesToIpfs:
                        await PinBackedUpFilesToIpfsAsync();
                        break;
                    case MenuOption.ListPinnedFiles:
                        await ListPinnedFilesAsync();
                        break;
                    case MenuOption.EncryptSecret:
                        await EncryptSecretAsync();
                        break;
                    case MenuOption.PostSecretAndCidToEmercoin:
                        await PostSecretAndCidToEmercoinAsync();
                        break;
                    case MenuOption.Help:
                        await DisplayHelpAsync();
                        break;
                    case MenuOption.Exit:
                        await ExitProgramAsync();
                        return;
                    default:
                        Console.WriteLine("Invalid Option, please try another option.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid Option, please try another option.");
            }
        }
    }
}