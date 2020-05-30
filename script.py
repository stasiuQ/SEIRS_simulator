import os
filename = ".gitignore"

with open(filename) as f:
    content = f.readlines()
# you may also want to remove whitespace characters like `\n` at the end of each line
content = [x.strip() for x in content] 

for item in content:
    if(item != "" and item[0] != '#'):
        os.system("git rm --cached "+ item)
