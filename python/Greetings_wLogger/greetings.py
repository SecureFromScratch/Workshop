import logging
import os

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
	except:
		pass

FILENAME = 'greetings.txt'

logging.basicConfig(filename='greetings.log',
				filemode='a',
				format='%(asctime)s,%(msecs)d %(name)s %(levelname)s %(message)s',
				datefmt='%H:%M:%S',
				level=logging.DEBUG)

def Greetings():
	username = os.environ['userdomain'] + '\\' + os.getlogin()

	logging.debug("This will be an DEBUGGING log entry");
	logging.info("This will be an INFORMATIONAL log entry");
	logging.warning("This will be an WARNING log entry");
	logging.error("This will be an ERROR log entry");
	logging.critical("This will be an CRITICAL log entry");

	print("Hello, do you have a greeting for your coworkers? (Press enter to just see others' greetings)")
	print(">>>", end=' ')

	newLine = input()

	if ContainsShouting(newLine):
		print("Please use moderate tones...")
		return None

	print("Here are some greetings from others...")
	ShowGreetings(FILENAME)

	if len(newLine) != 0:
		with open(FILENAME, 'a') as file:
			file.writelines([newLine, '\n'])

Greetings()

