def parse_board_input(lines, line_size):
    n_boards = len(lines) // line_size
    boards = []
    for i in range(n_boards):
        for line in lines[line_size * i + 1 : line_size * (i + 1)]:
            boards.append([int(num) for num in line.split()])
    return boards

def calc_winner(board_size, boards, draws):
    score_list = []
    excluded_board = dict()
    for draw in draws:
        for line_num, line in enumerate(boards):
            for index, num in enumerate(line):
                if(draw == num):
                    boards[line_num][index] = 0
                    winner_board_numer =  line_num//board_size
                    if(winner_board_numer not in excluded_board):
                        index_board = winner_board_numer*board_size
                        col_sum = sum([x[index] for x in boards[index_board:index_board+board_size]])
                        if(sum(boards[line_num]) == 0 or col_sum == 0):
                            s = sum(sum(n) for n in boards[index_board:index_board+board_size])*draw
                            score_list.append(s)
                            excluded_board[winner_board_numer]=True
    return (score_list[0], score_list[-1])

fp = open("input4.txt")
board_size = 5
line_size = board_size+1
draws = [int(x) for x in fp.readline()[:-1].split(",")]
lines = fp.readlines()
boards = parse_board_input(lines, line_size)
result = calc_winner(board_size, boards, draws)
print("Part 1: ", result[0])
print("Part 2: ", result[1])