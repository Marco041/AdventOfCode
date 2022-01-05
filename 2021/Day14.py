def polymer_reaction(polymer, rules):
    result = dict()
    for element in polymer:
        rules_result = rules.get(element, '')
        if(rules_result != ''):
            cnt = result.get(element[0]+rules_result, 0)
            result[element[0]+rules_result] = cnt+polymer[element]
            cnt = result.get(rules_result+element[1], 0)
            result[rules_result+element[1]] = cnt+polymer[element]
        else:
            cnt = result.get(element, 0)
            result[element] = cnt + polymer[element]
    return result

def element_count(polymer):
    cnt_result = dict()
    cnt_result[list(polymer.keys())[0][0]] = 1
    for value in polymer:
        cnt = cnt_result.get(value[1], 0) 
        cnt_result[value[1]] = cnt + polymer[value]
    return max(cnt_result.values())-min(cnt_result.values())

polymer = dict()
rules = dict()
fd = open("input14.txt")
first_line = fd.readline().rstrip()
for i in range(0, len(first_line)-1):
    cnt = polymer.get(first_line[i:i+2], 0)
    polymer[first_line[i:i+2]] = cnt+1

for i in fd:
    if("->" in i):
        element = i.rstrip().split()
        rules[element[0]] = element[2]

part1_polymer_state = dict()
for step in range(40):
    polymer = polymer_reaction(polymer, rules)
    if(step == 9):
        part1_polymer_state = polymer.copy()

print("Part 1: ", element_count(part1_polymer_state))
print("Part 2: ", element_count(polymer))