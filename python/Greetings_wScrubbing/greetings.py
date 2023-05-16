import logging
import os
import LogAux

def ContainsShouting(a_greeting):
	contigousUpperCase = 0;
	for c in a_greeting:
		contigousUpperCase = contigousUpperCase + 1 if c.isupper() else 0
		if 3 == contigousUpperCase:
			return True

	return False

def ShowGreetings(a_filename):
	try:
		with open(a_filename, 'r') as file:
			lines = file.readlines()
			for line in lines:
				print('* ', line, end='')
	except ex:
		logging.warning(f"Error reading from Greetings file {a_filename}: {ex}");

FILENAME = 'greetings.txt'

logging.basicConfig(filename='greetings.log',
				filemode='a',
				format='%(asctime)s,%(msecs)d %(name)s %(levelname)s %(message)s',
				datefmt='%H:%M:%S',
				level=logging.DEBUG)

def Greetings():
	username = os.environ['userdomain'] + '\\' + os.getlogin()

	logging.debug(LogAux.Scrub("This\n\t\a is\n scrubbed\n"));
	logging.debug(f"Obfuscated username: {LogAux.ObscureUsername(username)}");

	print("Hello, do you have a greeting for your coworkers? (Press enter to just see others' greetings)")
	print(">>>", end=' ')

	newLine = input()

	if ContainsShouting(newLine):
		logging.error(f'Shout attempt by {username}: "{newLine}"');
		print("Please use moderate tones...")
		return None

	print("Here are some greetings from others...")
	ShowGreetings(FILENAME)

	if len(newLine) != 0:
		try:
			with open(FILENAME, 'a') as file:
				file.writelines([newLine, '\n'])
			logging.info(f'{username} added: "{newLine}"');
		except ex:
			logging.warning(f"Error writing to greetings file {FILENAME}: {ex}");

Greetings()
