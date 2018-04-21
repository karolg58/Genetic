import argparse
from os import listdir
from os.path import isfile, join
import matplotlib.pyplot as plt
from matplotlib import colors as mcolors

parser = argparse.ArgumentParser()
parser.add_argument('-v','--versions', nargs='+', help='<Required> show plot for given versions', required=True)
parser.add_argument('-t','--isTimeBased', help="if true then x axis is time ms, else it is number of generation", required=False)
parser.add_argument('-p','--percent', help="percent of results(not implemented yet)", required=False)
parser.add_argument('-n','--numberOfBars', help="number of bars shown at plot (by maximum dataset)", required=False)
args = parser.parse_args()

percent = 100
if(args.percent):
     percent = args.percent
isTimeBased = True
if(args.isTimeBased):
     isTimeBased = args.isTimeBased

colors = list(mcolors.BASE_COLORS) + list(mcolors.CSS4_COLORS)
nr = 0
for v in args.versions:
    dirPath = join("data", "plots", v)

    fileNames = [f for f in listdir(dirPath) if isfile(join(dirPath, f))]

    for filePath in fileNames:
        generationNumber = []
        time = []
        fitness = []
        with open(join(dirPath, filePath), 'r') as f:
            lines = f.readlines()
        lines = lines[1:]
        for line in lines:
            values = line.split(',')
            generationNumber.append(int(values[0]))
            time.append(int(values[1]))
            fitness.append(int(values[2]))

        if(isTimeBased):
            x = time
        else:
            x = generationNumber

        plt.plot(x, fitness, colors[(nr)% len(colors)])
    nr += 1

print(colors[0:2])
plt.show()
    

