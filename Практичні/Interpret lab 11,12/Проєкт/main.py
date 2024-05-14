import math


items = {}
temp_name = 1


def calculate(line, string):
    global temp_name

    k = 0
    ind = []
    is_bool = False
    for i in range(0, len(line)):
        if line[i] == "(" and line[:i].endswith("log"):
            if len(ind) == 0:
                ind.append(i+1)
            k += 1
            is_bool = True

        if line[i] == ")" and is_bool:
            k -= 1

        if k == 0 and is_bool is True:
            ind.append(i)
            break
    if is_bool:
        temp = line[ind[0]: ind[1]]
        name, string = calculate(temp, string)
        temp = line[ind[0]: ind[1]]
        line = line.replace(temp, name)

    i = 0
    while i < len(line):
        ind = []
        if line[:i].endswith("log("):
            ind.append(i)
            for j in range(i+1, len(line)):
                if line[j].find(")") != -1:
                    ind.append(j)
                    break

            t = "t" + str(temp_name)
            temp_name += 1
            string += ("LOG " + line[ind[0]:ind[1]] + " " + t + '\n')
            temp = line[ind[0]-4:ind[1]+1]
            line = line.replace(temp, t)
            i = ind[0]-4
        i += 1

    k = 0
    ind = []
    is_bool = False
    for i in range(0, len(line)):
        if line[i] == "(" and line[:i].endswith("sqrt"):
            if len(ind) == 0:
                ind.append(i+1)
            k += 1
            is_bool = True

        if line[i] == ")" and is_bool:
            k -= 1

        if k == 0 and is_bool is True:
            ind.append(i)
            break
    if is_bool:
        temp = line[ind[0]: ind[1]]
        name, string = calculate(temp, string)
        temp = line[ind[0]: ind[1]]
        line = line.replace(temp, name)

    i = 0
    while i < len(line):
        ind = []
        if line[:i].endswith("sqrt("):
            ind.append(i)
            for j in range(i+1, len(line)):
                if line[j].find(")") != -1:
                    ind.append(j)
                    break

            t = "t" + str(temp_name)
            temp_name += 1
            string += ("SQRT " + line[ind[0]:ind[1]] + " " + t + '\n')
            temp = line[ind[0]-5:ind[1]+1]
            line = line.replace(temp, t)
            i = ind[0]-5
        i += 1

    k = 0
    ind = []
    is_bool = False
    for i in range(0, len(line)):
        if line[i] == "(":
            if len(ind) == 0:
                ind.append(i+1)
            k += 1
            is_bool = True

        if line[i] == ")":
            k -= 1

        if k == 0 and is_bool is True:
            ind.append(i)
            break
    if is_bool:
        temp = line[ind[0]: ind[1]]
        name, string = calculate(temp, string)
        temp = line[ind[0]-1: ind[1]+1]
        line = line.replace(temp, name)

    i = 0
    while i < len(line):
        ind = []
        if line[i] == "*":
            for j in range(i-1, -1, -1):
                if line[j].find("+") != -1 or line[j].find("-") != -1 or line[j].find("*") != -1 or line[j].find("/") != -1:
                    ind.append(j+1)
                    break
                if j == 0:
                    ind.append(j)
            for j in range(i+1, len(line)):
                if line[j].find("+") != -1 or line[j].find("-") != -1 or line[j].find("*") != -1 or line[j].find("/") != -1:
                    ind.append(j)
                    break
                if j == len(line)-1:
                    ind.append(j+1)

            t = "t" + str(temp_name)
            temp_name += 1
            string += ("MUL " + line[ind[0]:i] + " " + line[i+1:ind[1]] + " " + t + '\n')
            temp = line[ind[0]:ind[1]]
            line = line.replace(temp, t)
            i = ind[0]
        i += 1

    i = 0
    while i < len(line):
        ind = []
        if line[i] == "/":
            for j in range(i-1, -1, -1):
                if line[j].find("+") != -1 or line[j].find("-") != -1 or line[j].find("*") != -1 or line[j].find("/") != -1:
                    ind.append(j+1)
                    break
                if j == 0:
                    ind.append(j)
            for j in range(i+1, len(line)):
                if line[j].find("+") != -1 or line[j].find("-") != -1 or line[j].find("*") != -1 or line[j].find("/") != -1:
                    ind.append(j)
                    break
                if j == len(line)-1:
                    ind.append(j+1)

            t = "t" + str(temp_name)
            temp_name += 1
            string += ("DIV " + line[ind[0]:i] + " " + line[i+1:ind[1]] + " " + t + '\n')
            temp = line[ind[0]:ind[1]]
            line = line.replace(temp, t)
            i = ind[0]
        i += 1

    i = 0
    while i < len(line):
        ind = []
        if line[i] == "-":
            for j in range(i-1, -1, -1):
                if line[j].find("+") != -1 or line[j].find("-") != -1 or line[j].find("*") != -1 or line[j].find("/") != -1:
                    ind.append(j+1)
                    break
                if j == 0:
                    ind.append(j)
            for j in range(i+1, len(line)):
                if line[j].find("+") != -1 or line[j].find("-") != -1 or line[j].find("*") != -1 or line[j].find("/") != -1:
                    ind.append(j)
                    break
                if j == len(line)-1:
                    ind.append(j+1)

            t = "t" + str(temp_name)
            temp_name += 1
            string += ("SUB " + line[ind[0]:i] + " " + line[i+1:ind[1]] + " " + t + '\n')
            temp = line[ind[0]:ind[1]]
            line = line.replace(temp, t)
            i = ind[0]
        i += 1

    i = 0
    while i < len(line):
        ind = []
        if line[i] == "+":
            for j in range(i-1, -1, -1):
                if line[j].find("+") != -1 or line[j].find("-") != -1 or line[j].find("*") != -1 or line[j].find("/") != -1:
                    ind.append(j+1)
                    break
                if j == 0:
                    ind.append(j)
            for j in range(i+1, len(line)):
                if line[j].find("+") != -1 or line[j].find("-") != -1 or line[j].find("*") != -1 or line[j].find("/") != -1:
                    ind.append(j)
                    break
                if j == len(line)-1:
                    ind.append(j+1)

            t = "t" + str(temp_name)
            temp_name += 1
            string += ("ADD " + line[ind[0]:i] + " " + line[i+1:ind[1]] + " " + t + '\n')
            temp = line[ind[0]:ind[1]]
            line = line.replace(temp, t)
            i = ind[0]
        i += 1

    return line, string


