#pragma once

#include <string>

namespace LogAux
{

std::string Scrub(const std::string &a_untrustedValue);
std::string Obscure(const std::string &a_username);
std::string ObscureUsername(const std::string &a_username);

}
