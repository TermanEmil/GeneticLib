import time
import struct
import json
import sys
import socket
import select

"""
 The first 7 bytes of the message is the message length.
"""

class SocketProxy:
	def __init__(self, sock=None):
		if sock is None:
			self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
			self.sock.settimeout(1)
		else:
			self.sock = sock

		self.chunk_size = 1024

	def connect(self, host, port):
		self.sock.connect((host, port))
		print("py: Connected")

	def try_read_msg(self):
		socket_list = [sys.stdin, self.sock]

		# Get the list sockets which are readable
		read_sockets, write_sockets, error_sockets = select.select(socket_list , [], [])
		for sock in read_sockets:
			if sock == self.sock:
				return self.receive_msg()
		
		return ""

	def receive_msg(self):
		chunks = []
		bytes_recd = 0

		msglen_str = self.sock.recv(7)
		if not msglen_str:
			return "Disconnected"

		msglen_str = msglen_str.decode('utf-8')
		if msglen_str == '':
			return ''
		
		msglen = int(msglen_str)

		while bytes_recd < msglen:
			bytes_to_read = min(msglen - bytes_recd, self.chunk_size)
			chunk = self.sock.recv(bytes_to_read).decode('utf-8')

			if chunk == '':
				self.sock.close()
				break

			chunks.append(chunk)
			bytes_recd += len(chunk)

		return ''.join(chunks)

# argv = json.loads(sys.argv[1])

# client_socket = socket_proxy()
# client_socket.connect(argv['connection_link'], int(argv['connection_port']))

# while True:
# 	client_socket.try_read_msg();
