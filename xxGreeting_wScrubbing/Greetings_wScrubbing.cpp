#include <iostream>
#include <fstream>
#include <string>
#include <array>
#include "spdlog/spdlog.h"
#include "spdlog/fmt/ranges.h"
#include "spdlog/sinks/basic_file_sink.h"
#include "Userinfo.h"
#include "LogAux.h"

void ShowGreetings(const std::string& a_filename)
{
	std::ifstream file(a_filename);
	if (file.bad())
	{
		spdlog::warn("Error opening Greetings file {}. Probably doesn't exist.", a_filename);
		return;
	}

	while (file.good())
	{
		std::string line;
		std::getline(file, line);
		if (!line.empty())
		{
			std::cout << "* " << line << "\n";
		}
	}
	if (file.bad())
	{
		spdlog::critical("Error reading from Greetings file {}", a_filename);
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

static void SetupLogToFile(const std::string& a_logfilePath)
{
	auto fileLogger = spdlog::basic_logger_mt("main", a_logfilePath.c_str(), true);
    spdlog::set_default_logger(fileLogger);
    spdlog::flush_every(std::chrono::seconds(1));
}

int main()
{
	SetupLogToFile("greetings.log");

    std::string username = GetDomainName() + '\\' + GetUsername();
	spdlog::set_level(spdlog::level::debug);

    spdlog::debug(LogAux::Scrub("This\n\t\a is\n scrubbed\n"));
    spdlog::debug("Obfuscated username: " + LogAux::ObscureUsername(username));

	std::cout << "Hello, do you have a greeting for your coworkers? (Press enter to just see others' greetings)\n";
	std::cout << ">>> ";
	std::string line;
	std::getline(std::cin, line);

	if (ContainsShouting(line))
	{
        spdlog::error("Shout attempt by {}: \"{}\"", username, line);
		std::cout << "Please use moderate tones...\n";
		return 1;
	}

	std::cout << "Here are some greetings from others...\n";
	ShowGreetings(FILENAME);

	if (!line.empty())
	{
		std::ofstream out(FILENAME, std::ios_base::app);
		out << line << "\n";
		if (out.bad())
		{
			spdlog::warn("Error adding to file {}", FILENAME);
		}
		else
		{
	        spdlog::info("{} added: \"{}\"", username, line);
		}
	}

	return 0;
}