def numerate(i_t, string):
    k = 1
    n = 1
    key_t = 0
    for l in string:
        if l == '\n':
            n += 1

    res = n
    for l in range(i_t+1, len(lines_lst)):
        if lines_lst[l].find("{") != -1:
            k += 1

        if lines_lst[l].find("}") != -1:
            k -= 1

        if k == 0:
            key_t = l - i_t - 1
            break

        if lines_lst[l].find("if") != -1 and lines_lst[l].find("[") != -1:
            res += 1

        if lines_lst[l].find("while") != -1 and lines_lst[l].find("[") != -1:
            res += 2

        if lines_lst[l].find(".") != -1 and lines_lst[l].find("(") != -1:
            res += 1
            continue

        if lines_lst[l].find("=") != -1 and lines_lst[l].find("==") == -1:
            first, second = lines_lst[l].split("=")
        if lines_lst[l].find("=") != -1 and second.find("[") != -1 and lines_lst[l].find("==") == -1:
            second = second.replace(" ", "")
            if second[0] != "[":
                res += 2
                continue
            elif second == "[]":
                res += 1
                continue
            else:
                for j in lines_lst[l]:
                    if j == "," or j == "=":
                        res += 1
                res += 1
                continue

        if lines_lst[l].find("log") != -1 or lines_lst[l].find("sqrt") != -1:
            res += 1

        for j in lines_lst[l]:
            if j == "+" or j == "-" or j == "*" or j == "/" or j == "=" or j == ">":
                res += 1
    return res+1, key_t, n


# ---------- Compiler ------------

f = open("test.txt", "r")
lines_lst = []
for line in f:
    lines_lst.append(line)
f.close()

