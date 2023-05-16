#include "LogAux.h"
#include <cassert>
#include <algorithm>
#include <iterator>

namespace LogAux
{

std::string Scrub(const std::string &a_untrustedValue)
{
	std::string result;
	result.reserve(a_untrustedValue.size());
	std::transform(
		a_untrustedValue.begin(), a_untrustedValue.end(), 
		std::back_inserter(result),
		[](char c) { return std::isprint(c) ? c : '?'; });

	return result;
}

static std::string Obscure(const std::string &a_sensitive, size_t a_offset, size_t a_len)
{
	assert(a_len > 0);
	assert(a_offset < a_sensitive.size());
	assert(a_offset + a_len <= a_sensitive.size());

	if (a_len < 5) {
		std::string obscured;
		obscured.reserve(5);
		obscured.push_back(a_sensitive[a_offset]);
		obscured.append("***", 3);
		obscured.push_back(a_sensitive[a_offset + a_len - 1]);
		assert(obscured.size() == 5);
		return obscured;
	}
	else {
		std::string obscured{a_sensitive.begin() + a_offset, a_sensitive.begin() + a_offset + a_len};
		for (size_t obscureIdx = 1 ; obscureIdx < a_len - 1 ; ++obscureIdx)
		{
			obscured[obscureIdx] = '*';
		}
		return obscured;
	}
}

std::string Obscure(const std::string &a_sensitive)
{
	return Obscure(a_sensitive, 0, a_sensitive.size());
}

std::string ObscureUsername(const std::string &a_username)
{
	size_t backslashPos = a_username.find('\\');
	if (backslashPos == std::string::npos)
	{
		return Obscure(a_username, 0, a_username.size());
	}

	return Obscure(a_username, 0, backslashPos) + '\\' + Obscure(a_username, backslashPos + 1, a_username.size() - (backslashPos + 1));
}

}
