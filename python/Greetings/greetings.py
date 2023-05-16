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

def Greetings():
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