f = open('test2.txt', 'w')
i = 0
string = ""
temp_arr = []
while i != len(lines_lst):
    line = lines_lst[i].rstrip()
    line = line.replace(' ', '')

    if line == "{" or line == "}":
        for l in range(0, len(temp_arr)):
            if temp_arr[l][0] == 0:
                string += "GOTO " + str(temp_arr[l][1]) + '\n'
                del temp_arr[l]
        i += 1
        continue

    if line.find(".append(") != -1:
        line = line.replace(".append(", " ")
        line = line[:-1]
        first, second = line.split()
        line = "ADDMAT " + first + " " + second

    if line.find(".sort()") != -1:
        line = line.replace(".sort()", "")
        line = "SRT " + line

    if line.find(".reverse()") != -1:
        line = line.replace(".reverse()", "")
        line = "REV " + line

    if line.find(".delete(") != -1:
        line = line.replace(".delete(", " ")
        line = line.replace(")", "")
        fis, s = line.split()
        line = "DEL " + fis + " " + s

    if line.find("read>") != -1:
        line = line[5:]
        line = "READ " + line

    if line.find("write>") != -1:
        line = line[6:]
        line = "WRITE " + line

    if line.find("if[") != -1:
        line = line[3:]
        line = line[:-2]

        if line.find("==") == -1:
            a, string = calculate(line, string)
            b, tp, tp1 = numerate(i, string)

            line = "GOTOIFNOT " + a + " " + str(b)
        else:
            leftp, rightp = line.split("==")
            la, string = calculate(leftp, string)
            ra, string = calculate(rightp, string)
            b, tp, tp1 = numerate(i, string)

            t = "t" + str(temp_name)
            temp_name += 1

            line = "CMP " + la + " " + ra + " " + t + "\n"

            line += "GOTOIFNOT " + t + " " + str(b + 1)

    if line.find("ifnot[") != -1:
        line = line[6:]
        line = line[:-2]

        if line.find("==") == -1:
            a, string = calculate(line, string)
            b, tp, tp1 = numerate(i, string)

            line = "GOTOIF " + a + " " + str(b)
        else:
            leftp, rightp = line.split("==")
            la, string = calculate(leftp, string)
            ra, string = calculate(rightp, string)
            b, tp, tp1 = numerate(i, string)

            t = "t" + str(temp_name)
            temp_name += 1

            line = "CMP " + la + " " + ra + " " + t + "\n"

            line += "GOTOIFNOT " + t + " " + str(b)

    if line.find("=") != -1:
        first, second = line.split("=")

        if second == "[]":
            line = "DIM " + first
        elif second[0] == "[":
            line = "DIM " + first + '\n'
            second = second.replace("[", "")
            second = second.replace("]", "")
            temp_ar = second.split(",")
            for l in temp_ar:
                line += "ADDMAT " + first + " " + l + '\n'
            line = line[:-1]
        elif second.find("[") != -1 and second.find("]") != -1:
            second = second.replace("[", " ")
            second = second.replace("]", "")
            fis, s = second.split()

            t = "t" + str(temp_name)
            temp_name += 1
            line = "GET " + fis + " " + s + " " + t + '\n'
            if first[-1] == "]":
                first = first.replace("[", " ")
                first = first.replace("]", "")
                first, temp = first.split()
                line += "MOV " + first + " " + temp + " " + t
            else:
                line += "COPY " + t + " " + first
        elif first[-1] == "]":
            first = first.replace("[", " ")
            first = first.replace("]", "")
            first, temp = first.split()
            line = "MOV " + first + " " + temp + " " + second
        elif second.find("+") == -1 and second.find("-") == -1 and second.find("*") == -1 and second.find("/") == -1 and second.find("sqrt") == -1 and second.find("log") == -1:
            line = "COPY " + second + " " + first
        elif second.find("-") == 0:
            t_second = second
            second = second[1:]
            if second.find("+") == -1 and second.find("-") == -1 and second.find("*") == -1 and second.find("/") == -1 and second.find("sqrt") == -1 and second.find("log") == -1:
                line = "COPY " + t_second + " " + first

    if line.find("=") != -1:
        first, second = line.split("=")
        second, string = calculate(second, string)
        line = "COPY " + second + " " + first

    if line.find("while[") != -1:
        line = line[6:]
        line = line[:-2]

        tp, tp1, n = numerate(i, string)
        a, string = calculate(line, string)
        b, key_t, tp = numerate(i, string)
        temp_arr.append([key_t+1, n])

        line = "GOTOIFNOT " + a + " " + str(b+1)

    if line.find("whilenot[") != -1:
        line = line[9:]
        line = line[:-2]

        tp, tp1, n = numerate(i, string)
        a, string = calculate(line, string)
        b, key_t, tp = numerate(i, string)
        temp_arr.append([key_t+1, n])
        line = "GOTOIF " + a + " " + str(b+1)

    for j in range(0, len(temp_arr)):
        temp_arr[j][0] -= 1
    i += 1
    string += line + '\n'

f.write(string + '\n')
f.close()

# ---------- Compiler ------------


# ---------- Virtual ------------

f = open('test2.txt', 'r')
lines_lst = []
for line in f:
    lines_lst.append(line)
f.close()

