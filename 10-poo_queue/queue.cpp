#include "queue.h"

#include <stdio.h>

Queue::Queue()
{
	setFront(nullptr);
	setSize(0);
	setNodes(nullptr);
}

Queue::~Queue()
{
	cleanup();
}

void Queue::cleanup()
{
	if (!this)
	{
		return;
	}
	Node *previous = nullptr;
	Node *node = getNodes();

	while (node != nullptr)
	{
		previous = node;
		node = node->getNext();
		delete previous;
	}
	if (node != nullptr && previous == nullptr)
	{
		delete node;
	}
}

void Queue::setFront(Node *front)
{
	m_front = front;
}

void Queue::setSize(int size)
{
	m_size = size;
}

void Queue::setNodes(Node *nodes)
{
	m_nodes = nodes;
}

Node *Queue::getFront()
{
	return m_front;
}

int Queue::getSize()
{
	return m_size;
}

Node *Queue::getNodes()
{
	return m_nodes;
}

void Queue::growSize()
{
	setSize(getSize() + 1);
}

void Queue::shrinkSize()
{
	setSize(getSize() - 1);
}

void Queue::enqueue(int data)
{
	if (!this)
	{
		fprintf(stderr, "Queue was not created.\n");
		return;
	}
	else
	{
		Node *previous = 0;
		Node *node = getNodes();

		if (!node)
		{
			node = new Node(data);
			setFront(node);
			setNodes(getFront());
		}
		else
		{
			while (node)
			{
				previous = node;
				node = node->getNext();
			}
			node = new Node(data);
			previous->setNext(node);
		}
		growSize();
	}
}

Node *Queue::dequeue()
{
	Node *result = nullptr;
	if (!this)
	{
		return result;
	}
	if (!getNodes()->getNext())
	{
		setFront(0);
		setSize(0);
		delete getNodes();
	}
	else
	{
		result = getFront();
		setFront(getNodes()->getNext());
		shrinkSize();
		delete getNodes();
		setNodes(getFront());
	}
	return result;
}

void Queue::print()
{
	Node *node = getNodes();
	while (node)
	{
		fprintf(stdout, "%d ", node->getData());
		node = node->getNext();
	}
	fprintf(stdout, "\n");
}
