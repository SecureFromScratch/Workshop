import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.logging.FileHandler;
import java.util.logging.SimpleFormatter;
import java.io.IOException;
import java.lang.RuntimeException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardOpenOption;
import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

final class Greetings
{
	private static void setupFileLogger(String a_name, String a_filepath) {
		final Logger logger = Logger.getLogger("main");

		try {  
			// This block configure the logger with handler and formatter  
			final FileHandler fh = new FileHandler(a_filepath);  
			logger.addHandler(fh);
			SimpleFormatter formatter = new SimpleFormatter();  
			fh.setFormatter(formatter);  
		//} catch (SecurityException e) {  
		//    e.printStackTrace();  
		} catch (IOException e) {  
		    throw new RuntimeException(e.getMessage());  
		}
		logger.setUseParentHandlers(false);
	}

	private static final Path FILENAME = Paths.get("greetings.txt");

    public static void main(String[] a_args) throws IOException
    {
    	setupFileLogger("main", "grettings.log");
		final Logger theLogger = Logger.getLogger("main");

        String username = Userinfo.getDomainName() + '\\' + Userinfo.getUsername();

        theLogger.log(Level.INFO, LogAux.Scrub("This\n\t\f is\n scrubbed\n"));
        theLogger.log(Level.INFO, "Obfuscated username: " + LogAux.ObscureUsername(username));

        System.out.print("Hello, do you have a greeting for your coworkers? (Press enter to just see others' greetings)\n");
        System.out.print(">>> ");

        final String newLine;
        try (final Scanner in = new Scanner(System.in))
        {
            newLine = in.nextLine();
        }

        if (ContainsShouting(newLine))
        {
            theLogger.log(Level.WARNING, "Shout attempt by " + username + ": \"" + newLine + "\"");
            System.out.print("Please use moderate tones...\n");
            return;
        }

        System.out.print("Here are some greetings from others...\n");
        ShowGreetings(FILENAME);

        if (!newLine.isEmpty())
        {
        	try {
	            Files.write(FILENAME, Arrays.asList(newLine), new StandardOpenOption[]{ StandardOpenOption.CREATE, StandardOpenOption.APPEND });
	            theLogger.log(Level.INFO, username + " added: \"" + newLine + "\"");
        	}
        	catch (Exception ex) {
                theLogger.log(Level.SEVERE, "Error writing to greetings file " + FILENAME + ": " + ex.getMessage());
        	}
        }
    }

    private static void ShowGreetings(Path a_filename)
    {
        try
        {
            List<String> lines = Files.readAllLines(a_filename);
            for (String line : lines)
            {
                System.out.print("* ");
                System.out.println(line);
            }
        }
        catch (IOException ex)
        {
            // ignore
    		final Logger theLogger = Logger.getLogger("main");
            theLogger.log(Level.SEVERE, "Error reading from Greetings file " + a_filename + ": " + ex.getMessage());
        }
    }

    private static boolean ContainsShouting(String a_greeting)
    {
        int contigousUpperCase = 0;
        for (char c : a_greeting.toCharArray())
        {
            contigousUpperCase = Character.isUpperCase(c) ? contigousUpperCase + 1 : 0;
            if (3 == contigousUpperCase)
            {
                return true;
            }
        }

        return false;
    }
}