i = 0
while i != len(lines_lst):
    line = lines_lst[i].rstrip()

    if line.find("READ") != -1:
        line = line[5:]
        res = input("Input " + line + " --> ")
        if res.isdigit():
            items[line] = float(res)
        else:
            items[line] = res

    if line.find("WRITE") != -1:
        line = line[6:]
        print(items.get(line))

    if line.find("DIM") != -1:
        line = line[4:]
        items[line] = []

    if line.find("ADDMAT") != -1:
        line = line[7:]
        a, b = line.split()
        if items.get(b) is None:
            if b.isdigit():
                items[a].append(float(b))
            else:
                b = b.replace("\"", "")
                items[a].append(b)
        else:
            items[a].append(items.get(b))

    if line.find("MOV") != -1:
        line = line[4:]
        a, b, res = line.split()
        if items.get(res) is None:
            if res.isdigit():
                items[a][int(b)] = float(res)
            else:
                res = res.replace("\"", "")
                items[a][int(b)] = res
        else:
            items[a][int(b)] = items.get(res)

    if line.find("GET") != -1:
        line = line[4:]
        a, b, res = line.split()
        items[res] = items[a][int(b)]

    if line.find("SRT") != -1:
        line = line[4:]
        items[line].sort()

    if line.find("REV") != -1:
        line = line[4:]
        items[line].reverse()

    if line.find("DEL") != -1:
        line = line[4:]
        a, b = line.split()
        del items[a][int(b)]

    if line.find("CMP") != -1:
        line = line[4:]
        a, b, res = line.split()
        if items.get(a) is None and items.get(b) is None:
            items[res] = a == b
        elif items.get(a) is None:
            if a.isdigit():
                items[res] = float(a) == items.get(b)
            else:
                items[res] = a == items.get(b)
        elif items.get(b) is None:
            if b.isdigit():
                items[res] = items.get(a) == float(b)
            else:
                items[res] = items.get(a) == b
        else:
            items[res] = items.get(a) == items.get(b)

    if line.find("LOG") != -1:
        line = line[4:]
        a, res = line.split()
        if items.get(a) is None:
            items[res] = math.log10(float(a))
        else:
            items[res] = math.log10(items.get(a))

    if line.find("SQRT") != -1:
        line = line[5:]
        a, res = line.split()
        if items.get(a) is None:
            items[res] = math.sqrt(float(a))
        else:
            items[res] = math.sqrt(items.get(a))

    if line.find("ADD") != -1:
        line = line[4:]
        a, b, res = line.split()
        if items.get(a) is None and items.get(b) is None:
            items[res] = float(a) + float(b)
        elif items.get(a) is None:
            items[res] = float(a) + items.get(b)
        elif items.get(b) is None:
            items[res] = items.get(a) + float(b)
        else:
            items[res] = items.get(a) + items.get(b)

    if line.find("SUB") != -1:
        line = line[4:]
        a, b, res = line.split()
        if items.get(a) is None and items.get(b) is None:
            items[res] = float(a) - float(b)
        elif items.get(a) is None:
            items[res] = float(a) - items.get(b)
        elif items.get(b) is None:
            items[res] = items.get(a) - float(b)
        else:
            items[res] = items.get(a) - items.get(b)

    if line.find("MUL") != -1:
        line = line[4:]
        a, b, res = line.split()
        if items.get(a) is None and items.get(b) is None:
            items[res] = float(a) * float(b)
        elif items.get(a) is None:
            items[res] = float(a) * items.get(b)
        elif items.get(b) is None:
            items[res] = items.get(a) * float(b)
        else:
            items[res] = items.get(a) * items.get(b)

    if line.find("DIV") != -1:
        line = line[4:]
        a, b, res = line.split()
        if items.get(a) is None and items.get(b) is None:
            items[res] = float(a) / float(b)
        elif items.get(a) is None:
            items[res] = float(a) / items.get(b)
        elif items.get(b) is None:
            items[res] = items.get(a) / float(b)
        else:
            items[res] = items.get(a) / items.get(b)

    if line.find("GOTOIF ") != -1:
        line = line[7:]
        a, b = line.split()
        if items.get(a) != 0:
            i = int(b)-2

    if line.find("GOTOIFNOT") != -1:
        line = line[10:]
        a, b = line.split()
        if items.get(a) == 0:
            i = int(b)-2

    if line.find("GOTO ") != -1:
        b = line[5:]
        i = int(b)-2

    if line.find("COPY") != -1:
        line = line[5:]
        a, b = line.split()
        if items.get(a) is None:
            if a.isdigit():
                items[b] = float(a)
            else:
                items[b] = a
        else:
            items[b] = items.get(a)

    i += 1

# ---------- Virtual ------------
