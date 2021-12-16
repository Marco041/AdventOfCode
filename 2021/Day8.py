def get_string_whith_len(e: list, l: int) -> set:
    return [s for s in e if len(s) == l]

def shared_char(string1, string2):
    count = 0
    for letter in string1:
        count += string2.count(letter)
    return count

lines = open("input8.txt").readlines()
decode = dict()
cnt_part1, cnt_part2 = 0, 0
for line in lines:
    patterns, output = line.split("|")
    patterns = [tuple(sorted(pattern)) for pattern in patterns.split()]
    outputs = [tuple(sorted(output)) for output in output.split()]

    one = get_string_whith_len(patterns, 2)[0]
    four = get_string_whith_len(patterns, 4)[0]
    decode[one] = 1
    decode[four] = 4
    decode[get_string_whith_len(patterns, 3)[0]] = 7
    decode[get_string_whith_len(patterns, 7)[0]] = 8

    zero_six_nine = get_string_whith_len(patterns, 6)
    for num in zero_six_nine:
        if(shared_char(num, one) == 1):
            decode[num] = 6
            six = num
        elif(shared_char(num, four) == 4):
            decode[num] = 9
        elif(shared_char(num, four) == 3):
            decode[num] = 0

    two_three_five = get_string_whith_len(patterns, 5)
    for num in two_three_five:
        if(shared_char(num, one) == 2):
            decode[num] = 3
        elif(shared_char(num, six) == 5):
            decode[num] = 5
        elif(shared_char(num, six) == 4):
            decode[num] = 2
    output_number = ""
    for output in outputs:
        if(len(output) in (2,3,4,7)):
            cnt_part1 += 1
        output_number += str(decode[output])
    cnt_part2 += int(output_number)

print("Part 1: ", cnt_part1)
print("Part 2: ", cnt_part2)
    