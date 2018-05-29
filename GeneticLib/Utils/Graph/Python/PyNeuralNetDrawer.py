import SocketProxy
import json
import sys
import socket
import matplotlib.pyplot as plt
import matplotlib.animation as animation
import time

def clamp(t, min_v, max_v):
	if t < min_v:
		return min_v
	elif t > max_v:
		return max_v
	else:
		return t

def inverse_lerp(min_v, max_v, value):
	value = clamp(value, min_v, max_v)

	step = abs(max_v) + abs(min_v) + abs(value)
	min_v += step
	value += step
	max_v += step

	result = (value - min_v) / (max_v - min_v)
	return result

def lerp(min_v, max_v, t):
	step = abs(max_v) + abs(min_v) 
	min_v += step
	max_v += step

	result = min_v + t * (max_v - min_v)
	return result - step
	

class NeuralNetDrawer:
	def __init__(self):
		self.fig = plt.figure(figsize=(7, 7))
		self.ax = self.fig.gca()
		self.ax.axis('off')

		self.positive_edge_color = [0.113, 0.678, 0.062]
		self.negative_edge_color = [0.921, 0.070, 0    ]

	def get_edge_color(self, weigth):
		max_w = self.data['max_weight']
		min_w = -max_w

		gradient = inverse_lerp(min_w, max_w, weigth)
		result = []
		for pn in zip(self.positive_edge_color, self.negative_edge_color):
			val = lerp(pn[0], pn[1], gradient)
			result.append(val)

		alpha = max(inverse_lerp(0.0, max_w, abs(weigth)), 0.2)
		result.append(alpha)
		return result

	def map_neuron_dict(self):
		self.neuron_dict = {}
		for neuron in self.data['neurons']:
			self.neuron_dict[neuron['innov']] = neuron

	def draw_neurons(self):
		radius = self.data['neuron_radius']
		for neuron in self.data['neurons']:
			color = 'w'
			if 'color' in neuron:
				color = neuron['color']

			circle = plt.Circle(
				(neuron['x'], neuron['y']),
				radius,
				color=color,
				ec='k',
				zorder=4,
				label='1'
			)
			
			label = neuron['innov']
			if 'label' in neuron:
				label = neuron['label'] + str(label)

			self.ax.add_artist(circle)

			if self.data['print_neurons_txt']:
				txt = plt.Text(
					neuron['x'] - radius / 4, neuron['y'] + radius * 1.1,
					label,
					zorder=5)
				self.ax.add_artist(txt)

	def draw_edges(self):
		for edge in self.data['edges']:
			n1 = self.neuron_dict[edge['start']]
			n2 = self.neuron_dict[edge['end']]
			w = edge['w']

			deltaX = n2['x'] - n1['x']
			deltaY = n2['y'] - n1['y']
			arrow = plt.arrow(
				n1['x'] + deltaX / 5, n1['y'] + deltaY / 5,
				deltaX / 1000000, deltaY / 1000000,
				head_width=0.02,
			)
			arrow.set_color(self.get_edge_color(edge['w']))
			arrow.set_linewidth(self.data['edge_width'])
			self.ax.add_artist(arrow)

			arrow = plt.arrow(
				n1['x'] + deltaX * 0.8, n1['y'] + deltaY * 0.8,
				deltaX / 1000000, deltaY / 1000000,
				head_width=0.017,
			)
			arrow.set_color(self.get_edge_color(edge['w']))
			arrow.set_linewidth(self.data['edge_width'])
			self.ax.add_artist(arrow)

			line = plt.Line2D(
				[n1['x'], n2['x']],
				[n1['y'], n2['y']])
			line.set_color(self.get_edge_color(edge['w']))
			line.set_linewidth(self.data['edge_width'])
			self.ax.add_artist(line)


	def apply_default_data_values(self):
		if 'max_weight' not in self.data:
			self.data['max_weight'] = 1

		if 'neuron_radius' not in self.data:
			self.data['neuron_radius'] = 0.03

		if 'edge_width' not in self.data:
			self.data['edge_width'] = 3

		if 'print_neurons_txt' not in self.data:
			self.data['print_neurons_txt'] = True

	def is_valid(self):
		for edge in self.data['edges']:
			if edge['start'] not in self.neuron_dict:
				print("Neural net not valid: there is no neuron %d" % (edge['start']))
				return False

			if edge['end'] not in self.neuron_dict:
				print("Neural net not valid: there is no neuron %d" % (edge['end']))
				return False
		return True

	def draw(self, data):
		self.ax.artists = []
		self.data = data

		self.map_neuron_dict()
		if self.is_valid():
			self.apply_default_data_values()
			self.draw_neurons()
			self.draw_edges()

		plt.draw()

def update_drawing(i, drawer, socket_proxy):
	net_data = socket_proxy.try_read_msg()

	if net_data == "Disconnected":
		return

	if net_data != '':
		drawer.draw(json.loads(net_data))

drawer = NeuralNetDrawer()

argv = json.loads(sys.argv[1])
socket_proxy = SocketProxy.SocketProxy()
socket_proxy.connect(argv['connection_link'], int(argv['connection_port']))

live_drawing = animation.FuncAnimation(
	drawer.fig,
	update_drawing,
	interval=100,
	fargs=[drawer, socket_proxy])

plt.show()

