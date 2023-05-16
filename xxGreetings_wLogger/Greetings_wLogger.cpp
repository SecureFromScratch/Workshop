#include <iostream>
#include <fstream>
#include <string>
#include <array>
#include "spdlog/spdlog.h"
#include "spdlog/fmt/ranges.h"
#include "spdlog/sinks/basic_file_sink.h"
#include "Userinfo.h"

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

    spdlog::debug("This will be a DEBUGGING log entry");
    spdlog::info("This will be an INFORMATIONAL log entry");
    spdlog::warn("This will be a WARNING log entry");
    spdlog::error("This will be an ERROR log entry");
    spdlog::critical("This will be a CRITICAL log entry");

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
