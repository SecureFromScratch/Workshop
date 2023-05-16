#include <iostream>
#include <fstream>
#include <string>
#include <array>

void ShowGreetings(const std::string& a_filename)
{
	std::ifstream file(a_filename);
	while (file.good())
	{
		std::string line;
		std::getline(file, line);
		if (!line.empty())
		{
			std::cout << "* " << line << "\n";
		}
	}
}

bool ContainsShouting(const std::string& a_greeting)
{
	size_t contigousUpperCase = 0;
	for (char c : a_greeting)
	{
		contigousUpperCase = isupper(c) ? contigousUpperCase + 1 : 0;
		if (3 == contigousUpperCase)
		{
			return true;
		}
	}

	return false;
}

static constexpr const char *FILENAME = "greetings.txt";

int main()
{
	std::cout << "Hello, do you have a greeting for your coworkers? (Press enter to just see others' greetings)\n";
	std::cout << ">>> ";
	std::string line;
	std::getline(std::cin, line);

	if (ContainsShouting(line))
	{
		std::cout << "Please use moderate tones...\n";
		return 1;
	}

	std::cout << "Here are some greetings from others...\n";
	ShowGreetings(FILENAME);

	if (!line.empty())
	{
		std::ofstream out(FILENAME, std::ios_base::app);
		out << line << "\n";
	}

	return 0;
}
