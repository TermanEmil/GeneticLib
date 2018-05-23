import matplotlib.pyplot as plt
import sys
import json

"""

"""

def extract_from_argv(argv):
	data = json.loads(argv)
	for i in range(len(data['points'])):
		data['points'][i]['x'] = float(data['points'][i]['x'])
		data['points'][i]['y'] = float(data['points'][i]['y'])

	return data

data = extract_from_argv(sys.argv[1])

x = [p['x'] for p in data['points']]
y = [p['y'] for p in data['points']]

graph_style = 'b-'
if 'graph_style' in data:
	graph_style = data['graph_style']

if 'title' in data:
	plt.title(data['title'])

plt.plot(x, y, graph_style)
plt.show()