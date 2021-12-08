commands = [x.split() for x in open("input2.txt").readlines()]
x=0
y=0
for command in commands:
    if command[0] == 'forward':
        x += int(command[1])
    if command[0] == 'down':
        y += int(command[1])
    if command[0] == 'up':
        y -= int(command[1])
print("Part 1: ", x*y)

x=0
y=0
aim=0
for command in commands:
    if command[0] == 'forward':
        x += int(command[1])
        y += int(command[1])*aim
    if command[0] == 'down':
        aim += int(command[1])
    if command[0] == 'up':
        aim -= int(command[1])
print("Part 2: ", x*y)