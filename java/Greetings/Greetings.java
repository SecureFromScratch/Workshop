import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardOpenOption;
import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

class Greetings
{
    private static final Path FILENAME = Paths.get("greetings.txt");

    public static void main(String[] a_args) throws IOException
    {
        System.out.print("Hello, do you have a greeting for your coworkers? (Press enter to just see others' greetings)\n");
        System.out.print(">>> ");

        final String newLine;
        try (final Scanner in = new Scanner(System.in))
        {
            newLine = in.nextLine();
        }

        if (ContainsShouting(newLine))
        {
            System.out.print("Please use moderate tones...\n");
            return;
        }

        System.out.print("Here are some greetings from others...\n");
        ShowGreetings(FILENAME);

        if (!newLine.isEmpty())
        {
            Files.write(FILENAME, Arrays.asList(newLine), new StandardOpenOption[]{ StandardOpenOption.CREATE, StandardOpenOption.APPEND });
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
