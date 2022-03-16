#include "node.h"

Node::Node(int data)
{
	setData(data);
	setNext(nullptr);
}

void Node::setData(int data)
{
	m_data = data;
}

void Node::setNext(Node* next)
{
	m_next = next;
}

int Node::getData()
{
	return m_data;
}

Node* Node::getNext()
{
	return m_next;
}
